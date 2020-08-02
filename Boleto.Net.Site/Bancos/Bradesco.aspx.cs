using System;
using BoletoNet;

public partial class Bancos_Bradesco : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime vencimento = new DateTime(2009, 12, 17);

        Instrucao_Bradesco item = new Instrucao_Bradesco(9, 5);

        Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "1234", "5", "123456", "7");

        c.Codigo = 13000;

        Endereco end = new Endereco();
        end.Bairro = "Lago Sul";
        end.CEP = "71666660";
        end.Cidade = "Brasília- DF";
        end.Complemento = "Quadra XX Conjunto XX Casa XX";
        end.End = "Condominio de Brasilia - Quadra XX Conjunto XX Casa XX";
        end.Logradouro = "Cond. Brasilia";
        end.Numero = "55";
        end.UF = "DF";

        //Carteiras 
        Boleto b = new Boleto(vencimento, 1.01, "09", "01000000001", c);
        b.NumeroDocumento = "01000000001";

        b.Sacado = new Sacado("000.000.000-00", "Eduardo Frare");
        b.Sacado.Endereco = end;

        item.Descricao += " após " + item.QuantidadeDias.ToString() + " dias corridos do vencimento.";
        b.Instrucoes.Add(item); //"Não Receber após o vencimento");
        //b.Instrucoes.Add("Após o Vencimento pague somente no Bradesco");
        //b.Instrucoes.Add("Instrução 2");
        //b.Instrucoes.Add("Instrução 3");

        /* 
         * A data de vencimento não é usada
         * Usado para mostrar no lugar da data de vencimento o termo "Contra Apresentação";
         * Usado na carteira 06
         */
        //Bradesco.MostrarContraApresentacaoNaDataVencimento = true;

        Bradesco.Boleto = b;
        Bradesco.Boleto.Valida();

        Bradesco.MostrarComprovanteEntrega = (Request.Url.Query == "?show");
        Bradesco.MostrarContraApresentacaoNaDataVencimento = true;
    }
}
