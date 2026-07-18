using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dtos
{
    public class DInputUpdateCollection
    {
        public required string Name { get; set; } = string.Empty;

        public bool Published { get; set; }

        public List<int> MovieIds { get; set; } = [];
    }
}