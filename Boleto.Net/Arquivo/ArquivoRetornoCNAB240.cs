using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace BoletoNet
{
    public class ArquivoRetornoCNAB240 : AbstractArquivoRetorno, IArquivoRetorno
    {
        private Stream _streamArquivo;
        private string _caminhoArquivo;
        private List<DetalheRetornoCNAB240> _listaDetalhes = new List<DetalheRetornoCNAB240>();



        #region Propriedades
        public string CaminhoArquivo
        {
            get { return _caminhoArquivo; }
        }
        public Stream StreamArquivo
        {
            get { return _streamArquivo; }
        }
        public List<DetalheRetornoCNAB240> ListaDetalhes
        {
            get { return _listaDetalhes; }
            set { _listaDetalhes = value; }
        }
        #endregion Propriedades


        #region Construtores

        public ArquivoRetornoCNAB240()
		{
            this.TipoArquivo = TipoArquivo.CNAB240;
        }

        public ArquivoRetornoCNAB240(Stream streamArquivo)
        {
            this.TipoArquivo = TipoArquivo.CNAB240;
            _streamArquivo = streamArquivo;
        }

        public ArquivoRetornoCNAB240(string caminhoArquivo)
        {
            this.TipoArquivo = TipoArquivo.CNAB240;

            _streamArquivo = new StreamReader(caminhoArquivo).BaseStream;
        }
        #endregion

        #region Métodos de instância
        public void LerArquivoRetorno(IBanco banco)
        {
            LerArquivoRetorno(banco, StreamArquivo);
        }

        public override void LerArquivoRetorno(IBanco banco, Stream arquivo)
        {
            try
            {
                StreamReader Linha = new StreamReader(arquivo);
                string strline = "";

                // Lendo o arquivo
                strline = Linha.ReadLine();
                OnLinhaLida(null, strline, EnumTipodeLinhaLida.HeaderDeArquivo);

                switch (banco.Codigo)
                {
                    case 1:
                        // Próxima linha (DETALHE)
                        strline = Linha.ReadLine();
                        OnLinhaLida(null, strline, EnumTipodeLinhaLida.HeaderDeLote);
                        strline = Linha.ReadLine();

                        while (strline.Substring(7, 1) == "3")
                        {
                            DetalheRetornoCNAB240 detalheRetorno = new DetalheRetornoCNAB240();
                            detalheRetorno.SegmentoT.LerDetalheSegmentoTRetornoCNAB240(strline);
                            OnLinhaLida(detalheRetorno, strline, EnumTipodeLinhaLida.DetalheSegmentoT);

                            strline = Linha.ReadLine();
                            detalheRetorno.SegmentoU.LerDetalheSegmentoURetornoCNAB240(strline);
                            OnLinhaLida(detalheRetorno, strline, EnumTipodeLinhaLida.DetalheSegmentoU);
                            ListaDetalhes.Add(detalheRetorno);
                            strline = Linha.ReadLine();
                        }
                        OnLinhaLida(null, strline, EnumTipodeLinhaLida.TraillerDeLote);
                        strline = Linha.ReadLine();
                        OnLinhaLida(null, strline, EnumTipodeLinhaLida.TraillerDeArquivo);
                        break;
                    default:
                        // Próxima linha (DETALHE)
                        strline = Linha.ReadLine();
                        OnLinhaLida(null, strline, EnumTipodeLinhaLida.HeaderDeLote);
                        strline = Linha.ReadLine();

                        while (strline.Substring(7, 1) == "3")
                        {
                            DetalheRetornoCNAB240 detalheRetorno = new DetalheRetornoCNAB240();
                            OnLinhaLida(detalheRetorno, strline, EnumTipodeLinhaLida.DetalheSegmentoT);
                            detalheRetorno.SegmentoT.LerDetalheSegmentoTRetornoCNAB240(strline);
                            strline = Linha.ReadLine();
                            OnLinhaLida(detalheRetorno, strline, EnumTipodeLinhaLida.DetalheSegmentoU);

                            detalheRetorno.SegmentoU.LerDetalheSegmentoURetornoCNAB240(strline);
                            ListaDetalhes.Add(detalheRetorno);
                            strline = Linha.ReadLine();
                        }
                        OnLinhaLida(null, strline, EnumTipodeLinhaLida.TraillerDeLote);
                        strline = Linha.ReadLine();
                        OnLinhaLida(null, strline, EnumTipodeLinhaLida.TraillerDeArquivo);
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
