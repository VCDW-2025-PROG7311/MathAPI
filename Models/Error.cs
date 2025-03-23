namespace MathAPI.Models;

public class Error
{
    public string? ErrorMessage { get; set; }

    public Error(string? errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}

// dotnet ef dbcontext scaffold "Server=EBRAHIM-LAPTOP\SQLEXPRESS;Database=Math_DB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models
