using System.ComponentModel.DataAnnotations;

namespace UMSProject.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; } = "";

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = "";

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; } = "";
    }
}