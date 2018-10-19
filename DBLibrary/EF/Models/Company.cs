using System.Collections.Generic;

namespace DBLibrary.EF.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Phone> Phone { get; set; }
        public Company()
        {
            Phone = new List<Phone>();
        }
    }
}
