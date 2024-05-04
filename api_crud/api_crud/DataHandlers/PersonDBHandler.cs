
using jsonCrud.Models;
using Newtonsoft.Json;

using Microsoft.EntityFrameworkCore;
using jsonCrud.Repository;

namespace jsonCrud.Data
{ 
    public class PersonDBHandler : IDbRepo
    {
        public static string PersonFile = "/Volumes/OVERLORD1 3/dotnet_files/jsonCrud/jsonCrud/App_Data/Clients.json";
        public DbSet<PersonModel> People { get; set; }
        


        public async Task<List<PersonModel>> GetPersonDetails()
        {
            List<PersonModel> people = new List<PersonModel>();

            if (File.Exists(PersonFile))
            {
                string content = await File.ReadAllTextAsync(PersonFile);
                people = JsonConvert.DeserializeObject<List<PersonModel>>(content);
            }
            else
            {
                // Create an empty file if it doesn't exist
                File.Create(PersonFile).Close();
                File.WriteAllText(PersonFile, "[]");
            }

            return people;
        }

        public  async Task<bool> InsertPerson(PersonModel iList)
        {
            var PersonFile = PersonDBHandler.PersonFile;
            var PeopleData = System.IO.File.ReadAllText(PersonFile);
            List<PersonModel> PersonList = new List<PersonModel>();

            PersonList = JsonConvert.DeserializeObject<List<PersonModel>>(PeopleData);

            if (PersonList == null)
            {
                PersonList = new List<PersonModel>();
            }
            int maxId = PersonList.Any() ? PersonList.Max(p => p.ID) : 0;
            int newId = maxId + 1;
            iList.ID = newId;

            PersonList.Add(iList);

            await System.IO.File.WriteAllTextAsync(PersonFile, JsonConvert.SerializeObject(PersonList));

            return true;
        }

        public async Task<bool> UpdatePerson(PersonModel iList)
        {
            // Get all of the clients
            PersonDBHandler PersonHandler = new PersonDBHandler();
            var people = await PersonHandler.GetPersonDetails();

            foreach (PersonModel person in people)
            {
                if (person.ID == iList.ID)
                {
                    person.Name = iList.Name;
                    person.Address = iList.Address;
                    person.Phone = iList.Phone;
                    person.Email = iList.Email;
                    break;
                }
            }

            await System.IO.File.WriteAllTextAsync(PersonDBHandler.PersonFile, JsonConvert.SerializeObject(people));

            return true;
        }

        public async Task<bool> DeletePerson(int id)
        {
            PersonDBHandler PersonHandler = new PersonDBHandler();
            var People = await PersonHandler.GetPersonDetails();

            foreach (PersonModel person in People)
            {
                if (person.ID == id)
                {
                    var index = People.IndexOf(person);
                    People.RemoveAt(index);
                    await System.IO.File.WriteAllTextAsync(PersonDBHandler.PersonFile, JsonConvert.SerializeObject(People));
                    break;
                }
            }

            return true;
        }

        
    }
}