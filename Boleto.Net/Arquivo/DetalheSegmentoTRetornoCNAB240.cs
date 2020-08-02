using System;
using System.Collections.Generic;
using System.Text;

namespace BoletoNet
{
    public class DetalheSegmentoTRetornoCNAB240
    {

        #region Variáveis

        private int _codigoBanco;
        private int _idCodigoMovimento;
        private CodigoMovimento _codigoMovimento;
        private int _agencia;
        private int _digitoAgencia;
        private int _conta;
        private int _digitoConta;
        private int _dacAgConta;
        private string _nossoNumero; //identificação do título no banco
        private int _codigoCarteira;
        private string _numeroDocumento; //número utilizado pelo cliente para a identificação do título
        private DateTime _dataVencimento;
        private double _valorTitulo;
        private string _identificacaoTituloEmpresa;
        private int _tipoInscricao;
        private string _numeroInscricao;
        private string _nomeSacado;
        private double _valorTarifas;
        private int _codigoRejeicao;
        private string _registro;

        private List<DetalheSegmentoTRetornoCNAB240> _listaDetalhe = new List<DetalheSegmentoTRetornoCNAB240>();

        #endregion

        #region Construtores

        public DetalheSegmentoTRetornoCNAB240()
		{
        }

        #endregion

        #region Propriedades

        public int idCodigoMovimento
        {
            get { return _idCodigoMovimento; }
            set { _idCodigoMovimento = value; }
        }

        private int CodigoBanco
        {
            get { return _codigoBanco; }
            set { _codigoBanco = value; }
        }

        public string Registro
        {
            get { return _registro; }            
        }

        public CodigoMovimento CodigoMovimento
        {
            get 
            {
                _codigoMovimento = new CodigoMovimento(_codigoBanco, _idCodigoMovimento); 
                return _codigoMovimento;
            }
            set 
            { 
                _codigoMovimento = value;
                _idCodigoMovimento = _codigoMovimento.Codigo;
            }
        }

        public int Agencia
        {
            get { return _agencia; }
            set { _agencia = value; }
        }

        public int DigitoAgencia
        {
            get { return _digitoAgencia; }
            set { _digitoAgencia = value; }
        }

        public int Conta
        {
            get { return _conta; }
            set { _conta = value; }
        }

        public int DigitoConta
        {
            get { return _digitoConta; }
            set { _digitoConta = value; }
        }

        public int DACAgenciaConta
        {
            get { return _dacAgConta; }
            set { _dacAgConta = value; }
        }

        public string NossoNumero
        {
            get { return _nossoNumero; }
            set { _nossoNumero = value; }
        }

        public int CodigoCarteira
        {
            get { return _codigoCarteira; }
            set { _codigoCarteira = value; }
        }

        public string NumeroDocumento
        {
            get { return _numeroDocumento; }
            set { _numeroDocumento = value; }
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

        public string IdentificacaoTituloEmpresa
        {
            get { return _identificacaoTituloEmpresa; }
            set { _identificacaoTituloEmpresa = value; }
        }

        public int TipoInscricao
        {
            get { return _tipoInscricao; }
            set { _tipoInscricao = value; }
        }

        public string NumeroInscricao
        {
            get { return _numeroInscricao; }
            set { _numeroInscricao = value; }
        }

        public string NomeSacado
        {
            get { return _nomeSacado; }
            set { _nomeSacado = value; }
        }

        public double ValorTarifas
        {
            get { return _valorTarifas; }
            set { _valorTarifas = value; }
        }

        public int CodigoRejeicao
        {
            get { return _codigoRejeicao; }
            set { _codigoRejeicao = value; }
        }

        public List<DetalheSegmentoTRetornoCNAB240> ListaDetalhe
        {
            get { return _listaDetalhe; }
            set { _listaDetalhe = value; }
        }

        #endregion

        #region Métodos de Instância

        public void LerDetalheSegmentoTRetornoCNAB240(string Registro)
        {
            try
            {
                _registro = Registro;

                if (Registro.Substring(13, 1) != "T")
                    throw new Exception("Registro inválida. O detalhe não possuí as características do segmento T.");
                
                int dataVencimento = Convert.ToInt32(Registro.Substring(69, 8));

                _codigoBanco = Convert.ToInt32(Registro.Substring(0, 3));;
                _idCodigoMovimento = Convert.ToInt32(Registro.Substring(15, 2));
                _agencia = Convert.ToInt32(Registro.Substring(17, 4));
                _digitoAgencia = Convert.ToInt32(Registro.Substring(21, 1));
                _conta = Convert.ToInt32(Registro.Substring(22, 9));
                _digitoConta = Convert.ToInt32(Registro.Substring(31, 1));

                _nossoNumero = Registro.Substring(40, 13);
                _codigoCarteira = Convert.ToInt32(Registro.Substring(53, 1));
                _numeroDocumento = Registro.Substring(54, 15);
                _dataVencimento = Convert.ToDateTime(dataVencimento.ToString("##-##-####"));
                double valorTitulo = Convert.ToInt64(Registro.Substring(77, 15));
                _valorTitulo = valorTitulo / 100;
                _identificacaoTituloEmpresa = Registro.Substring(100, 25);
                _tipoInscricao = Convert.ToInt32(Registro.Substring(127, 1));
                _numeroInscricao = Registro.Substring(128, 15);
                _nomeSacado = Registro.Substring(143, 40);
                double valorTarifas = Convert.ToUInt64(Registro.Substring(193, 15));
                _valorTarifas = valorTarifas / 100;
                _codigoRejeicao = Convert.ToInt32(Registro.Substring(208, 10));
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao processar arquivo de RETORNO - SEGMENTO T.", ex);
            }
        }

        #endregion

    }
}
