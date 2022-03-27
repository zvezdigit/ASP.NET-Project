using JoinMyCarTrip.Application.Models.Messages;
using JoinMyCarTrip.Application.Services;
using JoinMyCarTrip.Data.Common;
using JoinMyCarTrip.Data.Entities;
using Microsoft.Extensions.Internal;
using NSubstitute;
using Xunit;

namespace JoinMyCarTrip.Application.Test
{
    public class MessageServiceTest
    {
        [Fact]
        public async Task WhenTextingAMessageANewMessageCreated()
        {
            // arrange
            var userId = "user123";
            var tripId = "trip123";
            var utcNow = new DateTime(2022, 05, 16, 15, 30, 10);


            var model = new TextMessageFormViewModel
            {
                Text = "Hello,guys!"
            };

            var repository = Substitute.For<IRepository>();
            var systemClock = Substitute.For<ISystemClock>();
            
            var trips = new List<Trip>
            {
                new Trip{ Id = "trip456"},
                new Trip { Id = "trip789"},
                new Trip { Id = "trip123"},
            }.AsQueryable();

            repository.All<Trip>().Returns(trips);

            systemClock.UtcNow.Returns(utcNow);

            var service = new MessageService(repository, systemClock);

            // act
            await service.TextMessage(model, tripId, userId);

            // assert
            await repository.Received(1).AddAsync<Message>(Arg.Is<Message>(m =>m.Text==model.Text
            && m.AuthorId==userId && m.TripId== tripId && m.Date==utcNow)); 
            await repository.Received(1).SaveChangesAsync();
        }

        [Fact]

        public async Task WhenTextingAMessageAndAModelIsNull()
        {
            var userId = "user123";
            var tripId = "trip123";

            var repository = Substitute.For<IRepository>();
            var systemClock = Substitute.For<ISystemClock>();
            var service = new MessageService(repository, systemClock);

            var trips = new List<Trip>
            {
                new Trip{ Id = "trip456"},
                new Trip { Id = "trip789"},
                new Trip { Id = "trip123"},
            }.AsQueryable();

            repository.All<Trip>().Returns(trips);


            await Assert.ThrowsAsync<ArgumentNullException>(
                    async () => await service.TextMessage(null, tripId, userId));
        }

        [Fact]
        public async Task WhenTextingAMessageAndTripIsNotFound()
        {
            var userId = "user123";
            var tripId = "trip123";

            var repository = Substitute.For<IRepository>();
            var systemClock = Substitute.For<ISystemClock>();
            var service = new MessageService(repository, systemClock);

            var trips = new List<Trip>
            {
                new Trip{ Id = "trip456"},
                new Trip { Id = "trip789"},
                new Trip { Id = "trip923"},
            }.AsQueryable();

            repository.All<Trip>().Returns(trips);


            await Assert.ThrowsAsync<ArgumentException>(
                    async () => await service.TextMessage(new TextMessageFormViewModel() , tripId, userId));

            var exception = await Assert.ThrowsAsync<ArgumentException>(
                    async () => await service.TextMessage(new TextMessageFormViewModel(), tripId, userId));

            Assert.Equal("Trip is not found", exception.Message);
        }
    }
}
