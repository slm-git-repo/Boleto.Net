using System;
using System.Collections.Generic;
using System.Text;

namespace BoletoNet
{
    public class EspecieDocumento : AbstractEspecieDocumento, IEspecieDocumento
    {

        #region Variaveis

        private IEspecieDocumento _IEspecieDocumento;

        #endregion

        #region Construtores

        internal EspecieDocumento()
        {
        }

        public EspecieDocumento(int CodigoBanco)
        {
            try
            {
                InstanciaEspecieDocumento(CodigoBanco, 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao instanciar objeto.", ex);
            }
        }

        public EspecieDocumento(int CodigoBanco, int codigoEspecie)
        {
            try
            {
                InstanciaEspecieDocumento(CodigoBanco, codigoEspecie);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao instanciar objeto.", ex);
            }
        }

        #endregion

        #region Propriedades da interface

        public override IBanco Banco
        {
            get { return _IEspecieDocumento.Banco; }
            set { _IEspecieDocumento.Banco = value; }
        }

        public override int Codigo
        {
            get { return _IEspecieDocumento.Codigo; }
            set { _IEspecieDocumento.Codigo = value; }
        }

        public override string Sigla
        {
            get { return _IEspecieDocumento.Sigla; }
            set { _IEspecieDocumento.Sigla = value; }
        }

        public override string Especie
        {
            get { return _IEspecieDocumento.Especie; }
            set { _IEspecieDocumento.Especie = value; }
        }

        #endregion

        # region Métodos Privados

        private void InstanciaEspecieDocumento(int codigoBanco, int codigoEspecie)
        {
            try
            {
                switch (codigoBanco)
                {
                    //341 - Itaú
                    case 341:
                        _IEspecieDocumento = new EspecieDocumento_Itau(codigoEspecie);
                        break;
                    //356 - BankBoston
                    case 479:
                        _IEspecieDocumento = new EspecieDocumento_BankBoston(codigoEspecie);
                        break;
                    //422 - Safra
                    case 1:
                        _IEspecieDocumento = new EspecieDocumento_BancoBrasil(codigoEspecie);
                        break;
                    default:
                        throw new Exception("Código do banco não implementando: " + codigoBanco);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a execução da transação.", ex);
            }
        }

        public static EspeciesDocumento CarregaTodas(int codigoBanco)
        {
            try
            {
                EspeciesDocumento alEspeciesDocumento = new EspeciesDocumento();
                IEspecieDocumento obj;
                switch (codigoBanco)
                {
                    case 1:

                        alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.Cheque));
                        alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.DuplicataMercantil));
                        alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.DuplicataMercantilIndicacao));
                        alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.DuplicataServico));
                        alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.DuplicataServicoIndicacao));
                        alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.DuplicataRural));
                        alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.LetraCambio));
                        alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.NotaCreditoComercial));
                        alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.NotaCreditoExportacao));
                        alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.NotaCreditoIndustrial));
                        alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.NotaCreditoRural));
                        alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.NotaPromissoria));
                        alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.NotaPromissoriaRural));
                        alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.TriplicataMercantil));
                        alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.TriplicataServico));
                        alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.NotaSeguro));
                        alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.Recibo));
                        alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.Fatura));
                        alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.NotaDebito));
                        alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.ApoliceSeguro));
                        alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.MensalidadeEscolar));
                        alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.ParcelaConsorcio));
                        alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.Outros));

                        return alEspeciesDocumento;
                    case 341:

                        alEspeciesDocumento.Add(new EspecieDocumento_Itau((int)EnumEspecieDocumento_Itau.DuplicataMercantil));
                        alEspeciesDocumento.Add(new EspecieDocumento_Itau((int)EnumEspecieDocumento_Itau.NotaPromissoria));
                        alEspeciesDocumento.Add(new EspecieDocumento_Itau((int)EnumEspecieDocumento_Itau.NotaSeguro));
                        alEspeciesDocumento.Add(new EspecieDocumento_Itau((int)EnumEspecieDocumento_Itau.MensalidadeEscolar));
                        alEspeciesDocumento.Add(new EspecieDocumento_Itau((int)EnumEspecieDocumento_Itau.Recibo));
                        alEspeciesDocumento.Add(new EspecieDocumento_Itau((int)EnumEspecieDocumento_Itau.Contrato));
                        alEspeciesDocumento.Add(new EspecieDocumento_Itau((int)EnumEspecieDocumento_Itau.Cosseguros));
                        alEspeciesDocumento.Add(new EspecieDocumento_Itau((int)EnumEspecieDocumento_Itau.DuplicataServico));
                        alEspeciesDocumento.Add(new EspecieDocumento_Itau((int)EnumEspecieDocumento_Itau.LetraCambio));
                        alEspeciesDocumento.Add(new EspecieDocumento_Itau((int)EnumEspecieDocumento_Itau.NotaDebito));
                        alEspeciesDocumento.Add(new EspecieDocumento_Itau((int)EnumEspecieDocumento_Itau.DocumentoDivida));
                        alEspeciesDocumento.Add(new EspecieDocumento_Itau((int)EnumEspecieDocumento_Itau.EncargosCondominais));
                        alEspeciesDocumento.Add(new EspecieDocumento_Itau((int)EnumEspecieDocumento_Itau.Diversos));

                        return alEspeciesDocumento;
                    default:
                        throw new Exception("Código do banco não implementando: " + codigoBanco);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar objetos", ex);
            }
        }

        # endregion

        public static string ValidaSigla(IEspecieDocumento especie)
        {
            try
            {
                return especie.Sigla;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static int ValidaCodigo(IEspecieDocumento especie)
        {
            try
            {
                return especie.Codigo;
            }
            catch
            {
                return 0;
            }
        }
    }
}
