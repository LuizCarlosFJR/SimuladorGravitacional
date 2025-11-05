using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.IO;

namespace Universo
{
    // GravadorMySQL implementa o contrato definido em GravadorUniverso
    public class GravadorMySQL : GravadorUniverso
    {
        // **!!! CONFIGURAÇÃO OBRIGATÓRIA !!!**
        // A connectionString está correta, usando 'universo_db'
        private string connectionString = "Server=localhost;Database=universo_db;Uid=root;Pwd=Byfc550124150;";

        // --- IMPLEMENTAÇÃO DOS MÉTODOS ABSTRATOS DE GRAVADORUNIVERSO ---

        public override void GravarUniversoInicial(Universo universo, string caminho)
        {
            string nome = System.IO.Path.GetFileNameWithoutExtension(caminho);
            SalvarSimulacao(universo, nome, 0, 0);
        }

        public override void GravarUniverso(Universo universo, string caminho, int numInterac, int numTempoInterac)
        {
            string nome = System.IO.Path.GetFileNameWithoutExtension(caminho);
            SalvarSimulacao(universo, nome, numInterac, numTempoInterac);
        }

        public override Universo CarregarUniversoInicial(string caminho)
        {
            throw new NotImplementedException("Use a interface de carregamento por ID (CarregarSimulacao(ID)) para MySQL.");
        }

        public override Universo CarregarSimulacao(string caminho, out int numInterac, out int numTempoInterac)
        {
            numInterac = 0;
            numTempoInterac = 0;
            MessageBox.Show("Esta função no GravadorMySQL requer o ID da simulação. Tente usar uma interface que liste os IDs ou chame CarregarSimulacao(ID).", "Erro de Chamada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }


        // --- MÉTODOS FOCADOS EM ID DO MYSQL (USADOS PELO FORM1) ---

        // Método Principal de Salvamento
        public int SalvarSimulacao(Universo u, string nomeSimulacao, int numInterac, int numTempoInterac)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    int idSimulacao = 0;

                    // 1. INSERIR NA TABELA SIMULACAO (Cabeçalho)
                    string sqlSimulacao = "INSERT INTO Simulacao (NomeSimulacao, TotalCorpos, NumInterac, NumTempoInterac) VALUES (@nome, @total, @interac, @tempo); SELECT LAST_INSERT_ID();";
                    using (var cmd = new MySqlCommand(sqlSimulacao, connection))
                    {
                        cmd.Parameters.AddWithValue("@nome", nomeSimulacao);
                        cmd.Parameters.AddWithValue("@total", u.qtdCorp());
                        cmd.Parameters.AddWithValue("@interac", numInterac);
                        cmd.Parameters.AddWithValue("@tempo", numTempoInterac);

                        idSimulacao = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 2. INSERIR NA TABELA CORPO (Detalhes)
                    string sqlCorpo = "INSERT INTO Corpo (IdSimulacao, Nome, Massa, Densidade, PosX, PosY, PosZ, VelX, VelY, VelZ) VALUES (@idSim, @nome, @massa, @dens, @pX, @pY, @pZ, @vX, @vY, @vZ)";
                    using (var cmd = new MySqlCommand(sqlCorpo, connection))
                    {
                        cmd.Prepare();

                        foreach (Corpos corpo in u.getCorpo())
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@idSim", idSimulacao);
                            cmd.Parameters.AddWithValue("@nome", corpo.getNome());
                            cmd.Parameters.AddWithValue("@massa", corpo.getMassa());
                            cmd.Parameters.AddWithValue("@dens", corpo.getDensidade());
                            cmd.Parameters.AddWithValue("@pX", corpo.getPosX());
                            cmd.Parameters.AddWithValue("@pY", corpo.getPosY());
                            cmd.Parameters.AddWithValue("@pZ", corpo.getPosZ());
                            cmd.Parameters.AddWithValue("@vX", corpo.getVelX());
                            cmd.Parameters.AddWithValue("@vY", corpo.getVelY());
                            cmd.Parameters.AddWithValue("@vZ", corpo.getVelZ());

                            cmd.ExecuteNonQuery();
                        }
                    }
                    return idSimulacao;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao salvar no MySQL: {ex.Message}\nVerifique sua ConnectionString e o Schema do BD.", "Erro de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
            }
        }

        // Método Principal de Carregamento
        public Universo CarregarSimulacao(int idSimulacao, out int numInterac, out int numTempoInterac)
        {
            Universo universoCarregado = new Universo();
            numInterac = 0;
            numTempoInterac = 0;

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // 1. LER O CABEÇALHO DA SIMULAÇÃO
                    string sqlHeader = "SELECT NumInterac, NumTempoInterac FROM Simulacao WHERE IdSimulacao = @id";
                    using (var cmdHeader = new MySqlCommand(sqlHeader, connection))
                    {
                        cmdHeader.Parameters.AddWithValue("@id", idSimulacao);
                        using (var reader = cmdHeader.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                numInterac = reader.GetInt32("NumInterac");
                                numTempoInterac = reader.GetInt32("NumTempoInterac");
                            }
                        }
                    }

                    // 2. LER OS CORPOS (Detalhes)
                    // Garantir que a conexão seja usada para a segunda consulta
                    connection.Close();
                    connection.Open();

                    string sqlCorpo = "SELECT Nome, Massa, Densidade, PosX, PosY, PosZ, VelX, VelY, VelZ FROM Corpo WHERE IdSimulacao = @id";
                    using (var cmdCorpo = new MySqlCommand(sqlCorpo, connection))
                    {
                        cmdCorpo.Parameters.AddWithValue("@id", idSimulacao);
                        using (var reader = cmdCorpo.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Cria um novo objeto Corpo usando o construtor:
                                Corpos novoCorpo = new Corpos(
                                    reader.GetString("Nome"),
                                    reader.GetDouble("Massa"),
                                    reader.GetDouble("PosX"),
                                    reader.GetDouble("PosY"),
                                    reader.GetDouble("PosZ"),
                                    reader.GetDouble("VelX"),
                                    reader.GetDouble("VelY"),
                                    reader.GetDouble("VelZ"),
                                    reader.GetDouble("Densidade")
                                );
                                universoCarregado.setCorpo(novoCorpo, universoCarregado.qtdCorp());
                            }
                        }
                    }
                    MessageBox.Show($"Simulação ID {idSimulacao} carregada com sucesso!");
                    return universoCarregado;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao carregar do MySQL: {ex.Message}", "Erro de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        // --- MÉTODO ADICIONADO PARA TESTE DE CONEXÃO ---
        public bool TestarConexao()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    connection.Close();
                    return true;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Falha na conexão com MySQL:\n{ex.Message}\n\nVerifique a ConnectionString e se o serviço MySQL está ativo.", "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
        // --- FIM DO MÉTODO DE TESTE ---
    }
}