namespace Api.DTO;

public class ClassDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string CenterName { get; set; } = null!;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Status { get; set; } = null!;
}