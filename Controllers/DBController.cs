using ConsoleApp8.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Numerics;
using System.Xml.Linq;

namespace CSTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DBController : ControllerBase
    {
        private readonly DB _db;
        public DBController(DB db)
        {
            _db = db;
        }

        [HttpGet("id/{id}")]
        public IActionResult GetById(int id)
        {
            var book = _db.SelectBook(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpGet("name/{name}")]
        public IActionResult GetByName(string name)
        {
            var book = _db.SearchBook(name);

            if (book == null)
            {

                return NotFound();


            }
            return Ok(book);
        }

        [HttpGet("category/{category}")]
        public IActionResult Categorize(string category)
        {
            List<Book> books = _db.CategorizeBook(category);
            if (books == null)
            {
                return NotFound();
            }
            return Ok(books);

        }

        [HttpGet("sortbyTitle")]
        public IActionResult SortByTitle()
        {
            List<Book>books = _db.SortByTitle();
         
            if (books==null)
            {
                return NotFound();
            }
            return Ok(books);
        }

        [HttpGet("sortbyPagNr")]

        public IActionResult SortByPagNr()
        {
            List<Book> books = _db.SortByPageNr();
            if(books==null)
            {
                return NotFound();
            }
            return Ok(books);
        }

        [HttpDelete("delB/{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                _db.deleteBook(id); 
                                   
                return Ok(new { message = "Book deleted successfully" });
            }
            catch (Exception ex)
            {
                
                return NotFound(new { message = "Book not found" });
            }
        }

        [HttpPost("addBook/{title}/{author}/{genre}/{PageNr}/{NrStock}")]
        public IActionResult PostBook(string title,string author,string genre,int PageNr,int NrStock)
        {
            try
            {
                Random random = new Random();
                var newBook = new Book(random.Next(), title, author, genre, PageNr, NrStock);
                _db.AddBook(newBook);
                return Ok(new { message = "Book added successfully" });
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }

            
        }

        [HttpPut("updateBook/{id}/{title}/{author}/{genre}/{PageNr}/{NrStock}")]
        public IActionResult UpddateBook(int id,string title, string author, string genre, int PageNr, int NrStock)
        {
            try
            {
                Random random = new Random();
                var newBook = new Book(id, title, author, genre, PageNr, NrStock);
                _db.updateBook(newBook);
                return Ok(new { message = "Book updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }


        [HttpGet("selectClient/{id}")]
        public IActionResult SelectClient(int id) 
        {
            var client = _db.SelectClient(id);
            if (client == null)
            {
                return NotFound(); 
            }
            return Ok(client);
        }

        [HttpGet("selectBorrow/{id}")]
        public IActionResult SelectBorrow(int id)
        {
            var borrow = _db.SelectBorrow(id);
            if (borrow == null)
            {
                return NotFound();
            }
            return Ok(borrow);
        }

        [HttpPost("registerClient/{name}/{registerDate}/{address}/{phone}")]
        public IActionResult RegisterClient(string name, string registerDate, string address,string phone) 
        {
            Random random = new Random();
            var newClient = new Client(random.Next(), name, registerDate, address, phone);
            try
            {
                _db.registerClient(newClient);
                return Ok(newClient);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);  
            }
            
            
        }


        [HttpPut("updateClient/{id}/{name}/{registerDate}/{address}/{phone}")]
        public IActionResult UpdateClient(int id,string name, string registerDate, string address, string phone)
        {
           
            var newClient = new Client(id, name, registerDate, address, phone);
            try
            {
                _db.updateClient(newClient);
                return Ok(newClient);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpDelete("deleteClient/{id}")]
        public IActionResult DeleteClient(int id)
        {
            try
            {
                _db.removeClient(id);
                return Ok(new { message = "Client data removed successfully" });
            }

            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        [HttpPost("addBorrowedBook/{bookId}/{clientId}/{date}")]
        public IActionResult  AddborrowedBook(int bookId,int clientId,string date)
        {

            try
            {
                Random random = new Random();
                var obj = new BorrowedBooks (random.Next(), bookId, clientId, date );
               
                var bookToBorrow = _db.SelectBook(bookId);
                if (bookToBorrow == null || bookToBorrow.NrStock <= 0)
                {
                    return BadRequest("The book is not available for borrowing.");
                }

                
                bookToBorrow.NrStock--;
                _db.updateBook(bookToBorrow);

              
                _db.AddborrowedBook(obj);

                return Ok(new { message = "Book added to the borrowlist successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error borrowing book: {ex.Message}");
            }
        }

        [HttpDelete("deleteBorrowedB/{id}/{bookid}")]
        public IActionResult deleteBorrowedB(int id,int bookid)
        {
            try
            {

                _db.RemoveBorrowedBook(id);
                var bookToReturn = _db.SelectBook(bookid);
                bookToReturn.NrStock++;
                _db.updateBook(bookToReturn);
                return Ok(new { message = "Book removed from borrowlist successfully" });
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("report")]
        public IActionResult GenerateReport()
        {
            try
            {
                var rep = _db.GenerateReport();
                if (rep == null)
                {
                    return NotFound();
                }
                return Ok(rep);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
           
        }


    }

}

