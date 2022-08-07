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

        public BackupCreator()
        {
            if (File.Exists("Settings.json")) settingsFile = Environment.CurrentDirectory + "\\Settings.json";
            string json = File.ReadAllText(settingsFile);
            settings = JsonConvert.DeserializeObject<Setting>(json);
        }
        public void Run()
        {
            Console.WriteLine("Начало резервного копирования...");
            string tempDirectory = settings.PathTo+"\\" + DateTime.Now.ToString("dd_MM_yyyy");
            foreach (string pathFrom in settings.PathFrom)
            {
                copyFileToTemp(pathFrom, tempDirectory);
            }
            ZipFile.CreateFromDirectory(tempDirectory, tempDirectory+".zip");
            Directory.Delete(tempDirectory, true);

        }

        private void copyFileToTemp(string pathFrom, string pathTo)
        {
            //Воссоздание иерархии папок аналогичной исходному каталогу
            foreach (string path in Directory.GetDirectories(pathFrom, "*", SearchOption.AllDirectories))
            {
                try
                {
                    Directory.CreateDirectory(path.Replace(pathFrom, pathTo));
                }
                catch (IOException exception)
                {
                    //TODO логирование
                }
            }
            //Копирование всех файлом из исходного каталога
            foreach (string newPath in Directory.GetFiles(pathFrom, "*.*", SearchOption.AllDirectories))
            {
                try
                {
                    File.Copy(newPath, newPath.Replace(pathFrom, pathTo), true);
                }
                catch (IOException exception)
                {
                    //TODO логирование
                }
            }
        }
    }
}
