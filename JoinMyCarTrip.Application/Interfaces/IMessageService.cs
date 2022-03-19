using JoinMyCarTrip.Application.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Application.Interfaces
{
    public interface IMessageService
    {
        Task TextMessage(TextMessageFormViewModel model, string tripId, string userId);
    }
}
