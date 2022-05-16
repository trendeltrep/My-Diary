using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryProjext
{
    public class Event
    {
        public Event(string name,string place,DateTime timeStart,DateTime howLong,bool check)
        {
            this.name = name;
            this.place = place;
            this.timeStart = timeStart;
            this.howLong = howLong;
            this.check = check;
        }

        public string name;
        public string place;
        public DateTime timeStart;
        public DateTime howLong;
        public bool check;
    }
}
