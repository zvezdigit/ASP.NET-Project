using JoinMyCarTrip.Application.Interfaces;
using JoinMyCarTrip.Application.Models.Trips;
using JoinMyCarTrip.Application.Models.Users;
using JoinMyCarTrip.Data.Common;
using JoinMyCarTrip.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Internal;

namespace JoinMyCarTrip.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;
        private readonly ISystemClock systemClock;

        public UserService(IRepository _repository, ISystemClock _systemClock)
        {
            repository = _repository;
            systemClock = _systemClock;
        }

        public async Task AddComment(AddCommentFormViewModel model, string tripOrganizerId, string userId)
        {
            var trip = repository.All<Trip>()
                .Include(t => t.TripOrganizer)
                .FirstOrDefault(t => t.TripOrganizerId == tripOrganizerId);

            if (trip == null)
            {
                throw new ArgumentException("Trip with given trip organizer not found");
            }

            var comment = new Comment
            {
                TripOrganizerId = trip.TripOrganizerId,
                Description = model.Description,
                Date = systemClock.UtcNow.DateTime,
                IsNiceOrganizer = model.IsNiceTripOrganizer,
                AuthorId = userId
            };

            await repository.AddAsync(comment);
            await repository.SaveChangesAsync();
        }

        public async Task AddPet(AddPetFormViewModel model, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("userId cannot be null or empty", nameof(userId));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var pet = new Pet
            {
                Type = model.Type,
                Description = model.Description,
                UserId = userId
            };

            await repository.AddAsync(pet);
            await repository.SaveChangesAsync();
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await repository.GetByIdAsync<ApplicationUser>(id);
        }


        public async Task<IEnumerable<UserTripViewModel>> GetUsers()
        {
            return await repository.All<ApplicationUser>()
                .Select(u => new UserTripViewModel()
                {
                    UserId = u.Id,
                    FullName=u.FullName,
                    Email = u.Email,
                    GravatarLink= Utils.Gravtar.GetUrl(u.Email),
                })
                .ToListAsync();
        }

        public ProfileUserViewModel Profile(string userId)
        {


            return repository.All<ApplicationUser>()
                .Include(x => x.Comments)
                .ThenInclude(x => x.Author)
                .ThenInclude(x => x.Pets)
                .Where(r => r.Id == userId)
                .Select(user => new ProfileUserViewModel
                {
                    UserId = userId,
                    GravatarLink = Utils.Gravtar.GetUrl(user.Email),
                    FullName = user.FullName,
                    Email = user.Email,
                    Phone = user.PhoneNumber,
                    Comments = user.Comments
                    .Select(comment => new CommentViewModel
                    {
                        Description = comment.Description,
                        Author = comment.Author.FullName,
                        Date = comment.Date
                    }).ToList(),
                    Pets = user.Pets
                    .Select(pet => new UserPetViewModel
                    {
                        Type = pet.Type,
                        Description = pet.Description
                    }).ToList(),
                    Likes = user.Comments.Count(c => c.IsNiceOrganizer)

                }).FirstOrDefault();
        }
    }
}
