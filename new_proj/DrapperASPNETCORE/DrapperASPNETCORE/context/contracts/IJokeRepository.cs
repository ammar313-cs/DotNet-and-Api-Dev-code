using System.Collections.Generic;
using System.Threading.Tasks;
using DrapperASPNETCORE.Models;

namespace DrapperASPNETCORE.Contracts
{
    public interface IJokeRepository


    {

        public void loadjson();
        public bool savejson();
        public Task<IEnumerable<joke>> GetAllJokes();
        public Task<joke> GetJokeById(int id);
        public Task<joke> AddJoke(NewJoke joke);
        public Task UpdateJoke(int id, JokeUpdate joke);
        
    }
}