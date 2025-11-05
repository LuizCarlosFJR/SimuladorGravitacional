using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;

namespace Universo
{
    public class GravadorMySQL : GravadorUniverso
    {
        // Altere esta string com seus dados reais
        private const string ConnectionString = "server=localhost;port=3306;database=universo_db;uid=root;password=Byfc550124150;";

        // --- MÉTODOS DE CONEXÃO E TESTE ---

        public bool TestarConexao()
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Falha na conexão com o MySQL. Verifique sua ConnectionString.\n\nDetalhes do Erro:\n{ex.Message}", "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        // --- MÉTODOS DE SALVAMENTO ---

        // Método principal de salvamento (adaptado para o uso no Form1)
        public int SalvarSimulacao(Universo u, string nomeSimulacao, int numInterac, int tempoInterac)
        {
            int idSimulacao = -1;

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // 1. INSERIR NA TABELA SIMULACAO e obter o ID
                    // ATENÇÃO: Os nomes das colunas aqui (Nome, NumCorpos, NumInteracoes, etc.) 
                    // devem corresponder exatamente ao seu CREATE TABLE!
                    string sqlSimulacao = "INSERT INTO Simulacao (NomeSimulacao, TotalCorpos, NumInterac, NumTempoInterac, DataGravacao) VALUES (@nomeSim, @numCorpos, @numInterac, @tempoInterac, NOW()); SELECT LAST_INSERT_ID();";
                    using (var cmd = new MySqlCommand(sqlSimulacao, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@nomeSim", nomeSimulacao);
                        cmd.Parameters.AddWithValue("@numCorpos", u.qtdCorp());
                        cmd.Parameters.AddWithValue("@numInterac", numInterac);
                        cmd.Parameters.AddWithValue("@tempoInterac", tempoInterac);

                        idSimulacao = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    if (idSimulacao > 0)
                    {
                        // 2. INSERIR NA TABELA CORPO (Detalhes)
                        string sqlCorpo = "INSERT INTO Corpo (IdSimulacao, Nome, Massa, Densidade, PosX, PosY, PosZ, VelX, VelY, VelZ) VALUES (@idSim, @nome, @massa, @dens, @pX, @pY, @pZ, @vX, @vY, @vZ)";

                        using (var cmd = new MySqlCommand(sqlCorpo, connection, transaction))
                        {
                            cmd.Parameters.Add("@idSim", MySqlDbType.Int32).Value = idSimulacao;
                            cmd.Parameters.Add("@nome", MySqlDbType.VarChar);
                            cmd.Parameters.Add("@massa", MySqlDbType.Double);
                            cmd.Parameters.Add("@dens", MySqlDbType.Double);
                            cmd.Parameters.Add("@pX", MySqlDbType.Double);
                            cmd.Parameters.Add("@pY", MySqlDbType.Double);
                            cmd.Parameters.Add("@pZ", MySqlDbType.Double);
                            cmd.Parameters.Add("@vX", MySqlDbType.Double);
                            cmd.Parameters.Add("@vY", MySqlDbType.Double);
                            cmd.Parameters.Add("@vZ", MySqlDbType.Double);

                            cmd.Prepare();

                            foreach (Corpos corpo in u.getCorpo())
                            {
                                cmd.Parameters["@nome"].Value = corpo.getNome();
                                cmd.Parameters["@massa"].Value = corpo.getMassa();
                                cmd.Parameters["@dens"].Value = corpo.getDensidade();
                                cmd.Parameters["@pX"].Value = corpo.getPosX();
                                cmd.Parameters["@pY"].Value = corpo.getPosY();
                                cmd.Parameters["@pZ"].Value = corpo.getPosZ();
                                cmd.Parameters["@vX"].Value = corpo.getVelX();
                                cmd.Parameters["@vY"].Value = corpo.getVelY();
                                cmd.Parameters["@vZ"].Value = corpo.getVelZ();

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    transaction.Commit();
                    return idSimulacao;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Erro ao salvar no MySQL: {ex.Message}", "Erro de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
        }

        // --- MÉTODOS DE CARREGAMENTO ---

        // Carrega a simulação completa a partir do ID
        public Universo CarregarSimulacao(int idSimulacao, out int numInterac, out int tempoInterac)
        {
            numInterac = 0;
            tempoInterac = 0;
            Universo u = new Universo();

            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    // 1. Carregar dados da Simulação
                    string sqlSimulacao = "SELECT NumInterac, NumTempoInterac FROM Simulacao WHERE IdSimulacao = @idSim";
                    using (var cmdSim = new MySqlCommand(sqlSimulacao, connection))
                    {
                        cmdSim.Parameters.AddWithValue("@idSim", idSimulacao);
                        using (var reader = cmdSim.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                numInterac = reader.GetInt32("NumInterac");
                                tempoInterac = reader.GetInt32("NumTempoInterac");
                            }
                            else
                            {
                                MessageBox.Show($"Simulação com ID {idSimulacao} não encontrada.", "Erro de Carregamento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return null;
                            }
                        }
                    }

                    // 2. Carregar Corpos
                    string sqlCorpos = "SELECT * FROM Corpo WHERE IdSimulacao = @idSim";
                    using (var cmdCorpos = new MySqlCommand(sqlCorpos, connection))
                    {
                        cmdCorpos.Parameters.AddWithValue("@idSim", idSimulacao);
                        using (var reader = cmdCorpos.ExecuteReader())
                        {
                            while (reader.Read())
                            {
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
                                u.setCorpo(novoCorpo, u.qtdCorp());
                            }
                        }
                    }
                    return u;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao carregar do MySQL: {ex.Message}", "Erro de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        // --- IMPLEMENTAÇÃO OBRIGATÓRIA DOS MEMBROS ABSTRATOS (CS0534) ---

        // Este é o método que o erro CS0534 estava reclamando que não tinha a assinatura correta.
        // A lógica real é feita em CarregarSimulacao(int, out int, out int).
        public override Universo CarregarSimulacao(string caminho, out int numInterac, out int tempoInterac)
        {
            numInterac = 0;
            tempoInterac = 0;
            MessageBox.Show("Para carregar do MySQL, utilize a opção 'Carregar Simulação' e insira o ID.", "Atenção");
            return null;
        }

        public override void GravarUniverso(Universo u, string nomeArquivo, int numInterac, int tempoInterac) { /* Requerido pelo GravadorUniverso */ }

        public override void GravarUniversoInicial(Universo u, string nomeArquivo) { /* Requerido pelo GravadorUniverso */ }

        public override Universo CarregarUniversoInicial(string nomeArquivo)
        {
            // Requerido pelo GravadorUniverso
            MessageBox.Show("Para carregar a configuração inicial do MySQL, utilize a opção de Simulação.", "Atenção");
            return null;
        }
        // --- FIM DOS MÉTODOS ABSTRATOS ---
    }
}