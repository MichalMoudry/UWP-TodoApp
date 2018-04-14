using Microsoft.Toolkit.Uwp.UI.Extensions;
using TodoApp.Helpers;
using TodoApp.Models.Database;
using TodoApp.Services;
using TodoApp.ViewModels.Database;
using TodoApp.Views.UserControls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace TodoApp.Views.Pages
{
    public sealed partial class TodoPage : Page
    {
        private List _list;

        public TodoPage()
        {
            InitializeComponent();
            LocalizeText();
        }

        /// <summary>
        /// OnNavigatedTo event override.
        /// </summary>
        /// <param name="e">Arguments.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            pageTitle.Text = ResourceLoaderHelper.GetResourceLoader().GetString("Todo");
            pageSymbol.Glyph = "\uE762";
            _list = (List)e.Parameter;
            listName.Text = _list.Name;
            todos.ItemsSource = TodoViewModel.Instance().GetTodos(_list.ID);
        }

        /// <summary>
        /// AppBarButton Click event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            AppBarButton button = (AppBarButton)sender;
            if (button.Name.Equals("addButton"))
            {
                await DialogService.Instance().ShowTodoDialogAsync(_list.ID);
            }
            else if (button.Name.Equals("editListButton"))
            {
                await DialogService.Instance().ShowListDialogAsync(_list);
                List list = ListViewModel.Instance().GetListByID(_list.ID);
                listName.Text = list.Name;
            }
        }

        /// <summary>
        /// DetailsButton Click event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private async void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            AppBarButton button = (AppBarButton)sender;
            if (button.Name.Equals("editTodoButton"))
            {
                await DialogService.Instance().ShowTodoDialogAsync((Todo)todos.SelectedItem);
            }
            else if (button.Name.Equals("deleteTodoButton"))
            {
                Todo todo = (Todo)todos.SelectedItem;
                ContentDialogResult result = 
                    await DialogService.Instance().ShowDeleteDialogAsync($"{ResourceLoaderHelper.GetResourceLoader().GetString("DialogDeleteTodo")} „{todo.Name}“");
                if (result.Equals(ContentDialogResult.Primary))
                {
                    await TodoViewModel.Instance().DeleteTodo(todo);
                }
            }
        }

        /// <summary>
        /// Method for setting localized values to XAML elements.
        /// </summary>
        private void LocalizeText()
        {
            editListButton.Label = ResourceLoaderHelper.GetResourceLoader().GetString("EditList");
            ToolTipService.SetToolTip(editListButton, editListButton.Label);
            settingsButton.Label = ResourceLoaderHelper.GetResourceLoader().GetString("Settings");
            ToolTipService.SetToolTip(settingsButton, settingsButton.Label);
            addButton.Label = ResourceLoaderHelper.GetResourceLoader().GetString("AddTodo");
            ToolTipService.SetToolTip(addButton, addButton.Label);
            editTodoButton.Label = ResourceLoaderHelper.GetResourceLoader().GetString("EditTodo");
            ToolTipService.SetToolTip(editTodoButton, editTodoButton.Label);
            deleteTodoButton.Label = ResourceLoaderHelper.GetResourceLoader().GetString("DeleteTodo");
            ToolTipService.SetToolTip(deleteTodoButton, deleteTodoButton.Label);
            noSelectionContent.Text = ResourceLoaderHelper.GetResourceLoader().GetString("NoSelectionContent");
        }

        /// <summary>
        /// AppBarButton Click event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void SecondaryAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }

        /// <summary>
        /// Todos SelectionChanged event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void Todos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (todos.SelectedItem != null)
            {
                TodoControl todoControl = todos.FindDescendant<TodoControl>();
                todoControl.SetDisplayedTodo((Todo)todos.SelectedItem);
            }
        }
    }
}