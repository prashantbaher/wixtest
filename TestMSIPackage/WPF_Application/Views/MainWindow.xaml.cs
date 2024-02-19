using Microsoft.Win32;
using Prism.Events;
using Syncfusion.Windows.Shared;
using System;
using System.Windows;
using WPF_Application.Services;
using WPF_Application.ViewModels;

namespace WPF_Application.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ChromelessWindow
    {
        private readonly IEventAggregator eventAggregator;

        public MainWindow(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<BrowseFileDialogService>().Subscribe(BrowseFile);
            this.eventAggregator.GetEvent<ConfirmationMessageService>().Subscribe(ConfirmationMessage);
            this.eventAggregator.GetEvent<InformationMessageService>().Subscribe(InformationMessage);
            this.eventAggregator.GetEvent<ErrorMessageService>().Subscribe(ErrorMessage);
        }

        private void BrowseFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Solidworks Part|*.sldprt|Solidworks Assembly|*.sldasm|Solidworks Drawing|*.slddrw";
            openFileDialog.DefaultExt = "*.sldprt";

            bool? result = openFileDialog.ShowDialog();

            if (result == false || string.IsNullOrEmpty(openFileDialog.FileName))
                return;

            var viewModel = DataContext as MainWindowViewModel;
            viewModel.FilePath = openFileDialog.FileName;
        }

        private void ConfirmationMessage(string messageToShow)
        {
            var result = MessageBox.Show(messageToShow, "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            var viewModel = DataContext as MainWindowViewModel;
            viewModel.DeleteFile = (result == MessageBoxResult.Yes) ? true : false;
        }

        private void InformationMessage(string messageToShow)
        {
            MessageBox.Show(messageToShow, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ErrorMessage(string messageToShow)
        {
            MessageBox.Show(messageToShow, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
