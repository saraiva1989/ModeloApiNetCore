using ModeloNetCoreBiblioteca.ClassesAuxiliar;
using ModeloNetCoreBiblioteca.Repositorio.Usuario;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ModeloNetCoreBiblioteca.Dominio.Usuario
{
    public class UsuarioServico : IUsuarioServico
    {
        IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<RetornoModelo> InserirAsync(UsuarioModelo usuario)
        {
            usuario.Nivel = 0;
            usuario.ChaveAtivacao = Criptografia.StringSha256Hash(usuario.Email);
            usuario.Email = Criptografia.EncryptString(usuario.Email);
            usuario.Senha = Criptografia.StringSha256Hash(usuario.Senha);
            usuario.DataCadastro = DateTime.Now;
            var cadastro = await _usuarioRepositorio.InserirAsync(usuario);
            return cadastro;
        }

        public async Task<RetornoModelo> BuscarUsuarioPorEmail(string email, string senha)
        {
            try
            {
                var usuario = await _usuarioRepositorio.BuscarUsuarioPorEmail(Criptografia.EncryptString(email));
                if (usuario.Objeto == null || usuario?.Objeto?.Senha != Criptografia.StringSha256Hash(senha))
                    return new RetornoModelo { Status = false, Mensagem = Constantes.LoginSenhaIncorreto };

                var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
                UsuarioModelo usuarioToken = usuario.Objeto;

                var token = Criptografia.StringSha256Hash(usuario?.Objeto?.Email + timestamp);
                var tokenModelo = new TokenModelo
                {
                    Token = token,
                    ValidadeToken = DateTime.Now.AddDays(30),
                    UsuarioEmail = usuario.Objeto.Email,
                };

                var retorno = await InserirTokens(tokenModelo);
                return new RetornoModelo { Status = retorno.Status, Objeto = retorno.Objeto };
            }
            catch 
            {
                return new RetornoModelo { Status = false, Mensagem = Constantes.LoginSenhaIncorreto };
            }
        }

        public async Task<RetornoModelo> InserirTokens(TokenModelo tokenModelo)
        {
            var retorno = await _usuarioRepositorio.InserirTokens(tokenModelo);
            return new RetornoModelo { Status = retorno.Status, Objeto = retorno.Objeto };
        }



        public async Task<RetornoModelo> BuscarTokens(string token)
        {
            var retorno = await _usuarioRepositorio.BuscarTokens(token);
            return retorno;
        }

        public async Task<RetornoModelo> BuscarEmail(string email)
        {
            var usuario = await _usuarioRepositorio.BuscarUsuarioPorEmail(Criptografia.EncryptString(email));
            if (usuario.Status is true)
                return new RetornoModelo { Status = true, Mensagem = Constantes.EmailJaCadastrado };
            return usuario;
        }
    }
}
