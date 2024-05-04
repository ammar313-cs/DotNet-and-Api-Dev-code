using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using System.IO;

namespace MvcCrudJson.Models
{
    public class PersonDBHandler
    {
        public static string PersonFile = HttpContext.Current.Server.MapPath("~/App_Data/Clients.json");

        public List<PersonModel> GetPersonDetails()
        {
            List<PersonModel> people = new List<PersonModel>();
            if (File.Exists(PersonFile))
            {
                string content = File.ReadAllText(PersonFile);
                people = JsonConvert.DeserializeObject<List<PersonModel>>(content);
                return people;
            }
            else
            {
                File.Create(PersonFile).Close();
                File.WriteAllText(PersonFile, "[]");
                GetPersonDetails();
            }

            return people;
        }

        public bool InsertPerson(PersonModel iList)
        {
            var PersonFile = PersonDBHandler.PersonFile;
            var PeopleData = System.IO.File.ReadAllText(PersonFile);
            List<PersonModel> PersonList = new List<PersonModel>();

            PersonList = JsonConvert.DeserializeObject<List<PersonModel>>(PeopleData);

            if (PersonList == null)
            {
                PersonList = new List<PersonModel>();
            }

            PersonList.Add(iList);

            System.IO.File.WriteAllText(PersonFile, JsonConvert.SerializeObject(PersonList));

            return true;
        }

        public bool UpdatePerson(PersonModel iList)
        {
            // Get all of the clients
            PersonDBHandler PersonHandler = new PersonDBHandler();
            var people = PersonHandler.GetPersonDetails();

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

            System.IO.File.WriteAllText(PersonDBHandler.PersonFile, JsonConvert.SerializeObject(people));

            return true;
        }

        public bool DeletePerson(int id)
        {
            PersonDBHandler PersonHandler = new PersonDBHandler();
            var People = PersonHandler.GetPersonDetails();

            foreach (PersonModel person in People)
            {
                if (person.ID == id)
                {
                    var index = People.IndexOf(person);
                    People.RemoveAt(index);
                    System.IO.File.WriteAllText(PersonDBHandler.PersonFile, JsonConvert.SerializeObject(People));
                    break;
                }
            }

            return true;
        }
    }
}