using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8.Models
{
    public class BorrowedBooks
    {


        public int BorrowedBookID { get; set; }
        public int ClientID { get; set; }
        public int BookID { get; set; }
        public string BorrowDate { get; set; }

        public BorrowedBooks(int borrowedBookID, int clientid, int bookid, string borrowdate)
        {
            BorrowedBookID = borrowedBookID;
            ClientID = clientid;
            BookID = bookid;
            BorrowDate = borrowdate;
        }
    }
}
