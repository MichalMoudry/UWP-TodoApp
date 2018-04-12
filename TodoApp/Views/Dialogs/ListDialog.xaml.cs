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
using TodoApp.Helpers;
using TodoApp.ViewModels;
using TodoApp.Models.Database;

namespace TodoApp.Views.Dialogs
{
    public sealed partial class ListDialog : ContentDialog
    {
        public ListDialog()
        {
            InitializeComponent();
            LocalizeText();
        }

        private void LocalizeText()
        {
            listNameBoxLabel.Text = ResourceLoaderHelper.GetResourceLoader().GetString("ListName");
            Title = ResourceLoaderHelper.GetResourceLoader().GetString("AddList");
            PrimaryButtonText = ResourceLoaderHelper.GetResourceLoader().GetString("Add");
            SecondaryButtonText = ResourceLoaderHelper.GetResourceLoader().GetString("Cancel");
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (ValidateForm())
            {
                ListViewModel.Instance().AddList(new List() { Name = listNameBox.Text });
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrEmpty(listNameBox.Text).Equals(false))
            {
                return true;
            }
            return false;
        }
    }
}
