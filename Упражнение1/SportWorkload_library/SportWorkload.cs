using System.Text;

namespace SportWorkload_library
{
    public class SportWorkload
    {
        // Класс для расчёта нагрузки спортсмена за время тренировки.
        // Исходные данные для расчёта: время нахождения спортсмена в пульсовой зоне, задаётся методами SetTimeInZone или AddTimeInZone.
        // Количество зон и баллы, соответствующие зонам, задаются по умолчанию в конструкторе SportWorkload или могут быть прочтены из файла методом ReadZoneScoresFromFile.
        // Результат расчёта нагрузки возвращается методом Calculate

        int zone_count;             // количество пульсовых зон
        List<int> zones;            // номера пульсовых зон
        List<double> zone_scores;   // баллы, соответствующие пульсовым зонам, ед./ч
        List<double> time_in_zone;  // время нахождения спортсмена в пульсовой зоне, ч

        public SportWorkload()
        {
            zone_count = 10;
            zones = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            zone_scores = new List<double>() { 20, 30, 40, 50, 60, 70, 80, 100, 120, 140 };
            time_in_zone = new List<double>();
            for (int i = 0; i < zone_count; i++)
                time_in_zone.Add(0);
        }

        // задать время нахождения спортсмена в пульсовой зоне, в часах
        public void SetTimeInZone(int zone, double hours)
        {
            // поиск зоны по номеру
            int i_zone = -1;
            for (int i = 0; i < zone_count; i++)
                if (zones[i] == zone)
                    i_zone = i;
            // если такой пульсовой зоны нет
            if (i_zone == -1)
                return;

            time_in_zone[i_zone] = hours;
        }

        // задать время нахождения спортсмена в пульсовой зоне, в часах, минутах, секундах
        public void SetTimeInZone(int zone, double hours, double minutes = 0, double seconds = 0)
        {
            double hours_in_zone = hours + minutes / 60.0 + seconds / 3600.0;
            SetTimeInZone(zone, hours_in_zone);
        }

        // увеличить время нахождения спортсмена в пульсовой зоне, в часах
        public void AddTimeInZone(int zone, double hours)
        {
            // поиск зоны по номеру
            int i_zone = -1;
            for (int i = 0; i < zone_count; i++)
                if (zones[i] == zone)
                    i_zone = i;
            // если такой пульсовой зоны нет
            if (i_zone == -1)
                return;

            time_in_zone[i_zone] += hours;
        }

        // увеличить время нахождения спортсмена в пульсовой зоне, в часах, минутах, секундах
        public void AddTimeInZone(int zone, double hours, double minutes = 0, double seconds = 0)
        {
            double hours_in_zone = hours + minutes / 60.0 + seconds / 3600.0;
            AddTimeInZone(zone, hours_in_zone);
        }

        // узнать занесённое значение времени (в часах) нахождения спортсмена в пульсовой зоне zone
        public double Time_in_zone(int zone)
        {
            // поиск зоны по номеру
            int i_zone = -1;
            for (int i = 0; i < zone_count; i++)
                if (zones[i] == zone)
                    i_zone = i;
            // если такой пульсовой зоны нет
            if (i_zone == -1)
                return 0;

            return time_in_zone[i_zone];
        }

        // рассчитать нагрузку, в ед.
        public double Calculate()
        {
            double load = 0;
            for (int i = 0; i < zone_count; i++)
                load = load + time_in_zone[i] * zone_scores[i];
            return load;
        }

        // вывод информации о нагрузке 
        public string Information
        {
            get
            {
                string information = "";
                information = "Время нахождения спортсмена в пульсовых зонах: \n";
                for (int i = 0; i < zone_count; i++)
                    information += $"Зона {Convert.ToString(zones[i])}  -  {Convert.ToString(time_in_zone[i])} ч\n";
                information += "Нагрузка спортсмена за тренировку: " + Convert.ToString(Calculate()) + " единиц\n";
                return information;
            }
        }

        // чтение из файла информации о количестве пульсовых зон и баллам, соответствующим им
        // формат файла: количество строк соответствует количеству пульсовых зон, в каждой строке: Номер_зоны  Баллы (разделитель табуляция)
        // метод возвращает true, если чтение успешно
        public bool ReadZoneScoresFromFile(string full_filename)
        {
            // попытка чтения из файла
            int[] zones = new int[0];
            double[] scores = new double[0];
            try
            {
                string[] strings_from_file;
                using (FileStream file = File.OpenRead(full_filename))
                {
                    byte[] buffer = new byte[file.Length];
                    file.Read(buffer,0,buffer.Length);
                    strings_from_file = Encoding.Default.GetString(buffer).Split('\n', StringSplitOptions.RemoveEmptyEntries);
                }
                zones = new int[strings_from_file.Length];
                scores = new double[strings_from_file.Length];
                for (int i = 0; i < strings_from_file.Length; i++)
                {
                    string[] s = strings_from_file[i].Split('\t', StringSplitOptions.RemoveEmptyEntries);
                    zones[i] = Convert.ToInt32(s[0]);
                    scores[i] = Convert.ToDouble(s[1]);
                }
            }
            catch
            {
                return false;
            }

            // занесение прочитанных данных в поля класса
            this.zones.Clear();
            this.zone_scores.Clear();
            this.zone_count = zones.Length;
            for (int i = 0; i < this.zone_count; i++)
            {
                this.zones.Add(zones[i]);
                this.zone_scores.Add(scores[i]);
            }

            return true;
        }

    }
}