using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using TesteLuizaLabs.Lib.Models;

namespace TesteLuizaLabs.Lib.Notificacao
{
    public static class Email
    {
        public static void Disparar(DisparoEmail disparoEmail)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(disparoEmail.Servidor);
                SmtpServer.Port = (!string.IsNullOrEmpty(disparoEmail.Porta)) ? Convert.ToInt32(disparoEmail.Porta) : 0;
                SmtpServer.Credentials = new System.Net.NetworkCredential(disparoEmail.Usuario, disparoEmail.Senha);
                SmtpServer.EnableSsl = true;

                mail.From = new MailAddress(disparoEmail.De);
                mail.To.Add(disparoEmail.Para);
                mail.Subject = disparoEmail.Assunto;
                mail.Body = disparoEmail.Mensagem;
                mail.IsBodyHtml = true;

                SmtpServer.Send(mail);
            }
            catch { }
        }

        public static void DispararRecuperacaoSenha(DisparoEmail disparoEmail)
        {
            try
            {
                var body = MontaHtml("recuperacao_senha.html", disparoEmail.Variaveis);

                disparoEmail.Assunto = "Recuperação de senha Teste Luizalabs";
                disparoEmail.Mensagem = body;

                Disparar(disparoEmail);
            }
            catch { }
        }

        public static void DispararCadastro(DisparoEmail disparoEmail)
        {
            try
            {
                var body = MontaHtml("cadastro.html", disparoEmail.Variaveis);

                disparoEmail.Assunto = "Cadastro realizado Teste Luizalabs";
                disparoEmail.Mensagem = body;

                Disparar(disparoEmail);
            }
            catch { }
        }

        public static void DispararSenhaAlterada(DisparoEmail disparoEmail)
        {
            try
            {
                var body = MontaHtml("senha_alterada.html", disparoEmail.Variaveis);

                disparoEmail.Assunto = "Senha alterada Teste Luizalabs";
                disparoEmail.Mensagem = body;

                Disparar(disparoEmail);
            }
            catch { }
        }

        private static string MontaHtml(string arquivo, Dictionary<string, string> variaveis)
        {
            var appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var body = File.ReadAllText($"{appPath}\\Templates\\{arquivo}");

            foreach(var variavel in variaveis)
                body = body.Replace($"##{variavel.Key.ToUpper()}##", variavel.Value);

            return body;
        }
    }
}