using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dtos
{
    public class DInputCastCrewCredit
    {
        public required string Name { get; set; } = string.Empty;

        public required string Role { get; set; } = string.Empty;

        public string? StageName { get; set; }
    }
}