using Dapper;
using JokesApp.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
//using JokesApp.contracts;


namespace YourProject.Repositories
{
    public class JokeRepository : Contracts.IJokeRepository
    {
        private readonly IDbConnection _connection;

        public JokeRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<joke>> GetAllJokes()
        {
            string query = "SELECT * FROM Jokes";
            return await _connection.QueryAsync<joke>(query);
        }

        public async Task<joke> GetJokeById(int id)
        {
            string query = "SELECT * FROM Jokes WHERE Id = @Id";
            return await _connection.QueryFirstOrDefaultAsync<joke>(query, new { Id = id });
        }

        public async Task<int> AddJoke(joke joke)
        {
            string query = "INSERT INTO Jokes (JokeQuestion, JokeAnswer) VALUES (@JokeQuestion, @JokeAnswer); SELECT CAST(SCOPE_IDENTITY() as int)";
            return await _connection.ExecuteScalarAsync<int>(query, joke);
        }

        public async Task<bool> UpdateJoke(joke joke)
        {
            string query = "UPDATE Jokes SET JokeQuestion = @JokeQuestion, JokeAnswer = @JokeAnswer WHERE Id = @Id";
            int rowsAffected = await _connection.ExecuteAsync(query, joke);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteJoke(int id)
        {
            string query = "DELETE FROM Jokes WHERE Id = @Id";
            int rowsAffected = await _connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }
}