using System.ComponentModel.DataAnnotations;
namespace SparxNexora.Models;
public class ContactRequest
{
    [Required, StringLength(100)] public string Name { get; set; } = "";
    [Required, EmailAddress, StringLength(150)] public string Email { get; set; } = "";
    [Phone, StringLength(30)] public string Phone { get; set; } = "";
    [StringLength(120)] public string Company { get; set; } = "";
    [StringLength(100)] public string Service { get; set; } = "";
    [StringLength(100)] public string ProjectType { get; set; } = "";
    [StringLength(100)] public string Budget { get; set; } = "";
    [StringLength(100)] public string Timeline { get; set; } = "";
    [Required, StringLength(4000)] public string Message { get; set; } = "";
}
