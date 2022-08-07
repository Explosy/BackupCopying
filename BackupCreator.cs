using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BackupCopying
{
    internal class BackupCreator
    {
        private string settingsFile = ""
        private string[] pathFrom;
        private string pathTo = "C:\\copyTo";

        public BackupCreator()
        {
            using (JsonDocument document = JsonDocument.Parse(settingsFile))
            {
                JsonElement root = document.RootElement;
                JsonElement pathFromElement = root.GetProperty("PathFrom");
            }
        }
        public void Run()
        {
            copyFileToTemp(pathFrom);


        }

        private void copyFileToTemp(string pathFrom)
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
