using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GongSolutions.Wpf.DragDrop;
using Swd.PlayCollector.Business;
using Swd.PlayCollector.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using static System.Net.WebRequestMethods;

namespace Swd.PlayCollector.Gui.Wpf.ViewModel
{
    public partial class fMainViewModel : ObservableObject, IDropTarget
    {
        // fields

        private string _searchValue;
        private string _statusBarText;
        private CollectionItem _selectedCollectionItem;
        private ObservableCollection<CollectionItem> _collectionItemsList;
        private ObservableCollection<Location> _locationList;
        private ObservableCollection<Theme> _themeList;
        private ICollectionView _collectionItemsView;



        public string SearchValue
        {
            get { return _searchValue; }
            set
            {
                SetProperty(ref _searchValue, value);
                SearchForCollectionItem();
            }
        }

        public string StatusBarText
        {
            get { return GetStatusBarText(); }
            set { SetProperty(ref _statusBarText, value); }
        }

        public CollectionItem SelectedCollectionItem
        {
            get { return _selectedCollectionItem; }
            set
            {
                SetProperty(ref _selectedCollectionItem, value);
                this.DeleteCollectionItemCommand.NotifyCanExecuteChanged();
                this.SaveCollectionItemCommand.NotifyCanExecuteChanged();
            }
        }

        public ObservableCollection<CollectionItem> CollectionItemsList
        {
            get { return _collectionItemsList; }
            set { SetProperty(ref _collectionItemsList, value); }
        }

        public ObservableCollection<Location> LocationList
        {
            get { return _locationList; }
            set { SetProperty(ref _locationList, value); }
        }

        public ObservableCollection<Theme> ThemeList
        {
            get { return _themeList; }
            set { SetProperty(ref _themeList, value); }
        }

        public ICollectionView CollectionItemsView
        {
            get { return _collectionItemsView; }
            set { SetProperty(ref _collectionItemsView, value); }
        }

        public IAsyncRelayCommand ExitCommand { get; }
        public IAsyncRelayCommand ImportCommand { get; }

        public IAsyncRelayCommand SearchForCollectionItemCommand { get; }
        public IAsyncRelayCommand AddCollectionItemCommand { get; }
        public IAsyncRelayCommand DeleteCollectionItemCommand { get; }
        public IAsyncRelayCommand SaveCollectionItemCommand { get; }

        public fMainViewModel()
        {
            this.StatusBarText = string.Empty;
            LoadDataAsync();


            ExitCommand = new AsyncRelayCommand(Exit, CanExitExecuted);
            ImportCommand = new AsyncRelayCommand(Import, CanImportExecuted);
            SearchForCollectionItemCommand = new AsyncRelayCommand(SearchForCollectionItem);
            AddCollectionItemCommand = new AsyncRelayCommand(AddCollectionItem, CanAddCollecionItemExecuted);
            DeleteCollectionItemCommand = new AsyncRelayCommand(DeleteCollectionItem, CanDeleteCollecionItemExecuted);
            SaveCollectionItemCommand = new AsyncRelayCommand(SaveForCollectionItem, CanSaveCollecionItemExecuted);

            CollectionItemsView = CollectionViewSource.GetDefaultView(CollectionItemsList);
        }

        private async Task LoadDataAsync()
        {
            CollectionItemService collectionItemService = new();
            LocationService locationService = new();
            ThemeService themeService = new();

            //Task<IQueryable<CollectionItem>> getCollectionItemTask = collectionItemService.GetAllAsync();
            Task<IQueryable<CollectionItem>> getCollectionItemTask = collectionItemService.GetAllInklusiveAsync();
            Task<IQueryable<Location>> getLocationTask = locationService.GetAllAsync();
            Task<IQueryable<Theme>> getThemeTask = themeService.GetAllAsync();

            this.CollectionItemsList = new ObservableCollection<CollectionItem>(await getCollectionItemTask);
            this.LocationList = new ObservableCollection<Location>(await getLocationTask);
            this.ThemeList = new ObservableCollection<Theme>(await getThemeTask);
        }

        private async Task SearchForCollectionItem()
        {
            CollectionItemsView = CollectionViewSource.GetDefaultView(CollectionItemsList);
            CollectionItemsView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            CollectionItemsView.Filter = CollectionItemFilter;
        }

        // Can execute methods

        private bool CanExitExecuted()
        {

            return false;

        }

        private bool CanImportExecuted()
        {

            return false;

        }

        private bool CanAddCollecionItemExecuted()
        {
            return true;
        }

        private bool CanSaveCollecionItemExecuted()
        {
            return SelectedCollectionItem != null;
        }

        private bool CanDeleteCollecionItemExecuted()
        {
            return SelectedCollectionItem != null;
        }

        // async methods

        private async Task Exit()
        {



        }



        private async Task Import()
        {



        }



        private async Task AddCollectionItem()
        {

            this.SelectedCollectionItem = new CollectionItem(true);

        }


        private async Task DeleteCollectionItem()
        {
            CollectionItem item = this.SelectedCollectionItem;
            if (item != null)
            {
                CollectionItemService service = new CollectionItemService();
                await service.DeleteAsync(item.Id);
                await LoadDataAsync();
                this.SelectedCollectionItem = CollectionItemsList.FirstOrDefault();
            }
        }

        private async Task SaveForCollectionItem()
        {
            CollectionItemService service = new CollectionItemService();
            CollectionItem item = this.SelectedCollectionItem;
            if (item.Id == 0)
            {
                item.CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                await service.AddAsync(item);
            }
            else
            {
                item.UpdatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                await service.UpdateAsync(item);
            }
            await LoadDataAsync();
        }

        private bool CollectionItemFilter(object item)
        {
            CollectionItem collectionitem = item as CollectionItem;
            return collectionitem.Name.Contains(this.SearchValue, StringComparison.OrdinalIgnoreCase); // https://learn.microsoft.com/en-us/dotnet/standard/base-types/best-practices-strings
        }

        private string GetStatusBarText()
        {
            return string.Format($"{this.CollectionItemsList.Count} item(s) found.");
        }

        public void DragOver(IDropInfo dropInfo)
        {
            if (this.SelectedCollectionItem != null)
            {
                var dragFileList = ((DataObject)dropInfo.Data).GetFileDropList().Cast<string>();
                dropInfo.Effects = dragFileList.Any(item =>
                {
                    var extension = Path.GetExtension(item);
                    return extension != null && extension.Equals(".png");
                }) ? DragDropEffects.Copy : DragDropEffects.None;
            }
            else
            {
                dropInfo.Effects = DragDropEffects.None;
            }

        }

        public async void Drop(IDropInfo dropInfo)
        {
            if (this.SelectedCollectionItem != null)
            {
                var dragFileList = ((DataObject)dropInfo.Data).GetFileDropList().Cast<string>();
                dropInfo.Effects = dragFileList.Any(item =>
                {
                    var extension = Path.GetExtension(item);
                    return extension != null && extension.Equals(".png");
                }) ? DragDropEffects.Copy : DragDropEffects.None;

                CollectionItemService service = new CollectionItemService();
                await service.AddMediaItems(dragFileList, SelectedCollectionItem);
            }
            else
            {
                dropInfo.Effects = DragDropEffects.None;
            }

        }

    }
}
