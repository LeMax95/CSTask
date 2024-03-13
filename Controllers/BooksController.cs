using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.IO;


namespace CSTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBooks()
        {
            try
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "BorrowedBooks.json");
                string jsonContent = System.IO.File.ReadAllText(filePath);
           
                return Ok(jsonContent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

