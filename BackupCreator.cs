using Newtonsoft.Json;
using System;
using System.IO.Compression;
using System.IO;


namespace BackupCopying
{
    internal class BackupCreator
    {
        private Setting settings;
        private string settingsFile;
        private Logger logger;
        
        public BackupCreator()
        {
            if (!File.Exists("Settings.json"))
            {
                Console.WriteLine("Файл Settings.json с настройками не найден");
            }
            settingsFile = Environment.CurrentDirectory + "\\Settings.json";
            string json = File.ReadAllText(settingsFile);
            settings = JsonConvert.DeserializeObject<Setting>(json);
            logger = new Logger(settings.PathTo, settings.LevelEventToWrite);
        }
        public void Run()
        {
            Console.WriteLine("Начало резервного копирования...");
            logger.Log("Начало резервного копирования...", 2);
            string tempDirectory = settings.PathTo+"\\" + DateTime.Now.ToString("dd_MM_yyyy");
            foreach (string pathFrom in settings.PathFrom)
            {
                string message = "Копирование директории " + pathFrom + " в во временную директорию " + settings.PathTo;
                logger.Log(message, 2);
                copyFileToTemp(pathFrom, tempDirectory);
                
            }
            ZipFile.CreateFromDirectory(tempDirectory, tempDirectory+".zip");
            logger.Log("Архив создан", 2);
            Directory.Delete(tempDirectory, true);
            logger.Log("Временная директория удалена", 2);
        }

        private void copyFileToTemp(string pathFrom, string pathTo)
        {
            //Воссоздание иерархии папок аналогичной исходному каталогу
            foreach (string path in Directory.GetDirectories(pathFrom, "*", SearchOption.AllDirectories))
            {
                try
                {
                    var newPath = path.Replace(pathFrom, pathTo);
                    Directory.CreateDirectory(newPath);
                    string message = "Создана директория " + newPath;
                    logger.Log(message, 3);
                }
                catch (IOException exception)
                {
                    string message = "Не удалось создать директорию " + path.Replace(pathFrom, pathTo);
                    logger.Log(message, 1);
                }
            }
            //Копирование всех файлом из исходного каталога
            foreach (string newPath in Directory.GetFiles(pathFrom, "*.*", SearchOption.AllDirectories))
            {
                try
                {
                    File.Copy(newPath, newPath.Replace(pathFrom, pathTo), true);
                    string message = "Скопирован файл " + newPath;
                    logger.Log(message, 3);
                }
                catch (IOException exception)
                {
                    string message = "Не удалось скопировать " + newPath;
                    logger.Log(message, 1);
                }
            }
        }
    }
}
