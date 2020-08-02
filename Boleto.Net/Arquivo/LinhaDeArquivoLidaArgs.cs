using System;
using System.Collections.Generic;
using System.Text;

namespace BoletoNet
{
    public enum EnumTipodeLinhaLida
    {
        HeaderDeArquivo = 1,
        HeaderDeLote = 2,
        DetalheSegmentoT = 3,
        DetalheSegmentoU = 4,
        TraillerDeLote = 5,
        TraillerDeArquivo = 6
    }
    
    public class LinhaDeArquivoLidaArgs : EventArgs
    {
        private string _linha;
        private DetalheRetornoCNAB240 _detalhe;
        private EnumTipodeLinhaLida _tipoLinha;

        public LinhaDeArquivoLidaArgs(DetalheRetornoCNAB240 detalhe, string linha, EnumTipodeLinhaLida tipoLinha)
        {
            try
            {
                _linha = linha;
                _detalhe = detalhe;
                _tipoLinha = tipoLinha;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao instanciar objeto", ex);
            }
        }       

        public string Linha
        {
            get { return _linha; }
        }

        public DetalheRetornoCNAB240 Detalhe
        {
            get { return _detalhe; }
        }

        public EnumTipodeLinhaLida TipoLinha
        {
            get { return _tipoLinha; }
        }
    }
}
