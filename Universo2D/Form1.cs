using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic; // Necessário para usar Interaction.InputBox


namespace Universo
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private Universo U, Uinicial;
        private int numCorpos, numInterac, numTempoInterac;
        private Timer simulationTimer;
        private int currentIteration;

        // --- Variáveis de Persistência ---
        private GravadorUniverso gravadorAtivo;
        private GravadorTexto gravadorTexto = new GravadorTexto();
        private GravadorMySQL gravadorMySQL = new GravadorMySQL();


        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            U = new Universo();
            Uinicial = new Universo();
            simulationTimer = new Timer();
            simulationTimer.Interval = 20;
            simulationTimer.Tick += SimulationTimer_Tick;

            // Define o gravador padrão ao iniciar
            gravadorAtivo = gravadorTexto;
        }

        // --- MÉTODOS ORIGINAIS ---

        private void button2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(qtdCorpos.Text, out numCorpos) &&
                int.TryParse(valXMax.Text, out int xMax) &&
                int.TryParse(valYMax.Text, out int yMax) &&
                int.TryParse(masMin.Text, out int mMin) &&
                int.TryParse(masMax.Text, out int mMax))
            {
                progressBar1.Value = 0;
                U.carCorp(numCorpos, 0, xMax, 0, yMax, mMin, mMax);
                Uinicial = new Universo();
                Uinicial.copiaUniverso(U);
                qtdCorposAtual.Text = U.qtdCorp().ToString();
                Form1.ActiveForm?.Refresh();
            }
            else
            {
                MessageBox.Show("Por favor, insira valores numéricos válidos.", "Erro de Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(qtdInterac.Text, out numInterac) || !int.TryParse(qtdTempoInterac.Text, out numTempoInterac))
            {
                MessageBox.Show("Insira valores numéricos válidos para interações e tempo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (U == null)
            {
                MessageBox.Show("Nenhum universo carregado para simular.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            progressBar1.Maximum = numInterac;
            progressBar1.Minimum = 0;
            currentIteration = 0;

            if (radioButton1.Checked)
            {
                simulationTimer.Start();
            }
            else if (radioButton2.Checked)
            {
                for (int i = 0; i <= numInterac; i++)
                {
                    U.interCorp(numTempoInterac);
                    progressBar1.Value = i;
                }
                Form1.ActiveForm?.Refresh();
            }
            else if (radioButton3.Checked)
            {
                using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                {
                    saveFileDialog1.Filter = "Arquivos Universo|*.uni|Todos os arquivos|*.*";
                    saveFileDialog1.Title = "Salvar arquivo de simulação";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        gravadorTexto.GravarUniverso(U, saveFileDialog1.FileName, numInterac, numTempoInterac);
                    }
                }
            }
        }

        private void SimulationTimer_Tick(object sender, EventArgs e)
        {
            if (currentIteration <= numInterac)
            {
                U.interCorp(numTempoInterac);
                progressBar1.Value = currentIteration;
                Form1.ActiveForm?.Refresh();
                currentIteration++;
            }
            else
            {
                simulationTimer.Stop();
                MessageBox.Show("Simulação concluída!");
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs pe)
        {
            // [CÓDIGO DE DESENHO ORIGINAL]
            if (U == null) { return; }

            Corpos cp;
            float prop = 1, propX = 1, propY = 1;
            float deslocX = 0, deslocY = 0, maxX = 0, maxY = 0;
            double posX = 0, posY = 0;
            int qtdCp;

            if (Form1.ActiveForm != null)
            {
                if (string.IsNullOrEmpty(valXMax.Text))
                {
                    valXMax.Text = (Form1.ActiveForm.Size.Width - 50).ToString();
                    valYMax.Text = (Form1.ActiveForm.Size.Height - 50).ToString();
                }

                float W = Form1.ActiveForm.Size.Width - 50;
                float H = Form1.ActiveForm.Size.Height - 50;

                g = pe.Graphics;
                qtdCp = U.qtdCorp();

                for (int i = 0; i < qtdCp; i++)
                {
                    cp = U.getCorpo(i);
                    if (cp != null && cp.getVal())
                    {
                        posX = cp.getPosX();
                        posY = cp.getPosY();
                        deslocX = Math.Min(deslocX, (float)posX);
                        deslocY = Math.Min(deslocY, (float)posY);
                        maxX = Math.Max(maxX, (float)posX);
                        maxY = Math.Max(maxY, (float)posY);
                    }
                }

                propX = (maxX - deslocX) > W ? (maxX - deslocX) / W : 1;
                propY = (maxY - deslocY) > H ? (maxY - deslocY) / H : 1;
                prop = Math.Max(propX, propY);

                txtProporcao.Text = (1 / prop).ToString();
                qtdCorposAtual.Text = qtdCp.ToString();

                for (int i = 0; i < qtdCp; i++)
                {
                    cp = U.getCorpo(i);
                    if (cp != null && cp.getVal())
                    {
                        posX = cp.getPosX() - deslocX;
                        posY = cp.getPosY() - deslocY;
                        float raio = (float)cp.getRaio();
                        int densidade = (int)cp.getDensidade();
                        if (densidade < 0)
                        {
                            densidade = 0;
                        }
                        else if (densidade > 255)
                        {
                            densidade = 255;
                        }

                        g.DrawEllipse(new Pen(Color.White),
                            (float)(posX - raio) / prop,
                            (float)(posY - raio) / prop,
                            (float)(raio * 2) / prop,
                            (float)(raio * 2) / prop);

                        g.DrawLine(new Pen(Color.Blue),
                            (float)posX / prop,
                            (float)posY / prop,
                            (float)(posX + (cp.getForcaX() * 50)) / prop,
                            (float)posY / prop);
                        g.DrawLine(new Pen(Color.Blue),
                            (float)posX / prop,
                            (float)posY / prop,
                            (float)posX / prop,
                            (float)(posY + (cp.getForcaY() * 50)) / prop);
                    }
                }
            }
        }

        // --- MÉTODOS DE PERSISTÊNCIA E NOVIDADES DA INTEGRAÇÃO ---

        public void Form1_Load(object sender, EventArgs e)
        {
            // Método Form1_Load (Esperado pelo Designer)
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            // Método radioButton3_CheckedChanged (Esperado pelo Designer)
            if (radioButton3.Checked)
            {
                gravadorAtivo = gravadorTexto;
            }
        }

        // --- NOVO MÉTODO: TESTAR CONEXÃO MYSQL (ESPERADO PELO DESIGNER) ---
        private void btn_testaMySQL_Click(object sender, EventArgs e)
        {
            if (gravadorMySQL.TestarConexao())
            {
                MessageBox.Show("Conexão com o MySQL estabelecida com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // --- NOVO MÉTODO: SELEÇÃO DE PERSISTÊNCIA MYSQL (ESPERADO PELO DESIGNER) ---
        private void rb_mysql_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                gravadorAtivo = gravadorMySQL;
            }
            else if (radioButton3.Checked)
            {
                gravadorAtivo = gravadorTexto;
            }
        }

        private void btn_grava_Click(object sender, EventArgs e)
        {
            if (U == null || U.qtdCorp() == 0)
            {
                MessageBox.Show("Não há corpos no Universo a serem salvos", "Atenção");
                return;
            }

            int.TryParse(qtdInterac.Text, out int interac);
            int.TryParse(qtdTempoInterac.Text, out int tempo);

            if (gravadorAtivo is GravadorMySQL)
            {
                // Lógica de Salvamento MySQL
                string nomeSimulacao = Interaction.InputBox("Digite o nome da simulação para salvar no MySQL:", "Salvar Simulação", "Simulacao Gravada");

                if (!string.IsNullOrWhiteSpace(nomeSimulacao))
                {
                    int idSalvo = gravadorMySQL.SalvarSimulacao(U, nomeSimulacao, interac, tempo);
                    if (idSalvo > 0)
                    {
                        MessageBox.Show($"Simulação salva no MySQL com ID: {idSalvo}.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                // Lógica de salvamento em Arquivo
                using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                {
                    saveFileDialog1.Filter = "Arquivos Universo|*.uni|Todos os arquivos|*.*";
                    saveFileDialog1.Title = "Salvar arquivo atual";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        gravadorAtivo.GravarUniverso(U, saveFileDialog1.FileName, interac, tempo);
                    }
                }
            }
        }

        private void btn_grava_ini_Click(object sender, EventArgs e)
        {
            if (Uinicial == null || Uinicial.qtdCorp() == 0)
            {
                MessageBox.Show("Não há corpos no Universo inicial a serem salvos", "Atenção");
                return;
            }

            int.TryParse(qtdInterac.Text, out int interac);
            int.TryParse(qtdTempoInterac.Text, out int tempo);

            if (gravadorAtivo is GravadorMySQL)
            {
                // Lógica de Salvamento MySQL para Configuração Inicial
                string nomeSimulacao = Interaction.InputBox("Digite o nome da configuração INICIAL para salvar no MySQL:", "Salvar Configuração", "Configuração Inicial");

                if (!string.IsNullOrWhiteSpace(nomeSimulacao))
                {
                    int idSalvo = gravadorMySQL.SalvarSimulacao(Uinicial, nomeSimulacao, 0, 0);
                    if (idSalvo > 0)
                    {
                        MessageBox.Show($"Configuração inicial salva no MySQL com ID: {idSalvo}.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                // Lógica de salvamento em Arquivo
                using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                {
                    saveFileDialog1.Filter = "Arquivos Universo|*.uni|Todos os arquivos|*.*";
                    saveFileDialog1.Title = "Salvar arquivo inicial";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        gravadorAtivo.GravarUniversoInicial(Uinicial, saveFileDialog1.FileName);
                    }
                }
            }
        }

        private void btn_carrega_Click(object sender, EventArgs e)
        {
            // Este botão carrega a CONFIGURAÇÃO INICIAL (do GravadorTexto)
            if (gravadorAtivo is GravadorMySQL)
            {
                MessageBox.Show("Use o botão 'Carregar Simulação' para carregar do MySQL, pois ele requer um ID.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                // Lógica de carregamento em Arquivo (Carrega Configuração Inicial)
                using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
                {
                    openFileDialog1.Filter = "Arquivos Universo|*.uni|Todos os arquivos|*.*";
                    openFileDialog1.Title = "Abrir arquivo";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        U = gravadorAtivo.CarregarUniversoInicial(openFileDialog1.FileName);

                        if (U == null)
                        {
                            MessageBox.Show("Falha ao carregar o universo inicial.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        progressBar1.Value = 0;
                        Uinicial = new Universo();
                        Uinicial.copiaUniverso(U);
                        Form1.ActiveForm?.Refresh();
                    }
                }
            }
        }

        private void btn_carregaSimulacao_Click(object sender, EventArgs e)
        {
            // Este método espera um método de clique do designer
            // Lógica de Carregamento Simulação Avançada
            if (gravadorAtivo is GravadorMySQL)
            {
                // Lógica de Carregamento MySQL (Requer ID)
                string idInput = Interaction.InputBox("Digite o ID da simulação para carregar do MySQL:", "Carregar Simulação", "1");

                int idSimulacao;
                if (int.TryParse(idInput, out idSimulacao) && idSimulacao > 0)
                {
                    int numInteracOut, numTempoInteracOut;
                    U = gravadorMySQL.CarregarSimulacao(idSimulacao, out numInteracOut, out numTempoInteracOut);

                    if (U != null)
                    {
                        qtdCorpos.Text = U.qtdCorp().ToString();
                        qtdInterac.Text = numInteracOut.ToString();
                        qtdTempoInterac.Text = numTempoInteracOut.ToString();

                        progressBar1.Maximum = numInteracOut;
                        progressBar1.Value = 0;
                        Uinicial = new Universo();
                        Uinicial.copiaUniverso(U);
                        Form1.ActiveForm?.Refresh();
                    }
                }
                else
                {
                    MessageBox.Show("ID inválido ou cancelado.", "Erro de Carregamento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Lógica de carregamento em Arquivo
                using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
                {
                    openFileDialog1.Filter = "Arquivos Universo|*.uni|Todos os arquivos|*.*";
                    openFileDialog1.Title = "Abrir arquivo";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        GravadorUniverso gravador = new GravadorTexto();
                        int numInteracOut, numTempoInteracOut;
                        U = gravador.CarregarSimulacao(openFileDialog1.FileName, out numInteracOut, out numTempoInteracOut);

                        if (U != null)
                        {
                            qtdCorpos.Text = U.qtdCorp().ToString();
                            qtdInterac.Text = numInteracOut.ToString();
                            qtdTempoInterac.Text = numTempoInteracOut.ToString();
                            progressBar1.Maximum = numInteracOut;
                            progressBar1.Value = 0;
                            Uinicial = new Universo();
                            Uinicial.copiaUniverso(U);
                            Form1.ActiveForm?.Refresh();
                        }
                    }
                }
            }
        }
    }
}