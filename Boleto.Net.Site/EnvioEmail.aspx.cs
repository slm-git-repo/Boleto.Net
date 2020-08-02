using System;
using System.IO;
using System.Net.Mail;
using System.Web.UI;
using BoletoNet;

public partial class EnvioEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DateTime vencimento = new DateTime(2007, 9, 10);

        Instrucao_Itau item1 = new Instrucao_Itau(9, 5);
        Instrucao_Itau item2 = new Instrucao_Itau(81, 10);
        Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "0542", "13000");
        //Na carteira 198 o código do Cedente é a conta bancária
        c.Codigo = 13000;

        Boleto b = new Boleto(vencimento, 1642, "198", "92082835", c);
        b.NumeroDocumento = "1008073";

        b.Sacado = new Sacado("000.000.000-00", "Fulano de Silva");
        b.Sacado.Endereco.End = "SSS 154 Bloco J Casa 23";
        b.Sacado.Endereco.Bairro = "Testando";
        b.Sacado.Endereco.Cidade = "Testelândia";
        b.Sacado.Endereco.CEP = "70000000";
        b.Sacado.Endereco.UF = "DF";

        item2.Descricao += " " + item2.QuantidadeDias.ToString() + " dias corridos do vencimento.";
        b.Instrucoes.Add(item1);
        b.Instrucoes.Add(item2);

        MailMessage mail = new MailMessage();
        mail.To.Add(new MailAddress(TextBox1.Text));
        mail.Subject = "Teste de envio de Boleto Bancário";
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;

        BoletoBancario itau = new BoletoBancario();
        itau.CodigoBanco = 341;
        itau.Boleto = b;
    
        if (RadioButton1.Checked)
        {
            mail.Subject += " - On-Line";
            Panel1.Controls.Add(itau);

            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htmlTW = new HtmlTextWriter(sw);
            Panel1.RenderControl(htmlTW);
            string html = sw.ToString();
            //
            mail.Body = html;
        }
        else
        {
            mail.Subject += " - Off-Line";
            mail.AlternateViews.Add(itau.HtmlBoletoParaEnvioEmail());
        }

        //string html1 = "";
        //using (StreamReader sr = new StreamReader(mail.AlternateViews[0].ContentStream))
        //{
        //    html1 = sr.ReadToEnd();
        //    sr.Close();
        //    sr.Dispose();
        //}

        //Response.Write(html1);

        SmtpClient objSmtpClient = new SmtpClient();
        objSmtpClient.Send(mail);

        Label1.Text = "Boleto enviado para o email: " + TextBox1.Text;
    }
}
