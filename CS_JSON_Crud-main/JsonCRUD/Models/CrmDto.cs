using System;

namespace JsonCRUD
{
    public class CrmDto
    {
        
        public Guid Id { get; set; }

        public string FirstName { get; set; } = String.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? CellPhoneNum { get; set; } = string.Empty;
        public string? DisplayCreateDate { get; set; }
        public int   SelectedIndex { get; set; }
    }

}
