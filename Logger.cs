using System.IO;


namespace BackupCopying
{
    internal class Logger : ILogger
    {
        private readonly string pathToWrite;
        private readonly uint LevelEventToWrite;
        public Logger(string pathToWrite, uint LevelEventToWrite)
        {
            this.pathToWrite = pathToWrite+"\\logfile.log";
            this.LevelEventToWrite = LevelEventToWrite;
        }

        public void Log(string log, uint LevelEvent)
        {
            if (LevelEvent<= LevelEventToWrite) File.AppendAllText(pathToWrite, log + "\n");
        }
    }
    interface ILogger
    {
        public void Log(string log, uint lvlEvent);
    }
}
