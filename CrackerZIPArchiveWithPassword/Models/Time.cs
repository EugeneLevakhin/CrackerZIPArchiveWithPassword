using System;
using System.Xml.Serialization;

namespace CrackerZIPArchiveWithPassword.Models
{
    public class Time
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        [XmlIgnore]
        public TimeSpan TimeSpan => new TimeSpan(Hours, Minutes, Seconds);

        public Time()
        {

        }

        public Time(TimeSpan timeSpan)
        {
            Hours = timeSpan.Hours;
            Minutes = timeSpan.Minutes;
            Seconds = timeSpan.Seconds;
        }
    }
}