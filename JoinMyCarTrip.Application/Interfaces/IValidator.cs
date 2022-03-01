using JoinMyCarTrip.Application.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Application.Interfaces
{
    internal interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserFormModel model);
    }
}
