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
                    LoanType = loanType,
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
        public  void GetLoanById()
        {
            try
            {
                Console.WriteLine("Enter the Loan-ID:");
                int loanId = Convert.ToInt32(Console.ReadLine());

                List<Loan> loans = loanRepo.GetLoanById(loanId);

                if (loans != null && loans.Any())
                {
                    Console.WriteLine("Loans for User using UserID " + loanId);
                    foreach (var loan in loans)
                    {
                        Console.WriteLine($"Loan ID: {loan.LoanId}, Principal Amount: {loan.PrincipalAmount}, Interest Rate: {loan.InterestRate}, Loan Term: {loan.LoanTerm}");
                    }
                }
                else
                {
                    Console.WriteLine("No loans found for UserID " + loanId);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        public void CalculateInterest()
        {
            Console.Write("Enter the loan id:");
            int loanId = int.Parse(Console.ReadLine());
            double calculatedInterest = loanRepo.CalculateInterest(loanId);
            Console.WriteLine($"The sum of Interest is{calculatedInterest}");

        }

        public void GetAllLoan()
        {
            List<Loan> loans = loanRepo.GetAllLoan();

            foreach (var loan in loans)
            {
                Console.WriteLine($"Loan Id: {loan.LoanId}, Customer Id: {loan.CustomerId}, Loan Type: {loan.LoanType}, Principal Amount: {loan.PrincipalAmount}, Loan Status: {loan.LoanStatus}");
            }
        }

        public void LoanStatus()
        {
            try
            {
                Console.Write("Enter the loan ID: ");
                int loanId = int.Parse(Console.ReadLine());

                
                LoanServices.LoanStatus(loanId);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid loan ID. Please enter a valid numeric loan ID.");
            }
        }

        private static void LoanStatus(int loanId)
        {
            throw new NotImplementedException();
        }
    }




    }


