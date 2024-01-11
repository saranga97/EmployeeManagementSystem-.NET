using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

// Controllers/EmployeesController.cs

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly DbConnector dbConnector;
    private readonly string connectionString = "your_connection_string_here";

    public EmployeesController()
    {
        this.dbConnector = new DbConnector(connectionString);
    }

    [HttpGet]
    public IActionResult GetAllEmployees()
    {
        string query = "SELECT * FROM Employees";
        DataTable employees = dbConnector.ExecuteQuery(query);

        return Ok(employees);
    }

    [HttpGet("{id}")]
    public IActionResult GetEmployeeById(int id)
    {
        string query = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";
        SqlParameter[] parameters = { new SqlParameter("@EmployeeID", id) };

        DataTable employee = dbConnector.ExecuteQuery(query, parameters);

        if (employee.Rows.Count == 0)
        {
            return NotFound("Employee not found");
        }

        return Ok(employee.Rows[0]);
    }

    [HttpPost]
    public IActionResult CreateEmployee([FromBody] Employee employee)
    {
        string query = "INSERT INTO Employees (FirstName, LastName, Email, DateOfBirth, Age, Salary, DepartmentID) " +
                       "VALUES (@FirstName, @LastName, @Email, @DateOfBirth, @Age, @Salary, @DepartmentID)";

        SqlParameter[] parameters =
        {
            new SqlParameter("@FirstName", employee.FirstName),
            new SqlParameter("@LastName", employee.LastName),
            new SqlParameter("@Email", employee.Email),
            new SqlParameter("@DateOfBirth", employee.DateOfBirth),
            new SqlParameter("@Age", employee.Age),
            new SqlParameter("@Salary", employee.Salary),
            new SqlParameter("@DepartmentID", employee.DepartmentID)
        };

        int rowsAffected = dbConnector.ExecuteNonQuery(query, parameters);

        if (rowsAffected > 0)
        {
            return Ok("Employee created successfully");
        }

        return BadRequest("Failed to create employee");
    }

    [HttpPut("{id}")]
    public IActionResult UpdateEmployee(int id, [FromBody] Employee employee)
    {
        string query = "UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, " +
                       "Email = @Email, DateOfBirth = @DateOfBirth, Age = @Age, Salary = @Salary, " +
                       "DepartmentID = @DepartmentID WHERE EmployeeID = @EmployeeID";

        SqlParameter[] parameters =
        {
            new SqlParameter("@EmployeeID", id),
            new SqlParameter("@FirstName", employee.FirstName),
            new SqlParameter("@LastName", employee.LastName),
            new SqlParameter("@Email", employee.Email),
            new SqlParameter("@DateOfBirth", employee.DateOfBirth),
            new SqlParameter("@Age", employee.Age),
            new SqlParameter("@Salary", employee.Salary),
            new SqlParameter("@DepartmentID", employee.DepartmentID)
        };

        int rowsAffected = dbConnector.ExecuteNonQuery(query, parameters);

        if (rowsAffected > 0)
        {
            return Ok("Employee updated successfully");
        }

        return NotFound("Employee not found");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEmployee(int id)
    {
        string query = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";
        SqlParameter[] parameters = { new SqlParameter("@EmployeeID", id) };

        int rowsAffected = dbConnector.ExecuteNonQuery(query, parameters);

        if (rowsAffected > 0)
        {
            return Ok("Employee deleted successfully");
        }

        return NotFound("Employee not found");
    }
}
