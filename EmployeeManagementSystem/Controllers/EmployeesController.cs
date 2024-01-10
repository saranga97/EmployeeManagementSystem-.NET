using Microsoft.AspNetCore.Mvc;

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
    
}