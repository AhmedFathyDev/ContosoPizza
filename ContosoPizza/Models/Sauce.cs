using System.ComponentModel.DataAnnotations;

namespace ContosoPizza.Models;

public class Sauce
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }
}