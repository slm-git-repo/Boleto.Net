using System;
using BoletoNet;

public partial class Bancos_Itau : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime vencimento = new DateTime(2007, 9, 10);

        Instrucao_Itau item1 = new Instrucao_Itau(9, 5);
        Instrucao_Itau item2 = new Instrucao_Itau(81, 10);
        Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "0542", "13000");
        //Na carteira 198 o código do Cedente é a conta bancária
        c.Codigo = 13000;

        Boleto b = new Boleto(vencimento, 1642, "198", "92082835", c, new EspecieDocumento(341, 1));
        b.NumeroDocumento = "1008073";

        b.Sacado = new Sacado("000.000.000-00", "Fulano de Silva");
        b.Sacado.Endereco.End = "SSS 154 Bloco J Casa 23";
        b.Sacado.Endereco.Bairro = "Testando";
        b.Sacado.Endereco.Cidade = "Testelândia";
        b.Sacado.Endereco.CEP = "70000000";
        b.Sacado.Endereco.UF = "DF";

        // Exemplo de como adicionar mais informações ao sacado
        b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + "2541245"));

        item2.Descricao += " " + item2.QuantidadeDias.ToString() + " dias corridos do vencimento.";
        b.Instrucoes.Add(item1);
        b.Instrucoes.Add(item2);

        // juros/descontos

        if (b.ValorDesconto == 0)
        {
            Instrucao_Itau item3 = new Instrucao_Itau(999, 1);
            item3.Descricao += ("1,00 por dia de antecipação.");
            b.Instrucoes.Add(item3);
        }


        itau.Boleto = b;
        itau.Boleto.Valida();

        itau.MostrarComprovanteEntrega = (Request.Url.Query == "?show");
    }
}
