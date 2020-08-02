using System;
using System.Threading;

namespace BoletoNet
{
    public abstract class AbstractBanco
    {

        #region Variaveis

        private int _codigo = 0;
        private int _digito = 0;
        private string _nome = string.Empty;
        private Cedente _cedente = null;

        #endregion Variaveis

        #region Propriedades

        /// <summary>
        /// Código do Banco
        /// 237 - Bradesco; 341 - Itaú
        /// </summary>
        public virtual int Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        /// <summary>
        /// Dígito do Banco
        /// </summary>
        public virtual int Digito
        {
            get { return _digito; }
            protected set { _digito = value; }
        }

        /// <summary>
        /// Nome da Instituição Financeira
        /// </summary>
        public virtual string Nome
        {
            get { return _nome; }
            protected set { _nome = value; }
        }

        /// <summary>
        /// Cedente
        /// </summary>
        public virtual Cedente Cedente
        {
            get { return _cedente; }
            protected set { _cedente = value; }
        }

        #endregion Propriedades

        # region Métodos

        /// <summary>
        /// Retorna o campo que compos o código de barras que para todos os bancos são iguais foramado por:
        /// </summary>
        /// <returns></returns>
        public virtual string CampoFixo()
        {
            throw new NotImplementedException("Função não implementada");
        }
        /// <summary>
        /// Gera os registros de header do aquivo de remessa
        /// </summary>
        public virtual string GerarHeaderRemessa(string numeroConvenio, Cedente cedente, TipoArquivo tipoArquivo)
        {
            string _header = "";
            return _header;
        }
        /// <summary>
        /// Gera registros de detalhe do arquivo remessa
        /// </summary>
        public virtual string GerarDetalheRemessa(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
        {
            string _remessa = "";
            return _remessa;
        }
        /// <summary>
        /// Gera os registros de Trailer do arquivo de remessa
        /// </summary>
        public virtual string GerarTrailerRemessa(int numeroRegistro, TipoArquivo tipoArquivo)
        {
            string _trailer = "";
            return _trailer;
        }
        /// <summary>
        /// Gera os registros de header de aquivo do arquivo de remessa
        /// </summary>
        public virtual string GerarHeaderRemessa(Cedente cedente, TipoArquivo tipoArquivo)
        {
            string _headerArquivo = "";
            return _headerArquivo;
        }
        /// <summary>
        /// Gera os registros de header de lote do arquivo de remessa
        /// </summary>
        public virtual string GerarHeaderLoteRemessa(string numeroConvenio, Cedente cedente, int numeroArquivoRemessa)
        {
            string _headerLote = "";
            return _headerLote;
        }
        /// <summary>
        /// Gera os registros de header de lote do arquivo de remessa
        /// </summary>
        public virtual string GerarHeaderLoteRemessa(string numeroConvenio, Cedente cedente, int numeroArquivoRemessa, TipoArquivo tipoArquivo)
        {
            string _headerLote = "";
            return _headerLote;
        }
        /// <summary>
        /// Gera registros de detalhe do arquivo remessa - SEGMENTO P
        /// </summary>
        public virtual string GerarDetalheSegmentoPRemessa(Boleto boleto, int numeroRegistro, string numeroConvenio)
        {
            string _segmentoP = "";
            return _segmentoP;
        }
        /// <summary>
        /// Gera registros de detalhe do arquivo remessa - SEGMENTO Q
        /// </summary>
        public virtual string GerarDetalheSegmentoQRemessa(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
        {
            string _segmentoQ = "";
            return _segmentoQ;
        }
        /// <summary>
        /// Gera registros de detalhe do arquivo remessa - SEGMENTO R
        /// </summary>
        public virtual string GerarDetalheSegmentoRRemessa(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
        {
            string _segmentoR = "";
            return _segmentoR;
        }
        /// <summary>
        /// Gera os registros de Trailer de arquivo do arquivo de remessa
        /// </summary>
        public virtual string GerarTrailerArquivoRemessa(int numeroRegistro)
        {
            string _trailerArquivo = "";
            return _trailerArquivo;
        }
        /// <summary>
        /// Gera os registros de Trailer de lote do arquivo de remessa
        /// </summary>
        public virtual string GerarTrailerLoteRemessa(int numeroRegistro)
        {
            string _trailerLote = "";
            return _trailerLote;
        }
        /// <summary>
        /// Formata código de barras
        /// </summary>      
        public virtual void FormataCodigoBarra(Boleto boleto)
        {
            throw new NotImplementedException("Função não implementada na classe filha. Implemente na classe que está sendo criada.");
        }
        /// <summary>
        /// Formata linha digitável
        /// </summary>
        public virtual void FormataLinhaDigitavel(Boleto boleto)
        {
            throw new NotImplementedException("Função não implementada na classe filha. Implemente na classe que está sendo criada.");
        }
        /// <summary>
        /// Formata nosso número
        /// </summary>
        public virtual void FormataNossoNumero(Boleto boleto)
        {
            throw new NotImplementedException("Função não implementada na classe filha. Implemente na classe que está sendo criada.");
        }
        /// <summary>
        /// Formata número do documento
        /// </summary>
        public virtual void FormataNumeroDocumento(Boleto boleto)
        {
            throw new NotImplementedException("Função não implementada na classe filha. Implemente na classe que está sendo criada.");
        }
        /// <summary>
        /// Valida o boleto
        /// </summary>
        public virtual void ValidaBoleto(Boleto boleto)
        {
            throw new NotImplementedException("Função não implementada na classe filha. Implemente na classe que está sendo criada.");
        }

        # endregion

        /// <summary>
        /// Fator de vencimento do boleto
        /// </summary>
        /// <param name="boleto"></param>
        /// <returns></returns>
        protected static long FatorVencimento(Boleto boleto)
        {
            DateTime dateBase = new DateTime(2000, 7, 3, 0, 0, 0);
            return Utils.DateDiff(DateInterval.Day, dateBase, boleto.DataVencimento) + 1000;
        }

        //internal static string CalcFatorVencimento(DateTime dtv)
        //{
        //    DateTime dateBase = new DateTime(2000, 7, 3, 0, 0, 0);
        //    return Convert.ToString(Utils.DateDiff(DateInterval.Day, dateBase, dtv) + 1000);
        //}

        #region Mod
        internal static int Mod10(string seq)
        {
            /* Variáveis
             * -------------
             * d - Dígito
             * s - Soma
             * p - Peso
             * b - Base
             * r - Resto
             */

            int d, s = 0, p = 2, r;

            for (int i = seq.Length; i > 0; i--)
            {
                r = (Convert.ToInt32(Microsoft.VisualBasic.Strings.Mid(seq, i, 1)) * p);

                if (r > 9)
                    r = (r / 10) + (r % 10);

                s += r;

                if (p == 2)
                    p = 1;
                else
                    p = p + 1;
            }
            d = ((10 - (s % 10)) % 10);
            return d;
        }

        protected static int Mod11(string seq)
        {
            /* Variáveis
             * -------------
             * d - Dígito
             * s - Soma
             * p - Peso
             * b - Base
             * r - Resto
             */

            int d, s = 0, p = 2, b = 9;

            for (int i = 0; i < seq.Length; i++)
            {
                s = s + (Convert.ToInt32(seq[i]) * p);
                if (p < b)
                    p = p + 1;
                else
                    p = 2;
            }

            d = 11 - (s % 11);
            if (d > 9)
                d = 0;
            return d;
        }

        protected static int Mod11(string seq, int b)
        {
            /* Variáveis
             * -------------
             * d - Dígito
             * s - Soma
             * p - Peso
             * b - Base
             * r - Resto
             */

            int d, s = 0, p = 2;


            for (int i = seq.Length; i > 0; i--)
            {
                s = s + (Convert.ToInt32(Microsoft.VisualBasic.Strings.Mid(seq, i, 1)) * p);
                if (p == b)
                    p = 2;
                else
                    p = p + 1;
            }

            d = 11 - (s % 11);


            if ((d > 9) || (d == 0) || (d == 1))
                d = 1;

            return d;
        }

        protected static int Mod11Base9(string seq)
        {
            /* Variáveis
             * -------------
             * d - Dígito
             * s - Soma
             * p - Peso
             * b - Base
             * r - Resto
             */

            int d, s = 0, p = 2, b = 9;


            for (int i = seq.Length - 1; i >= 0; i--)
            {
                string aux = Convert.ToString(seq[i]);
                s += (Convert.ToInt32(aux) * p);
                if (p >= b)
                    p = 2;
                else
                    p = p + 1;
            }

            if (s < 11)
            {
                d = 11 - s;
                return d;
            }
            else
            {
                d = 11 - (s % 11);
                if ((d > 9) || (d == 0))
                    d = 0;

                return d;
            }
        }

        protected static int Mod11(string seq, int lim, int flag)
        {
            int mult = 0;
            int total = 0;
            int pos = 1;
            //int res = 0;
            int ndig = 0;
            int nresto = 0;
            string num = string.Empty;

            mult = 1 + (seq.Length % (lim - 1));

            if (mult == 1)
                mult = lim;


            while (pos <= seq.Length)
            {
                num = Microsoft.VisualBasic.Strings.Mid(seq, pos, 1);
                total += Convert.ToInt32(num) * mult;

                mult -= 1;
                if (mult == 1)
                    mult = lim;

                pos += 1;
            }
            nresto = (total % 11);
            if (flag == 1)
                return nresto;
            else
            {
                if (nresto == 0 || nresto == 1 || nresto == 10)
                    ndig = 1;
                else
                    ndig = (11 - nresto);

                return ndig;
            }
        }
        #endregion Mod
    }
}
