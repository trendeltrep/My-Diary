using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DiaryProjext
{
    public static class Database
    {
        public static List<Event> events = new List<Event>();

        public static List<Event> expired = new List<Event>();
        public static List<Event> today = new List<Event>();
        public static List<Event> tomorrow = new List<Event>();
        public static List<Event> thisWeek = new List<Event>();
        public static List<Event> nextWeek = new List<Event>();
        public static List<Event> overThanWeek = new List<Event>();
        public static List<Event> finished = new List<Event>();

        public static void GetData()
        {
          
            StreamReader sr = new StreamReader(File.OpenRead(@"DIARYBASE.txt"));

            while (sr.EndOfStream == false)
            {
                string line = sr.ReadLine();
                if (line == null) continue;

                var helpString = line.Split(';');
                
                DateTime date = DateTime.Parse(helpString[2]);
                DateTime time = DateTime.Parse(helpString[3]);
                bool check = Boolean.Parse(helpString[4]);
                Event obj = new Event(helpString[0],helpString[1],date,time,check);

                events.Add(obj);
            }
            sr.Close();
        }

        public static void LoadToFileData()
        {
            StreamWriter sw = new StreamWriter(@"DIARYbase.txt");
            for (int i = 0; i < events.Count; i++)
            {
                sw.WriteLine($"{events[i].name};{events[i].place};{events[i].timeStart};{events[i].howLong};{events[i].check}");
            }
            sw.Close();
        }

        public static void SortDatabaseEventsByDateTime()
        {
            events.Sort((x, y) => x.timeStart.CompareTo(y.timeStart));
        }

        public static void SortDatabaseEventsByCheck()
        {
            events.Sort((x, y) => x.check.CompareTo(true));
        }

        public static void RecreateEvents()
        {
            foreach (Event helpEvent in expired)
            {
                events.Add(helpEvent);
            }
            foreach (Event helpEvent in today)
            {
                events.Add(helpEvent);
            }
            foreach (Event helpEvent in tomorrow)
            {
                events.Add(helpEvent);
            }
            foreach (Event helpEvent in thisWeek)
            {
                events.Add(helpEvent);
            }
            foreach (Event helpEvent in nextWeek)
            {
                events.Add(helpEvent);
            }
            foreach (Event helpEvent in overThanWeek)
            {
                events.Add(helpEvent);
            }
            foreach (Event helpEvent in finished)
            {
                events.Add(helpEvent);
            }
        }
        public static void SortDatabaseEventsByTime()
        {                        
            SortDatabaseEventsByCheck();
            SortDatabaseEventsByDateTime();
            expired.Clear();
            today.Clear();
            tomorrow.Clear();
            thisWeek.Clear();
            nextWeek.Clear();
            overThanWeek.Clear();
            finished.Clear();
            for (int i = 0;i< events.Count; i++)
            {
                if(events[i].check == true)
                {
                    finished.Add(events[i]);
                    continue;
                }
                if(events[i].timeStart<= DateTime.Now)
                {
                    expired.Add(events[i]);
                }
                if (events[i].timeStart >= DateTime.Now && events[i].timeStart<= DateTime.Today.AddDays(1))
                {
                    today.Add(events[i]);
                }
                if (events[i].timeStart >= DateTime.Today.AddDays(1) && events[i].timeStart <= DateTime.Today.AddDays(2))
                {
                    tomorrow.Add(events[i]);
                }
                if (events[i].timeStart >= DateTime.Today.AddDays(2) && events[i].timeStart <= DateTime.Today.AddDays(7))
                {
                    thisWeek.Add(events[i]);
                }
                if (events[i].timeStart >= DateTime.Today.AddDays(7) && events[i].timeStart <= DateTime.Today.AddDays(14))
                {
                    nextWeek.Add(events[i]);
                }
                if (events[i].timeStart >= DateTime.Today.AddDays(14))
                {
                    overThanWeek.Add(events[i]);
                }
                
            }
            events.Clear();
            RecreateEvents();
            
        }
    }
}
