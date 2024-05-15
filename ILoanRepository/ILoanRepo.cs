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
      public  bool ApplyLoan(Loan loan);
        //double CalculateInterest(int loanId);
        //double CalculateInterest(int loanId, double principalAmount, double interestRate, int loanTerm);
        //void LoanStatus(int loanId);
        //double CalculateEMI(int loanId);
        //double CalculateEMI(int loanId, double principalAmount, double interestRate, int loanTerm);
        //void LoanRepayment(int loanId, double amount);
        //public List<Loan> GetAllLoan();
        Loan GetLoanById(int loanId);
    }
}
