using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using TodoApp.Services;
using TodoApp.Shared.Models.Entity;
using TodoApp.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

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