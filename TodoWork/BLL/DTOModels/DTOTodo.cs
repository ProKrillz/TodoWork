using System.ComponentModel.DataAnnotations;

namespace TodoWork.BLL.DTOModels;
public class DTOTodo
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    [Display(Name = "Titel"), MaxLength(30), Required]
    public string? Title { get; set; }
    [Display(Name = "Beskrivelse"), MaxLength(250), Required]
    public string? Description { get; set; }
    [Display(Name = "Prioritet")]
    public Priority? TaskPriority { get; set; }
    [Display(Name = "Oprettet")]
    public DateTime? Created { get; set; }
    [Display(Name = "Færdig")]
    public DateTime? Completed { get; set; }
    public enum Priority
    {
        High = 1,
        Medium = 2,
        Low = 3,
    }
    public TimeSpan GetUsedTime()
    {
        if (Completed != null && Created != null)
            return (Completed - Created).Value;

        return TimeSpan.Zero;
    }
}
