using System;


namespace JsonCRUD
{
    public class Crm
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } =   String.Empty;
        public string LastName { get; set; } = string.Empty;

        public string? CellPhoneNum { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public DateTime? LastTouchDate { get; set; }
    }

}
