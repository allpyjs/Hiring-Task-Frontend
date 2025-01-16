using System.ComponentModel.DataAnnotations;

public class TodoItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    public string Description { get; set; }

    public bool Status { get; set; }

    public DateTime DueDate { get; set; }

    public string UserId { get; set; }
}
