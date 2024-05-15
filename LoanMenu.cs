using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanManagement.LoanService;
using LoanManagement.ILoanRepository;
using LoanManagement.Models;
namespace LoanManagement
{
    internal class LoanMenu
    {
        readonly ILoanServices loanServices;

        public LoanMenu()
        {
            loanServices = new LoanServices();
        }

        public void DisplayMenu()
        {
            while (true)
            {
                Console.WriteLine("Loan Management System");
                Console.WriteLine("1. Apply for a loan");
                Console.WriteLine("2. Get Loan By ID");
                Console.WriteLine("3. Calculate Interest");
                Console.WriteLine("4.Get All Loans");
                Console.WriteLine("5.Loan Status");
                Console.WriteLine("Enter your choice:");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        loanServices.ApplyLoan();
                        break;
                    case "2":
                        loanServices.GetLoanById();
                        break;
                        case "3":
                            loanServices.CalculateInterest();
                        break;
                        case "4":
                        loanServices.GetAllLoan();
                        break;
                        case "5":
                        loanServices.LoanStatus();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
