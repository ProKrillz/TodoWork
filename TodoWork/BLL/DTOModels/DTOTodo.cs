using System.ComponentModel.DataAnnotations;

namespace TodoWork.BLL.DTOModels;
public class DTOTodo
{
    public Guid Id { get; set; }
    [MaxLength(100)]
    public string? Title { get; set; }
    public string? Description { get; set; }
    [Display(Name = "prioritet")]
    public Priority? TaskPriority { get; set; }
    public DateTime Created { get; set; }
    [Display(Name = "Completed")]
    public DateTime Completed { get; set; }
    public enum Priority
    {
        High = 1,
        Medium = 2,
        Low = 3,
    }
}
