﻿using JoinMyCarTrip.Application.Models.Users;


namespace JoinMyCarTrip.Application.Interfaces
{
    public interface IUserService
    {
        ProfileUserViewModel Profile(string userId);

        Task AddPet(AddPetFormViewModel model, string userId);

        Task AddComment(AddCommentFormViewModel model, string tripOrganizerId, string userId);

      
    }
}
