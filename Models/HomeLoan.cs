using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagement.Models
{
    
        internal class HomeLoan : Loan
        {
            public string PropertyAddress { get; set; }
            public int PropertyValue { get; set; }

            public HomeLoan() { }

            public HomeLoan(int loanId, int customer, double principalAmount, double interestRate, int loanTerm, string loanStatus, string propertyAddress, int propertyValue)
                : base(loanId, customer, principalAmount, interestRate, loanTerm, "HomeLoan", loanStatus)
            {
                PropertyAddress = propertyAddress;
                PropertyValue = propertyValue;
            }

            public override string ToString()
            {
                return base.ToString() + $", PropertyAddress: {PropertyAddress}, PropertyValue: {PropertyValue}";
            }
        }
    }
