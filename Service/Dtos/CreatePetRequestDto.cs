using System.ComponentModel.DataAnnotations;

namespace Service;

public class CreatePetRequestDto
{
    public CreatePetRequestDto(string name)
    {
        Name = name;
    }

    [MinLength(3)]
    public string Name { get; set; }
}