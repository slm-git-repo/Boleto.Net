using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace BoletoNet
{
    public class ArquivoRetornoCNAB400 : AbstractArquivoRetorno, IArquivoRetorno
    {

        private List<DetalheRetorno> _listaDetalhe = new List<DetalheRetorno>();

        public List<DetalheRetorno> ListaDetalhe
        {
            get { return _listaDetalhe; }
            set { _listaDetalhe = value; }
        }

        #region Construtores

        public ArquivoRetornoCNAB400()
		{
            this.TipoArquivo = TipoArquivo.CNAB400;
        }

        #endregion

        #region Métodos de instância

        public override void LerArquivoRetorno(IBanco banco, Stream arquivo)
        {
            try
            {
                StreamReader Linha = new StreamReader(arquivo);
                string strline = "";
                int _codigoBanco;

                // Lendo o arquivo
                strline = Linha.ReadLine();

                _codigoBanco = 341; // Convert.ToInt16(strline.Substring(76, 3));

                switch (_codigoBanco)
                {
                    case 341:
                        // Próxima linha (DETALHE)
                        strline = Linha.ReadLine();

                        DetalheRetorno detalhex = new DetalheRetorno(); // gamb

                        while (DetalheRetorno.PrimeiroCaracter(strline) == "1")
                        {
                            DetalheRetorno detalhe = new  DetalheRetorno();
                            detalhe.LerDetalheRetornoCNAB400(strline);
                            this.ListaDetalhe.Add(detalhe);
                            //detalhe.ListaDetalhe.Add(detalhe);
                            strline = Linha.ReadLine();
                        }
                        break;
                    default:
                        // Próxima linha (DETALHE)
                        strline = Linha.ReadLine();

                        while (DetalheRetorno.PrimeiroCaracter(strline) == "1")
                        {
                            DetalheRetorno detalhe = new DetalheRetorno();
                            detalhe.LerDetalheRetornoCNAB400(strline);
                            detalhe.ListaDetalhe.Add(detalhe);
                            strline = Linha.ReadLine();
                        }
                        break;
                }

                Linha.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao ler arquivo.", ex);
            }
        }

        #endregion
    }
}
