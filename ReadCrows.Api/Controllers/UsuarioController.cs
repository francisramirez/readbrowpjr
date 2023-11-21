using Microsoft.AspNetCore.Mvc;
using ReadCrows.Api.Models;
using ReadCrows.Usuario.Api.Extentions;
using ReadCrows.Core.Models;
using ReadCrows.Infraestructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ReadCrows.Usuario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly ILogger<UsuarioController> logger;

        public UsuarioController(IUsuarioRepository usuarioRepository, ILogger<UsuarioController> logger)
        {
            this.usuarioRepository = usuarioRepository;
            this.logger = logger;
        }

        [HttpGet("GetUsuarios")]
        
        public async Task<IActionResult> Get()
        {
            var usuarios = await this.usuarioRepository.GetEntities();
            return Ok(usuarios);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody]UsuarioCreateModel usuarioCreate)
        {

            if (!this.ModelState.IsValid)
                return BadRequest();

            OperationResult operationResult = new OperationResult();

            Core.Entities.Usuario usuario = usuarioCreate.GetUsuarioFromUsuarioCreateModel();

            try
            {
                operationResult = await this.usuarioRepository.Save(usuario);

                if (!operationResult.Success)
                    return BadRequest(operationResult);

            }
            catch (Exception ex)
            {
                this.logger.LogError("", ex.ToString());
            }

            return Ok(operationResult);

        }
        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody]UsuarioUpdateModel usuarioUpdateModel)
        {

            if (!this.ModelState.IsValid)
                return BadRequest();

            OperationResult operationResult = new OperationResult();

            Core.Entities.Usuario usuario = usuarioUpdateModel.GetUsuarioFromUsuarioUpdateModel();

            try
            {
                operationResult = await this.usuarioRepository.Save(usuario);

                if (!operationResult.Success)
                    return BadRequest(operationResult);

            }
            catch (Exception ex)
            {
                this.logger.LogError("", ex.ToString());
            }

            return Ok(operationResult);

        }

    }
}
