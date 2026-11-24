using System.ComponentModel.DataAnnotations;

namespace ConectionDataCrud.Models;
public class CreateUserViewModel
{
    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;

    public bool Activo { get; set; } = true;
}