using Casino_ProyectoFinal.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
                return await ConstruirToken(credenciales);
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
                return await ConstruirToken(credencialUsuario);
            }
            else
            {
                return BadRequest("Login Incorrecto");
            }
        }

        [HttpGet("RenovarToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<ActionResult<RespuestaAutenticacion>> Renovar()
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;

            var credenciales = new CredencialUsuario()
            {
                Email = email
            };

            return await ConstruirToken(credenciales);
        }

        private async Task<RespuestaAutenticacion> ConstruirToken(CredencialUsuario credencialUsuario)
        {
            var claims = new List<Claim>
            {
                new Claim("email", credencialUsuario.Email),
                new Claim("claimprueba", "Esta prueba")
            };

            var usuario = await userManager.FindByEmailAsync(credencialUsuario.Email);
            var claimsDB = await userManager.GetClaimsAsync(usuario);

            claims.AddRange(claimsDB);


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyjwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(10);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: creds);

            return new RespuestaAutenticacion()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiration
            };
        }

        [HttpPost("HacerAdmin")]

        public async Task<ActionResult> HacerAdmin(EditarAdminDTO editarAdminDTO)
        {
            var usuario = await userManager.FindByEmailAsync(editarAdminDTO.Email);

            await userManager.AddClaimAsync(usuario, new Claim("EsAdmin", "1"));

            return NoContent();
        }

        [HttpPost("RemoverAdmin")]

        public async Task<ActionResult> RemoverAdmin(EditarAdminDTO editarAdminDTO)
        {
            var usuario = await userManager.FindByEmailAsync(editarAdminDTO.Email);

            await userManager.RemoveClaimAsync(usuario, new Claim("EsAdmin", "1"));

            return NoContent();
        }

    }
}
