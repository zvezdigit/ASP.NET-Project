﻿using JoinMyCarTrip.Application.Interfaces;
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

        public async Task AddComment(AddCommentFormViewModel model, string tripOrganizerId, string userId)
        {
            var tripOrganizer = repository.All<Trip>()
                 .FirstOrDefault(t => t.Id == tripOrganizerId);

            if (tripOrganizer == null)
            {
                throw new ArgumentException("TripOrganizer not found");
            }

            var comment = new Comment
            {
                TripOrganizerId = tripOrganizer.Id,
                Description = model.Description,
                Date = model.Date,
                AuthorId = userId

            };

            await repository.AddAsync(comment);
            await repository.SaveChangesAsync();
        }

        public async Task AddPet(AddPetFormViewModel model, string userId)
        {
            var pet = new Pet
            {
                Type = model.Type,
                Description = model.Description,
                UserId = userId
            };

            await repository.AddAsync(pet);
            await repository.SaveChangesAsync();
        }

        public ProfileUserViewModel Profile(string userId)
        {


            return repository.All<ApplicationUser>()
                .Include(x => x.Comments)
                .ThenInclude(x => x.Author)
                .ThenInclude(x=>x.Pets)
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
                    .Select(pet=> new UserPetViewModel
                    {
                        Type = pet.Type,
                        Description = pet.Description
                    }).ToList(),
                    Likes = user.Comments.Count(c => c.IsNiceOrganizer)

                }).FirstOrDefault();
        }
    }
}
