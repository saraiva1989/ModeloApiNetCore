using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModeloNetCoreBiblioteca.Dominio.Usuario;

namespace ModeloNetCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        IUsuarioServico _usuarioServico;

        public UsuarioController(IUsuarioServico usuarioDominio)
        {
            _usuarioServico = usuarioDominio;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Cadastrar(UsuarioModelo usuario)
        {
            usuario.ValidarEmail(usuario.Email);
            var emailExiste = await _usuarioServico.BuscarEmail(usuario.Email);
            if (emailExiste.Status)
            {
                emailExiste.Objeto = string.Empty;
                emailExiste.Status = false;
                return Ok(emailExiste);
            }
            var retorno = await _usuarioServico.InserirAsync(usuario);
            return Ok(retorno);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("autenticar")]
        public async Task<IActionResult> Autenticar(UsuarioModelo usuario)
        {
            usuario.ValidarEmail(usuario.Email);
            var autenticado = await _usuarioServico.BuscarUsuarioPorEmail(usuario.Email, usuario.Senha);
            return Ok(autenticado);
        }
    }
}
