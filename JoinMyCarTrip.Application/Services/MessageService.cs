using JoinMyCarTrip.Application.Interfaces;
using JoinMyCarTrip.Application.Models.Messages;
using JoinMyCarTrip.Data.Common;
using JoinMyCarTrip.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Internal;

namespace JoinMyCarTrip.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepository repository;
        private readonly ISystemClock systemClock;

        public MessageService(IRepository _repository, ISystemClock _systemClock)
        {
            this.repository = _repository;
            this.systemClock = _systemClock;
        }
        public async Task TextMessage(TextMessageFormViewModel model, string tripId, string userId)
        {
            var trip = repository.All<Trip>()
                .FirstOrDefault(t => t.Id == tripId);

            if (trip == null)
            {
                throw new ArgumentException("Trip not found");
            }

            var message = new Message
            {
                TripId = trip.Id,
                Date = systemClock.UtcNow.DateTime,
                Text = model.Text,
                AuthorId = userId,
            };

            await repository.AddAsync(message);
            await repository.SaveChangesAsync();
        }
    }
}
