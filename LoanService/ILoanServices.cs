namespace LoanManagement.LoanService
{
    internal interface ILoanServices
    {
        void ApplyLoan();
       void GetLoanById();
        void CalculateInterest();
        void LoanStatus();
        void GetAllLoan();
    }
}
