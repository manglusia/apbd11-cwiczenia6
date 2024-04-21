using Cwiczenie5.Models;
using Cwiczenie5.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Cwiczenie5.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AnimalsController:ControllerBase
{
    private readonly IConfiguration _configuration;
    public AnimalsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    [HttpGet]
    public IActionResult GetAnimals()
    {
        
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();

        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT * FROM Animal";

        var reader = command.ExecuteReader();

        List<Animal> animals = new List<Animal>();

        int idAnimalOrdinal = reader.GetOrdinal("IdAnimal");
        int nameAnimalOrdinal = reader.GetOrdinal("Name");
        int descriptionAnimalOrdinal = reader.GetOrdinal("Description");
        int categoryAnimalOrdinal = reader.GetOrdinal("Category");
        int areaAnimalOrdinal = reader.GetOrdinal("Area");
        
        while (reader.Read())
        {
            animals.Add(new Animal()
            {
                IdAnimal = reader.GetInt32(idAnimalOrdinal),
                Name = reader.GetString(nameAnimalOrdinal),
                Description = reader.GetString(descriptionAnimalOrdinal),
                Category = reader.GetString(categoryAnimalOrdinal),
                Area = reader.GetString(areaAnimalOrdinal)
            });
        }

        return Ok(animals);
    }

    [HttpPost]
    public IActionResult AddAnimal(AddAnimal addAnimal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();

        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "INSERT INTO Animal VALUES (@animalName,@animalDescription,@animalCategory,@animalArea)";
        command.Parameters.AddWithValue("@animalName", addAnimal.Name);
        command.Parameters.AddWithValue("@animalDescription", addAnimal.Description);
        command.Parameters.AddWithValue("@animalCategory", addAnimal.Category);
        command.Parameters.AddWithValue("@animalArea", addAnimal.Area);

        
        command.ExecuteNonQuery();
        
        //_repository.AddAnimal(addAnimal)
        
        return Created("", null);
    }

}