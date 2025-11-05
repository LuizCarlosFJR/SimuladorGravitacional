using System;
using System.IO;
using System.Linq;

namespace Universo
{
    // A classe concreta implementa a lógica para arquivos de texto
    public class GravadorTexto : GravadorUniverso
    {
        public override void GravarUniverso(Universo u, string caminho, int numInterac, int numTempoInterac)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(caminho))
                {
                    // Cabeçalho da simulação
                    sw.WriteLine($"{u.qtdCorp()};{numInterac};{numTempoInterac}");

                    // Escreve os dados de cada corpo
                    foreach (Corpos corpo in u.getCorpo())
                    {
                        // Formato: <Nome>;<massa>;<raio>;<PosX>;<PosY>;<VelX>;<VelY>
                        // O raio é calculado dinamicamente pelo método getRaio()
                        sw.WriteLine($"{corpo.getNome()};{corpo.getMassa()};{corpo.getRaio()};{corpo.getPosX()};{corpo.getPosY()};{corpo.getVelX()};{corpo.getVelY()}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Erro ao salvar o arquivo: {ex.Message}", "Erro de Salvamento", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public override void GravarUniversoInicial(Universo u, string caminho)
        {
            GravarUniverso(u, caminho, 0, 0);
        }

        public override Universo CarregarUniversoInicial(string caminho)
        {
            int numInterac, numTempoInterac;
            return CarregarSimulacao(caminho, out numInterac, out numTempoInterac);
        }

        public override Universo CarregarSimulacao(string caminho, out int numInterac, out int numTempoInterac)
        {
            numInterac = 0;
            numTempoInterac = 0;
            Universo universoCarregado = null;

            try
            {
                using (StreamReader sr = new StreamReader(caminho))
                {
                    universoCarregado = new Universo();
                    string linha;

                    // Lê o cabeçalho
                    if ((linha = sr.ReadLine()) != null)
                    {
                        string[] header = linha.Split(';');
                        if (header.Length >= 3 && int.TryParse(header[1], out numInterac) && int.TryParse(header[2], out numTempoInterac))
                        {
                            // Cabeçalho lido com sucesso
                        }
                    }

                    // Lê os corpos linha por linha
                    while ((linha = sr.ReadLine()) != null)
                    {
                        string[] dados = linha.Split(';');
                        if (dados.Length == 7)
                        {
                            if (double.TryParse(dados[1], out double massa) &&
                                double.TryParse(dados[2], out double raio) &&
                                double.TryParse(dados[3], out double posX) &&
                                double.TryParse(dados[4], out double posY) &&
                                double.TryParse(dados[5], out double velX) &&
                                double.TryParse(dados[6], out double velY))
                            {
                                // Cria o corpo usando o construtor existente, com valores padrão para 3D e densidade
                                Corpos novoCorpo = new Corpos(dados[0], massa, posX, posY, 0, velX, velY, 0, 0);

                                // Recalcula a densidade a partir do raio e da massa lidos do arquivo
                                // Formula original: raio = ((3 * PI * massa) / (4 * densidade))^(1/3) / 5
                                // Invertendo para densidade: densidade = (3 * PI * massa) / (4 * (raio * 5)^3)
                                double densidade = (3 * Math.PI * massa) / (4 * Math.Pow(raio * 5, 3));
                                novoCorpo.setDensidade(densidade);

                                universoCarregado.setCorpo(novoCorpo, universoCarregado.qtdCorp());
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                System.Windows.Forms.MessageBox.Show("O arquivo não foi encontrado.", "Erro de Leitura", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ocorreu um erro ao carregar o arquivo: {ex.Message}", "Erro de Leitura", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return null;
            }

            return universoCarregado;
        }
    }
}