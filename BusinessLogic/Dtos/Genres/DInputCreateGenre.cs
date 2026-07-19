using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dtos.Genres;

public class DInputCreateGenre
{
    [Required] public string Name { get; set; } = string.Empty;
}