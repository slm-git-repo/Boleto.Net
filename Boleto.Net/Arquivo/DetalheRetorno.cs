using System;
using System.Collections.Generic;
using System.Text;

namespace BoletoNet
{
    public class DetalheRetorno
    {

        #region Variáveis

        private int _codigoInscricao;
        private string _numeroInscricao;
        private int _agencia;
        private int _conta;
        private int _dacConta;
        private string _usoEmpresa;
        private int _nossoNumero;
        private int _dacNossoNumero;
        private string _carteira;
        private int _codigoOcorrencia;
        private DateTime _dataOcorrencia;
        private string _numeroDocumento;
        private int _confirmacaoNossoNumero;
        private DateTime _dataVencimento;
        private double _valorTitulo;
        private int _codigoBanco;
        private int _agenciaCobradora;
        private int _dacAgenciaCobradora;
        private int _especie;
        private double _tarifaCobranca;
        private double _iof;
        private double _valorAbatimento;
        private double _descontos;
        private double _valorPrincipal;
        private double _jurosMora;
        private double _outrosCreditos;
        private DateTime _dataCredito;
        private int _instrucaoCancelada;
        private string _nomeSacado;
        private string _erros;
        private string _codigoLiquidacao;
        private int _numeroSequencial;

        private List<DetalheRetorno> _listaDetalhe = new List<DetalheRetorno>();

        #endregion

        #region Construtores

        public DetalheRetorno()
		{
        }

        #endregion

        #region Propriedades

        public int CodigoInscricao
        {
            get { return _codigoInscricao; }
            set { _codigoInscricao = value; }
        }

        public string NumeroInscricao
        {
            get { return _numeroInscricao; }
            set { _numeroInscricao = value; }
        }

        public int Agencia
        {
            get { return _agencia; }
            set { _agencia = value; }
        }

        public int Conta
        {
            get { return _conta; }
            set { _conta = value; }
        }

        public int DACConta
        {
            get { return _dacConta; }
            set { _dacConta = value; }
        }

        public string UsoEmpresa
        {
            get { return _usoEmpresa; }
            set { _usoEmpresa = value; }
        }

        public int NossoNumero
        {
            get { return _nossoNumero; }
            set { _nossoNumero = value; }
        }

        public int DACNossoNumero
        {
            get { return _dacNossoNumero; }
            set { _dacNossoNumero = value; }
        }

        public string Carteira
        {
            get { return _carteira; }
            set { _carteira = value; }
        }

        public int CodigoOcorrencia
        {
            get { return _codigoOcorrencia; }
            set { _codigoOcorrencia = value; }
        }

        public DateTime DataOcorrencia
        {
            get { return _dataOcorrencia; }
            set { _dataOcorrencia = value; }
        }

        public string NumeroDocumento
        {
            get { return _numeroDocumento; }
            set { _numeroDocumento = value; }
        }

        public int ConfirmacaoNossoNumero
        {
            get { return _confirmacaoNossoNumero; }
            set { _confirmacaoNossoNumero = value; }
        }

        public DateTime DataVencimento
        {
            get { return _dataVencimento; }
            set { _dataVencimento = value; }
        }

        public double ValorTitulo
        {
            get { return _valorTitulo; }
            set { _valorTitulo = value; }
        }

        public int CodigoBanco
        {
            get { return _codigoBanco; }
            set { _codigoBanco = value; }
        }

        public int AgenciaCobradora
        {
            get { return _agenciaCobradora; }
            set { _agenciaCobradora = value; }
        }

        public int DACAgenciaCobradora
        {
            get { return _dacAgenciaCobradora; }
            set { _dacAgenciaCobradora = value; }
        }

        public int Especie
        {
            get { return _especie; }
            set { _especie = value; }
        }

        public double TarifaCobranca
        {
            get { return _tarifaCobranca; }
            set { _tarifaCobranca = value; }
        }

        public double IOF
        {
            get { return _iof; }
            set { _iof = value; }
        }

        public double ValorAbatimento
        {
            get { return _valorAbatimento; }
            set { _valorAbatimento = value; }
        }

        public double Descontos
        {
            get { return _descontos; }
            set { _descontos = value; }
        }

        public double ValorPrincipal
        {
            get { return _valorPrincipal; }
            set { _valorPrincipal = value; }
        }

        public double JurosMora
        {
            get { return _jurosMora; }
            set { _jurosMora = value; }
        }

        public double OutrosCreditos
        {
            get { return _outrosCreditos; }
            set { _outrosCreditos = value; }
        }

        public DateTime DataCredito
        {
            get { return _dataCredito; }
            set { _dataCredito = value; }
        }

        public int InstrucaoCancelada
        {
            get { return _instrucaoCancelada; }
            set { _instrucaoCancelada = value; }
        }

        public string NomeSacado
        {
            get { return _nomeSacado; }
            set { _nomeSacado = value; }
        }

        public string Erros
        {
            get { return _erros; }
            set { _erros = value; }
        }

        public string CodigoLiquidacao
        {
            get { return _codigoLiquidacao; }
            set { _codigoLiquidacao = value; }
        }

        public int NumeroSequencial
        {
            get { return _numeroSequencial; }
            set { _numeroSequencial = value; }
        }

        public List<DetalheRetorno> ListaDetalhe
        {
            get { return _listaDetalhe; }
            set { _listaDetalhe = value; }
        }

        #endregion

        #region Métodos de Instância

        public void LerDetalheRetornoCNAB400(string registro)
        {
            try
            {
                int dataOcorrencia = Utils.ToInt32(registro.Substring(110, 6));
                int dataVencimento = Utils.ToInt32(registro.Substring(146, 6));
                int dataCredito = Utils.ToInt32(registro.Substring(295, 6));

                _codigoInscricao = Utils.ToInt32(registro.Substring(1, 2));
                _numeroInscricao = registro.Substring(3, 14);
                _agencia = Utils.ToInt32(registro.Substring(17, 4));
                _conta = Utils.ToInt32(registro.Substring(23, 5));
                _dacConta = Utils.ToInt32(registro.Substring(28, 1));
                _usoEmpresa = registro.Substring(37, 25);
                _nossoNumero = Utils.ToInt32(registro.Substring(85, 8));
                _dacNossoNumero = Utils.ToInt32(registro.Substring(93, 1));
                _carteira = registro.Substring(107, 1);
                _codigoOcorrencia = Utils.ToInt32(registro.Substring(108, 2));
                _dataOcorrencia = Utils.ToDateTime(dataOcorrencia.ToString("##-##-##"));
                _numeroDocumento = registro.Substring(116, 10);
                _nossoNumero = Utils.ToInt32(registro.Substring(126, 9));
                _dataVencimento = Utils.ToDateTime(dataVencimento.ToString("##-##-##"));
                double valorTitulo = Convert.ToInt64(registro.Substring(152, 13));
                _valorTitulo = valorTitulo / 100;
                _codigoBanco = Utils.ToInt32(registro.Substring(165, 3));
                _agenciaCobradora = Utils.ToInt32(registro.Substring(168, 4));
                _especie = Utils.ToInt32(registro.Substring(173, 2));
                double tarifaCobranca = Convert.ToUInt64(registro.Substring(174, 13));
                _tarifaCobranca = tarifaCobranca / 100;
                // 26 brancos
                double iof = Convert.ToUInt64(registro.Substring(214, 13));
                _iof = iof / 100;
                double valorAbatimento = Convert.ToUInt64(registro.Substring(227, 13));
                _valorAbatimento = valorAbatimento / 100;
                double valorPrincipal = Convert.ToUInt64(registro.Substring(253, 13));
                _valorPrincipal = valorPrincipal / 100;
                double jurosMora = Convert.ToUInt64(registro.Substring(266, 13));
                _jurosMora = jurosMora / 100;
                _dataOcorrencia = Utils.ToDateTime(dataOcorrencia.ToString("##-##-##"));
                // 293 - 3 brancos
                _dataCredito = Utils.ToDateTime(dataCredito.ToString("##-##-##"));
                _instrucaoCancelada = Utils.ToInt32(registro.Substring(301, 4));
                // 306 - 6 brancos
                // 311 - 13 zeros
                _nomeSacado = registro.Substring(324, 30);
                // 354 - 23 brancos
                _erros = registro.Substring(377, 8);
                // 377 - Registros rejeitados ou alegação do sacado
                // 386 - 7 brancos

                _codigoLiquidacao = registro.Substring(392, 2);
                _numeroSequencial = Utils.ToInt32(registro.Substring(394, 6));

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao ler detalhe do arquivo de RETORNO / CNAB 400.", ex);
            }
        }

        public static string PrimeiroCaracter(string retorno)
        {
            try
            {
                return retorno.Substring(0, 1);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao desmembrar registro.", ex);
            }
        }

        #endregion

    }
}
