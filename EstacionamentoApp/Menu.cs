using EstacionamentoApp.DataBase;
using EstacionamentoApp.Interface;
using EstacionamentoApp.Management;
using MySql.Data.MySqlClient;
using System.Numerics;

namespace EstacionamentoApp
{
    public partial class Menu : Form
    {
        private readonly EstacionamentoService _estacionamentoService;

        public Menu()
        {
            InitializeComponent();

            // Defina o valor manualmente
            int vagas = 10; // Por exemplo, 10 vagas
            decimal valor = 5.00m; // Por exemplo, R$5,00 por hora

            _estacionamentoService = new EstacionamentoService(valor, vagas);
        }

        private void buttonEstacionar_Click(object sender, EventArgs e)
        {
            string placa = txtPlaca.Text.Trim();

            try
            {
                _estacionamentoService.AdicionarVeiculo(placa);
                MessageBox.Show($"Veículo com placa {placa} adicionado.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRetirar_Click(object sender, EventArgs e)
        {
            string placa = txtPlaca.Text.Trim();

            try
            {
                _estacionamentoService.RemoverVeiculo(placa);
                MessageBox.Show($"Veículo com placa {placa} removido.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSair_Click(object sender, EventArgs e)
        {
            Form1 form1 = Application.OpenForms["Form1"] as Form1;

            if (form1 != null)
            {
                form1.Show();
            }
            else
            {
                form1 = new Form1();
                form1.Show();
            }

            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                _estacionamentoService.ListarVeiculos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _estacionamentoService.VagasDesocupadas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPlaca_TextChanged(object sender, EventArgs e)
        {
            
        }

    }
}
