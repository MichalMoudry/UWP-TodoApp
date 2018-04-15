using System.Linq;
using TodoApp.Helpers;
using TodoApp.Models.Database;
using TodoApp.Services;
using TodoApp.ViewModels.Database;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

namespace TodoApp.Views.Pages
{
    /// <summary>
    /// Prázdná stránka, která se dá použít samostatně nebo se na ni dá přejít v rámci
    /// </summary>
    public sealed partial class ListsPage : Page
    {
        public ListsPage()
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
            pageTitle.Text = ResourceLoaderHelper.GetResourceLoader().GetString("TodoLists");
            pageSymbol.Glyph = "\uE179";
        }

        /// <summary>
        /// AppBarButton Click event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            AppBarButton button = (AppBarButton)sender;
            if (button.Name.Equals("selectButton"))
            {
                ChangeSelectionMode();
            }
            else if (button.Name.Equals("addButton"))
            {
                DisplayNotification(await DialogService.Instance().ShowListDialogAsync());
            }
            else if (button.Name.Equals("sortButton"))
            {
                FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            }
            else if (button.Name.Equals("deleteButton"))
            {
                ContentDialogResult result = await DialogService.Instance().ShowDeleteDialogAsync(ResourceLoaderHelper.GetResourceLoader().GetString("DialogDeleteLists"));
                if (result.Equals(ContentDialogResult.Primary) && lists.SelectedItems.Count > 0)
                {
                    await ListViewModel.Instance().DeleteLists(lists.SelectedItems.ToList());
                }
                ChangeSelectionMode();
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
                inAppNotification.Show($"{ResourceLoaderHelper.GetResourceLoader().GetString("AddListNotificationContent")}", "#205624");
            }
            else if (dialogResult.Equals(ContentDialogResult.None))
            {
                inAppNotification.Show($"{ResourceLoaderHelper.GetResourceLoader().GetString("AddError")}", "#ad2929");
            }
        }

        /// <summary>
        /// Method for changing selection mode of ListView.
        /// </summary>
        private void ChangeSelectionMode()
        {
            lists.SelectedItem = null;
            if (lists.SelectionMode.Equals(ListViewSelectionMode.Single))
            {
                lists.SelectionMode = ListViewSelectionMode.Multiple;
                selectButton.Label = ResourceLoaderHelper.GetResourceLoader().GetString("Cancel");
                selectButton.Icon = new SymbolIcon(Symbol.ClearSelection);
                ToolTipService.SetToolTip(selectButton, selectButton.Label);
                separator.Visibility = Visibility.Visible;
                deleteButton.Visibility = Visibility.Visible;
            }
            else if (lists.SelectionMode.Equals(ListViewSelectionMode.Multiple))
            {
                lists.SelectionMode = ListViewSelectionMode.Single;
                selectButton.Label = ResourceLoaderHelper.GetResourceLoader().GetString("Select");
                selectButton.Icon = new SymbolIcon(Symbol.Bullets);
                ToolTipService.SetToolTip(selectButton, selectButton.Label);
                separator.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// List SelectionChanged event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void Lists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lists.SelectionMode.Equals(ListViewSelectionMode.Single) && lists.SelectedItem != null)
            {
                Frame.Navigate(typeof(TodoPage), (List)lists.SelectedItem);
                lists.SelectedItem = null;
            }
        }

        /// <summary>
        /// Method for setting localized values to XAML elements.
        /// </summary>
        private void LocalizeText()
        {
            addButton.Label = ResourceLoaderHelper.GetResourceLoader().GetString("Add");
            ToolTipService.SetToolTip(addButton, addButton.Label);
            sortButton.Label = ResourceLoaderHelper.GetResourceLoader().GetString("Sort");
            ToolTipService.SetToolTip(sortButton, sortButton.Label);
            selectButton.Label = ResourceLoaderHelper.GetResourceLoader().GetString("Select");
            ToolTipService.SetToolTip(selectButton, selectButton.Label);
            deleteButton.Label = ResourceLoaderHelper.GetResourceLoader().GetString("Delete");
            ToolTipService.SetToolTip(deleteButton, deleteButton.Label);
        }

        /// <summary>
        /// PageLoaded event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lists.ItemsSource = ListViewModel.Instance().GetListsAsObservable();
        }

        /// <summary>
        /// SortButton Click event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = (MenuFlyoutItem)sender;
            if (item.Name.Equals("sortByNameButton"))
            {
                ListViewModel.Instance().SortLists("Name");
            }
            else if (item.Name.Equals("sortByDateButton"))
            {
                ListViewModel.Instance().SortLists("Date");
            }
        }
    }
}