using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8.Models
{
    public class Client
    {
       
            public int Id { get; set; }
            public string Name { get; set; }
            public string RegisterDate { get; set; }

            public string Address { get; set; }
            public string Phone { get; set; }
            public Client(int id, string name, string registerDate, string address, string phone)
            {
                Id = id;
                Name = name;
                RegisterDate = registerDate;
                Address = address;
                Phone = phone;
            }

        }
    
}
