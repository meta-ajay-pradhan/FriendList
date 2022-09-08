using System.ComponentModel.DataAnnotations;

namespace FriendList.Models;
public class Friend
{

    [Display(Name = "Friend Id")]
    public int FriendId { get; set; }

    [Display(Name = "Friend Name")]
    [StringLength(20, MinimumLength = 3)]
    [Required]
    public string? FriendName { get; set; }

    public string? OwnerId { get; set; }

    [StringLength(50, MinimumLength = 3)]
    [Required]
    public string? Place { get; set; }
}