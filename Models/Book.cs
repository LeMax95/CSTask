using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8.Models
{
    public class Book
    {
       
            public int Id { get; set; }

            public string Title { get; set; }
            public string Author { get; set; }
            public string Genre { get; set; }
            public int PageNr { get; set; }
            public int NrStock { get; set; }
            public Book(int id, string title, string author, string genre, int pageNr, int nrStock)
            {
                Id = id;
                Title = title;
                Author = author;
                Genre = genre;
                PageNr = pageNr;
                NrStock = nrStock;
            }

        }
    
}
