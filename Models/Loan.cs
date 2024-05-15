using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagement.Models
{
     class Loan
    {
        private string v;

        public int LoanId { get; set; }
        public int Customer { get; set; }
        public double PrincipalAmount { get; set; }
        public double InterestRate { get; set; }
        public int LoanTerm { get; set; }
        public string LoanType { get; set; }
        public string LoanStatus { get; set; }
        public object CustomerId { get; internal set; }

        public Loan() { }

        public Loan(int loanId, int customer, double principalAmount, double interestRate, int loanTerm, string loanType, string loanStatus)
        {
            LoanId = loanId;
            Customer = customer;
            PrincipalAmount = principalAmount;
            InterestRate = interestRate;
            LoanTerm = loanTerm;
            LoanType = loanType;
            LoanStatus = loanStatus;
        }

        public Loan(int loanId, double principalAmount, double interestRate, int loanTerm, string v, string loanStatus)
        {
            LoanId = loanId;
            PrincipalAmount = principalAmount;
            InterestRate = interestRate;
            LoanTerm = loanTerm;
            this.v = v;
            LoanStatus = loanStatus;
        }

        public override string ToString()
        {
            return $"LoanId: {LoanId}, Customer: {Customer}, PrincipalAmount: {PrincipalAmount}, InterestRate: {InterestRate}, LoanTerm: {LoanTerm}, LoanType: {LoanType}, LoanStatus: {LoanStatus}";
        }
    }
}