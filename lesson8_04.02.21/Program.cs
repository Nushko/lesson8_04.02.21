using System;
using System.Data.SqlClient;

namespace lesson8_04._02._21
{
    class Program
    {
        static void Main(string[] args)
        {

            string conString = "Data source=WIN-AK9539CDTPS; initial catalog=Baza; integrated security=true;";

            SqlConnection connection = new SqlConnection(conString);

            string choice, lastname, middlename, firstname, birthdate;
            int id;

            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Connected\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                SqlCommand command = connection.CreateCommand();

                Console.WriteLine(
                    "Выберите операцию:\n " +
                    "1.Добавление\n " +
                    "2.Удалить один по Id\n " +
                    "3.Обновить каждый столбец кроме Id\n " +
                    "4.Выбрать всё\n " +
                    "5.Выбрать один по Id");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Введите Фамилию: ");
                        lastname = Console.ReadLine();
                        Console.Write("Введите Имя: ");
                        firstname = Console.ReadLine();
                        Console.Write("Введите Отчество (если нету, то просто нажмите Enter): ");
                        middlename = Console.ReadLine();
                        Console.Write("Введите День Рождения: ");
                        birthdate = Console.ReadLine();
                        command.CommandText =
                            "insert into Person(LastName, FirstName, MiddleName, BirthDate)" +
                            $"values('{lastname}','{firstname}','{middlename}','{birthdate}')";
                        command.ExecuteNonQuery();
                        break;

                    case "2":
                        Console.Write("Введите Id элемента, который нужно удалить: ");
                        id = int.Parse(Console.ReadLine());
                        command.CommandText = $"delete from Person where Id={id}\n";
                        command.ExecuteNonQuery();
                        break;

                    case "3":
                        Console.Write("Введите Id элемента, который нужно обновить: ");
                        id = int.Parse(Console.ReadLine());
                        Console.Write("Введите Фамилию: ");
                        lastname = Console.ReadLine();
                        Console.Write("Введите Имя: ");
                        firstname = Console.ReadLine();
                        Console.Write("Введите Отчество (если нету, то просто нажмите Enter): ");
                        middlename = Console.ReadLine();
                        Console.Write("Введите День Рождения: ");
                        birthdate = Console.ReadLine();
                        command.CommandText =
                            $"update Person set LastName='{lastname}', FirstName='{firstname}', MiddleName='{middlename}', BirthDate='{birthdate}'" +
                            $"where Id={id}";
                        command.ExecuteNonQuery();
                        break;

                    case "4":
                        command.CommandText = "select * from Person\n";
                        SqlDataReader readerAll = command.ExecuteReader();
                        while (readerAll.Read())
                        {
                            Console.WriteLine(
                                $" id:{readerAll["Id"]},\n " +
                                $"LastName:{readerAll["LastName"]},\n " +
                                $"FirstName:{readerAll["FirstName"]},\n " +
                                $"MiddleName:{readerAll["MiddleName"]},\n " +
                                $"BirthDate:{readerAll["BirthDate"]}\n");
                        }
                        break;

                    case "5":
                        Console.Write("Введите Id: ");
                        id = int.Parse(Console.ReadLine());
                        command.CommandText = $"select * from Person where Id={id}\n";
                        SqlDataReader readerId = command.ExecuteReader();
                        while (readerId.Read())
                        {
                            Console.WriteLine(
                                $" id:{readerId["Id"]},\n " +
                                $"LastName:{readerId["LastName"]},\n " +
                                $"FirstName:{readerId["FirstName"]},\n " +
                                $"MiddleName:{readerId["MiddleName"]},\n " +
                                $"BirthDate:{readerId["BirthDate"]}\n");
                        }
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Неправильная команда");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nDisconnected");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}
