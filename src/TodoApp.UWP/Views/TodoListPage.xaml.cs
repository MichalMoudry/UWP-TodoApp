using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TodoApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.ComponentModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TodoApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TodoListPage : Page
    {
        public TodoListPage()
        {
            InitializeComponent();
            DataContext = App.Services.GetRequiredService<TodoListPageViewModel>();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
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

        public TodoListPageViewModel ViewModel => (TodoListPageViewModel)DataContext;
    }
}
