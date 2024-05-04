using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCrudJson.Models
{
    public class PersonModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}