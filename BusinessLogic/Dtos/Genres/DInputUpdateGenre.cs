using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dtos.Genres;

public class DInputUpdateGenre
{
    [Required]
    public string Name { get; set; } = string.Empty;
}
