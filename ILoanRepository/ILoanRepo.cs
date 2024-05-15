using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanManagement.Models;

namespace LoanManagement.ILoanRepository
{
    internal interface ILoanRepo
    {
        public bool ApplyLoan(Loan loan);
     public  double CalculateInterest(int loanId);
        //double CalculateInterest(int loanId, double principalAmount, double interestRate, int loanTerm);
        //void LoanStatus(int loanId);
        //double CalculateEMI(int loanId);
        //double CalculateEMI(int loanId, double principalAmount, double interestRate, int loanTerm);
        //void LoanRepayment(int loanId, double amount);
        public void LoanStatus(int loanId);
      public   List  <Loan> GetLoanById(int loanId);
     public void UpdateLoan(Loan loan);
     public int GetCreditScore(int loanId);
      public   List <Loan> GetAllLoan();
    }
}