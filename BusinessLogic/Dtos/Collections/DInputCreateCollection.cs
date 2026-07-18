using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dtos
{
    public class DInputCreateCollection
    {
        public required string Name { get; set; } = string.Empty;

        public bool Published { get; set; } = false;

        public List<int> MovieIds { get; set; } = [];
    }
}