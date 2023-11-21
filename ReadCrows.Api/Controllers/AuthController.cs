using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReadCrows.Infraestructure.Interfaces;
using ReadCrows.Usuario.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReadCrows.Usuario.Api.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IConfiguration configuration;

        public AuthController(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            this.usuarioRepository = usuarioRepository;
            this.configuration = configuration;
        }
        [HttpPost("CreateToken")]
        public async Task<IActionResult> CreateToken([FromBody] CrearUsuarioToken crearUsuarioToken)
        {
            if (!this.ModelState.IsValid)
                return BadRequest();


            var result = await this.usuarioRepository.GetUserInfo(crearUsuarioToken.Correo);

            if (!result.Success)
                return BadRequest(result);

            if (result.Data != null)
            {
                var usuario = (Core.Entities.Usuario)result.Data;

                var myToken = GetToken(usuario);

                return Ok(myToken);
            }
            else
            {
                return BadRequest("Usuario no registrado.");
            }


        }

        private TokenInfo GetToken(Core.Entities.Usuario usuario)
        {

            TokenInfo tokenInfo = new TokenInfo();

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(this.configuration["TokenInfo:SigningKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {

                Subject = new ClaimsIdentity(new Claim[]
                 {
                     new Claim(JwtRegisteredClaimNames.Email,usuario.Correo),
                     new Claim(JwtRegisteredClaimNames.Name,usuario.Nombre),
                     new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                 }),

                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            //  HmacSha256Signature
            var token = tokenHandler.CreateToken(tokenDescriptor);

            tokenInfo.Token = tokenHandler.WriteToken(token);
            tokenInfo.Expiration = tokenDescriptor.Expires;

            return tokenInfo;
        }

    }
}

