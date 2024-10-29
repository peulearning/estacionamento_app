using EstacionamentoApp.DataBase;
using EstacionamentoApp.Interface;
using MySql.Data.MySqlClient;


namespace EstacionamentoApp.Management
{
    public class EstacionamentoService : IVeiculoRepository

    {
        private string connectionString = "Server=localhost;Database=estacionamento;User ID=root;Password=;";

        private decimal valorHora;
        public int Vagas { get; set; }

        public EstacionamentoService(decimal valorHora, int vagas)
        {
            this.valorHora = valorHora;
            Vagas = vagas;
        }


        public void AdicionarVeiculo(string placa)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT count(placa) as qtde FROM veiculos WHERE horasaida is null";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            Console.WriteLine("");
                        }
                        int vagasOcupadas = reader.GetInt32("qtde");


                        if (string.IsNullOrEmpty(placa))
                        {
                            Console.WriteLine("Campo não pode ser vazio!");
                            return;
                        }

                        if (vagasOcupadas >= Vagas)
                        {
                            Console.WriteLine("Estacionamento cheio!");
                            return;
                        }
                        var veiculo = new Veiculo(placa);
                        Console.WriteLine($"Veículo {placa} adicionado ao estacionamento às {veiculo.HoraEntrada}.");
                        var banco = new BancoDados();
                        banco.EntradaVeiculo(veiculo);

                    }
                }
            }
        }
        public void RemoverVeiculo(string placa)
        {

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT placa, HoraEntrada FROM veiculos WHERE placa = @placa and HoraSaida is null";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Placa", placa);
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            Console.WriteLine("Veiculo não encontrado");
                            return;
                        }

                        DateTime horaEntrada = reader.GetDateTime("HoraEntrada");

                        var horaSaida = DateTime.Now;
                        var tempoEstacionado = horaSaida - horaEntrada;
                        var horasEstacionadas = tempoEstacionado.TotalHours;
                        var minutosEstacionados = tempoEstacionado.Minutes;

                        var valorTotal = (decimal)tempoEstacionado.TotalHours * valorHora;

                        var banco = new BancoDados();
                        banco.SaidaVeiculo(placa, horaSaida, horasEstacionadas, minutosEstacionados, valorTotal);
                        Console.WriteLine($"Veículo {placa} removido do estacionamento.");
                        Console.WriteLine
                            ($"Entrada: {horaEntrada} | Saída: {horaSaida} | Valor Total: R${valorTotal:F2} | " +
                            $"HorasEstacionadas:{Math.Round(horasEstacionadas)}h {minutosEstacionados}min ");

                    }
                }
            }
        }
        public void ListarVeiculos()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT placa, HoraEntrada FROM veiculos WHERE horasaida is null";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string message = $"Placa: {reader.GetString("placa")} - Hora Entrada: {reader.GetDateTime("HoraEntrada")}";
                                MessageBox.Show(message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Nenhum veículo estacionado");
                        }
                    }
                }
            }
        }
        public void VagasDesocupadas()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT count(placa) as qtde FROM veiculos WHERE horasaida is null";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string message = ($"Vagas Desocupadas:{Vagas}");
                            MessageBox.Show(message);
                            return;
                        }

                        int vagasOcupadas = reader.GetInt32("qtde");
                        var vagasLivres = Vagas - vagasOcupadas;
                        Console.WriteLine($"Vagas Disponíveis: {vagasLivres}");
                    }
                }
            }
        }
    }
}
