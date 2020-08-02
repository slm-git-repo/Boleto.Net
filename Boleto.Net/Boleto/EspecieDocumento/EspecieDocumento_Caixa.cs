using System;
using System.Collections.Generic;
using System.Text;

namespace BoletoNet
{
    #region Enumerador

    public enum EnumEspecieDocumento_Caixa
    {
        DuplicataMercantil = 2,
    }

    #endregion 

    public class EspecieDocumento_Caixa: AbstractEspecieDocumento, IEspecieDocumento
    {
        #region Construtores 

		public EspecieDocumento_Caixa()
		{
			try
			{
			}
			catch (Exception ex)
			{
                throw new Exception("Erro ao carregar objeto", ex);
			}
		}

        public EspecieDocumento_Caixa(int codigo)
        {
            try
            {
                this.carregar(codigo);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

		#endregion 

        #region Metodos Privados

        private void carregar(int idCodigo)
        {
            try
            {
                this.Banco = new Banco_Caixa();

                switch ((EnumEspecieDocumento_Caixa)idCodigo)
                {
                    case  EnumEspecieDocumento_Caixa.DuplicataMercantil:
                        this.Codigo = (int)EnumEspecieDocumento_Caixa.DuplicataMercantil;
                        this.Especie = "Duplicata mercantil";
                        this.Sigla = "DM";
                        break;
                    default:
                        this.Codigo = 0;
                        this.Especie = "( Selecione )";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        public static EspeciesDocumento CarregaTodas()
        {
            try
            {
                EspeciesDocumento alEspeciesDocumento = new EspeciesDocumento();

                EspecieDocumento_Caixa obj;

                obj = new EspecieDocumento_Caixa((int)EnumEspecieDocumento_Caixa.DuplicataMercantil);
                alEspeciesDocumento.Add(obj);

                return alEspeciesDocumento;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar objetos", ex);
            }
        }

        #endregion
    }
}
