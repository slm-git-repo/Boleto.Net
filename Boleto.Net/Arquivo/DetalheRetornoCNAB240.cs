using System;
using System.Collections.Generic;
using System.Text;

namespace BoletoNet
{
    public class DetalheRetornoCNAB240
    {

        #region Variáveis

        private DetalheSegmentoTRetornoCNAB240 _segmentoT = new DetalheSegmentoTRetornoCNAB240();
        private DetalheSegmentoURetornoCNAB240 _segmentoU = new DetalheSegmentoURetornoCNAB240();

        #endregion

        #region Construtores

        public DetalheRetornoCNAB240()
		{
        }

        #endregion

        #region Propriedades

        public DetalheSegmentoTRetornoCNAB240 SegmentoT
        {
            get { return _segmentoT; }
            set { _segmentoT = value; }
        }

        public DetalheSegmentoURetornoCNAB240 SegmentoU
        {
            get { return _segmentoU; }
            set { _segmentoU = value; }
        }
        
        #endregion

        #region Métodos de Instância

        #endregion

    }
}
