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
        public List<Loan> GetLoanById(int loanId)
        {

            try
            {
                List<Loan> loans = new List<Loan>();
                cmd.Connection = sql;
                cmd.CommandText = "SELECT * FROM Loan where loanId=@loanId";
                cmd.Parameters.AddWithValue("@loanId", loanId);
                sql.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Loan loan = new Loan
                    {
                        LoanId = (int)reader["LoanId"],
                        PrincipalAmount = (double)reader["PrincipalAmount"],
                        InterestRate = (double)reader["InterestRate"],
                        LoanTerm = (int)reader["LoanTerm"],
                        LoanType = reader["LoanType"].ToString(),
                        LoanStatus = reader["LoanStatus"].ToString()
                    };
                    loans.Add(loan);
                }
                sql.Close();

                return loans;
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error in retrieving loan:" + ex.Message);
                throw;
            }
            finally
            {
                sql.Close();
            }   }
        public double CalculateInterest(int loanId)
        {
            double interestAmount = 0;

            try
            {
                sql.Open();
                    cmd.Connection = sql;
                    cmd.CommandText = "SELECT PrincipalAmount, InterestRate, LoanTerm FROM Loan WHERE LoanID = @LoanId";
                    cmd.Parameters.AddWithValue("@LoanId", loanId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    
                        if (reader.Read())
                        {
                            double principalAmount = reader.GetDouble(0);
                            double interestRate = reader.GetDouble(1);
                            int loanTerm = reader.GetInt32(2);

                            
                            interestAmount = (principalAmount * interestRate * loanTerm) / 12;
                        }
                    
                }
            
            catch (IOException ex)
            {
              
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                
                sql.Close();
            }

            return interestAmount;
        }

        public List<Loan> GetAllLoan()
        {
            try
            {
                List<Loan> loans = new List<Loan>();
                 sql.Open();
                   cmd.Connection = sql;
                cmd.CommandText = "SELECT * FROM Loan ";
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Loan loan = new Loan
                            {
                                LoanId = reader.GetInt32(reader.GetOrdinal("LoanId")),
                                PrincipalAmount = (float)reader.GetDouble(reader.GetOrdinal("PrincipalAmount")),
                                InterestRate = (float)reader.GetDouble(reader.GetOrdinal("InterestRate")),
                                LoanTerm = reader.GetInt32(reader.GetOrdinal("LoanTerm")),
                                LoanType = reader.GetString(reader.GetOrdinal("LoanType")),
                                LoanStatus = reader.GetString(reader.GetOrdinal("LoanStatus"))
                            };
                            loans.Add(loan);
                        }
                return loans;

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error in retrieving loan:" + ex.Message);
                throw;
            }
            finally
            {
                sql.Close();
            }
            
        }
        public void LoanStatus(int loanId)
        {
            try
            {
                
                int creditScore = GetCreditScore(loanId);

                
               
               string loanStatus = (creditScore > 650) ? "Approved" : "Rejected";

                UpdateLoanStatus(loanId, loanStatus);

                Console.WriteLine($"Loan with ID {loanId} is {loanStatus}");
            }
            catch (IOException ex)
            {
                
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public int GetCreditScore(int loanId)
        {
            int creditScore = 0;

            try
            {
                sql.Open();
               cmd.Connection = sql;
                 cmd.CommandText = "SELECT CreditScore FROM Customer WHERE CustomerId = (SELECT CustomerId FROM Loan WHERE LoanId = @LoanId)";
                    cmd.Parameters.AddWithValue("@LoanId", loanId);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        creditScore = Convert.ToInt32(result);
                    }
                }
            
            catch (IOException ex)
            {
               
                Console.WriteLine("An error occurred while retrieving credit score: " + ex.Message);
            }
            finally
            {
               
                sql.Close();
            }

            return creditScore;
        }

        public void UpdateLoanStatus(int loanId, string loanStatus)
        {
            try
            {
                sql.Open();
                cmd.Connection = sql;
                cmd.CommandText = "UPDATE Loan SET LoanStatus = @LoanStatus WHERE LoanId = @LoanId";
                
                    cmd.Parameters.AddWithValue("@LoanStatus", loanStatus);
                    cmd.Parameters.AddWithValue("@LoanId", loanId);
                    cmd.ExecuteNonQuery();
                
            }
            catch (IOException ex)
            {
                Console.WriteLine("An error occurred while updating loan status: " + ex.Message);
            }
            finally
            {
                
                sql.Close();
            }
        }

        public void UpdateLoan(Loan loan)
        {
            throw new NotImplementedException();
        }
    }


}

    





