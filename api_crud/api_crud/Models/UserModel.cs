using System;

namespace api_crud.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }

        public UserModel(int id, string name, string role, string password)
        {
            this.ID = id;
            this.Name = name;
            this.Role = role;
            this.Password = password;
        }
    }
}
