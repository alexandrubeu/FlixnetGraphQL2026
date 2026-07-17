using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dtos
{
    public class DInputUpdateGenre
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}