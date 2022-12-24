using System.ComponentModel.DataAnnotations;


namespace Lab2
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string PatronymicName { get; set; } = null!;
        public string Position { get; set; } = null!;
        public string Address { get; set; }

       
    

    }
}
