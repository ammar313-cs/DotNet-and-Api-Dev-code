using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using jsonCrud.Models;
using Newtonsoft.Json;
using jsonCrud.Repository;
using api_crud.Models;
using Microsoft.EntityFrameworkCore;

namespace jsonCrud.Data
{
    public class UserDBHandler : IUDbRepo
    {
        public static string UserFile = "/Volumes/OVERLORD1 3/dotnet_files/api_crud/api_crud/App_Data/Users.json";
        

        private async Task<List<UserModel>> GetUsersFromFile()
        {
            List<UserModel> users = new List<UserModel>();

            if (File.Exists(UserFile))
            {
                try
                {
                    string content = await File.ReadAllTextAsync(UserFile);
                    users = JsonConvert.DeserializeObject<List<UserModel>>(content);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Error reading the file: {ex.Message}");
                }
            }
            else
            {
                // Create an empty file if it doesn't exist
                File.Create(UserFile).Close();
                File.WriteAllText(UserFile, "[]");
            }

            return users;
        }

        public async Task<UserModel> GetUserByUsername(string username)
        {
            List<UserModel> users = await GetUsersFromFile();
            // UserModel dta = users.FirstOrDefault(u => u.Name == username);

            //return dta.Role;
            return users.FirstOrDefault(u => u.Name == username);
        }
    }
}
