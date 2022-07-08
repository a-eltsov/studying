using System;
using SportWorkload_library;

namespace ConsoleApp_Exercise1
{
    // класс "Тренировка"
    public class Practice
    {
        string practice_name;       // название тренировки
        SportWorkload workload;     // нагрузка за тренировку

        public Practice()
        {
            workload = new SportWorkload();
        }
        public string Practice_name { get { return practice_name; } set { practice_name = value; } }
        public SportWorkload Workload { get { return workload; } set { workload = value; } }
    }
}
