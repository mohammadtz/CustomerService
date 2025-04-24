using System.Text.RegularExpressions;
using Customers.Domain.Customers.Services;

namespace Customers.Domain.Service;

public class EmailFormatChecker : IEmailFormatChecker
{
    private const string Pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    
    public bool IsValid(string email)
    {
        return Regex.IsMatch(email, Pattern, RegexOptions.IgnoreCase);
    }
}
