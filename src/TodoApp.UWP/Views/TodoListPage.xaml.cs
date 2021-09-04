using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TodoApp.Services;
using TodoApp.Shared.Models.Entity;
using TodoApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace TodoApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TodoListPage : Page
    {
        private readonly NavigationService _navigationService;

        public TodoListPage()
        {
            InitializeComponent();
            _navigationService = App.Services.GetRequiredService<NavigationService>();
            DataContext = App.Services.GetRequiredService<TodoListPageViewModel>();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        public TodoListPageViewModel ViewModel => (TodoListPageViewModel)DataContext;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            todoList.SelectedIndex = -1;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _navigationService.Navigate(typeof(TodoDetailsPage), todoList.SelectedItem as Todo, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
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
