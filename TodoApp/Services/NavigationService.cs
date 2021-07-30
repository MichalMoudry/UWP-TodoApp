using TodoApp.Views;
using Windows.UI.Xaml.Controls;

namespace TodoApp.Services
{
    public class NavigationService
    {
        public Frame Frame { get; set; }

        public void NavigateToTodoListPage()
        {
            _ = Frame.Navigate(typeof(TodoListPage));
        }
    }
}