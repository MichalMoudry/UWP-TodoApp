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
    /// Page with to-do's details.
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

        /// <summary>
        /// Public property with a instance of TodoDetails page view model.
        /// </summary>
        public TodoDetailsPageViewModel ViewModel => (TodoDetailsPageViewModel)DataContext;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.Todo = (Shared.Models.Entity.Todo)e.Parameter;
        }

        /// <summary>
        /// SelectedDatesChanged event handler for a DateView for setting completion date.
        /// </summary>
        /// <param name="sender">DateView for setting completion date.</param>
        /// <param name="args">Event arguments.</param>
        private void CompletionDateView_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine(sender.SelectedDates.First());
        }

        /// <summary>
        /// Click event handler for a button for saving to-do note.
        /// </summary>
        /// <param name="sender">Button for saving to-do note.</param>
        /// <param name="e">Event arguments.</param>
        private void SaveNoteButton_Click(object sender, RoutedEventArgs e)
        {
            saveNoteGlyph.Glyph = "\uE9A1";
            saveNoteButtonText.Text = "Saved";
        }

        /// <summary>
        /// TextChanged event handler for a TextBox used for to-do notes.
        /// </summary>
        /// <param name="sender">TextBox used for to-do notes.</param>
        /// <param name="e">Event arguments.</param>
        private void TodoNoteTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            saveNoteGlyph.Glyph = "\uEB4A";
            saveNoteButtonText.Text = "Save note";
        }

        private async void SubTodoNameTextBox_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && !string.IsNullOrEmpty(subTodoNameTextBox.Text))
            {
                bool res = await ViewModel.AddSubTodo();
            }
        }
    }
}