﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagement.Models
{
   
    
        internal class Customer
        {
            public int CustomerId { get; set; }
            public string Name { get; set; }
            public string EmailAddress { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }
            public int CreditScore { get; set; }

            public Customer() { }

            public Customer(int customerId, string name, string emailAddress, string phoneNumber, string address, int creditScore)
            {
                CustomerId = customerId;
                Name = name;
                EmailAddress = emailAddress;
                PhoneNumber = phoneNumber;
                Address = address;
                CreditScore = creditScore;
            }

            public override string ToString()
            {
                return $"CustomerId: {CustomerId}, Name: {Name}, EmailAddress: {EmailAddress}, PhoneNumber: {PhoneNumber}, Address: {Address}, CreditScore: {CreditScore}";
            }
        }
    }
