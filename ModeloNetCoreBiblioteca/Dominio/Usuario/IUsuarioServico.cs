using ModeloNetCoreBiblioteca.ClassesAuxiliar;

namespace ModeloNetCoreBiblioteca.Dominio.Usuario
{
    public interface IUsuarioServico
    {
        Task<RetornoModelo> InserirAsync(UsuarioModelo usuario);
        Task<RetornoModelo> BuscarUsuarioPorEmail(string email, string senha);
        Task<RetornoModelo> BuscarEmail(string email);
        Task<RetornoModelo> BuscarTokens(string token);
        Task<RetornoModelo> InserirTokens(TokenModelo tokenModelo);
    }
}
