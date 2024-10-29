using EstacionamentoApp.DataBase;

namespace EstacionamentoApp.Management
{
    public class AutenticacaoService
    {
        private readonly BancoDados bancoDados;

        public AutenticacaoService()
        {
            bancoDados = new BancoDados();
        }

        public bool Autenticar(string username, string password)
        {
            return bancoDados.ValidarLogin(username, password);
        }
    }
}
