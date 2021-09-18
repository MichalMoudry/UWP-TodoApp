using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Linq;
using TodoApp.Services;
using TodoApp.Shared.Models.Entity;
using TodoApp.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;

namespace TodoApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TodoListPage : Page
    {
        /// <summary>
        /// Private read-only field with <see cref="NavigationService"/> instance.
        /// </summary>
        private readonly NavigationService _navigationService;

        public TodoListPage()
        {
            InitializeComponent();
            _navigationService = App.Services.GetRequiredService<NavigationService>();
            DataContext = App.Services.GetRequiredService<TodoListPageViewModel>();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        /// <summary>
        /// View model of this page.
        /// </summary>
        public TodoListPageViewModel ViewModel => (TodoListPageViewModel)DataContext;

        /// <summary>
        /// Event handler for Button (button for adding to-dos) click event.
        /// </summary>
        /// <param name="sender">Button for adding to-dos.</param>
        /// <param name="e">Event parameters.</param>
        private async void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            bool res = await ViewModel.AddTodo();
            if (res)
            {
                todoList.ScrollIntoView(ViewModel.Todos.Last());
            }
        }

        /// <summary>
        /// Click event handler for MenuFlyoutItem used for deleting to-dos.
        /// </summary>
        /// <param name="sender">MenuFlyoutItem used for deleting to-dos.</param>
        /// <param name="e">Event arguments.</param>
        private async void DeleteTodoButton_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem menuFlyoutItem = (MenuFlyoutItem)sender;
            await ViewModel.DeleteTodo(menuFlyoutItem.DataContext as Todo);
        }

        /// <summary>
        /// Grid RightTapped event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void Grid_RightTapped(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            MenuFlyout attachedFlyout = (MenuFlyout)FlyoutBase.GetAttachedFlyout(element);
            attachedFlyout.ShowAt(element, e.GetPosition(element));
        }

        /// <summary>
        /// Click event handler for to-do check box.
        /// </summary>
        /// <param name="sender">To-do check box.</param>
        /// <param name="e">Event arguments.</param>
        private async void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            Todo todo = (Todo)checkBox.DataContext;
            todo.IsCompleted = (bool)checkBox.IsChecked;
            await ViewModel.UpdateTodo(todo);
        }

        /// <summary>
        /// Event handler for to-do list ListView SelectionChanged event.
        /// </summary>
        /// <param name="sender">To-do list ListView.</param>
        /// <param name="e">Event parameters.</param>
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (todoList.SelectedItem != null)
            {
                _navigationService.Navigate(typeof(TodoDetailsPage), todoList.SelectedItem as Todo, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
                todoList.SelectedItem = null;
            }
        }

        /// <summary>
        /// Event handler for KeyDown event on a TextBox for adding to-dos.
        /// </summary>
        /// <param name="sender">TextBox for adding to-dos.</param>
        /// <param name="e">Event parameters.</param>
        private async void TextBox_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && !string.IsNullOrEmpty(todoNameTextBox.Text))
            {
                ViewModel.TodoName = todoNameTextBox.Text;
                bool res = await ViewModel.AddTodo();
                if (res)
                {
                    todoNameTextBox.Text = "";
                    todoList.ScrollIntoView(ViewModel.Todos.Last());
                }
            }
        }

        /// <summary>
        /// Click event handler for MenuFlyoutItem for navigating to to-do details.
        /// </summary>
        /// <param name="sender">MenuFlyoutItem for navigating to to-do details.</param>
        /// <param name="e">Event arguments.</param>
        private void ViewDetailsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem menuFlyoutItem = (MenuFlyoutItem)sender;
            _navigationService.Navigate(typeof(TodoDetailsPage), menuFlyoutItem.DataContext as Todo, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        /// <summary>
        /// Event handler for PropertyChanged event.
        /// </summary>
        /// <param name="sender">View model class of this page.</param>
        /// <param name="e">Event parameters.</param>
        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("IsNotificationDisplayed"))
            {
                notification.Show(ViewModel.NotificationContent, "#FF7E7E");
            }
        }
    }
}