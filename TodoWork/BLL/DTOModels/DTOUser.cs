using System.ComponentModel.DataAnnotations;

namespace TodoWork.BLL.DTOModels
{
    public class DTOUser
    {
        public Guid users_id { get; set; }
        [Display(Name = "Navn")]
        public string? users_name { get; set; }
        [EmailAddress, Display(Name = "Email")]
        public string? users_email { get; set; }
        [Display(Name = "Adgangskode")]
        public string? users_password { get; set; }
        public List<DTOTodo>? Todos { get; set; } = new();
    }
}

