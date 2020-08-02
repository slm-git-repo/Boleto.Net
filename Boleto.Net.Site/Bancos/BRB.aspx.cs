using System;
using BoletoNet;

public partial class Bancos_BRB : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime vencimento = new DateTime(2007, 11, 15);

        Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "208", "", "010357", "6");


        c.Codigo = 13000;

        Endereco end = new Endereco();
        end.Bairro = "Lago Sul";
        end.CEP = "71666-666";
        end.Cidade = "Brasília- DF";
        end.Complemento = "Quadra XX Conjunto XX Casa XX";
        end.End = "Condominio de Brasilia - Quadra XX Conjunto XX Casa XX";
        end.Logradouro = "Cond. Brasilia";
        end.Numero = "55";
        end.UF = "DF";        

        Boleto b = new Boleto(vencimento, 0.01, "COB", "119964", c);
        b.NumeroDocumento = "119964";

        b.Sacado = new Sacado("000.000.000-00", "Fulano de Tal");
        b.Sacado.Endereco = end;

        //b.Instrucoes.Add("Não Receber após o vencimento");
        //b.Instrucoes.Add("Após o Vencimento pague somente no Bradesco");
        //b.Instrucoes.Add("Instrução 2");
        //b.Instrucoes.Add("Instrução 3");

        brb.Boleto = b;
        brb.Boleto.Valida();

        brb.MostrarComprovanteEntrega = (Request.Url.Query == "?show");
    }
}
