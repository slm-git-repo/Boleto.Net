using System;
using BoletoNet;

public partial class Bancos_HSBC : System.Web.UI.Page
{
    void Page_PreInit(object sender, EventArgs e)
    {
        if (IsPostBack)
            MasterPageFile = "~/MasterPrint.master";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime vencimento = new DateTime(2008, 7, 4);

        Cedente c = new Cedente("00.000.000/0000-00", "Minha empresa", "0000", "", "00000", "00");
        // Código fornecido pela agencia, NÃO é o numero da conta
        c.Codigo = 0000000; // 7 posicoes

        Boleto b = new Boleto(vencimento, 2, "CNR", "888888888", c); //cod documento
        b.NumeroDocumento = "9999999999999"; // nr documento

        b.Sacado = new Sacado("000.000.000-00", "Fulano de Tal");
        b.Sacado.Endereco.End = "lala";
        b.Sacado.Endereco.Bairro = "lala";
        b.Sacado.Endereco.Cidade = "Curitiba";
        b.Sacado.Endereco.CEP = "82000000";
        b.Sacado.Endereco.UF = "PR";

        hsbc.Boleto = b;
        hsbc.Boleto.Valida();

        hsbc.MostrarComprovanteEntrega = (Request.Url.Query == "?show");
    }
}
