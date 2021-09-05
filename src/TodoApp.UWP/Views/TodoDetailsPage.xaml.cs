using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using TodoApp.ViewModels;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace TodoApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TodoDetailsPage : Page
    {
        public TodoDetailsPage()
        {
            InitializeComponent();
            Window.Current.SetTitleBar(AppTitleBar);
            DataContext = App.Services.GetRequiredService<TodoDetailsPageViewModel>();
            ApplicationView.GetForCurrentView().TitleBar.ButtonForegroundColor = Colors.Black;
            CompletionDateView.MinDate = System.DateTimeOffset.Now;
        }

        public TodoDetailsPageViewModel ViewModel => (TodoDetailsPageViewModel)DataContext;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.Todo = (Shared.Models.Entity.Todo)e.Parameter;
        }

        private void CompletionDateView_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine(sender.SelectedDates.First());
            CompletionDateButton.Flyout.Hide();
        }
    }
}