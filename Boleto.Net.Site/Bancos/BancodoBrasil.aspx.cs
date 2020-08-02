using System;
using BoletoNet;

public partial class Bancos_BancodoBrasil : System.Web.UI.Page
{
    void Page_PreInit(object sender, EventArgs e)
    {
        if (IsPostBack)
            MasterPageFile = "~/MasterPrint.master";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime vencimento = new DateTime(2008, 11, 11);

        #region Exemplo Carteira 16, com nosso número de 11 posições
        /*
         * Nesse exemplo utilizamos a carteira 16 e o nosso número no máximo de 11 posições.
         * Não é necessário informar o numero do convênio e nem o tipo da modalidade.
         * O nosso número tem que ter no máximo 11 posições.
         */

        Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "1234", "1", "123456", "1");
        Boleto b = new Boleto(vencimento, 0.01, "16", "09876543210", c);

        #endregion Exemplo Carteira 16, com nosso número de 11 posições

        #region Exemplo Carteira 16, convênio de 6 posições e tipo modalidade 21
        /*
         * Nesse exemplo utilizamos a carteira 16 e o número do convênio de 6 posições.
         * É obrigatório informar o tipo da modalidade 21.
         * O nosso número tem que ter no máximo 10 posições.
         */

        //Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "1234", "1", "123456", "1");
        //c.Convenio = 123456;

        //Boleto b = new Boleto(vencimento, 0.01, "16", "09876543210", c);
        //b.TipoModalidade = "21";
        #endregion Exemplo Carteira 16, convênio de 6 posições e tipo modalidade 21

        #region Exemplo Carteira 18, com nosso número de 11 posições
        /*
         * Nesse exemplo utilizamos a carteira 18 e o nosso número no máximo de 11 posições.
         * Não é necessário informar o numero do convênio e nem o tipo da modalidade.
         * O nosso número tem que ter no máximo 11 posições.
         */

        //Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "1234", "1", "123456", "1");
        //Boleto b = new Boleto(vencimento, 0.01, "18", "09876543210", c);

        #endregion Exemplo Carteira 18, com nosso número de 11 posições

        #region Exemplo Carteira 18, convênio de 6 posições e tipo modalidade 21
        /*
         * Nesse exemplo utilizamos a carteira 18 e o número do convênio de 6 posições.
         * É obrigatório informar o tipo da modalidade 21.
         * O nosso número tem que ter no máximo 10 posições.
         */

        //Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "1234", "1", "123456", "1");
        //c.Convenio = 123456;
        //Boleto b = new Boleto(vencimento, 0.01, "18", "09876543210", c);
        //b.TipoModalidade = "21";
        #endregion Exemplo Carteira 18, convênio de 6 posições e tipo modalidade 21


        b.NumeroDocumento = "12415487";

        b.Sacado = new Sacado("000.000.000-00", "Fulano de Tal");
        b.Sacado.Endereco.End = "Rua sem numero";
        b.Sacado.Endereco.Bairro = "Testando";
        b.Sacado.Endereco.Cidade = "Testelândia";
        b.Sacado.Endereco.CEP = "70000000";
        b.Sacado.Endereco.UF = "DF";

        //Adiciona as instruções ao boleto
        #region Instruções
        //Protestar
        Instrucao_BancoBrasil item = new Instrucao_BancoBrasil(9, 5);
        b.Instrucoes.Add(item);
        //ImportanciaporDiaDesconto
        item = new Instrucao_BancoBrasil(30, 0);
        b.Instrucoes.Add(item);
        //ProtestarAposNDiasCorridos
        item = new Instrucao_BancoBrasil(81, 15);
        b.Instrucoes.Add(item);
        #endregion Instruções

        BancodoBrasil.Boleto = b;
        BancodoBrasil.Boleto.Valida();

        BancodoBrasil.MostrarComprovanteEntrega = (Request.Url.Query == "?show");
    }
}
