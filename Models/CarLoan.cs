using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagement.Models
{
    internal class CarLoan : Loan
    {
        public string CarModel { get; set; }
        public int CarValue { get; set; }

        public CarLoan() { }

        public CarLoan(int loanId, Customer customer,double principalAmount, double interestRate, int loanTerm, string loanStatus, string carModel, int carValue)
            : base(loanId,principalAmount, interestRate, loanTerm, "CarLoan", loanStatus)
        {
            CarModel = carModel;
            CarValue = carValue;
        }

        public override string ToString()
        {
            return base.ToString() + $", CarModel: {CarModel}, CarValue: {CarValue}";
        }
    }
}