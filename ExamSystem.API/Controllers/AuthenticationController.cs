using ExamSystem.API.Configurations;
using ExamSystem.Application.DTO;
using ExamSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExamSystem.API.Controllers
{
    
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<Student> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtConfig _jwtConfig;
        
        public AuthenticationController(UserManager<Student> userManager, IOptions<JwtConfig> jwtConfig, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _jwtConfig = jwtConfig.Value;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto requestDot)
        {

           

            if (ModelState.IsValid)
            {
                var UserManager = await _userManager.FindByEmailAsync(requestDot.Email);

                
                    if (UserManager != null)
                {
                    return BadRequest("User already exists with this email.");
                }
                var NewUser = new Student
                {
                    UserName = requestDot.Name,
                    Email = requestDot.Email
                };

                var IsCreated = await _userManager.CreateAsync(NewUser, requestDot.PassWord);
                
                if (IsCreated.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync("Student"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Student"));
                    }

                    await _userManager.AddToRoleAsync(NewUser, "Student");
                    var savedUser = await _userManager.FindByEmailAsync(NewUser.Email);
                    var token =await GenerateJwtToken(savedUser);
                    return Ok(new AuthResult()
                    {
                        Result = true, 
                        Token = token,
                    });
                }
            }

            return BadRequest("User creation failed. Please check your details and try again.");


        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserRegistrationRequestDto loginRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResult
                {
                    Errors = new List<string> { "Invalid payload." },
                    Result = false
                });
            }

            var existingUser = await _userManager.FindByEmailAsync(loginRequestDto.Email);

            if (existingUser == null)
            {
                return BadRequest(new AuthResult
                {
                    Errors = new List<string> { "Invalid login attempt." },
                    Result = false
                });
            }

            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, loginRequestDto.PassWord);

            if (!isCorrect)
            {
                return BadRequest(new AuthResult
                {
                    Errors = new List<string> { "Invalid login attempt." },
                    Result = false
                });
            }
            var savedUser = await _userManager.FindByEmailAsync(existingUser.Email);
            var jwtToken = await GenerateJwtToken(savedUser);

            return Ok(new AuthResult
            {
                Token = jwtToken,
                Result = true
            });
        }
        private async Task<string> GenerateJwtToken(Student user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtConfig.Secret);

            // Get user roles
            var userRoles = await _userManager.GetRolesAsync(user);

            // Create claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
            };

            // Add roles to claims
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }


    }
}
