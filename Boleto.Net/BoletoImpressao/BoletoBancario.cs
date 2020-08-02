using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;
using Microsoft.VisualBasic;
//Envio por email
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;

[assembly: WebResource("BoletoNet.BoletoImpressao.BoletoNet.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("BoletoNet.Imagens.barra.gif", "image/gif")]
//[assembly: WebResource("BoletoNet.Imagens.corte.gif", "image/gif")]
//[assembly: WebResource("BoletoNet.Imagens.barrainterna.gif", "image/gif")]
//[assembly: WebResource("BoletoNet.Imagens.ponto.gif", "image/gif")]

namespace BoletoNet
{
    [Serializable(),
    Designer(typeof(BoletoBancarioDesigner)),
    ToolboxBitmap(typeof(BoletoBancario)),
    ToolboxData("<{0}:BoletoBancario Runat=\"server\"></{0}:BoletoBancario>")]
    public class BoletoBancario : System.Web.UI.Control
    {
        #region Variaveis

        private Banco _ibanco = null;
        private short _codigoBanco = 0;
        private Boleto _boleto;
        private Cedente _cedente;
        private Sacado _sacado;
        private List<IInstrucao> _instrucoes = new List<IInstrucao>();
        private string _instrucoesHtml = string.Empty;
        private bool _mostrarCodigoCarteira = false;

        #endregion Variaveis

        #region Propriedades

        [Browsable(true), Description("Código do banco em que será gerado o boleto. Ex. 341-Itaú, 237-Bradesco")]
        public short CodigoBanco
        {
            get { return _codigoBanco; }
            set { _codigoBanco = value; }
        }

        /// <summary>
        /// Mostra o código da carteira
        /// </summary>
        [Browsable(true), Description("Mostra o código da carteira")]
        public bool MostrarCodigoCarteira
        {
            get { return _mostrarCodigoCarteira; }
            set { _mostrarCodigoCarteira = value; }
        }

        [Browsable(false)]
        public Boleto Boleto
        {
            get { return _boleto; }
            set
            {
                _boleto = value;

                if (_ibanco == null)
                    _boleto.Banco = this.Banco;

                _cedente = _boleto.Cedente;
                _sacado = _boleto.Sacado;
            }
        }

        [Browsable(false)]
        public Sacado Sacado
        {
            get { return _sacado; }
        }

        [Browsable(false)]
        public Cedente Cedente
        {
            get { return _cedente; }
        }

        [Browsable(false)]
        public Banco Banco
        {
            get
            {
                if ((_ibanco == null) ||
                    (_ibanco.Codigo != _codigoBanco))
                {
                    _ibanco = new Banco(_codigoBanco);
                }

                if (_boleto != null)
                    _boleto.Banco = _ibanco;

                return _ibanco;
            }
        }

        #region Propriedades
        [Browsable(true), Description("Mostra o comprovante de entrega")]
        public bool MostrarComprovanteEntrega
        {
            get { return Utils.ToBool(ViewState["MostrarComprovanteEntrega"]); }
            set { ViewState["MostrarComprovanteEntrega"] = value; }
        }

        [Browsable(true), Description("Oculta as intruções do boleto")]
        public bool OcultarEnderecoSacado
        {
            get { return Utils.ToBool(ViewState["OcultarEnderecoSacado"]); }
            set { ViewState["OcultarEnderecoSacado"] = value; }
        }

        [Browsable(true), Description("Oculta as intruções do boleto")]
        public bool OcultarInstrucoes
        {
            get { return Utils.ToBool(ViewState["OcultarInstrucoes"]); }
            set { ViewState["OcultarInstrucoes"] = value; }
        }

        [Browsable(true), Description("Oculta o recibo do sacado do boleto")]
        public bool OcultarReciboSacado
        {
            get { return Utils.ToBool(ViewState["OcultarReciboSacado"]); }
            set { ViewState["OcultarReciboSacado"] = value; }
        }

        [Browsable(true), Description("Gerar arquivo de remessa")]
        public bool GerarArquivoRemessa
        {
            get { return Utils.ToBool(ViewState["GerarArquivoRemessa"]); }
            set { ViewState["GerarArquivoRemessa"] = value; }
        }
        /// <summary> 
        /// Mostra o termo "Contra Apresentação" na data de vencimento do boleto
        /// </summary>
        public bool MostrarContraApresentacaoNaDataVencimento
        {
            get { return Utils.ToBool(ViewState["MCANDV"]); }
            set { ViewState["MCANDV"] = value; }
        }
        #endregion Propriedades

        /// <summary> 
        /// Retorna o campo para a 1 linha da instrucao.
        /// </summary>
        public List<IInstrucao> Instrucoes
        {
            get
            {
                return _instrucoes;
            }

            set
            {
                _instrucoes = value;
            }
        }


        #endregion Propriedades

        #region Override
        protected override void OnPreRender(EventArgs e)
        {
            string alias = "BoletoNet.BoletoImpressao.BoletoNet.css";
            string csslink = "<link rel=\"stylesheet\" type=\"text/css\" href=\"" +
                Page.ClientScript.GetWebResourceUrl(typeof(BoletoBancario), alias) + "\" />";

            LiteralControl include = new LiteralControl(csslink);
            Page.Header.Controls.Add(include);

            base.OnPreRender(e);
        }

        protected override void OnLoad(EventArgs e)
        {
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "Execution")]
        protected override void Render(HtmlTextWriter output)
        {
            if (_ibanco == null)
            {
                output.Write("<b>Erro gerando o boleto bancário: faltou definir o banco.</b>");
                return;
            }
            string urlImagemLogo = Page.ClientScript.GetWebResourceUrl(typeof(BoletoBancario), "BoletoNet.Imagens." + Utils.FormatCode(_ibanco.Codigo.ToString(), 3) + ".jpg");
            string urlImagemBarra = Page.ClientScript.GetWebResourceUrl(typeof(BoletoBancario), "BoletoNet.Imagens.barra.gif");
            //string urlImagemBarraInterna = Page.ClientScript.GetWebResourceUrl(typeof(BoletoBancario), "BoletoNet.Imagens.barrainterna.gif");
            //string urlImagemCorte = Page.ClientScript.GetWebResourceUrl(typeof(BoletoBancario), "BoletoNet.Imagens.corte.gif");
            //string urlImagemPonto = Page.ClientScript.GetWebResourceUrl(typeof(BoletoBancario), "BoletoNet.Imagens.ponto.gif");

            //Atribui os valores ao html do boleto bancário
            //output.Write(MontaHtml(urlImagemCorte, urlImagemLogo, urlImagemBarra, urlImagemPonto, urlImagemBarraInterna,
            //    "<img src=\"ImagemCodigoBarra.ashx?code=" + Boleto.CodigoBarra.Codigo + "\" alt=\"Código de Barras\" />"));
            output.Write(MontaHtml(urlImagemLogo, urlImagemBarra, "<img src=\"ImagemCodigoBarra.ashx?code=" + Boleto.CodigoBarra.Codigo + "\" alt=\"Código de Barras\" />"));
        }
        #endregion Override

        #region Html
        public string GeraHtmlInstrucoes()
        {
            try
            {
                StringBuilder html = new StringBuilder();

                string titulo = "Instruções de Impressão";
                string instrucoes = "Imprimir em impressora jato de tinta (ink jet) ou laser em qualidade normal. (Não use modo econômico).<br>Utilize folha A4 (210 x 297 mm) ou Carta (216 x 279 mm) - Corte na linha indicada<br>";

                html.Append(Html.Instrucoes);
                html.Append("<br />");

                return html.ToString()
                    .Replace("@TITULO", titulo)
                    .Replace("@INSTRUCAO", instrucoes);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a execução da transação.", ex);
            }
        }

        public string GeraHtmlReciboSacado()
        {
            try
            {
                StringBuilder html = new StringBuilder();

                html.Append(Html.ReciboSacadoParte1);
                html.Append("<br />");
                html.Append(Html.ReciboSacadoParte2);
                html.Append(Html.ReciboSacadoParte3);
                html.Append(Html.ReciboSacadoParte4);
                html.Append(Html.ReciboSacadoParte5);
                html.Append(Html.ReciboSacadoParte6);
                html.Append(Html.ReciboSacadoParte7);

                if (Instrucoes.Count == 0)
                    html.Append(Html.ReciboSacadoParte8);

                MontaInstrucoes();

                return html.ToString().Replace("@INSTRUCOES", _instrucoesHtml);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a execução da transação.", ex);
            }
        }

        public string GeraHtmlReciboCedente()
        {
            try
            {
                StringBuilder html = new StringBuilder();

                html.Append(Html.ReciboCedenteParte1);
                html.Append(Html.ReciboCedenteParte2);
                html.Append(Html.ReciboCedenteParte3);
                html.Append(Html.ReciboCedenteParte4);
                html.Append(Html.ReciboCedenteParte5);
                html.Append(Html.ReciboCedenteParte6);
                html.Append(Html.ReciboCedenteParte7);
                html.Append(Html.ReciboCedenteParte8);
                html.Append(Html.ReciboCedenteParte9);
                html.Append(Html.ReciboCedenteParte10);
                html.Append(Html.ReciboCedenteParte11);
                html.Append(Html.ReciboCedenteParte12);

                MontaInstrucoes();

                return html.ToString().Replace("@INSTRUCOES", _instrucoesHtml);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na execução da transação.", ex);
            }
        }

        public string HtmlComprovanteEntrega
        {
            get
            {
                StringBuilder html = new StringBuilder();

                html.Append(Html.ComprovanteEntrega1);
                html.Append(Html.ComprovanteEntrega2);
                html.Append(Html.ComprovanteEntrega3);
                html.Append(Html.ComprovanteEntrega4);
                html.Append(Html.ComprovanteEntrega5);
                html.Append(Html.ComprovanteEntrega6);
                html.Append(Html.ComprovanteEntrega7);
                html.Append("<br />");
                return html.ToString();
            }
        }

        private void MontaInstrucoes()
        {
            if (string.IsNullOrEmpty(_instrucoesHtml))
                if (Boleto.Instrucoes.Count > 0)
                {
                    _instrucoesHtml = string.Empty;
                    //Flavio(fhlviana@hotmail.com) - retirei a tag <span> de cada instrução por não ser mais necessáras desde que dentro
                    //da div que contem as instruções a classe cpN se aplica, que é a mesma, em conteudo, da classe cp
                    foreach (IInstrucao instrucao in Boleto.Instrucoes)
                        _instrucoesHtml += string.Format("{0}<br />", instrucao.Descricao);

                    _instrucoesHtml = Strings.Left(_instrucoesHtml, _instrucoesHtml.Length - 6);
                }
        }

        //private string MontaHtml(string urlImagemCorte, string urlImagemLogo, string urlImagemBarra, string urlImagemPonto, string urlImagemBarraInterna, string imagemCodigoBarras)
        private string MontaHtml(string urlImagemLogo, string urlImagemBarra, string imagemCodigoBarras)
        {
            StringBuilder html = new StringBuilder();

            //Oculta o cabeçalho das instruções do boleto
            if (!OcultarInstrucoes)
                html.Append(GeraHtmlInstrucoes());

            //Mostra o comprovante de entrega
            if (MostrarComprovanteEntrega)
                html.Append(HtmlComprovanteEntrega);

            //Oculta o recibo do sacabo do boleto
            if (!OcultarReciboSacado)
                html.Append(GeraHtmlReciboSacado());

            string sacado = "";
            //Flavio(fhlviana@hotmail.com) - adicionei a possibilidade de o boleto não ter, necessáriamente, que informar o CPF ou CNPJ do sacado.
            //Formata o CPF/CNPJ(se houver) e o Nome do Sacado para apresentação
            if (Sacado.CPFCNPJ == string.Empty)
            {
                sacado = Sacado.Nome;
            }
            else
            {
                if (Sacado.CPFCNPJ.Length <= 11)
                    sacado = string.Format("{0}  CPF: {1}", Sacado.Nome, Utils.FormataCPF(Sacado.CPFCNPJ));
                else
                    sacado = string.Format("{0}  CNPJ: {1}", Sacado.Nome, Utils.FormataCNPJ(Sacado.CPFCNPJ));
            }

            String infoSacado = Sacado.InformacoesSacado.GeraHTML(false);

            //Caso não oculte o Endereço do Sacado,
            if (!OcultarEnderecoSacado)
            {
                String enderecoSacado = "";

                if (Sacado.Endereco.CEP == String.Empty)
                    enderecoSacado = string.Format("{0} - {1}/{2}", Sacado.Endereco.Bairro, Sacado.Endereco.Cidade, Sacado.Endereco.UF);
                else
                    enderecoSacado = string.Format("{0} - {1}/{2} - CEP: {3}", Sacado.Endereco.Bairro,
                    Sacado.Endereco.Cidade, Sacado.Endereco.UF, Utils.FormataCEP(Sacado.Endereco.CEP));

                if (Sacado.Endereco.End != string.Empty && enderecoSacado != string.Empty)
                    if (infoSacado == string.Empty)
                        infoSacado += InfoSacado.Render(Sacado.Endereco.End, enderecoSacado, false);
                    else
                        infoSacado += InfoSacado.Render(Sacado.Endereco.End, enderecoSacado, true);
                //"Informações do Sacado" foi introduzido para possibilitar que o boleto na informe somente o endereço do sacado
                //como em outras situaçoes onde se imprime matriculas, codigos e etc, sobre o sacado.
                //Sendo assim o endereço do sacado passa a ser uma Informaçao do Sacado que é adicionada no momento da renderização
                //de acordo com a flag "OcultarEnderecoSacado"
            }

            string agenciaConta = Utils.FormataAgenciaConta(Cedente.ContaBancaria.Agencia, Cedente.ContaBancaria.DigitoAgencia, Cedente.ContaBancaria.Conta, Cedente.ContaBancaria.DigitoConta);

            // Trecho adicionado por Fabrício Nogueira de Almeida :fna - fnalmeida@gmail.com - 09/12/2008
            /* Esse código foi inserido pq no campo Agência/Cod Cedente, estava sendo impresso sempre a agência / número da conta
             * No boleto da caixa que eu fiz, coloquei no método validarBoleto um trecho para calcular o dígito do cedente, e adicionei esse atributo na classe cedente
             * O trecho abaixo testa se esse digito foi calculado, se foi insere no campo Agencia/Cod Cedente, a agência e o código com seu digito
             * caso contrário mostra a agência / conta, como era anteriormente.
             * Com esse código ele ira atender as necessidades do boleto caixa e não afetará os demais
             * Caso queira que apareça o Agência/cod. cedente para outros boletos, basta calcular e setar o digito, como foi feito no boleto Caixa 
             */

            string agenciaCodigoCedente;
            if (!Cedente.DigitoCedente.Equals(-1))
                agenciaCodigoCedente = string.Format("{0}/{1}-{2}", Cedente.ContaBancaria.Agencia, Utils.FormatCode(Cedente.Codigo.ToString(), 6), Cedente.DigitoCedente.ToString());
            else
                agenciaCodigoCedente = agenciaConta;

            html.Append(GeraHtmlReciboCedente());

            string dataVencimento = Boleto.DataVencimento.ToString("dd/MM/yyyy");

            if (MostrarContraApresentacaoNaDataVencimento)
                dataVencimento = "Contra Apresentação";

            return html.ToString()
                .Replace("@CODIGOBANCO", Utils.FormatCode(_ibanco.Codigo.ToString(), 3))
                .Replace("@DIGITOBANCO", _ibanco.Digito.ToString())
                //.Replace("@URLIMAGEMBARRAINTERNA", urlImagemBarraInterna)
                //.Replace("@URLIMAGEMCORTE", urlImagemCorte)
                //.Replace("@URLIMAGEMPONTO", urlImagemPonto)
                .Replace("@URLIMAGEMLOGO", urlImagemLogo)
                .Replace("@URLIMAGEMBARRA", urlImagemBarra)
                .Replace("@LINHADIGITAVEL", Boleto.CodigoBarra.LinhaDigitavel)
                .Replace("@LOCALPAGAMENTO", Boleto.LocalPagamento)
                .Replace("@DATAVENCIMENTO", dataVencimento)
                .Replace("@CEDENTE", Cedente.Nome)
                .Replace("@DATADOCUMENTO", Boleto.DataDocumento.ToString("dd/MM/yyyy"))
                .Replace("@NUMERODOCUMENTO", Boleto.NumeroDocumento)
                .Replace("@ESPECIEDOCUMENTO", EspecieDocumento.ValidaSigla(Boleto.EspecieDocumento))
                .Replace("@DATAPROCESSAMENTO", Boleto.DataProcessamento.ToString("dd/MM/yyyy"))
                .Replace("@NOSSONUMERO", Boleto.NossoNumero)
                .Replace("@CARTEIRA", (MostrarCodigoCarteira ? string.Format("{0} - {1}", Boleto.Carteira.ToString(), new Carteira_Santander(Utils.ToInt32(Boleto.Carteira)).Codigo) : Boleto.Carteira.ToString()))
                .Replace("@ESPECIE", Boleto.Especie)
                .Replace("@QUANTIDADE", (Boleto.QuantidadeMoeda == 0 ? "" : Boleto.QuantidadeMoeda.ToString()))
                .Replace("@VALORDOCUMENTO", Boleto.ValorMoeda)
                .Replace("@=VALORDOCUMENTO", (Boleto.ValorBoleto == 0 ? "" : Boleto.ValorBoleto.ToString("R$ ##,##0.00")))
                .Replace("@VALORCOBRADO", "")
                .Replace("@OUTROSACRESCIMOS", "")
                .Replace("@OUTRASDEDUCOES", "")
                .Replace("@DESCONTOS", "")
                .Replace("@AGENCIACONTA", agenciaCodigoCedente)
                .Replace("@SACADO", sacado)
                .Replace("@INFOSACADO", infoSacado)
                .Replace("@AGENCIACODIGOCEDENTE", agenciaCodigoCedente)
                .Replace("@CPFCNPJ", Cedente.CPFCNPJ)
                .Replace("@MORAMULTA", "")
                .Replace("@AUTENTICACAOMECANICA", "")
                .Replace("@USODOBANCO", Boleto.UsoBanco)
                .Replace("@IMAGEMCODIGOBARRA", imagemCodigoBarras);
        }
        #endregion Html

        #region Geração do Html OffLine

        /// <summary>
        /// Função utilizada para gerar o html do boleto sem que o mesmo esteja dentro de uma página Web.
        /// </summary>
        /// <param name="srcCorte">Local apontado pela imagem de corte.</param>
        /// <param name="srcLogo">Local apontado pela imagem de logo.</param>
        /// <param name="srcBarra">Local apontado pela imagem de barra.</param>
        /// <param name="srcPonto">Local apontado pela imagem de ponto.</param>
        /// <param name="srcBarraInterna">Local apontado pela imagem de barra interna.</param>
        /// <param name="srcCodigoBarra">Local apontado pela imagem do código de barras.</param>
        /// <returns>StringBuilder conténdo o código html do boleto bancário.</returns>
        protected StringBuilder HtmlOffLine(string srcLogo, string srcBarra, string srcCodigoBarra)
        {//protected StringBuilder HtmlOffLine(string srcCorte, string srcLogo, string srcBarra, string srcPonto, string srcBarraInterna, string srcCodigoBarra)
            this.OnLoad(EventArgs.Empty);

            StringBuilder html = new StringBuilder();

            html.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n");
            html.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\n");
            html.Append("<head>");
            html.Append("    <title>Boleto.Net</title>\n");

            #region Css
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("BoletoNet.BoletoImpressao.BoletoNet.css");

                using (StreamReader sr = new StreamReader(stream))
                {
                    html.Append("<style>\n");
                    html.Append(sr.ReadToEnd());
                    html.Append("</style>\n");
                    sr.Close();
                    sr.Dispose();
                }
            }
            #endregion Css

            html.Append("     </head>\n");
            html.Append("<body>\n");

            //html.Append(MontaHtml(srcCorte, srcLogo, srcBarra, srcPonto, srcBarraInterna, "<img src=\"" + srcCodigoBarra + "\" alt=\"Código de Barras\" />"));
            html.Append(MontaHtml(srcLogo, srcBarra, "<img src=\"" + srcCodigoBarra + "\" alt=\"Código de Barras\" />"));
            html.Append("</body>\n");
            html.Append("</html>\n");
            return html;
        }

        /// <summary>
        /// Função utilizada gerar o AlternateView necessário para enviar um boleto bancário por e-mail.
        /// </summary>
        /// <returns>AlternateView com os dados do boleto.</returns>
        public AlternateView HtmlBoletoParaEnvioEmail()
        {
            this.OnLoad(EventArgs.Empty);

            //MemoryStream ms = new MemoryStream(Utils.ConvertImageToByte(Html.corte));
            //LinkedResource lrImagemCorte = new LinkedResource(ms, MediaTypeNames.Image.Gif);
            //lrImagemCorte.ContentId = "corte";

            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("BoletoNet.Imagens." + Utils.FormatCode(_ibanco.Codigo.ToString(), 3) + ".jpg");
            LinkedResource lrImagemLogo = new LinkedResource(stream, MediaTypeNames.Image.Jpeg);
            lrImagemLogo.ContentId = "logo";

            //ms = new MemoryStream(Utils.ConvertImageToByte(Html.ponto));
            //LinkedResource lrImagemPonto = new LinkedResource(ms, MediaTypeNames.Image.Gif);
            //lrImagemPonto.ContentId = "ponto";

            //ms = new MemoryStream(Utils.ConvertImageToByte(Html.barrainterna));
            //LinkedResource lrImagemBarraInterna = new LinkedResource(ms, MediaTypeNames.Image.Gif);
            //lrImagemBarraInterna.ContentId = "barrainterna";

            MemoryStream ms = new MemoryStream(Utils.ConvertImageToByte(Html.barra));
            LinkedResource lrImagemBarra = new LinkedResource(ms, MediaTypeNames.Image.Gif);
            lrImagemBarra.ContentId = "barra";

            C2of5i cb = new C2of5i(Boleto.CodigoBarra.Codigo, 1, 50, Boleto.CodigoBarra.Codigo.Length);
            ms = new MemoryStream(Utils.ConvertImageToByte(cb.ToBitmap()));

            LinkedResource lrImagemCodigoBarra = new LinkedResource(ms, MediaTypeNames.Image.Gif);
            lrImagemCodigoBarra.ContentId = "codigobarra";

            //StringBuilder html = HtmlOffLine("cid:" + lrImagemCorte.ContentId,
            //    "cid:" + lrImagemLogo.ContentId,
            //    "cid:" + lrImagemBarra.ContentId,
            //    "cid:" + lrImagemPonto.ContentId,
            //    "cid:" + lrImagemBarraInterna.ContentId,
            //    "cid:" + lrImagemCodigoBarra.ContentId);
            StringBuilder html = HtmlOffLine("cid:" + lrImagemLogo.ContentId, "cid:" + lrImagemBarra.ContentId, "cid:" + lrImagemCodigoBarra.ContentId);


            AlternateView av = AlternateView.CreateAlternateViewFromString(html.ToString(), Encoding.Default, "text/html");

            //av.LinkedResources.Add(lrImagemCorte);
            //av.LinkedResources.Add(lrImagemBarraInterna);
            av.LinkedResources.Add(lrImagemLogo);
            av.LinkedResources.Add(lrImagemBarra);
            //av.LinkedResources.Add(lrImagemPonto);
            av.LinkedResources.Add(lrImagemCodigoBarra);
            return av;
        }

        /// <summary>
        /// Função utilizada para gravar em um arquivo local o conteúdo do boleto. Este arquivo pode ser aberto em um browser sem que o site esteja no ar.
        /// </summary>
        /// <param name="fileName">Path do arquivo que deve conter o código html.</param>
        public void MontaHtmlNoArquivoLocal(string fileName)
        {
            using (FileStream f = new FileStream(fileName, FileMode.Create))
            {
                StreamWriter w = new StreamWriter(f, System.Text.Encoding.Default);
                w.Write(MontaHtml());
                w.Close();
                f.Close();
            }
        }

        /// <summary>
        /// Monta o Html do boleto bancário
        /// </summary>
        /// <returns>string</returns>
        public string MontaHtml()
        {
            return MontaHtml(null);
        }

        /// <summary>
        /// Monta o Html do boleto bancário
        /// </summary>
        /// <param name="fileName">Caminho do arquivo</param>
        /// <returns>Html do boleto gerado</returns>
        public string MontaHtml(string fileName)
        {
            if (fileName == null)
                fileName = System.IO.Path.GetTempPath();

            this.OnLoad(EventArgs.Empty);

            //string fnCorte = fileName + @"BoletoNetCorte.gif";
            //if (!System.IO.File.Exists(fnCorte))
            //    Html.corte.Save(fnCorte);

            string fnLogo = fileName + @"BoletoNet" + Utils.FormatCode(_ibanco.Codigo.ToString(), 3) + ".jpg";

            if (!System.IO.File.Exists(fnLogo))
                Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("BoletoNet.Imagens." + Utils.FormatCode(_ibanco.Codigo.ToString(), 3) + ".jpg")).Save(fnLogo);

            //string fnPonto = fileName + @"BoletoNetPonto.gif";
            //if (!System.IO.File.Exists(fnPonto))
            //    Html.ponto.Save(fnPonto);

            //string fnBarraInterna = fileName + @"BoletoNetBarraInterna.gif";
            //if (!File.Exists(fnBarraInterna))
            //    Html.barrainterna.Save(fnBarraInterna);

            string fnBarra = fileName + @"BoletoNetBarra.gif";
            if (!File.Exists(fnBarra))
                Html.barra.Save(fnBarra);

            string fnCodigoBarras = System.IO.Path.GetTempFileName();
            C2of5i cb = new C2of5i(Boleto.CodigoBarra.Codigo, 1, 50, Boleto.CodigoBarra.Codigo.Length);
            cb.ToBitmap().Save(fnCodigoBarras);

            //return HtmlOffLine(fnCorte, fnLogo, fnBarra, fnPonto, fnBarraInterna, fnCodigoBarras).ToString();
            return HtmlOffLine(fnLogo, fnBarra, fnCodigoBarras).ToString();
        }
        #endregion Geração do Html OffLine
    }
}
