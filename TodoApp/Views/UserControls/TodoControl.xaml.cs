using TodoApp.Helpers;
using TodoApp.Models.Database;
using TodoApp.Services;
using TodoApp.ViewModels.Database;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace TodoApp.Views.UserControls
{
    public sealed partial class TodoControl : UserControl
    {
        private Todo _todo;

        public TodoControl()
        {
            InitializeComponent();
            LocalizeText();
        }

        /// <summary>
        /// Method for displaying data of a to-do.
        /// </summary>
        /// <param name="todo">To-do.</param>
        public void SetDisplayedTodo(Todo todo)
        {
            _todo = todo;
            todoCompletedBox.IsChecked = todo.IsCompleted;
            todoName.Text = _todo.Name;
            todoAdded.Text = $"{ResourceLoaderHelper.GetResourceLoader().GetString("Added")}: {_todo.Added}";
            subtasks.ItemsSource = SubtaskViewModel.Instance().GetSubtasks(_todo.ID);
        }

        /// <summary>
        /// CommandButton Click event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private async void CommandButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Name.Equals("addSubtaskButton"))
            {
                await DialogService.Instance().ShowSubtaskDialogAsync(_todo.ID);
            }
            else if (button.Name.Equals("sortSubtasksButton"))
            {
                FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            }
        }

        /// <summary>
        /// CheckBox Check event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private async void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            bool checkBoxState = (bool)checkBox.IsChecked;
            if (checkBox.Name.Equals("subtaskCheckbox"))
            {
                Subtask subtask = (Subtask)checkBox.DataContext;
                if (subtask.IsCompleted != checkBoxState)
                {
                    subtask.IsCompleted = checkBoxState;
                    await SubtaskViewModel.Instance().UpdateSubtask(subtask, false);
                }
            }
            else
            {
                _todo.IsCompleted = checkBoxState;
                await TodoViewModel.Instance().UpdateTodo(_todo);
                checkBox.IsChecked = false;
            }
        }

        /// <summary>
        /// ListCommandButton Click event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private async void ListCommandButton_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem button = (MenuFlyoutItem)sender;
            Subtask subtask = (Subtask)button.DataContext;
            if (button.Name.Equals("editTaskButton"))
            {
                await DialogService.Instance().ShowSubtaskDialogAsync(subtask);
            }
            else if (button.Name.Equals("deleteTaskButton"))
            {
                ContentDialogResult result = await DialogService.Instance().ShowDeleteDialogAsync($"{ResourceLoaderHelper.GetResourceLoader().GetString("DialogDeleteSubtask")} „{subtask.Name}“");
                if (result.Equals(ContentDialogResult.Primary))
                {
                    await SubtaskViewModel.Instance().DeleteSubtask(subtask);
                }
            }
        }

        /// <summary>
        /// ListItem Click event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void ListItem_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        /// <summary>
        /// Method for setting localized values to XAML elements.
        /// </summary>
        private void LocalizeText()
        {
            subtodosTitle.Text = ResourceLoaderHelper.GetResourceLoader().GetString("Subtodos");
        }

        /// <summary>
        /// SortButton Click event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = (MenuFlyoutItem)sender;
            if (item.Name.Equals("sortByNameButton"))
            {
                SubtaskViewModel.Instance().SortSubtasks("Name");
            }
            else
            {
                SubtaskViewModel.Instance().SortSubtasks("Date");
            }
        }
    }
}