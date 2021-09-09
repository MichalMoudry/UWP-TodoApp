using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TodoApp.Controls
{
    /// <summary>
    /// UserControl with content for a dialog window for sorting to-do list.
    /// </summary>
    public sealed partial class SortListControl : UserControl
    {
        public SortListControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Property with an attribute which will be used for sorting.
        /// </summary>
        public string SortProperty { get; set; }

        /// <summary>
        /// Event handler for RadioButton's Click event.
        /// </summary>
        /// <param name="sender">Any RadioButton in this UserControl.</param>
        /// <param name="e">Event arguments.</param>
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            SortProperty = (sender as RadioButton).Name;
        }
    }
}