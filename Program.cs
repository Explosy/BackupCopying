using System;

namespace BackupCopying
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BackupCreator bc = new BackupCreator();
            bc.Run();
        }
    }
}
