using api_crud.Models;
using jsonCrud.Models;

namespace jsonCrud.Repository
{
    public interface IUDbRepo
    {
        Task<UserModel> GetUserByUsername(string username);


    }
}
