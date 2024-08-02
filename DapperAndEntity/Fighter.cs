using System.ComponentModel.DataAnnotations;

namespace DapperAndEntity
{
    public class Fighter
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Division { get; set; }
    }
}
