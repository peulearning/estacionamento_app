using EstacionamentoApp.Management;

namespace EstacionamentoApp
{
    public partial class Form1 : Form
    {
        private readonly AutenticacaoService autenticacaoService; // Inicializei a Vari�vel

        public Form1()
        {
            InitializeComponent();
            autenticacaoService = new AutenticacaoService();  // Chamei o Objeto 
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonEntrar_Click(object sender, EventArgs e)
        {
            string username = txtUsuario.Text.Trim();
            string password = txtSenha.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, insira o usu�rio e a senha.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool loginValido = autenticacaoService.Autenticar(username, password);

            if (loginValido)
            {
                MessageBox.Show("Login bem-sucedido!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Abrir o Menu principal ou outro formul�rio
                Menu menu = new Menu();
                menu.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usu�rio ou senha incorretos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
