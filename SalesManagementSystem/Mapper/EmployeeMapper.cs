using SalesManagementSystem.Dtos.EmployeeDtos;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Mapper
{
    public static class EmployeeMapper
    {
        public static GetEmployeeDto ToGetAllEmployeeDto(this Employee employee)
        {
            return new GetEmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                SupervisorId = employee.SupervisorId,
                BranchId = employee.BranchId
            };
        }

        public static DetailedInfoEmployeeDto ToGetByIdDetailedInfoEmployeeDto(this Employee employee)
        {
            return new DetailedInfoEmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                BirthDate = employee.BirthDay,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                Sex = employee.Sex,
                SuperId = employee.SupervisorId,
            };
        }

        public static ShortInfoEmployeeDto ToShortInfoEmployeeDto(this Employee employee)
        {
            return new ShortInfoEmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName
            };
        }

        public static Employee ToEmployeeFromCreateEmployeeDto(this CreateEmployeeDto employeeDto)
        {
            return new Employee
            {
                BirthDay = employeeDto.BirthDate,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Salary = employeeDto.Salary,
                Sex = employeeDto.Sex,
                SupervisorId = employeeDto.SuperId,
                BranchId = employeeDto.BranchId
            };
        }

        public static void ToEmployeeFromUpdateEmployeeDto(this Employee employee, UpdateEmployeeDto employeeDto)
        {
            employee.BirthDay = employeeDto.BirthDate;
            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.Salary = employeeDto.Salary;
            employee.Sex = employeeDto.Sex;
            employee.SupervisorId = employeeDto.SuperId;
            employee.BranchId = employeeDto.BranchId;

            return;
        }
    }
}
