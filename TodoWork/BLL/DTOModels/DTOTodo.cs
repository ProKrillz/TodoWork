using System.ComponentModel.DataAnnotations;

namespace TodoWork.BLL.DTOModels;
public class DTOTodo
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string? Title { get; set; }
    public string? Description { get; set; }
    [Display(Name = "Priority")]
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
