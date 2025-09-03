using System.ComponentModel.DataAnnotations;
using Infrastructure.Postgres.Scaffolding;
using Service;

namespace API.Controllers;

public class PetService(MyDbContext ctx)
{
    public async Task<Pet> CreatePet(CreatePetRequestDto dto)
    {
        Validator.ValidateObject(dto, 
            new ValidationContext(dto),
            true);
        
        var pet = new Pet()
        {
            CreatedAt = DateTime.UtcNow,
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name
        };
        ctx.Pets.Add(pet);
        await ctx.SaveChangesAsync();
        return pet;
    }
}