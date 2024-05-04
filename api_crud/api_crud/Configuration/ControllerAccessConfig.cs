using System.Collections.Generic;

namespace api_crud.Configuration
{
    public class ControllerAccessConfig
    {
        public Dictionary<string, Dictionary<string, List<string>>>? ControllerRoles { get; set; }
    }
}
