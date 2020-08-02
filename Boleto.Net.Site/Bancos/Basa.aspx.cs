using System;
using BoletoNet;

public partial class Bancos_Basa : System.Web.UI.Page
{
    void Page_PreInit(object sender, EventArgs e)
    {
        if (IsPostBack)
            MasterPageFile = "~/MasterPrint.master";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime vencimento = new DateTime(2008, 02, 07);

        Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "1234", "5", "12345678", "9");

        c.Codigo = 12548;

        Boleto b = new Boleto(vencimento, 45.50, "CNR", "125478", c);

        b.Sacado = new Sacado("000.000.000-00", "Fulano de Silva");
        b.Sacado.Endereco.End = "SSS 154 Bloco J Casa 23";
        b.Sacado.Endereco.Bairro = "Testando";
        b.Sacado.Endereco.Cidade = "Testelândia";
        b.Sacado.Endereco.CEP = "70000000";
        b.Sacado.Endereco.UF = "DF";


        b.NumeroDocumento = "12345678901";

        Basa.Boleto = b;
        Basa.Boleto.Valida();

        Basa.MostrarComprovanteEntrega = (Request.Url.Query == "?show");
    }
}
