using JoinMyCarTrip.Application.Interfaces;
using JoinMyCarTrip.Application.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Application.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateUser(RegisterUserFormModel user)
        {
            var errors = new List<string>();

            if (user.Username == null || user.Username.Length < UsernameMinLength || user.Username.Length > DefaultLength)
            {
                errors.Add($"Username '{user.Username}' is not valid. It must be between {UsernameMinLength} and {DefaultLength} characters long.");
            }

            if (user.Email == null || !Regex.IsMatch(user.Email, UserEmailRegularExpression))
            {
                errors.Add($"Email '{user.Email}' is not a valid e-mail address.");
            }

            if (user.Password == null || user.Password.Length < PasswordMinLength || user.Password.Length > DefaultLength)
            {
                errors.Add($"The provided password is not valid. It must be between {PasswordMinLength} and {DefaultLength} characters long.");
            }

            if (user.Password != null && user.Password.Any(x => x == ' '))
            {
                errors.Add($"The provided password cannot contain whitespaces.");
            }

            if (user.Password != user.ConfirmPassword)
            {
                errors.Add("Password and its confirmation are different.");
            }

            return errors;

        }
    }
}
