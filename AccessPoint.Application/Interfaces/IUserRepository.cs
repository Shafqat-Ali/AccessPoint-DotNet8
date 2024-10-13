using AccessPoint.Domain.Entities;

namespace AccessPoint.Application.Interfaces
{
    public interface IUserRepository : IRepository<Users>
    {
        Task<Users> GetUserByUsername(string username);
        Task<bool> IfUserExistsWithUserName(string username);
    }
}
