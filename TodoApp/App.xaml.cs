using System;
using TodoApp.Views.Pages;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models.Database;

namespace TodoApp
{
    /// <summary>
    /// Poskytuje chování specifické pro aplikaci, které doplňuje výchozí třídu Application.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Inicializuje objekt aplikace typu singleton. Jedná se o první řádek spuštěného vytvořeného kódu,
        /// který je proto logickým ekvivalentem metod main() nebo WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;

            using (DatabaseContext db = new DatabaseContext())
            {
                db.Database.Migrate();
            }
        }

        /// <summary>
        /// Vyvolá se při normálním spuštění aplikace koncovým uživatelem. Ostatní vstupní body
        /// se použijí například při spuštění aplikace za účelem otevření konkrétního souboru.
        /// </summary>
        /// <param name="e">Podrobnosti o žádosti o spuštění a procesu</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
            {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;
                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                }
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(ShellPage), e.Arguments);
                }
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Vyvolá se, když selže přechod na určitou stránku.
        /// </summary>
        /// <param name="sender">Objekt Frame, u kterého selhala navigace</param>
        /// <param name="e">Podrobnosti o chybě navigace</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Vyvolá se při pozastavení běhu aplikace. Stav aplikace se uloží
        /// bez informace, jestli se aplikace ukončí nebo obnoví s obsahem
        /// stále neporušené paměti.
        /// </summary>
        /// <param name="sender">Zdroj žádosti o pozastavení</param>
        /// <param name="e">Podrobnosti žádosti o pozastavení</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
    }
}