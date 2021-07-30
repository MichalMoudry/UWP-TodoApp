using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.ViewModels;
using TodoApp.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TodoApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShellPage : Page
    {
        public ShellPage()
        {
            InitializeComponent();
            DataContext = App.Services.GetRequiredService<ShellPageViewModel>();
            App.Services.GetRequiredService<NavigationService>().Frame = rootFrame;
        }

        public ShellPageViewModel ViewModel => (ShellPageViewModel)DataContext;
    }
}
