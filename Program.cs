using System;

namespace BackupCopying
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в программу резервного копирования");
            BackupCreator backupCreator = new BackupCreator();
            backupCreator.Run();
            Console.WriteLine("Для выхода из приложения нажмите Enter...");
            Console.ReadLine();
        }
    }
}
