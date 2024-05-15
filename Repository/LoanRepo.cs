using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanManagement.Models;
using LoanManagement.Util;
using LoanManagement.ILoanRepository;
using LoanManagement.Exception;
namespace LoanManagement.Repository
{
    internal class LoanRepo : ILoanRepo
    {
        SqlConnection sql = null;
        SqlCommand cmd = null;

        public LoanRepo()
        {
            sql = new SqlConnection(DbConnUtil.GetConnectionString());
            cmd = new SqlCommand();
        }

        public bool ApplyLoan(Loan loan)
        {
            try
            {
                loan.LoanStatus = "Pending";

                //Displaying loan details to the user for confirmation
                Console.WriteLine("Loan details:");
                Console.WriteLine($"Principal Amount: {loan.PrincipalAmount}");
                Console.WriteLine($"Interest Rate: {loan.InterestRate}");
                Console.WriteLine($"Loan Term: {loan.LoanTerm}");
                Console.WriteLine($"Loan Type: {loan.LoanType}");
                
                Console.WriteLine("Do you want to apply for this loan? (Yes/No)");

                string userInput = Console.ReadLine();

                if (!userInput.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Loan application cancelled.");
                    return false;
                }

                sql.Open();
                cmd.CommandText = "INSERT INTO Loan (PrincipalAmount, InterestRate, LoanTerm, LoanStatus,LoanType) " +
                                  "VALUES (@PrincipalAmount, @InterestRate, @LoanTerm, @LoanStatus,@LoanType)";
                cmd.Parameters.AddWithValue("@PrincipalAmount", loan.PrincipalAmount);
                cmd.Parameters.AddWithValue("@InterestRate", loan.InterestRate);
                cmd.Parameters.AddWithValue("@LoanTerm", loan.LoanTerm);
                cmd.Parameters.AddWithValue("@LoanStatus", loan.LoanStatus);
                cmd.Parameters.AddWithValue("@LoanType", loan.LoanType);

                cmd.Connection = sql;
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine("Error applying for loan: " + ex.Message);
                return false;
            }
            finally
            {
                sql.Close();
            }
        }
        public Loan GetLoanById(int loanId)
        {
            Loan loan = null;

            try
            {
                sql.Open();
                cmd.CommandText = "SELECT * FROM Loans WHERE LoanId = @LoanId";
                cmd.Parameters.AddWithValue("@LoanId", loanId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        loan = new Loan
                        {
                            LoanId = (int)reader["LoanId"],
                            // Check if the "Customer" column exists before accessing its value
                            Customer = reader.IsDBNull(reader.GetOrdinal("Customer")) ? null : reader["Customer"].ToString(),
                            PrincipalAmount = (double)reader["PrincipalAmount"],
                            InterestRate = (double)reader["InterestRate"],
                            LoanTerm = (int)reader["LoanTerm"],
                            LoanStatus = reader["LoanStatus"].ToString(),
                            LoanType = reader["LoanType"].ToString()
                        };
                    }
                }
            }
            catch (ApplicationException ex) { 
            
                Console.WriteLine("Error retrieving loan details: " + ex.Message);
            }
            finally
            {
                sql.Close();
            }

            return loan;
        }






    }
}
