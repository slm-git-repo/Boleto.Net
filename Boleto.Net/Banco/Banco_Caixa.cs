using System;
using System.Web.UI;
using BoletoNet;
using Microsoft.VisualBasic;

[assembly: WebResource("BoletoNet.Imagens.104.jpg", "image/jpg")]

namespace BoletoNet
{
    /// <summary>
    /// Classe referente ao banco Banco_Caixa Economica Federal
    /// </summary>
    internal class Banco_Caixa : AbstractBanco, IBanco
    {
        private const int EMISSAO_CEDENTE = 4;

        string _dacBoleto = String.Empty;
        
        internal Banco_Caixa()
        {
            this.Codigo = 104;
            this.Digito = 0;
            this.Nome = "Banco Caixa";            
        }

        public override void FormataCodigoBarra(Boleto boleto) 
        {
            // Posição 01-03
            string banco = Codigo.ToString();
            //POsição 04
            string moeda = "9";
            
            //Posição 05 - No final ...   

            // Posição 06 - 09
            string vencimento = boleto.DataVencimento.ToShortDateString();
            long fatorVencimento = FatorVencimento(boleto);

            // Posição 10 - 19     
            string valorDocumento = boleto.ValorBoleto.ToString("f").Replace(",", "").Replace(".", "");
            valorDocumento = Utils.FormatCode(valorDocumento, 10);
            

            // Inicio Campo livre
            //Posição 20 - 25
            string codigoCedente = Utils.FormatCode(boleto.Cedente.Codigo.ToString(), 6);
            
            // Posição 26
            string dvCodigoCedente = Mod11Base9(codigoCedente).ToString();
            
            //Posição 27 - 29
            string primeiraParteNossoNumero = boleto.NossoNumero.Substring(0, 3);
            
            //Posição 30
            string primeiraConstante = boleto.Carteira;
            
            // Posição 31 - 33
            string segundaParteNossoNumero = boleto.NossoNumero.Substring(3, 3);
            
            // Posição 24
            string segundaConstante = EMISSAO_CEDENTE.ToString();
            
            //Posição 35 - 43
            string terceiraParteNossoNumero = boleto.NossoNumero.Substring(6, 9);
          
            //Posição 44
            string ccc = string.Format("{0}{1}{2}{3}{4}{5}{6}", codigoCedente, dvCodigoCedente, primeiraParteNossoNumero,
                    primeiraConstante, segundaParteNossoNumero, segundaConstante, terceiraParteNossoNumero);

            string dvCampoLivre = Mod11Base9(ccc).ToString();

            string campoLivre = string.Format("{0}{1}", ccc, dvCampoLivre);

            
          
            string xxxx = string.Format("{0}{1}{2}{3}{4}", banco, moeda, fatorVencimento, valorDocumento, campoLivre);

            string dvGeral = Mod11(xxxx, 9).ToString();
          // Posição 5
            _dacBoleto = dvGeral;
         
            boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}",
                   banco,
                   moeda,
                   dvGeral,
                   fatorVencimento,
                   valorDocumento,
                   campoLivre
                   );
        }

        public override void FormataLinhaDigitavel(Boleto boleto) 
        {
            
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

            long FFFF = FatorVencimento(boleto);

            string VVVVVVVVVV = boleto.ValorBoleto.ToString("f").Replace(",", "").Replace(".", "");
            VVVVVVVVVV = Utils.FormatCode(VVVVVVVVVV, 10);

            if (Utils.ToInt64(VVVVVVVVVV) == 0)
                VVVVVVVVVV = "000";

            Grupo5 = string.Format("{0}{1}", FFFF, VVVVVVVVVV);

            #endregion Campo 5

            boleto.CodigoBarra.LinhaDigitavel = Grupo1 + Grupo2 + Grupo3 + Grupo4 + Grupo5;

        }

        public override void FormataNossoNumero(Boleto boleto) 
        {
            boleto.NossoNumero = string.Format("{0}{1}/{2}-{3}", boleto.Carteira, EMISSAO_CEDENTE, boleto.NossoNumero, Mod11Base9(boleto.Carteira + EMISSAO_CEDENTE + boleto.NossoNumero));
        }

        public override void FormataNumeroDocumento(Boleto boleto) 
        { 
        }

        public override void ValidaBoleto(Boleto boleto) 
        {

            if (boleto.NossoNumero.Length != 15)
            {
                throw new Exception("Nosso Número inválido, Para Caixa Econômica o Nosso Número deve conter 15 caracteres.");
            }

            if (!boleto.Cedente.Codigo.Equals(0))
            {
                string codigoCedente = Utils.FormatCode(boleto.Cedente.Codigo.ToString(), 6);
                string dvCodigoCedente = Mod11Base9(codigoCedente).ToString();
                boleto.Cedente.DigitoCedente = Convert.ToInt32(dvCodigoCedente);
            }
            else
            {
                throw new Exception("Informe o código do cedente.");
            }

            //Atribui o nome do banco ao local de pagamento
            boleto.LocalPagamento = "PREFERENCIALMENTE NAS CASAS LOTÉRICAS E AGÊNCIAS DA CAIXA";
          
            FormataCodigoBarra(boleto);
            FormataLinhaDigitavel(boleto);
            FormataNossoNumero(boleto);
        }

        
              
    }
}
