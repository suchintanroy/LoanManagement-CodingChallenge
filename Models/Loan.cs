using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagement.Models
{
     class Loan
    {
        public int LoanId { get; set; }
        public string Customer { get; set; }
        public double PrincipalAmount { get; set; }
        public double InterestRate { get; set; }
        public int LoanTerm { get; set; }
        public string LoanType { get; set; }
        public string LoanStatus { get; set; }

        public Loan() { }

        public Loan(int loanId, string customer, double principalAmount, double interestRate, int loanTerm, string loanType, string loanStatus)
        {
            LoanId = loanId;
            Customer = customer;
            PrincipalAmount = principalAmount;
            InterestRate = interestRate;
            LoanTerm = loanTerm;
            LoanType = loanType;
            LoanStatus = loanStatus;
        }

        public override string ToString()
        {
            return $"LoanId: {LoanId}, Customer: {Customer}, PrincipalAmount: {PrincipalAmount}, InterestRate: {InterestRate}, LoanTerm: {LoanTerm}, LoanType: {LoanType}, LoanStatus: {LoanStatus}";
        }
    }
}