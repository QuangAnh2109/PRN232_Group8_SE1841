namespace Api.DTO
{
    public class ClassResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CenterId { get; set; }
        public string CenterName { get; set; } = null!;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

