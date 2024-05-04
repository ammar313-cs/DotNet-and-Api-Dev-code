
using jsonCrud.Models;

namespace jsonCrud.Repository
{
    public interface IDbRepo
    {
        Task<List<PersonModel>> GetPersonDetails();
        Task<bool> InsertPerson(PersonModel personModel);
        Task<bool> UpdatePerson(PersonModel personModel);
        Task<bool> DeletePerson(int id);
    }
}