using LoanManagement.ILoanRepository;
using System;
using LoanManagement.Repository;
using LoanManagement.LoanService;
namespace LoanManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ILoanServices loanServices = new LoanServices();

            // Pass loanServices to the LoanMenu constructor
            LoanMenu loanMenu = new LoanMenu(loanServices);

            loanMenu.DisplayMenu();
        }
    }
}
