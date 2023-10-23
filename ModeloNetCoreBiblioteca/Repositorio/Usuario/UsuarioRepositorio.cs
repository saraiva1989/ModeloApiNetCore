using ModeloNetCoreBiblioteca.ClassesAuxiliar;
using ModeloNetCoreBiblioteca.Dominio.Usuario;
using MongoDB.Driver;

namespace ModeloNetCoreBiblioteca.Repositorio.Usuario
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly string _conexao;

        public UsuarioRepositorio(string conexao)
        {
            _conexao = conexao;
        }

        public async Task<RetornoModelo> BuscarTokens(string token)
        {
            try
            {
                var client = new MongoClient(_conexao);
                var colecao = client.GetDatabase("seubanco").GetCollection<TokenModelo>("TokenModelo");
                FilterDefinition<TokenModelo> filtro = Builders<TokenModelo>.Filter.Eq("Token", token);
                TokenModelo retorno = await colecao.Find(filtro).FirstOrDefaultAsync();
                return new RetornoModelo { Status = true, Objeto = retorno };
            }
            catch
            {
                return new RetornoModelo { Status = false, Objeto = string.Empty };
            }
        }

        public async Task<RetornoModelo> BuscarUsuarioPorEmail(string email)
        {
            try
            {
                var client = new MongoClient(_conexao);
                var colecao = client.GetDatabase("seubanco").GetCollection<UsuarioModelo>("UsuarioModelo");
                FilterDefinition<UsuarioModelo> filtro = Builders<UsuarioModelo>.Filter.Eq("Email", email);
                UsuarioModelo retorno = await colecao.Find(filtro).FirstOrDefaultAsync();
                if (retorno is null)
                    return new RetornoModelo { Status = false, Mensagem = Constantes.InformacaoNaoEncontrada };
                return new RetornoModelo { Status = true, Objeto = retorno };
            }
            catch
            {
                return new RetornoModelo { Status = false, Mensagem = Constantes.BuscaError };
            }
        }

        public async Task<RetornoModelo> InserirAsync(UsuarioModelo usuario)
        {
            try
            {
                var client = new MongoClient(_conexao);
                var collection = client.GetDatabase("seubanco").GetCollection<UsuarioModelo>("UsuarioModelo");

                await collection.InsertOneAsync(usuario);
                return new RetornoModelo { Status = true, Mensagem = Constantes.CadastroSucesso };
            }
            catch
            {
                return new RetornoModelo { Status = false, Mensagem = Constantes.CadastroError };
            }
        }

        public async Task<RetornoModelo> InserirTokens(TokenModelo tokenModelo)
        {
            try
            {
                var client = new MongoClient(_conexao);
                var collection = client.GetDatabase("seubanco").GetCollection<TokenModelo>("TokenModelo");
                await collection.InsertOneAsync(tokenModelo);
                return new RetornoModelo { Status = true, Objeto = tokenModelo };
            }
            catch
            {
                return new RetornoModelo { Status = false };
            }
        }
    }
}
