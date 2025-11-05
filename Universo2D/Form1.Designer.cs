namespace Universo
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_aleatorio = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.qtdCorpos = new System.Windows.Forms.TextBox();
            this.qtdInterac = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.qtdTempoInterac = new System.Windows.Forms.TextBox();
            this.btn_executa = new System.Windows.Forms.Button();
            this.btn_grava = new System.Windows.Forms.Button();
            this.btn_carrega = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_testaMySQL = new System.Windows.Forms.Button(); // NOVO COMPONENTE
            this.label10 = new System.Windows.Forms.Label();
            this.qtdCorposAtual = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtProporcao = new System.Windows.Forms.TextBox();
            this.btn_carregaSimulacao = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rb_mysql = new System.Windows.Forms.RadioButton(); // NOVO RADIOBUTTON
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.masMax = new System.Windows.Forms.TextBox();
            this.masMin = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.valYMax = new System.Windows.Forms.TextBox();
            this.valXMax = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btn_grava_ini = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_aleatorio
            // 
            this.btn_aleatorio.Location = new System.Drawing.Point(350, 13);
            this.btn_aleatorio.Name = "btn_aleatorio";
            this.btn_aleatorio.Size = new System.Drawing.Size(75, 23);
            this.btn_aleatorio.TabIndex = 8;
            this.btn_aleatorio.Text = "Aleatório";
            this.btn_aleatorio.UseVisualStyleBackColor = true;
            this.btn_aleatorio.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, -1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Qtd. Corpos";
            // 
            // qtdCorpos
            // 
            this.qtdCorpos.Location = new System.Drawing.Point(0, 15);
            this.qtdCorpos.Name = "qtdCorpos";
            this.qtdCorpos.Size = new System.Drawing.Size(100, 20);
            this.qtdCorpos.TabIndex = 1;
            this.qtdCorpos.Text = "0";
            // 
            // qtdInterac
            // 
            this.qtdInterac.Location = new System.Drawing.Point(106, 15);
            this.qtdInterac.Name = "qtdInterac";
            this.qtdInterac.Size = new System.Drawing.Size(100, 20);
            this.qtdInterac.TabIndex = 2;
            this.qtdInterac.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(103, -1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Num. Interações";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(209, -1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Tempo entre interações (s)";
            // 
            // qtdTempoInterac
            // 
            this.qtdTempoInterac.Location = new System.Drawing.Point(212, 15);
            this.qtdTempoInterac.Name = "qtdTempoInterac";
            this.qtdTempoInterac.Size = new System.Drawing.Size(100, 20);
            this.qtdTempoInterac.TabIndex = 3;
            this.qtdTempoInterac.Text = "0";
            // 
            // btn_executa
            // 
            this.btn_executa.Location = new System.Drawing.Point(431, 13);
            this.btn_executa.Name = "btn_executa";
            this.btn_executa.Size = new System.Drawing.Size(75, 23);
            this.btn_executa.TabIndex = 9;
            this.btn_executa.Text = "Executar";
            this.btn_executa.UseVisualStyleBackColor = true;
            this.btn_executa.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_grava
            // 
            this.btn_grava.Location = new System.Drawing.Point(431, 41);
            this.btn_grava.Name = "btn_grava";
            this.btn_grava.Size = new System.Drawing.Size(75, 23);
            this.btn_grava.TabIndex = 11;
            this.btn_grava.Text = "Gravar";
            this.btn_grava.UseVisualStyleBackColor = true;
            this.btn_grava.Click += new System.EventHandler(this.btn_grava_Click);
            // 
            // btn_carrega
            // 
            this.btn_carrega.Location = new System.Drawing.Point(350, 42);
            this.btn_carrega.Name = "btn_carrega";
            this.btn_carrega.Size = new System.Drawing.Size(75, 23);
            this.btn_carrega.TabIndex = 10;
            this.btn_carrega.Text = "Carregar";
            this.btn_carrega.UseVisualStyleBackColor = true;
            this.btn_carrega.Click += new System.EventHandler(this.btn_carrega_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.btn_testaMySQL); // NOVO BOTÃO
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.qtdCorposAtual);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtProporcao);
            this.groupBox1.Controls.Add(this.btn_carregaSimulacao);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.masMax);
            this.groupBox1.Controls.Add(this.masMin);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.valYMax);
            this.groupBox1.Controls.Add(this.valXMax);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.btn_grava_ini);
            this.groupBox1.Controls.Add(this.btn_carrega);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btn_grava);
            this.groupBox1.Controls.Add(this.btn_executa);
            this.groupBox1.Controls.Add(this.qtdTempoInterac);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.qtdInterac);
            this.groupBox1.Controls.Add(this.qtdCorpos);
            this.groupBox1.Controls.Add(this.btn_aleatorio);
            this.groupBox1.Location = new System.Drawing.Point(847, 584);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(507, 149);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // btn_testaMySQL
            // 
            this.btn_testaMySQL.Location = new System.Drawing.Point(223, 93);
            this.btn_testaMySQL.Name = "btn_testaMySQL";
            this.btn_testaMySQL.Size = new System.Drawing.Size(121, 23);
            this.btn_testaMySQL.TabIndex = 14;
            this.btn_testaMySQL.Text = "Testar MySQL";
            this.btn_testaMySQL.UseVisualStyleBackColor = true;
            this.btn_testaMySQL.Click += new System.EventHandler(this.btn_testaMySQL_Click);
            //
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 36);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Qtd. Corpos Atual";
            // 
            // qtdCorposAtual
            // 
            this.qtdCorposAtual.Location = new System.Drawing.Point(0, 49);
            this.qtdCorposAtual.Name = "qtdCorposAtual";
            this.qtdCorposAtual.Size = new System.Drawing.Size(100, 20);
            this.qtdCorposAtual.TabIndex = 23;
            this.qtdCorposAtual.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Proporção";
            // 
            // txtProporcao
            // 
            this.txtProporcao.Location = new System.Drawing.Point(4, 86);
            this.txtProporcao.Name = "txtProporcao";
            this.txtProporcao.Size = new System.Drawing.Size(100, 20);
            this.txtProporcao.TabIndex = 20;
            // 
            // btn_carregaSimulacao
            // 
            this.btn_carregaSimulacao.Location = new System.Drawing.Point(350, 93);
            this.btn_carregaSimulacao.Name = "btn_carregaSimulacao";
            this.btn_carregaSimulacao.Size = new System.Drawing.Size(156, 23);
            this.btn_carregaSimulacao.TabIndex = 13;
            this.btn_carregaSimulacao.Text = "Carregar Simulação";
            this.btn_carregaSimulacao.UseVisualStyleBackColor = true;
            this.btn_carregaSimulacao.Click += new System.EventHandler(this.btn_carregaSimulacao_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rb_mysql); // NOVO RADIOBUTTON
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Location = new System.Drawing.Point(10, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(491, 37);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            // 
            // rb_mysql
            // 
            this.rb_mysql.AutoSize = true;
            this.rb_mysql.Location = new System.Drawing.Point(104, 12);
            this.rb_mysql.Name = "rb_mysql";
            this.rb_mysql.Size = new System.Drawing.Size(65, 17);
            this.rb_mysql.TabIndex = 22;
            this.rb_mysql.Text = "MySQL";
            this.rb_mysql.UseVisualStyleBackColor = true;
            this.rb_mysql.CheckedChanged += new System.EventHandler(this.rb_mysql_CheckedChanged); // NOVO EVENTO
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(269, 12);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(86, 17);
            this.radioButton1.TabIndex = 20;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Atualiza Tela";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(175, 12);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(83, 17);
            this.radioButton2.TabIndex = 21;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Background";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(12, 12);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(86, 17);
            this.radioButton3.TabIndex = 13;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Para Arquivo";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(211, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Massa Máxima (kg)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(105, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Massa Mínima (kg)";
            // 
            // masMax
            // 
            this.masMax.Location = new System.Drawing.Point(212, 86);
            this.masMax.Name = "masMax";
            this.masMax.Size = new System.Drawing.Size(100, 20);
            this.masMax.TabIndex = 7;
            this.masMax.Text = "500000";
            // 
            // masMin
            // 
            this.masMin.Location = new System.Drawing.Point(106, 86);
            this.masMin.Name = "masMin";
            this.masMin.Size = new System.Drawing.Size(100, 20);
            this.masMin.TabIndex = 6;
            this.masMin.Text = "10000";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(211, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Y Máximo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(105, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "X Máximo";
            // 
            // valYMax
            // 
            this.valYMax.Location = new System.Drawing.Point(212, 49);
            this.valYMax.Name = "valYMax";
            this.valYMax.Size = new System.Drawing.Size(100, 20);
            this.valYMax.TabIndex = 5;
            // 
            // valXMax
            // 
            this.valXMax.Location = new System.Drawing.Point(106, 49);
            this.valXMax.Name = "valXMax";
            this.valXMax.Size = new System.Drawing.Size(100, 20);
            this.valXMax.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Execução";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(0, 120);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(206, 23);
            this.progressBar1.TabIndex = 13;
            // 
            // btn_grava_ini
            // 
            this.btn_grava_ini.Location = new System.Drawing.Point(350, 70);
            this.btn_grava_ini.Name = "btn_grava_ini";
            this.btn_grava_ini.Size = new System.Drawing.Size(156, 23);
            this.btn_grava_ini.TabIndex = 12;
            this.btn_grava_ini.Text = "Gravar Configuração Inicial";
            this.btn_grava_ini.UseVisualStyleBackColor = true;
            this.btn_grava_ini.Click += new System.EventHandler(this.btn_grava_ini_Click);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1366, 745);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Universo 2D";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.Button btn_aleatorio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox qtdCorpos;
        private System.Windows.Forms.TextBox qtdInterac;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox qtdTempoInterac;
        private System.Windows.Forms.Button btn_executa;
        private System.Windows.Forms.Button btn_grava;
        private System.Windows.Forms.Button btn_carrega;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_grava_ini;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox valYMax;
        private System.Windows.Forms.TextBox valXMax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox masMax;
        private System.Windows.Forms.TextBox masMin;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_carregaSimulacao;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtProporcao;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox qtdCorposAtual;

        // --- NOVOS COMPONENTES DECLARADOS ---
        private System.Windows.Forms.RadioButton rb_mysql;
        private System.Windows.Forms.Button btn_testaMySQL;
        // ------------------------------------
    }
}