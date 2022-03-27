using JoinMyCarTrip.Application.Models.Users;
using JoinMyCarTrip.Application.Services;
using JoinMyCarTrip.Data.Common;
using JoinMyCarTrip.Data.Entities;
using Microsoft.Extensions.Internal;
using NSubstitute;
using Xunit;

namespace JoinMyCarTrip.Application.Test
{
    public class UserServiceTest
    {

        [Fact]

        public async Task WhenAddingACommentANewCommentIsAdded()
        {
            var userId = "123";
            var utcNow = new DateTime(2022, 05, 5, 11, 30, 10);

            var commentModel = new AddCommentFormViewModel
            {
                Description = "nice trip",
                IsNiceTripOrganizer = true,
            };

            var repository = Substitute.For<IRepository>();
            var systemClock = Substitute.For<ISystemClock>();

            var trip1 = new Trip
            {
                Id = "trip123",
                TripOrganizerId = "t123"
            };

            var trip2 = new Trip
            {
                Id = "trip456",
                TripOrganizerId = "t456"
            };

            var trips = new List<Trip>
            {
              trip1, trip2

            }.AsQueryable();

            repository.All<Trip>().Returns(trips);

            systemClock.UtcNow.Returns(utcNow);


            var service = new UserService(repository, systemClock);

            // act
            await service.AddComment(commentModel, trip1.TripOrganizerId, userId);

            // assert
            await repository.Received(1).AddAsync<Comment>(Arg.Is<Comment>(c => c.Description == commentModel.Description
            && c.IsNiceOrganizer == commentModel.IsNiceTripOrganizer && c.TripOrganizerId == trip1.TripOrganizerId
            && c.Date == utcNow && c.AuthorId == userId));

            await repository.Received(1).SaveChangesAsync();
        }

        [Fact]

        public async Task WhenAddingACommentAndATripIsNotFound()
        {
            var trip1 = new Trip
            {
                Id = "trip123",
                TripOrganizerId = "t123"
            };

            var trip2 = new Trip
            {
                Id = "trip456",
                TripOrganizerId = "t456"
            };

            var trip3 = new Trip
            {
                Id = "trip789",
                TripOrganizerId = "t789"
            };

            var trips = new List<Trip>
            {
              trip2

            }.AsQueryable();

            var repository = Substitute.For<IRepository>();
            repository.All<Trip>().Returns(trips);

            var service = new UserService(repository, Substitute.For<ISystemClock>());

            await Assert.ThrowsAsync<ArgumentException>(() => service.AddComment(null, trip1.TripOrganizerId, null));
        }

        [Fact]

        public async Task WhenAddingAPetNewPetIsAdded()
        {
            //arrange
            var userId = "123";

            var model = new AddPetFormViewModel
            {
                Type = "cat",
                Description = "cute",
            };

            var repository = Substitute.For<IRepository>();
            var systemClock = Substitute.For<ISystemClock>();

            var service = new UserService(repository, systemClock);

            // act
            await service.AddPet(model, userId);

            // assert
            await repository.Received(1).AddAsync<Pet>(Arg.Is<Pet>(p => p.Description == model.Description
            && p.Type == model.Type && p.UserId == userId));

            await repository.Received(1).SaveChangesAsync();
        }

        [Fact]

        public async Task WhenAddingAPetAndAUserIsNull()
        {
            string userId = null;

            var repository = Substitute.For<IRepository>();
            var systemClock = Substitute.For<ISystemClock>();

            var service = new UserService(repository, systemClock);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            await service.AddPet(new AddPetFormViewModel(), userId));

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
            await service.AddPet(new AddPetFormViewModel(), userId));

            Assert.Equal("userId cannot be null or empty (Parameter 'userId')", exception.Message);
        }

        [Fact]

        public async Task WhenAddingAPetAndAModelIsNull()
        {
            string userId = "123";

            var repository = Substitute.For<IRepository>();
            var systemClock = Substitute.For<ISystemClock>();

            var service = new UserService(repository, systemClock);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await service.AddPet(null, userId));
        }

        [Fact]
        public async Task WhenGivingUserIdReturnUser()
        {
            var userId = "123";

            var repository = Substitute.For<IRepository>();
            var systemClock = Substitute.For<ISystemClock>();

            var service = new UserService(repository, systemClock);

            var testUser = new ApplicationUser { Id = "123", FullName = "user1", Email = "user1@abv.bg" };

            repository.GetByIdAsync<ApplicationUser>(userId).Returns(testUser);

            var returnedUser = await service.GetUserById(userId);

            Assert.Equal(testUser.Id, returnedUser.Id);
            Assert.Equal(testUser.UserName, returnedUser.UserName);
        }

        // [Fact]

        public async Task ReturnAllUsers()
        {
            var user1 = new ApplicationUser
            {
                Id = "123",
                FullName = "user1",
                Email = "user1@abv.bg"
            };

            var user2 = new ApplicationUser
            {
                Id = "456",
                FullName = "user2",
                Email = "user2@abv.bg"
            };

            var repository = Substitute.For<IRepository>();
            var systemClock = Substitute.For<ISystemClock>();

            var service = new UserService(repository, systemClock);

            repository.All<ApplicationUser>().Returns(new[] { user1, user2 }.AsQueryable());

            var returnedUsers = await service.GetUsers();

            Assert.Equal(2, returnedUsers.Count());
        }

        [Fact]
        public void ReturnUserProfileWhenGiveAUser()
        {
            var userId = "123";

            var author = new ApplicationUser
            {

                Id = "a1",
                FullName = "a",
                Email = "a@aaa.bg"

            };

            var comments = new List<Comment>
            {
                new Comment{Id="1", Description = "nice person", Author= author, IsNiceOrganizer = true},
                new Comment{Id="2", Description = "very nice person", Author= author, IsNiceOrganizer = true}
            };

            var user = new ApplicationUser
            {
                Id = userId,
                FullName = "user",
                Email = "user@abv.bg",
                PhoneNumber = "9999999999",
                Comments = comments,
            };

            var repository = Substitute.For<IRepository>();
            var systemClock = Substitute.For<ISystemClock>();

            repository.All<ApplicationUser>().Returns(new[] { user }.AsQueryable());

            var service = new UserService(repository, systemClock);

            var userProfile = service.Profile(userId);

            Assert.Equal(2, userProfile.Likes);
            Assert.Equal(user.Email, userProfile.Email);
            Assert.Equal(user.FullName, userProfile.FullName);
            Assert.Equal(user.PhoneNumber, userProfile.Phone);
        }

        [Fact]

        public void WhenReturningAUserProfileAndUserIsNotFound()
        {
            var userId = "123";

            var repository = Substitute.For<IRepository>();
            var systemClock = Substitute.For<ISystemClock>();


            repository.All<ApplicationUser>().Returns(new[]
            {
                 new ApplicationUser { Id = "1" },
                 new ApplicationUser { Id = "2" },
                 new ApplicationUser { Id = "3" },
            }.AsQueryable());


            var service = new UserService(repository, systemClock);

            var userProfile = service.Profile(userId);

            Assert.Null(userProfile);
        }

    }
}