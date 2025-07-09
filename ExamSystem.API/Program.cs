using ExamSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddAuthentication(x => {
//    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(x => {
//    var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfig:Secret").Value);

//    x.SaveToken = true;
//    x.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        RequireExpirationTime = false,
//        ValidateLifetime = true
//    };
//});
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("role", "Admin"));
//    options.AddPolicy("StudentOnly", policy => policy.RequireClaim("role", "Student"));
//});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(ExamSystem.Application.Mapper.MappingProfile));
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("ExamSystem.Infrastructure") 
    )
);
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddResponseCompression(options =>
    {
        options.EnableForHttps = false;
    });
}
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
