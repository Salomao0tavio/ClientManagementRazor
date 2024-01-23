using Microsoft.AspNetCore.Identity;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace B2B.Forms.Register
{
    public partial class RegisterForm : Form
    {
        private readonly RegisterService registerService;
        private readonly IdentityDataContext _context;

        public RegisterForm(RegisterService registerService, IdentityDataContext context)
        {
            // Inicializa os componentes do formulário
            InitializeComponent();

            this.registerService = registerService;
            this._context = context;
        }

        private async void btn_salvar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validação do e-mail
                if (!IsValidEmail(input_email.Text))
                {
                    MessageBox.Show("Por favor, insira um e-mail válido.");
                    return;
                }

                // Verifica se o e-mail já existe
                if (AlreadyExists(input_email.Text))
                {
                    MessageBox.Show("Usuário com este e-mail já existe.");
                    return;
                }

                // Validação das senhas
                string senha = input_senha.Text;
                string confirmacaoSenha = input_confirmarSenha.Text;

                // Verifica se as senhas coincidem
                if (senha != confirmacaoSenha)
                {
                    MessageBox.Show("As senhas não coincidem.");
                    return;
                }

                // Verifica se a senha é forte
                if (!IsStrongPassword(senha))
                {
                    MessageBox.Show("A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caractere especial.");
                    return;
                }

                // Chama o RegisterService para cadastrar o usuário
                await registerService.CadastrarUsuario(input_email.Text, senha);
                MessageBox.Show("Usuário registrado com sucesso!");
            }
            catch (Exception ex)
            {
                // Trata exceções e exibe mensagens de erro
                MessageBox.Show("Erro: " + ex.Message);
                if (ex.InnerException != null)
                {
                    MessageBox.Show("Exceção interna: " + ex.InnerException.Message);
                }
            }
        }

        // Função para validar o formato do e-mail usando uma expressão regular
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        // Função para validar uma senha forte usando uma expressão regular
        private bool IsStrongPassword(string password)
        {
            // Pelo menos uma letra minúscula, uma letra maiúscula, um número e um caractere especial, com comprimento mínimo de 8 caracteres
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$");
        }

        // Função para verificar se o e-mail já existe no banco de dados
        private bool AlreadyExists(string email)
        {
            // Consulta o contexto do banco de dados para encontrar um usuário com o e-mail fornecido
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == email);

            // Se um usuário com o e-mail existir, retorna true; caso contrário, retorna false
            return existingUser != null;
        }
    }
}
