namespace SalesManagementSystem.Dtos.EmployeeDtos
{
    public class ShortInfoEmployeeDto
    {
        public int EmployeeId { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
