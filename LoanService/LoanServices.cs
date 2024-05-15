using System;
using LoanManagement.Models;
using LoanManagement.Repository;
using LoanManagement.ILoanRepository;
namespace LoanManagement.LoanService
{
    internal class LoanServices : ILoanServices
    {
        readonly ILoanRepo loanRepo;

        public LoanServices()
        {
            loanRepo = new LoanRepo();
        }

        public void ApplyLoan()
        {
            try
            {
                Console.WriteLine("Enter loan details:");

                Console.WriteLine("Enter principal amount:");
                double principalAmount = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Enter interest rate:");
                double interestRate = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter loan type:");
                string loanType = Console.ReadLine();

                Console.WriteLine("Enter loan term (in months):");
                int loanTerm = Convert.ToInt32(Console.ReadLine());

                Loan loan = new Loan
                {
                    PrincipalAmount = principalAmount,
                    InterestRate = interestRate,
                    LoanType =loanType,
                    LoanTerm = loanTerm
                };
                loanRepo.ApplyLoan(loan);

                Console.WriteLine("Loan applied successfully!");
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        public void GetLoanById()
        {
            try
            {
                Console.WriteLine("Enter loan ID:");
                int loanId = Convert.ToInt32(Console.ReadLine());

                Loan loan = loanRepo.GetLoanById(loanId);

                if (loan != null)
                {
                    Console.WriteLine("Loan details:");
                    Console.WriteLine($"Loan ID: {loan.LoanId}");
                    Console.WriteLine($"Customer: {loan.Customer}");
                    Console.WriteLine($"Principal Amount: {loan.PrincipalAmount}");
                    Console.WriteLine($"Interest Rate: {loan.InterestRate}");
                    Console.WriteLine($"Loan Type: {loan.LoanType}");
                    Console.WriteLine($"Loan Term: {loan.LoanTerm}");
                    Console.WriteLine($"Loan Status: {loan.LoanStatus}");
                }
                else
                {
                    Console.WriteLine("Loan not found.");
                }
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }




    }
}
