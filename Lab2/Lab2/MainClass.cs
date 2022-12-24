using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using static System.Console;
namespace Lab2
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            
            var config = new ConfigurationBuilder().AddJsonFile(path: "appsettings.json").Build();

            var optionsBuilder = new DbContextOptionsBuilder<DbConnection>();
            DbConnection db = new DbConnection(optionsBuilder.UseSqlServer(config.GetConnectionString("DbConnection")).Options);
            
                    var result = db.InfoBooks.Include(p=>p.Ticket).Include(p=>p.Ticket.Reader).Include(p=>p.Book).OrderBy(p=>p.Ticket.Reader.Surname);

                    WriteLine("Surname\tName\tPatronymic name\tAddress\tPhone\tDate birth\tID ticket\tAuthor\tBook\tDate take book\tDate return book\tPrice");
                    foreach(var info in result)
                    {
                        WriteLine($"{info.Ticket.Reader.Surname}\t{info.Ticket.Reader.Name}\t{info.Ticket.Reader.PatronymicName}\t{info.Ticket.Reader.Address}\t{info.Ticket.Reader.Phone}\t{info.Ticket.Reader.DateBirth}\t{info.TicketID}\t{info.Book.Author}\t{info.Book.Name}\t{info.DateTakeBook}\t{info.DateReturnBook}\t{info.Book.Price}");
                    }
               
        }
    }
}
