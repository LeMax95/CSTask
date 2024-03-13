using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp8.Models
{
    public class DB
    {

        public string BooksourceFile { get; set; }
        public string ClientsourceFile { get; set; }
        public string BorrowListsourceFile { get; set; }


        public DB(string BookSourceFile, string ClientSourceFile, string BorrowListsourceFile)
        {
            this.BooksourceFile = BookSourceFile;
            this.ClientsourceFile = ClientSourceFile;
            this.BorrowListsourceFile = BorrowListsourceFile;
            CreateFile();
        }
        public static void save(string obj, string sourceFile)
        {


            try
            {

                File.WriteAllText(sourceFile, obj);
            }


            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data {ex.Message}");
            }
        }

        public static List<object> load(string sourceFile)
        {
            try
            {
                string existingJson = File.ReadAllText(sourceFile);
                List<object> existingList = JsonSerializer.Deserialize<List<object>>(existingJson);
                return existingList;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data from file: {ex.Message}");
                throw;
            }
        }

        private void CreateFile()
        {

            try
            {
                List<string> paths = new List<string>();
                paths.Add(this.BooksourceFile);
                paths.Add(this.ClientsourceFile);
                paths.Add(this.BorrowListsourceFile);

                foreach (string path in paths)
                {
                    if (!File.Exists(path))
                    {

                        string templateJson = "[]";
                        File.WriteAllText(path, templateJson);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing database file: {ex.Message}");
            }
        }

        public static List<T> ConvertToThis<T>(List<object> Obj)
        {
            return Obj.Select(item => JsonSerializer.Deserialize<T>(item.ToString())).ToList();
        }

        public static List<T> SelectThis<T>(string sourcefile)
        {
            List<object> existingTList = load(sourcefile);
            List<T> Tlist = ConvertToThis<T>(existingTList);
            return Tlist;
        }

        public void AddBook(Book book)
        {

            try
            {
                List<Object> existingList = load(BooksourceFile);
                existingList.Add(book);
                string updatedJson = JsonSerializer.Serialize(existingList, new JsonSerializerOptions { WriteIndented = true });
                save(updatedJson, BooksourceFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding data to file: {ex.Message}");
            }

        }

        public Book SelectBook(int id)
        {
            List<Book> bookList = SelectThis<Book>(BooksourceFile);
            var selectedBook = bookList.FirstOrDefault(x => x.Id == id);
            return selectedBook;

        }
        public void deleteBook(int id)
        {
            List<Book> bookList = SelectThis<Book>(BooksourceFile);
            bookList.RemoveAll(x => x.Id == id);
            string updatedJson = JsonSerializer.Serialize(bookList, new JsonSerializerOptions { WriteIndented = true });
            save(updatedJson, BooksourceFile);

        }



        public void updateBook(Book book)
        {

            List<Book> bookList = SelectThis<Book>(BooksourceFile);
            bookList = bookList.Select(b => b.Id == book.Id ? book : b).ToList();
            string updatedJson = JsonSerializer.Serialize(bookList, new JsonSerializerOptions { WriteIndented = true });
            save(updatedJson, BooksourceFile);
        }

        public Book SearchBook(string name)
        {
            List<Book> bookList = SelectThis<Book>(BooksourceFile);
            Book foundBook = bookList.FirstOrDefault(b => b.Title.ToLower() == name.ToLower());
            return foundBook;
        }

        public List<Book> CategorizeBook(string category)
        {
            List<Book> bookList = SelectThis<Book>(BooksourceFile);
            List<Book> foundBooks = bookList.Where(b => b.Genre.ToLower() == category.ToLower()).ToList();
            return foundBooks;
        }

        public List<Book> SortByTitle()
        {
            List<Book> bookList = SelectThis<Book>(BooksourceFile);
            bookList.Sort((x, y) => x.Title.CompareTo(y.Title));
            return bookList;
        }

        public List<Book> SortByPageNr()
        {
            List<Book> bookList = SelectThis<Book>(BooksourceFile);
            bookList.Sort((x, y) => x.PageNr.CompareTo(y.PageNr));
            return bookList;
        }



        public void registerClient(Client client)
        {
            try
            {
                List<Object> existingList = load(ClientsourceFile);
                existingList.Add(client);
                string updatedJson = JsonSerializer.Serialize(existingList, new JsonSerializerOptions { WriteIndented = true });
                save(updatedJson, ClientsourceFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding data to file: {ex.Message}");
            }
        }

        public void removeClient(int id)
        {
            List<Client> clientList = SelectThis<Client>(ClientsourceFile);
            clientList.RemoveAll(x => x.Id == id);
            string updatedJson = JsonSerializer.Serialize(clientList, new JsonSerializerOptions { WriteIndented = true });
            save(updatedJson, ClientsourceFile);
        }

        public Client SelectClient(int id)
        {
            List<Client> clientList = SelectThis<Client>(ClientsourceFile);
            var selectedClient = clientList.FirstOrDefault(x => x.Id == id);


            return selectedClient;
        }

        public BorrowedBooks SelectBorrow(int id)
        {
            List<BorrowedBooks> borrowList = SelectThis<BorrowedBooks>(BorrowListsourceFile);
            var selectedBorrow = borrowList.FirstOrDefault(x => x.BorrowedBookID == id);


            return selectedBorrow;
        }

        public void updateClient(Client client)
        {
            List<Client> clientList = SelectThis<Client>(ClientsourceFile);
            clientList = clientList.Select(b => b.Id == client.Id ? client : b).ToList();
            string updatedJson = JsonSerializer.Serialize(clientList, new JsonSerializerOptions { WriteIndented = true });
            save(updatedJson, ClientsourceFile);

        }

        public void AddborrowedBook(BorrowedBooks obj)
        {

            try
            {
                List<Object> existingList = load(BorrowListsourceFile);
                existingList.Add(obj);
                string updatedJson = JsonSerializer.Serialize(existingList, new JsonSerializerOptions { WriteIndented = true });
                save(updatedJson, BorrowListsourceFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding data to file: {ex.Message}");
            }
        }
        public void RemoveBorrowedBook(int id)
        {
            List<BorrowedBooks> borrowedBookList = SelectThis<BorrowedBooks>(BorrowListsourceFile);
            var borrowedBook = borrowedBookList.FirstOrDefault(x => x.BorrowedBookID == id);

            if (borrowedBook != null)
            {
                var idd = borrowedBook.BookID;
                var bookToReturn =  SelectBook(idd);
                
                borrowedBookList.RemoveAll(x => x.BorrowedBookID == id);
                string updatedJson = JsonSerializer.Serialize(borrowedBookList, new JsonSerializerOptions { WriteIndented = true });
                save(updatedJson, BorrowListsourceFile);

                if (bookToReturn != null)
                {
                    bookToReturn.NrStock++;
                    updateBook(bookToReturn);
                }
                else
                {
                    Console.WriteLine("oops");
                }
            }
            else
            {
                Console.WriteLine("Borrowed book not found.");
            }
        }

        public List<BorrowedBooks> GenerateReport()
        {
            List<BorrowedBooks> borrowedBookList = SelectThis<BorrowedBooks>(BorrowListsourceFile);
            return borrowedBookList;
        }

    }
}


