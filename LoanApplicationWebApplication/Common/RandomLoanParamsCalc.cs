using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanApplicationWebApplication.Common
{
    public  class RandomLoanParamsCalc
    {
        /* This class is just generate different Loan related parameters Randomly */
        public short CreditRating {  get; private set; }

        /* RandomNumberGenerator is a Common Method being in genearting random number
        * for different loan parameters */
        int RandomNumberGenerator(int lowerBound, int upperBound)
        {
            Random randNumber = new Random();
            int generatedNumber= randNumber.Next(lowerBound, upperBound);
            return generatedNumber;
        }

        internal byte GetAPRRate()
        {
            return (byte) RandomNumberGenerator(4,12);
        }
        internal short GetCreditRating()
        {
            CreditRating = (short)RandomNumberGenerator(600, 750);
            return CreditRating;
        }
        internal short GetNumberofLatePaymentsinLast5Years()
        {
            return (short)RandomNumberGenerator(0, 20);
        }
        internal int GetTotalOutstandingDebt()
        {
            return RandomNumberGenerator(25000, 1000000);
        }
        internal int GetNumberofOutstandingDebts()
        {
            return RandomNumberGenerator(0,10);
        }
        internal int GetRiskRating()
        {
           /*Risk Rating  is assumed to be below options
           1 - High
           2- Moderate
           3- Low*/

            int riskRating=0;

            if (CreditRating <= 630)
                riskRating = 1;
            else if (CreditRating > 630 && CreditRating < 650)
                riskRating = 2;
            else if (CreditRating > 650)
                riskRating = 3;

            return riskRating;
        }
    }
}
