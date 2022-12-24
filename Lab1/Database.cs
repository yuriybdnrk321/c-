using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace Lab1
{
    internal class Database
    {
        public static async Task Main(string[] args)
        {
           SqlConnection sqlConnection = new SqlConnection("Server=DESKTOP-OUM80I2\\SQLEXPRESS01;Database=Library;Integrated Security=true ");
           await sqlConnection.OpenAsync();

            SqlCommand command = new SqlCommand();

            command.Connection = sqlConnection;
            command.CommandText = "SELECT Employee.Surname, Employee.Name, Employee.PatronymicName, Employee.Position, Employee.Address, Employee.Сharacteristic, Employee.ProjectName, Project.Start, Project.End, Salary.PositionSalary, Project.ProjectDescripton FROM Employee AND Project AND Salary";


            SqlDataReader dataReader = await command.ExecuteReaderAsync();

            WriteLine("SurnameEmployee\tNameEmployee\tPatronymicName\tPosition\tEmployeeСharacteristic\tProjectName\tDateStart\tDateEnd\tPositionSalary\t ProjectDescription");
            
            while (await dataReader.ReadAsync())
            {

                WriteLine($"{dataReader["Surname"]}\t{dataEmployee["Name"]}\t{dataReader["PatronymicName"]}\t{dataReader["Position"]}\t{dataReader["Address"]}\t{dataReader["Characteristic"]}\t{dataReader["ProjectName"]}\t{dataReader["Project Start"]}\t{dataReader["Project End"]}\t{dataReader["Position Salary"]}\t{dataReader[""]}");
            }
           await sqlConnection.CloseAsync();
        }
    }
}
