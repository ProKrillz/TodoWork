using System.ComponentModel.DataAnnotations;

namespace TodoWork.BLL.DTOModels
{
    public class DTOUser
    {
        public Guid Id { get; set; }
        [Display(Name = "Navn")]
        public string? Name { get; set; }
        [EmailAddress, Display(Name = "Email")]
        public string? Email { get; set; }
        [Display(Name = "Adgangskode")]
        public string? Password { get; set; }
        public List<DTOTodo>? Todos { get; set; } = new();
        public List<DTOTodo> CompletedTodos { get; set; } = new();
    }
}

