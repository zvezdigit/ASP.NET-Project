using JoinMyCarTrip.Application.Models.Users;


namespace JoinMyCarTrip.Application.Interfaces
{
    public interface IUserService
    {
        ProfileUserViewModel Profile(string userId);
    }
}
