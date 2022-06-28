using System.ComponentModel.DataAnnotations;

namespace APIwithGraphQL.Models
{
    public class Platform
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string LicenseKey { get; set; }
        public ICollection<Command> Commands { get; set; } = new List<Command>();
    }
}
