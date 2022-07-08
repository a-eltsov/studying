using System;

namespace ConsoleApp_Exercise1
{
    // класс "Спортсмен"
    public class Sportsman
    {
        string name;                // имя спортсмена (или ФИО)
        string section;             // название секции
        string group;               // номер группы

        public Sportsman()
        {

        }

        public Sportsman(string name)
        {
            this.name = name;
        }

        public string Name { get { return name; } set { name = value; } }
        public string Section { get { return section; } set { section = value; } }
        public string Group { get { return group; } set { group = value; } }

        // вывод информации о спортсмене 
        public string Information_about_the_sportsman
        {
            get
            {
                string information = "";
                information = "Спортсмен: " + this.Name + '\n' +
                     "Название секции: " + this.Section + '\n' +
                     "Номер группы: " + this.group + '\n';
                return information;
            }
        }

    }
}
