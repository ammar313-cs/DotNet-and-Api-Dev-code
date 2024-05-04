using System.Collections.Generic;
using System.Threading.Tasks;
using JokesApp.Models;

namespace YourProject.Contracts
{
    public interface IJokeRepository
    {
        Task<IEnumerable<joke>> GetAllJokes();
        Task<joke> GetJokeById(int id);
        Task<int> AddJoke(joke joke);
        Task<bool> UpdateJoke(joke joke);
        Task<bool> DeleteJoke(int id);
    }
}