using Syncfusion.UI.Xaml.Kanban;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Kanban
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        KanbanCardItem previousCard = null;
        private Compositor _compositor;

        public MainPage()
        {
            this.InitializeComponent();
        }

        //Apply Selected Card Style.
        private void Kanban_CardTapped(object sender, KanbanTappedEventArgs e)
        {
            if (previousCard != null && e.SelectedCard.ContentTemplateRoot is Grid)
            {
                (previousCard.ContentTemplateRoot as Grid).Background = new SolidColorBrush(Colors.Transparent);
                ((previousCard.ContentTemplateRoot as Grid).FindName("KANBAN_Title") as TextBlock).ClearValue(TextBlock.ForegroundProperty);
                ((previousCard.ContentTemplateRoot as Grid).FindName("KANBAN_Description") as TextBlock).ClearValue(TextBlock.ForegroundProperty);
            }
            previousCard = e.SelectedCard;
            if (e.SelectedCard.ContentTemplateRoot is Grid)
            {
                (e.SelectedCard.ContentTemplateRoot as Grid).Background = new SolidColorBrush(Colors.Orange);
                ((e.SelectedCard.ContentTemplateRoot as Grid).FindName("KANBAN_Title") as TextBlock).Foreground = new SolidColorBrush(Colors.Red);
                ((e.SelectedCard.ContentTemplateRoot as Grid).FindName("KANBAN_Description") as TextBlock).Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        //Shadow effects for the Card.
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Grid rootGrid = sender as Grid;
            Canvas shadow = null;
            Border border = null;
            foreach (UIElement child in rootGrid.Children)
            {
                if (child is Canvas)
                {
                    shadow = child as Canvas;
                }
                else
                {
                    border = child as Border;
                    //To apply selected card style.
                    if (previousCard != null && (previousCard.Content as KanbanModel).Equals(rootGrid.DataContext as KanbanModel))
                    {
                        rootGrid.Background = new SolidColorBrush(Colors.Orange);
                        ((child as Border).FindName("KANBAN_Title") as TextBlock).Foreground = new SolidColorBrush(Colors.Red);
                        ((child as Border).FindName("KANBAN_Description") as TextBlock).Foreground = new SolidColorBrush(Colors.Red);
                    }
                }
            }

            var compositor = ElementCompositionPreview.GetElementVisual(border).Compositor;
            SpriteVisual spriteVisual = compositor.CreateSpriteVisual();
            spriteVisual.Size = new Vector2((float)border.ActualWidth, (float)border.ActualHeight);
            var dropShadow = compositor.CreateDropShadow();
            dropShadow.Offset = new Vector3(10, 10, 10);
            dropShadow.Color = Colors.LightGray;
            spriteVisual.Shadow = dropShadow;

            ElementCompositionPreview.SetElementChildVisual(shadow, spriteVisual);
        }
    }

    public class TaskDetails
    {
        public TaskDetails()
        {
            Tasks = new ObservableCollection<KanbanModel>();

            KanbanModel task = new KanbanModel();
            task.Title = "UWP Issue";
            task.ID = "6593";
            task.Description = "Sorting is not working properly in DateTimeAxis";
            task.Category = "Postponed";
            task.ColorKey = "High";
            task.Tags = new string[] { "Bug Fixing" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image0.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "New Feature";
            task.ID = "6593";
            task.Description = "Need to create code base for Gantt control";
            task.Category = "Postponed";
            task.ColorKey = "Low";
            task.Tags = new string[] { "GanttControl UWP" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image1.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "UG";
            task.ID = "6516";
            task.Description = "Need to do post processing work for closed incidents";
            task.Category = "Postponed";
            task.ColorKey = "Normal";
            task.Tags = new string[] { "Post processing" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image2.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "UWP Issue";
            task.ID = "651";
            task.Description = "Crosshair label template not visible in UWP.";
            task.Category = "Open";
            task.ColorKey = "High";
            task.Tags = new string[] { "Bug Fixing" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image3.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "UWP Issue";
            task.ID = "646";
            task.Description = "AxisLabel cropped when rotate the axis label.";
            task.Category = "Open";
            task.ColorKey = "Low";
            task.Tags = new string[] { "Bug Fixing" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image4.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "WPF Issue";
            task.ID = "23822";
            task.Description = "Need to implement tooltip support for histogram series.";
            task.Category = "Open";
            task.ColorKey = "High";
            task.Tags = new string[] { "Bug Fixing" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image0.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "Kanban Feature";
            task.ID = "25678";
            task.Description = "Need to prepare SampleBrowser sample";
            task.Category = "InProgress";
            task.ColorKey = "Low";
            task.Tags = new string[] { "New control" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image5.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "WP Issue";
            task.ID = "1254";
            task.Description = "HorizontalAlignment for tooltip is not working";
            task.Category = "InProgress";
            task.ColorKey = "High";
            task.Tags = new string[] { "Bug fixing" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image2.png");
            Tasks.Add(task);

            

            task = new KanbanModel();
            task.Title = "New Feature";
            task.ID = "29574";
            task.Description = "Dragging events support for SfKanban";
            task.Category = "Closed";
            task.ColorKey = "Normal";
            task.Tags = new string[] { "New Control" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image5.png");
            Tasks.Add(task);

            task = new KanbanModel();
            task.Title = "New Feature";
            task.ID = "29574";
            task.Description = "Swimlane support for SfKanban";
            task.Category = "Open";
            task.ColorKey = "Low";
            task.Tags = new string[] { "New Control" };
            task.ImageURL = new Uri("ms-appx:///Images/SoftwareDevelopmentProcess/Image8.png");
            Tasks.Add(task);
        }
        public ObservableCollection<KanbanModel> Tasks { get; set; }

        public void Dispose()
        {
            if (Tasks != null)
                Tasks.Clear();
        }
    }
}