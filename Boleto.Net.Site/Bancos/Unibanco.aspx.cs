using System;
using BoletoNet;

public partial class Bancos_Unibanco : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // ----------------------------------------------------------------------------------------
        // Exemplo 1

        //DateTime vencimento = new DateTime(2001, 12, 31);

        //Cedente c = new Cedente("00.000.000/0000-00", "Next Consultoria", "1234", "5", "123456", "7");
        //c.Codigo = 123456;

        //Boleto b = new Boleto(vencimento, 1000.00, "20", "1803029901", c);
        //b.NumeroDocumento = b.NossoNumero;

        // ----------------------------------------------------------------------------------------
        // Exemplo 2

        DateTime vencimento = new DateTime(2008, 03, 10);

        Cedente c = new Cedente("00.000.000/0000-00", "Next Consultoria Ltda.", "0123", "100618", "9");
        c.Codigo = 203167;

        Boleto b = new Boleto(vencimento, 2952.95, "20", "1803029901", c);
        b.NumeroDocumento = b.NossoNumero;

        // ----------------------------------------------------------------------------------------

        //b.Instrucoes.Add("Não receber após o vencimento");
        //b.Instrucoes.Add("Após o vencimento pague somente no Unibanco");
        //b.Instrucoes.Add("Taxa bancária - R$ 2,95");
        //b.Instrucoes.Add("Emitido pelo componente Boleto.NET");

        // ----------------------------------------------------------------------------------------

        b.Sacado = new Sacado("000.000.000-00", "Marlon Oliveira");
        b.Sacado.Endereco.End = "Rua Dr. Henrique Portugal, XX";
        b.Sacado.Endereco.Bairro = "São Francisco";
        b.Sacado.Endereco.Cidade = "Niterói";
        b.Sacado.Endereco.CEP = "24360080";
        b.Sacado.Endereco.UF = "RJ";
        b.Sacado.Endereco.Logradouro = "Rua Dr. Henrique Portugal";
        b.Sacado.Endereco.Numero = "XX";
        b.Sacado.Endereco.Complemento = "Casa";

        Unibanco.Boleto = b;
        Unibanco.Boleto.Valida();

        Unibanco.MostrarComprovanteEntrega = (Request.Url.Query == "?show");
    }
}
