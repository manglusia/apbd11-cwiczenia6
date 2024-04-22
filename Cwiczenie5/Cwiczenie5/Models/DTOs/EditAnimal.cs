using System.ComponentModel.DataAnnotations;
namespace Cwiczenie5.Models.DTOs;

public class EditAnimal
{
    [MaxLength(200)]
    public string Name { get; set; }
    
    [MaxLength(200)]
    public string Description { get; set;}
    
    [MaxLength(200)]
    public string Category { get; set; }
    
    [MaxLength(200)]
    public string Area { get; set; }
}