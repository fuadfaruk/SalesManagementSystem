namespace SalesManagementSystem.Dtos.WorksWithDtos
{
    public class TransactionRequestDto
    {
        public required string IdempotencyKey { get; set; }
        public required int EmployeeId { get; set; }
        public required int ClientId { get; set; }
        public required decimal TransactionAmount { get; set; }
    }
}
