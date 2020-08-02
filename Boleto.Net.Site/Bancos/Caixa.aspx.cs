using System;
using BoletoNet;

public partial class Bancos_Caixa : System.Web.UI.Page
{
    void Page_PreInit(object sender, EventArgs e)
    {
        if (IsPostBack)
            MasterPageFile = "~/MasterPrint.master";
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime vencimento = new DateTime(2008, 12, 20);

        Cedente c = new Cedente("000.000.000-00", "Boleto.net ILTDA", "1234", "12345678", "9");

        c.Codigo = 112233;   
        

        Boleto b = new Boleto(vencimento, 20.00, "2", "000000000000050", c);

        b.Sacado = new Sacado("000.000.000-00", "Meu Nome");
        b.Sacado.Endereco.End = "meu Endereço";
        b.Sacado.Endereco.Bairro = "Meu Bairro";
        b.Sacado.Endereco.Cidade = "Minha Cidade";
        b.Sacado.Endereco.CEP = "70000000";
        b.Sacado.Endereco.UF = "UF";

        //Adiciona as instruções ao boleto
        #region Instruções
        Instrucao_Caixa item; 
        //ImportanciaporDiaDesconto
        item = new Instrucao_Caixa((int)EnumInstrucoes_Caixa.Multa, Convert.ToDecimal(0.40));
        b.Instrucoes.Add(item);
        item = new Instrucao_Caixa((int)EnumInstrucoes_Caixa.JurosdeMora, Convert.ToDecimal(0.01));
        b.Instrucoes.Add(item);
        item = new Instrucao_Caixa((int)EnumInstrucoes_Caixa.NaoReceberAposNDias, 90);
        b.Instrucoes.Add(item);
        #endregion Instruções

        b.EspecieDocumento = new EspecieDocumento_Caixa((int)EnumEspecieDocumento_Caixa.DuplicataMercantil);
        b.NumeroDocumento = "00001";
        b.DataProcessamento = DateTime.Now;
        b.DataDocumento = DateTime.Now;
        caixa.Boleto = b;
        caixa.Boleto.Valida();
        //lblCodigoBarras.Text = b.CodigoBarra.Codigo.ToString();

        caixa.MostrarComprovanteEntrega = (Request.Url.Query == "?show");
    }
}
