using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dtos.Collections;

public class DInputUpdateCollection
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public bool Published { get; set; }

    public List<int> MovieIds { get; set; } = new();
}
