using System.ComponentModel.DataAnnotations;
public class UserViewModel
{

    [DataType(DataType.EmailAddress)]
    [Required]
    public string? Email;

    [Required]
    public bool EmailConfirmed { get; set; }

    [Required]
    public string? Id { get; set; }

    [DataType(DataType.PhoneNumber)]
    [Required]
    public string? PhoneNumber { get; set; }

    [Required]
    public bool PhoneNumberConfirmed {get; set; }
}