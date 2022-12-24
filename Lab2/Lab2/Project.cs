using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab2
{
    
    public class Book
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public string Decsription { get; set; } = null!;

        
        
    }
}
