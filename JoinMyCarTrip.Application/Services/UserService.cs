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

        public ProfileUserViewModel Profile(string userId)
        {


            return repository.All<ApplicationUser>()
                .Include(x => x.Comments)
                .ThenInclude(x => x.Author)
                .ThenInclude(x=>x.Pets)
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