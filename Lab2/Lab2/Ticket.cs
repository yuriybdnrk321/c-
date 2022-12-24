using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab2
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        
        public int ProjectID { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        public Employee Employee { get; set; } = null!;

        

    }
}
