using System.Text.RegularExpressions;

namespace API_Usuarios.Validations
{
    public class EmailValidator
    {
        public static bool IsValidEmail(string emailAddress)
        {
            //if (string.IsNullOrEmpty(emailAddress))
            //{
            //    throw new EmailNotProvidedException();
            //}

            Regex regex = new Regex(@"^[\w0-9._%+-]+@[\w0-9.-]+\.[\w]{2,6}$");
            return regex.IsMatch(emailAddress);
        }
    }
}
