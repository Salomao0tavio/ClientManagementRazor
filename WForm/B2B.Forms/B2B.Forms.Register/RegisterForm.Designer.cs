namespace B2B.Forms.Register
{
    partial class RegisterForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_salvar = new Button();
            l_email = new Label();
            l_senha = new Label();
            l_confirmarSenha = new Label();
            l_titulo = new Label();
            input_email = new TextBox();
            input_senha = new TextBox();
            input_confirmarSenha = new TextBox();
            SuspendLayout();
            // 
            // btn_salvar
            // 
            btn_salvar.Location = new Point(285, 304);
            btn_salvar.Name = "btn_salvar";
            btn_salvar.Size = new Size(192, 61);
            btn_salvar.TabIndex = 0;
            btn_salvar.Text = "Salvar";
            btn_salvar.UseVisualStyleBackColor = true;
            btn_salvar.Click += btn_salvar_Click;
            // 
            // l_email
            // 
            l_email.AutoSize = true;
            l_email.Location = new Point(16, 83);
            l_email.Name = "l_email";
            l_email.Size = new Size(88, 39);
            l_email.TabIndex = 1;
            l_email.Text = "Email";
            // 
            // l_senha
            // 
            l_senha.AutoSize = true;
            l_senha.Location = new Point(16, 181);
            l_senha.Name = "l_senha";
            l_senha.Size = new Size(99, 39);
            l_senha.TabIndex = 3;
            l_senha.Text = "Senha";
            // 
            // l_confirmarSenha
            // 
            l_confirmarSenha.AutoSize = true;
            l_confirmarSenha.Location = new Point(425, 181);
            l_confirmarSenha.Name = "l_confirmarSenha";
            l_confirmarSenha.Size = new Size(245, 39);
            l_confirmarSenha.TabIndex = 4;
            l_confirmarSenha.Text = "Comfirmar Senha";
            // 
            // l_titulo
            // 
            l_titulo.AutoSize = true;
            l_titulo.Location = new Point(247, 21);
            l_titulo.Name = "l_titulo";
            l_titulo.Size = new Size(288, 39);
            l_titulo.TabIndex = 5;
            l_titulo.Text = "Dados para Registro";
            // 
            // input_email
            // 
            input_email.Location = new Point(16, 125);
            input_email.Multiline = true;
            input_email.Name = "input_email";
            input_email.Size = new Size(754, 48);
            input_email.TabIndex = 7;
            // 
            // input_senha
            // 
            input_senha.Location = new Point(16, 223);
            input_senha.Multiline = true;
            input_senha.Name = "input_senha";
            input_senha.Size = new Size(345, 48);
            input_senha.TabIndex = 8;
            // 
            // input_confirmarSenha
            // 
            input_confirmarSenha.Location = new Point(425, 223);
            input_confirmarSenha.Multiline = true;
            input_confirmarSenha.Name = "input_confirmarSenha";
            input_confirmarSenha.Size = new Size(345, 48);
            input_confirmarSenha.TabIndex = 9;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(17F, 39F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(782, 403);
            Controls.Add(input_confirmarSenha);
            Controls.Add(input_senha);
            Controls.Add(input_email);
            Controls.Add(l_titulo);
            Controls.Add(l_confirmarSenha);
            Controls.Add(l_senha);
            Controls.Add(l_email);
            Controls.Add(btn_salvar);
            Font = new Font("Candara Light", 19F);
            Margin = new Padding(6);
            MaximizeBox = false;
            MaximumSize = new Size(800, 450);
            MinimumSize = new Size(800, 450);
            Name = "Form1";
            Text = "Cadastro";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_salvar;
        private Label l_email;
        private Label l_nome;
        private Label l_senha;
        private Label l_confirmarSenha;
        private Label l_titulo;
        private TextBox input_nome;
        private TextBox input_email;
        private TextBox input_senha;
        private TextBox input_confirmarSenha;
    }
}
