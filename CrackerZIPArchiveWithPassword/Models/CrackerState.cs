using System;

namespace CrackerZIPArchiveWithPassword.Models
{
    public class CrackerState
    {
        public string FileName { get; set; }
        public string CurrentPassword { get; set; }
        public Time ElapsedTime { get; set; }

        public CrackerState()
        {

        }

        public CrackerState(string fileName, string currentPassword, Time elapsedTime)
        {
            FileName = fileName;
            CurrentPassword = currentPassword;
            ElapsedTime = elapsedTime;
        }
    }
}