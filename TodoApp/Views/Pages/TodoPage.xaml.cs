using Microsoft.Toolkit.Uwp.UI.Extensions;
using System.Collections.Specialized;
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
            taskProgress.Maximum = TodoViewModel.Instance().GetNumberOfTodos(_list.ID);
            taskProgress.Value = TodoViewModel.Instance().GetNumberOfTodos(_list.ID, true);
            listName.Text = _list.Name;
            var list = TodoViewModel.Instance().GetTodos(_list.ID);
            list.CollectionChanged += Todos_CollectionChanged;
            todos.ItemsSource = list;
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
                DisplayNotification(await DialogService.Instance().ShowTodoDialogAsync(_list.ID));
            }
            else if (button.Name.Equals("editListButton"))
            {
                ContentDialogResult result = await DialogService.Instance().ShowListDialogAsync(_list);
                DisplayNotification(result);
                if (result.Equals(ContentDialogResult.Secondary))
                {
                    List list = ListViewModel.Instance().GetListByID(_list.ID);
                    listName.Text = list.Name;
                }
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
        /// Method for displaying in-app notification.
        /// </summary>
        /// <param name="dialogResult">Result of a dialog.</param>
        private void DisplayNotification(ContentDialogResult dialogResult)
        {
            if (dialogResult.Equals(ContentDialogResult.Primary))
            {
                inAppNotification.Show($"{ResourceLoaderHelper.GetResourceLoader().GetString("AddTodoNotificationContent")}", "#205624");
            }
            else if (dialogResult.Equals(ContentDialogResult.None))
            {
                inAppNotification.Show($"{ResourceLoaderHelper.GetResourceLoader().GetString("AddError")}", "#ad2929");
            }
        }

        /// <summary>
        /// Method for setting localized values to XAML elements.
        /// </summary>
        private void LocalizeText()
        {
            editListButton.Label = ResourceLoaderHelper.GetResourceLoader().GetString("EditList");
            ToolTipService.SetToolTip(editListButton, editListButton.Label);
            addButton.Label = ResourceLoaderHelper.GetResourceLoader().GetString("AddTodo");
            ToolTipService.SetToolTip(addButton, addButton.Label);
            editTodoButton.Label = ResourceLoaderHelper.GetResourceLoader().GetString("EditTodo");
            ToolTipService.SetToolTip(editTodoButton, editTodoButton.Label);
            deleteTodoButton.Label = ResourceLoaderHelper.GetResourceLoader().GetString("DeleteTodo");
            ToolTipService.SetToolTip(deleteTodoButton, deleteTodoButton.Label);
            noSelectionContent.Text = ResourceLoaderHelper.GetResourceLoader().GetString("NoSelectionContent");
        }

        /// <summary>
        /// Todos CollectionChanged event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void Todos_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            taskProgress.Value = TodoViewModel.Instance().GetNumberOfTodos(_list.ID, true);
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