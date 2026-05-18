using server.Models;

namespace server.Data
{
    public interface IRepo
    {
        Task<List<User>> GetUsers();

        Task AddUser(User user);
    }
}
