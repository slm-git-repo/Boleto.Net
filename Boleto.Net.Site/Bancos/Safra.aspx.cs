using System;
using BoletoNet;

public partial class Bancos_Safra : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime vencimento = new DateTime(2007, 9, 10);

        Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "0542", "5413000");

        c.Codigo = 13000;

        Boleto b = new Boleto(vencimento, 1642, "198", "02592082835", c);
        b.NumeroDocumento = "1008073";

        b.Sacado = new Sacado("000.000.000-00", "Eduardo Frare");
        b.Sacado.Endereco.End = "SSS 154 Bloco J Casa 23";
        b.Sacado.Endereco.Bairro = "Testando";
        b.Sacado.Endereco.Cidade = "Testelândia";
        b.Sacado.Endereco.CEP = "70000000";
        b.Sacado.Endereco.UF = "DF";

        //b.Instrucoes.Add("Não Receber após o vencimento");
        //b.Instrucoes.Add("Após o Vencimento pague somente no Bradesco");
        //b.Instrucoes.Add("Instrução 2");
        //b.Instrucoes.Add("Instrução 3");

        Safra.Boleto = b;
        Safra.Boleto.Valida();

        Safra.MostrarComprovanteEntrega = (Request.Url.Query == "?show");
    }
}
