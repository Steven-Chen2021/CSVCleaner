using Microsoft.AspNetCore.Mvc;

namespace CSVCleanApiProject.Controllers;

[ApiController]
[Route("api/csv")]
public class CsvController : ControllerBase
{
    [HttpGet("test")]
    public IActionResult TestConnection()
    {
        return Ok(new { message = "CSV Cleanup API is alive and running!" });
    }

    [HttpPost("clean")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CleanCsvFile([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded or the file is empty.");
        }

        using var stream = file.OpenReadStream();
        using var reader = new StreamReader(stream);
        var content = await reader.ReadToEndAsync();

        var response = new
        {
            originalRowCount = 150,
            cleanedRowCount = 147,
            standardizedHeaders = new[] { "CustomerID", "Name", "DateOfBirth", "Email" },
            corrections = new
            {
                blankRowsRemoved = 3,
                invalidEmailsCorrected = 2,
                extraWhitespaceTrimmed = 147,
                dateFormatStandardized = "yyyy-MM-dd"
            },
            downloadUrl = "https://yourapi.com/downloads/cleaned-abc123.csv"
        };

        return Ok(response);
    }
}