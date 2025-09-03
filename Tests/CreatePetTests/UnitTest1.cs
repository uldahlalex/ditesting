using System.ComponentModel.DataAnnotations;
using API.Controllers;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.AspNetCore.Components;
using Service;

namespace Tests.CreatePetTests;

public class UnitTest1(PetService petService, MyDbContext ctx)
{
    
    //Happy path test
    [Fact]
    public async Task CreatePet_ShouldSuccessfullyCreatePet_WhenNoDataViolationsOccur()
    {
        //Arrange
        var myValidRequestDto = new CreatePetRequestDto(name: "Bob");
        Assert.Equal(ctx.Pets.Count(), 0);

        
        //Act
        var result = await petService.CreatePet(myValidRequestDto);
        
        //Assert
        Assert.Equal(result.Name, "Bob");
        Assert.Equal(ctx.Pets.Count(), 1);
    }
    
    //Sad path test
    [Theory]
    [InlineData("Bo")]
    [InlineData("B")]
    [InlineData("")]
    public async Task CreatePet_ShouldThrowException_WhenDataValidationViolationOccurs(string name)
    {
        await Assert.ThrowsAnyAsync<ValidationException>(
            async() => await petService.CreatePet(new CreatePetRequestDto(name)));

    }
    

}
