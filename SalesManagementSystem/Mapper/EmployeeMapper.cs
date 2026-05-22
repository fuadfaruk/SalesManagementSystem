using SalesManagementSystem.Dtos.EmployeeDtos;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Mapper
{
    public static class EmployeeMapper
    {
        public static GetAllEmployeeDto ToGetAllEmployeeDto(this Employee e)
        {
            return new GetAllEmployeeDto
            {
                EmployeeId = e.EmployeeId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                SupervisorId = e.SupervisorId,
                BranchId = e.BranchId
            };
        }

        public static GetByIdDetailedInfoEmployeeDto ToGetByIdDetailedInfoEmployeeDto(this Employee employee)
        {
            return new GetByIdDetailedInfoEmployeeDto
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
