using JoinMyCarTrip.Application.Interfaces;
using JoinMyCarTrip.Application.Models.Users;
using JoinMyCarTrip.Data.Common;
using JoinMyCarTrip.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace JoinMyCarTrip.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repository;

        public UserService(IRepository _repository)
        {
            this.repository = _repository;
        }

        public ProfileUserViewModel Profile(string userId)
        {
           var pet = repository.All<Pet>()
                    .Where(p => p.UserId == userId)
                    .Select(p => new UserPetViewModel
                    {
                        Type = p.Type,
                        Description = p.Description
                    }).FirstOrDefault();

            return repository.All<ApplicationUser>()
                .Include(x => x.Comments)
                .ThenInclude(x => x.Author)
                .Where(r => r.Id == userId)
                .Select(user => new ProfileUserViewModel
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    Phone = user.PhoneNumber,
                    Comments = user.Comments
                    .Select(comment => new CommentViewModel
                    {
                        Description = comment.Description,
                        Author = comment.Author.FullName,
                        Likes = comment.IsNiceOrganizer ? 1 : 0,
                        Date = comment.Date
                    }).ToList(),
                    Pet = pet
                }).FirstOrDefault();
        }
    }
}
