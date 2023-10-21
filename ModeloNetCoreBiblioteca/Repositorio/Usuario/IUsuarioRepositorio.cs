using ModeloNetCoreBiblioteca.ClassesAuxiliar;
using ModeloNetCoreBiblioteca.Dominio.Usuario;

namespace ModeloNetCoreBiblioteca.Repositorio.Usuario
{
    public interface IUsuarioRepositorio
    {
        Task<RetornoModelo> InserirAsync(UsuarioModelo usuario);
        Task<RetornoModelo> BuscarUsuarioPorEmail(string email);
        Task<RetornoModelo> BuscarTokens(string token);
        Task<RetornoModelo> InserirTokens(TokenModelo tokenModelo);
    }
}
