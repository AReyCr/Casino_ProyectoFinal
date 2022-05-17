using Casino_ProyectoFinal.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace Casino_ProyectoFinal.Controllers
{ 
    [ApiController]
    [Route("api/cuentas")]
    public class CuentasCrontroller : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public CuentasCrontroller(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> singInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = singInManager;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<RespuestaAutenticacion>> Registrar(CredencialUsuario credenciales)
        {
            var user = new IdentityUser
            {
                UserName = credenciales.Email,
                Email = credenciales.Email
            };
            var result = await userManager.CreateAsync(user, credenciales.Password);

            if (result.Succeeded)
            {
                return ConstruirToken(credenciales);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<RespuestaAutenticacion>> Login(CredencialUsuario credencialUsuario)
        {
            var result = await signInManager.PasswordSignInAsync(credencialUsuario.Email, credencialUsuario.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return ConstruirToken(credencialUsuario);
            }
            else
            {
                return BadRequest("Login Incorrecto");
            }
        }

        private RespuestaAutenticacion ConstruirToken(CredencialUsuario credencialUsuario)
        {
            var claims = new List<Claim>
            {
                new Claim("email", credencialUsuario.Email),
                new Claim("claimprueba", "Esta prueba")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyjwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: creds);

            return new RespuestaAutenticacion()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiration
            };
        }
    }
}
