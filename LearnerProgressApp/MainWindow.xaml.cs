using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using LearnerProgressApp.Models;
using LearnerProgressApp.Services;
using System.Linq;

namespace LearnerProgressApp
{
    public sealed partial class MainWindow : Window
    {
        private AppData _data;

        public MainWindow()
        {
            this.InitializeComponent();
            LoadData();
            RenderUI();
        }

        private void LoadData()
        {
            _data = DataService.Load();
        }

        private void RenderUI()
        {
            SubjectsPanel.Children.Clear();

            foreach (var subject in _data.Subjects)
            {
                var subjectBlock = new TextBlock
                {
                    Text = subject.Name,
                    FontSize = 20,
                    Margin = new Thickness(0, 10, 0, 5)
                };

                SubjectsPanel.Children.Add(subjectBlock);

                int completed = subject.Tasks.Count(t => t.Completed);
                int total = subject.Tasks.Count;

                var progress = new ProgressBar
                {
                    Minimum = 0,
                    Maximum = total,
                    Value = completed,
                    Height = 20,
                    Margin = new Thickness(0, 0, 0, 10)
                };

                SubjectsPanel.Children.Add(progress);

                foreach (var task in subject.Tasks)
                {
                    var checkbox = new CheckBox
                    {
                        Content = task.Title,
                        IsChecked = task.Completed,
                        Margin = new Thickness(10, 2, 0, 2)
                    };

                    checkbox.Checked += (s, e) => { task.Completed = true; SaveAndRefresh(); };
                    checkbox.Unchecked += (s, e) => { task.Completed = false; SaveAndRefresh(); };

                    SubjectsPanel.Children.Add(checkbox);
                }
            }
        }

        private void SaveAndRefresh()
        {
            DataService.Save(_data);
            RenderUI();
        }
    }
}
