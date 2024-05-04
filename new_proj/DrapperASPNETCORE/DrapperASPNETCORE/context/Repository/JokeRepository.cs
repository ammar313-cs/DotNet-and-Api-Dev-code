using Dapper;
using DrapperASPNETCORE.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DrapperASPNETCORE.Contracts;

using Newtonsoft.Json;



namespace DrapperASPNETCORE.Repositories
{
    public class JokeRepository : IJokeRepository
    {
        private readonly DapperContext _context;

        public List<joke>? myJokes = null;

        public JokeRepository(DapperContext context)
        {
            _context = context;
            loadjson();

        }

        public void loadjson()
        {

            string file_name = @"overlord1: \dotnet_files\DrapperASPNETCORE\DrapperASPNETCORE\data\mydata.json";
            if(File.Exists(file_name) == true){
                var list = JsonConvert.DeserializeObject<List<joke>>(File.ReadAllText(file_name));

                if(list != null)
                {
                    myJokes = list;
                }
                else
                {
                    myJokes = new List<joke>();
                }



            }
            else
            {
                myJokes = new List<joke>();
            }

        }

        public bool savejson()
        {

            string file_name = @"overlord1: \dotnet_files\DrapperASPNETCORE\DrapperASPNETCORE\data\mydata.json";

            var json = JsonConvert.SerializeObject(myJokes);

            if (File.Exists(file_name) == true)
            {
                System.IO.File.Delete(file_name);
            }
           System.IO.File.WriteAllBytes(file_name, json);

            return (true);

        }

    


    public async Task<IEnumerable<joke>> GetAllJokes()
        {
            var query = "SELECT * FROM Jokes";

            using (var connection = _context.CreateConnection())
            {
                var jokes1 = await connection.QueryAsync<joke>(query);
                return jokes1.ToList();

            }
        }

        public async Task<joke> GetJokeById(int id)
        {
            string query = "SELECT * FROM Jokes WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var joke1 = await connection.QuerySingleOrDefaultAsync<joke>(query, new { id});
                return joke1;
            }
        }

        public async Task<joke>AddJoke(NewJoke joke)
        {
            var query = "INSERT INTO Jokes (Id , JokeQuestion, JokeAnswer) VALUES (@Id, @JokeAnswer, @JokeQuestion)" +
                "SELECT CAST(SCOPE_IDENTITY() AS int)";


            var parameters = new DynamicParameters();

            parameters.Add("Id", joke.newId, DbType.Int32);
            parameters.Add("JokeAnswer", joke.newJokeA, DbType.String);
            parameters.Add("JokeQuestion", joke.newJokeQ, DbType.String);

            using (var connection = _context.CreateConnection())
            {
               var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdJoke = new joke
                {
                    Id = id,
                    JokeAnswer = joke.newJokeA,
                    JokeQuestion = joke.newJokeQ

                };
                return createdJoke;
            }

        }

        public async Task UpdateJoke(int id, JokeUpdate joke)
        {
            var query = "UPDATE Jokes SET   JokeQuestion = @jokeQ, JokeAnswer = @jokeA WHERE Id = @Id  ";

            var parameters = new DynamicParameters();

            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("JokeQ", joke.uJokeA, DbType.String);
            parameters.Add("JokeA", joke.uJokeQ, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);

            }

        }

        

        
    }
}

