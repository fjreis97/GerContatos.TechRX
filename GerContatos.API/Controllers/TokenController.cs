using AutoMapper;
using Business.Dtos.Request.Token;
using Business.Interfaces.Services;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerContatos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController(ITokenService _tokenService, IMapper _mapper) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GetUsuarioTokenRequest request)
        {
            var usuario = _mapper.Map<Usuario>(request);
            var token = await _tokenService.GetToken(usuario);

            if(!string.IsNullOrWhiteSpace(token))
                return Ok(token);

            return Unauthorized();
        }
    }
}
