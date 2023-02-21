using System.ComponentModel.DataAnnotations;

namespace TodoWork.BLL.DTOModels;
public class DTOTodo
{
    public Guid Id { get; set; }
    [Display(Name = "Titel"), MaxLength(100)]
    public string? Title { get; set; }
    [Display(Name = "Beskrivelse")]
    public string? Description { get; set; }
    [Display(Name = "Prioritet")]
    public Priority? TaskPriority { get; set; }
    public DateTime Created { get; set; }
    [Display(Name = "Completed")]
    public DateTime? Completed { get; set; }
    public enum Priority
    {
        High = 1,
        Medium = 2,
        Low = 3,
    }
    public TimeSpan GetUsedTime()
    {
        return (this.Completed - this.Created).Value;
    }
}
