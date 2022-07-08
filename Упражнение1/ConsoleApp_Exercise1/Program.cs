using SportWorkload_library;

namespace ConsoleApp_Exercise1
{

    class Program
    {

        static void Main()
        {

            // создание объекта класса "Спортсмен"
            Sportsman sportsman = new Sportsman();

            // ввод данных о спортсмене
            Console.WriteLine("ВВОД ДАННЫХ");
            Console.WriteLine("Введите ФИО спортсмена:");
            sportsman.Name = Console.ReadLine();
            Console.WriteLine("Введите название секции:");
            sportsman.Section = Console.ReadLine();
            Console.WriteLine("Введите номер группы:");
            sportsman.Group = Console.ReadLine();

            // создание объекта класса "Тренировка"
            Practice practice = new Practice();
            Console.WriteLine("Введите название тренировки:");
            practice.Practice_name = Console.ReadLine();

            // чтение информации о пульсовых зонах из файла
            //if (practice.Workload.ReadZoneScoresFromFile("d:\\Пульсовые зоны.txt") == false)
            //    Console.WriteLine("Ошибка чтения данных из файла");

            // ввод данных о тренировке
            Console.WriteLine("\nВвод данных о времени нахождения спортсмена в пульсовых зонах");
            ConsoleKeyInfo key;
            do
            {
                int zone = 0;
                double hours = 0, minutes = 0, seconds = 0;
                try
                {
                    Console.Write("Номер пульсовой зоны: ");
                    zone = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Время нахождения в пульсовой зоне (в формате ЧЧ ММ СС, разделитель пробел): ");
                    string[] input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    hours = Convert.ToDouble(input[0]);
                    minutes = Convert.ToDouble(input[1]);
                    seconds = Convert.ToDouble(input[2]);
                }
                catch
                {
                    Console.WriteLine("Ошибка ввода данных");
                    continue;
                }
                finally
                {
                    practice.Workload.AddTimeInZone(zone, hours, minutes, seconds);
                    Console.WriteLine("Нажмите Enter для ввода следующих значений времени или Esc для завершения ввода данных");
                    key = Console.ReadKey();
                }
            }
            while (key.Key != ConsoleKey.Escape);

            // вывод данных о спортсмене
            Console.WriteLine("\nВВЫВОД ДАННЫХ");
            Console.Write(sportsman.Information_about_the_sportsman);

            // вывод данных о тренировке
            Console.WriteLine(practice.Workload.Information);
        }

    }
}