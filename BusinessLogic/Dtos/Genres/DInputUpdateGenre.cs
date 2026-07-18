using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dtos
{
    public class DInputUpdateGenre
    {
        public required string Name { get; set; } = string.Empty;
    }
}