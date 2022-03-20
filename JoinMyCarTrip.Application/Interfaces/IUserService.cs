using JoinMyCarTrip.Application.Models.Trips;
using JoinMyCarTrip.Application.Models.Users;
using JoinMyCarTrip.Data.Entities;

namespace JoinMyCarTrip.Application.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserById(string id);
        ProfileUserViewModel Profile(string userId);

        Task AddPet(AddPetFormViewModel model, string userId);

        Task AddComment(AddCommentFormViewModel model, string tripOrganizerId, string userId);

        Task<IEnumerable<UserTripViewModel>> GetUsers();


    }
}
