using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dtos
{
    public class DInputCreateGenre
    {
        public required string Name { get; set; } = string.Empty;
    }
}