using Microsoft.AspNetCore.Mvc;
using System.Data;

[Route("api/[controller]")]
[ApiController]
public class DepartmentsController : ControllerBase
{
    private readonly DbConnector dbConnector;
    private readonly string connectionString = "your_connection_string_here";

    public DepartmentsController()
    {
        this.dbConnector = new DbConnector(connectionString);
    }

    [HttpGet]
    public IActionResult GetAllDepartments()
    {
        string query = "SELECT * FROM Departments";
        DataTable departments = dbConnector.ExecuteQuery(query);

        return Ok(departments);
    }
}