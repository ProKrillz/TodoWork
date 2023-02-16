using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using TodoWork.Domain.Entities;

namespace TodoWork.BLL.DTOModels;
public class DTOTodo
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    [Display(Name = "Priority")]
    public Priority? TaskPriority { get; set; }
    [Display(Name = "Completed")]
    public bool IsCompleted { get; set; }
}
