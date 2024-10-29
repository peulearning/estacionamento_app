using EstacionamentoApp.Management;
using MySql.Data.MySqlClient;


namespace EstacionamentoApp.DataBase
{
    public class BancoDados
    {
        private string connectionString = "Server=localhost;Database=estacionamento;User ID=root;Password=;";

        public void EntradaVeiculo(Veiculo veiculo)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string insertQuery = "INSERT INTO veiculos (Placa, HoraEntrada) " +
                    "VALUES (@Placa, @HoraEntrada)";

                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@Placa", veiculo.PlacaVeiculo);
                    command.Parameters.AddWithValue("@HoraEntrada", veiculo.HoraEntrada);

                    command.ExecuteNonQuery();
                }
            }
        }

        public bool ValidarLogin(string username, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM usuarios WHERE username = @username AND password = @password";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }


    public void SaidaVeiculo(string placa, DateTime horaSaida, double horasEstacionadas, double minutosEstacionados, decimal Valor)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string updateQuery =
                    "UPDATE veiculos SET HoraSaida = @HoraSaida, PermanenciaHora = @PermanenciaHora, PermanenciaMin = @PermanenciaMin, Valor = @Valor" +
                    " WHERE Placa = @Placa and HoraSaida is null";

                using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@Placa", placa);
                    command.Parameters.AddWithValue("@HoraSaida", horaSaida);
                    command.Parameters.AddWithValue("@PermanenciaHora", Math.Round(horasEstacionadas));
                    command.Parameters.AddWithValue("@PermanenciaMin", minutosEstacionados);
                    command.Parameters.AddWithValue("@Valor", Valor.ToString("F2"));

                    command.ExecuteNonQuery();
                    command.ExecuteReader();
                }
            }
        }
    }
}

