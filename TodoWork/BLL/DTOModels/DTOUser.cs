using System.ComponentModel.DataAnnotations;
using TodoWork.Domain.Entities;

namespace TodoWork.BLL.DTOModels
{
    public class DTOUser
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Password { get; set; }
        public List<Todo>? Todos { get; set; }
    }
}

