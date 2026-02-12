using System;
using System.IO;
using System.Text.Json;
using LearnerProgressApp.Models;

namespace LearnerProgressApp.Services
{
    public static class DataService
    {
        private static readonly string FolderPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "LearnerProgressApp");

        private static readonly string FilePath = Path.Combine(FolderPath, "data.json");

        public static AppData Load()
        {
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            if (!File.Exists(FilePath))
            {
                var defaultData = CreateDefaultData();
                Save(defaultData);
                return defaultData;
            }

            string json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<AppData>(json);
        }

        public static void Save(AppData data)
        {
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }

        private static AppData CreateDefaultData()
        {
            return new AppData
            {
                Subjects =
                {
                    new Subject
                    {
                        Name = "IT Skills",
                        Tasks =
                        {
                            new TaskItem { Title = "File Management Worksheet 1" },
                            new TaskItem { Title = "File Management Worksheet 2" }
                        }
                    },
                    new Subject
                    {
                        Name = "Word Processing",
                        Tasks =
                        {
                            new TaskItem { Title = "Formatting Exercise" }
                        }
                    }
                }
            };
        }
    }
}
