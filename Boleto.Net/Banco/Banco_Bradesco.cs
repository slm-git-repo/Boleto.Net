using System;
using System.Collections.Generic;
using System.Web.UI;
using BoletoNet;
using Microsoft.VisualBasic;

[assembly: WebResource("BoletoNet.Imagens.237.jpg", "image/jpg")]

namespace BoletoNet
{ 
    /// <author>  
    /// Eduardo Frare
    /// Stiven 
    /// </author>    
    internal class Banco_Bradesco : AbstractBanco, IBanco
    {
        private string _dacNossoNumero = string.Empty;
        private int _dacBoleto = 0;

        /// <summary>
        /// Classe responsavel em criar os campos do Banco Banco_Bradesco.
        /// </summary>
        internal Banco_Bradesco()
        {
            this.Codigo = 237;
            this.Digito = 2;
            this.Nome = "Bradesco";
        }

        #region IBanco Members

        /// <summary>
        /// A linha digitável será composta por cinco campos:
        ///      1º campo
        ///          composto pelo código de Banco, código da moeda, as cinco primeiras posições do campo 
        ///          livre e o dígito verificador deste campo;
        ///      2º campo
        ///          composto pelas posições 6ª a 15ª do campo livre e o dígito verificador deste campo;
        ///      3º campo
        ///          composto pelas posições 16ª a 25ª do campo livre e o dígito verificador deste campo;
        ///      4º campo
        ///          composto pelo dígito verificador do código de barras, ou seja, a 5ª posição do código de 
        ///          barras;
        ///      5º campo
        ///          Composto pelo fator de vencimento com 4(quatro) caracteres e o valor do documento com 10(dez) caracteres, sem separadores e sem edição.
        /// 
        /// </summary>
        public override void FormataLinhaDigitavel(Boleto boleto)
        {

            //BBBMC.CCCCD1 CCCCC.CCCCCD2 CCCCC.CCCCCD3 D4 FFFFVVVVVVVVVV
           

            #region Campo 1

                string Grupo1 = string.Empty;

                string BBB = boleto.CodigoBarra.Codigo.Substring(0, 3);
                string M = boleto.CodigoBarra.Codigo.Substring(3, 1);
                string CCCCC = boleto.CodigoBarra.Codigo.Substring(19, 5);
                string D1 = Mod10(BBB + M + CCCCC).ToString();

                Grupo1 = string.Format("{0}{1}{2}.{3}{4} ", BBB, M, CCCCC.Substring(0, 1), CCCCC.Substring(1, 4), D1);


            #endregion Campo 1

            #region Campo 2

                string Grupo2 = string.Empty;

                string CCCCCCCCCC2 = boleto.CodigoBarra.Codigo.Substring(24, 10);
                string D2 = Mod10(CCCCCCCCCC2).ToString();

                Grupo2 = string.Format("{0}.{1}{2} ", CCCCCCCCCC2.Substring(0, 5), CCCCCCCCCC2.Substring(5, 5), D2);

            #endregion Campo 2

            #region Campo 3

                string Grupo3 = string.Empty;

                string CCCCCCCCCC3 = boleto.CodigoBarra.Codigo.Substring(34, 10);
                string D3 = Mod10(CCCCCCCCCC3).ToString();

                Grupo3 = string.Format("{0}.{1}{2} ", CCCCCCCCCC3.Substring(0, 5), CCCCCCCCCC3.Substring(5, 5), D3);


            #endregion Campo 3

            #region Campo 4

                string Grupo4 = string.Empty;

                string D4 = _dacBoleto.ToString();

                Grupo4 = string.Format("{0} ", D4);

            #endregion Campo 4

            #region Campo 5

                string Grupo5 = string.Empty;

                string FFFF = boleto.CodigoBarra.Codigo.Substring(5, 4);//FatorVencimento(boleto).ToString() ;

                if (boleto.Carteira == "06" && !Utils.DataValida(boleto.DataVencimento))
                    FFFF = "0000";

                string VVVVVVVVVV = boleto.ValorBoleto.ToString("f").Replace(",", "").Replace(".", "");
                VVVVVVVVVV = Utils.FormatCode(VVVVVVVVVV, 10);

                //if (Utils.ToInt64(VVVVVVVVVV) == 0)
                //    VVVVVVVVVV = "000";

                Grupo5 = string.Format("{0}{1}", FFFF, VVVVVVVVVV);

            #endregion Campo 5

            boleto.CodigoBarra.LinhaDigitavel = Grupo1 + Grupo2 + Grupo3 + Grupo4 + Grupo5;

        }

        /// <summary>
        /// 
        ///   *******
        /// 
        ///	O código de barra para cobrança contém 44 posições dispostas da seguinte forma:
        ///    01 a 03 - 3 - Identificação  do  Banco
        ///    04 a 04 - 1 - Código da Moeda
        ///    05 a 05 – 1 - Dígito verificador do Código de Barras
        ///    06 a 09 - 4 - Fator de vencimento
        ///    10 a 19 - 10 - Valor
        ///    20 a 44 – 25 - Campo Livre
        /// 
        ///   *******
        /// 
        /// </summary>
        /// 

        public override void FormataCodigoBarra(Boleto boleto)
        {
            string valorBoleto = boleto.ValorBoleto.ToString("f").Replace(",", "").Replace(".", "");
            valorBoleto = Utils.FormatCode(valorBoleto, 10);

            if (boleto.Carteira == "02" || boleto.Carteira == "09")
            {
                boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}", Codigo.ToString(), boleto.Moeda,
                FatorVencimento(boleto), valorBoleto, FormataCampoLivre(boleto));
            }
            else if (boleto.Carteira == "06")
            {
                boleto.CodigoBarra.Codigo = string.Format("{0}{1}0000{2}{3}", Codigo.ToString(), boleto.Moeda,
                        valorBoleto, FormataCampoLivre(boleto));
            }


            _dacBoleto = Mod11(boleto.CodigoBarra.Codigo, 9);

            boleto.CodigoBarra.Codigo = Strings.Left(boleto.CodigoBarra.Codigo, 4) + _dacBoleto + Strings.Right(boleto.CodigoBarra.Codigo, 39);
        }


        ///<summary>
        /// Campo Livre
        ///    20 a 23 -  4 - Agência Cedente (Sem o digito verificador,completar com zeros a esquerda quandonecessário)
        ///    24 a 25 -  2 - Carteira
        ///    26 a 36 - 11 - Número do Nosso Número(Sem o digito verificador)
        ///    37 a 43 -  7 - Conta do Cedente (Sem o digito verificador,completar com zeros a esquerda quando necessário)
        ///    44 a 44	- 1 - Zero            
        ///</summary>

        public string FormataCampoLivre(Boleto boleto)
        {

            string FormataCampoLivre = string.Format("{0}{1}{2}{3}{4}", boleto.Cedente.ContaBancaria.Agencia, boleto.Carteira,
                                            boleto.NossoNumero, boleto.Cedente.ContaBancaria.Conta, "0");

            return FormataCampoLivre;
        }


        public override void FormataNumeroDocumento(Boleto boleto)
        {
            throw new NotImplementedException("Função ainda não implementada.");
        }


        public override void FormataNossoNumero(Boleto boleto)
        {
            boleto.NossoNumero = string.Format("{0}/{1}-{2}", boleto.Carteira, boleto.NossoNumero,
                                            Mod11(boleto.Carteira + boleto.NossoNumero, 7));
        }


        public override void ValidaBoleto(Boleto boleto)
        {
            if (boleto.Carteira != "02" && boleto.Carteira != "06" && boleto.Carteira != "09")
                throw new NotImplementedException("Carteira não implementada. Carteiras implementadas 02, 06, 09.");

            //O valor é obrigatório para a carteira 09
            if (boleto.Carteira == "09")
            {
                if (boleto.ValorBoleto == 0)
                    throw new NotImplementedException("Para a carteira 06, o valor do boleto não pode ser igual a zero");
            }
            else if (boleto.Carteira == "06")
            {
                boleto.ValorBoleto = 0;
            }

            //Verifica se o nosso número é válido
            if (boleto.NossoNumero.Length > 11)
                throw new NotImplementedException("A quantidade de dígitos do nosso número, são 11 números.");
            else if (boleto.NossoNumero.Length < 11)
                boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 11);

            //Verificar se a Agencia esta correta
            if (boleto.Cedente.ContaBancaria.Agencia.Length > 4)
                throw new NotImplementedException("A quantidade de dígitos da Agência " + boleto.Cedente.ContaBancaria.Agencia + ", são de 4 números.");
            else if (boleto.Cedente.ContaBancaria.Agencia.Length < 4)
                boleto.Cedente.ContaBancaria.Agencia = Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, 4);

           //Verificar se a Conta esta correta
            if (boleto.Cedente.ContaBancaria.Conta.Length > 7)
                throw new NotImplementedException("A quantidade de dígitos da Conta " + boleto.Cedente.ContaBancaria.Conta + ", são de 04 números.");
            else if (boleto.Cedente.ContaBancaria.Conta.Length < 7)
                boleto.Cedente.ContaBancaria.Conta = Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta, 7);


            //Atribui o nome do banco ao local de pagamento
            boleto.LocalPagamento += Nome + "";


            //Verifica se data do processamento é valida
            if (boleto.DataProcessamento.ToString("dd/MM/yyyy") == "01/01/0001")
                boleto.DataProcessamento = DateTime.Now;


            //Verifica se data do documento é valida
            if (boleto.DataDocumento.ToString("dd/MM/yyyy") == "01/01/0001")
                boleto.DataDocumento = DateTime.Now;

            boleto.QuantidadeMoeda = 0;


            FormataCodigoBarra(boleto);
            FormataLinhaDigitavel(boleto);
            FormataNossoNumero(boleto);
        }
        #endregion IBanco Members

        /// <summary>
        /// Verifica o tipo de ocorrência para o arquivo remessa
        /// </summary>
        public string Ocorrencia(string codigo)
        {
            switch (codigo)
            {
                case "02":
                    return "02-Entrada Confirmada";
                case "03":
                    return "03-Entrada Rejeitada";
                case "06":
                    return "06-Liquidação  normal";
                case "09":
                    return "09-Baixado Automaticamente via Arquivo";
                case "10":
                    return "10-Baixado conforme instruções da Agência";
                case "11":
                    return "11-Em Ser - Arquivo de Títulos pendentes";
                case "12":
                    return "12-Abatimento Concedido";
                case "13":
                    return "13-Abatimento Cancelado";
                case "14":
                    return "14-Vencimento Alterado";
                case "15":
                    return "15-Liquidação em Cartório";
                case "17":
                    return "17-Liquidação após baixa ou Título não registrado";
                case "18":
                    return "18-Acerto de Depositária";
                case "19":
                    return "19-Confirmação Recebimento Instrução de Protesto";
                case "20":
                    return "20-Confirmação Recebimento Instrução Sustação de Protesto";
                case "21":
                    return "21-Acerto do Controle do Participante";
                case "23":
                    return "22-Entrada do Título em Cartório";
                case "24":
                    return "23-Entrada rejeitada por CEP Irregular";
                case "27":
                    return "27-Baixa Rejeitada";
                case "28":
                    return "28-Débito de tarifas/custas";
                case "30":
                    return "30-Alteração de Outros Dados Rejeitados";
                case "32":
                    return "32-Instrução Rejeitada";
                case "33":
                    return "33-Confirmação Pedido Alteração Outros Dados";
                case "34":
                    return "34-Retirado de Cartório e Manutenção Carteira";
                case "35":
                    return "35-Desagendamento ) débito automático";
                case "68":
                    return "68-Acerto dos dados ) rateio de Crédito";
                case "69":
                    return "69-Cancelamento dos dados ) rateio";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Verifica o código do motivo da rejeição informada pelo banco
        /// </summary>
        public string MotivoRejeicao(string codigo)
        {
            switch (codigo)
            {
                case "02":
                    return "02-Código do registro detalhe inválido";
                case "03":
                    return "03-Código da ocorrência inválida";
                case "04":
                    return "04-Código de ocorrência não permitida para a carteira";
                case "05":
                    return "05-Código de ocorrência não numérico";
                case "07":
                    return "07-Agência/conta/Digito - Inválido";
                case "08":
                    return "08-Nosso número inválido";
                case "09":
                    return "09-Nosso número duplicado";
                case "10":
                    return "10-Carteira inválida";
                case "16":
                    return "16-Data de vencimento inválida";
                case "18":
                    return "18-Vencimento fora do prazo de operação";
                case "20":
                    return "19-Valor do Título inválido";
                case "21":
                    return "21-Espécie do Título inválida";
                case "22":
                    return "22-Espécie não permitida para a carteira";
                case "24":
                    return "24-Data de emissão inválida";
                case "38":
                    return "38-Prazo para protesto inválido";
                case "44":
                    return "44-Agência Cedente não prevista";
                case "50":
                    return "50-CEP irregular - Banco Correspondente";
                case "63":
                    return "63-Entrada para Título já cadastrado";
                case "68":
                    return "68-Débito não agendado - erro nos dados de remessa";
                case "69":
                    return "69-Débito não agendado - Sacado não consta no cadastro de autorizante";
                case "70":
                    return "70-Débito não agendado - Cedente não autorizado pelo Sacado";
                case "71":
                    return "71-Débito não agendado - Cedente não participa da modalidade de débito automático";
                case "72":
                    return "72-Débito não agendado - Código de moeda diferente de R$";
                case "73":
                    return "73-Débito não agendado - Data de vencimento inválida";
                case "74":
                    return "74-Débito não agendado - Conforme seu pedido, Título não registrado";
                case "75":
                    return "75-Débito não agendado - Tipo de número de inscrição do debitado inválido";
                default:
                    return "";
            }
        }
    }
}
