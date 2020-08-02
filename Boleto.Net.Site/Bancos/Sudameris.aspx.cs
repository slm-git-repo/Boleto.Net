using System;
using BoletoNet;

public partial class Bancos_Sudameris : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime vencimento = new DateTime(2007, 9, 10);

        Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "0501", "6703255");

        c.Codigo = 13000;

        //Nosso número com 7 dígitos
        string nn = "0003020";
        //Nosso número com 13 dígitos
        //nn = "0000000003025";

        Boleto b = new Boleto(vencimento, 1642, "198", nn, c);// EnumEspecieDocumento_Sudameris.DuplicataMercantil);
        b.NumeroDocumento = "1008073";

        b.Sacado = new Sacado("000.000.000-00", "Fulano de Silva");
        b.Sacado.Endereco.End = "SSS 154 Bloco J Casa 23";
        b.Sacado.Endereco.Bairro = "Testando";
        b.Sacado.Endereco.Cidade = "Testelândia";
        b.Sacado.Endereco.CEP = "70000000";
        b.Sacado.Endereco.UF = "DF";

        //b.Instrucoes.Add("Não Receber após o vencimento");
        //b.Instrucoes.Add("Após o Vencimento pague somente no Sudameris");
        //b.Instrucoes.Add("Instrução 2");
        //b.Instrucoes.Add("Instrução 3");

        sudameris.Boleto = b;
        sudameris.Boleto.Valida();

        sudameris.MostrarComprovanteEntrega = (Request.Url.Query == "?show");
    }
}
