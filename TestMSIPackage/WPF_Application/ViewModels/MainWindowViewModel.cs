using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WPF_Application.Services;

namespace WPF_Application.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private string _FilePath;
        private readonly IEventAggregator eventAggregator;

        public string FilePath
        {
            get { return _FilePath; }
            set { SetProperty(ref _FilePath, value); }
        }

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        private DelegateCommand _BrowseFileCommand;
        public DelegateCommand BrowseFileCommand =>
            _BrowseFileCommand ?? (_BrowseFileCommand = new DelegateCommand(ExecuteBrowseFileCommand));

        void ExecuteBrowseFileCommand()
        {
            this.eventAggregator.GetEvent<BrowseFileDialogService>().Publish();
        }

        private DelegateCommand _ClickCommand;
        public DelegateCommand ClickCommand =>
            _ClickCommand ?? (_ClickCommand = new DelegateCommand(ExecuteClickCommand));

        private bool _DeleteFile;
        public bool DeleteFile
        {
            get { return _DeleteFile; }
            set { SetProperty(ref _DeleteFile, value); }
        }

        async void ExecuteClickCommand()
        {
            // Check if we browsed a file
            if (string.IsNullOrEmpty(FilePath))
            {
                // Show that file is not browsed
                this.eventAggregator.GetEvent<ErrorMessageService>().Publish("Empty File Path.");
                return;
            }

            // Check if browsed file exist or not
            if (File.Exists(FilePath) == false)
            {
                // Show that file is not browsed
                this.eventAggregator.GetEvent<ErrorMessageService>().Publish($"[{FilePath}] did not exist.");
                return;
            }

            // Get confirmation from user
            this.eventAggregator.GetEvent<ConfirmationMessageService>().Publish($"Do you want to delete [{FilePath}]?");

            // Not confirmed then clear data and exit function
            if (!DeleteFile)
            {
                ClearData();
                return;
            }

            // Show busy indicator
            IsBusy = true;

            await Task.Run(() =>
            {
                // Delete selected file.
                File.Delete(FilePath);
            });

            // Hide busy indicator
            IsBusy = false;

            // Show information that file is deleted
            this.eventAggregator.GetEvent<InformationMessageService>().Publish("File successfully deleted.");

            // Clear data
            ClearData();
        }

        /// <summary>
        /// Method for clearing <see cref="FilePath"/> property
        /// </summary>
        private void ClearData() => FilePath = string.Empty;
    }
}
