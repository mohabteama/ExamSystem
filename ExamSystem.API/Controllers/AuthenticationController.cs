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
        private readonly JwtConfig _jwtConfig;
        public AuthenticationController(UserManager<Student> userManager, IOptions<JwtConfig> jwtConfig)
        {
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
                    var token = GenerateJwtToken(NewUser);
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

            var jwtToken = GenerateJwtToken(existingUser);

            return Ok(new AuthResult
            {
                Token = jwtToken,
                Result = true
            });
        }
        private string GenerateJwtToken(IdentityUser user)
        {
            var JwtTokenHandler = new JwtSecurityTokenHandler();
            var Key = Encoding.UTF8.GetBytes(_jwtConfig.Secret);
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id",user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                    new Claim(JwtRegisteredClaimNames.Email,value:user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToUniversalTime().ToString()),
                }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256)
            };

            var Token = JwtTokenHandler.CreateToken(TokenDescriptor);
            var jwtToken = JwtTokenHandler.WriteToken(Token);
            return jwtToken;

        }


    }
}
