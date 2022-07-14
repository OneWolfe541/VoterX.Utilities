using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VoterX.Utilities.Commands;
using VoterX.Utilities.Extensions;
using VoterX.Utilities.Factories;
using VoterX.Utilities.Models;

namespace VoterX.Utilities.Views
{
    public class WizardSelectorViewModel : NotifyPropertyChanged
    {
        public WizardSelectorViewModel()
        {
            IsEnabled = true;
        }

        // Turn on Category drop down
        public bool UseCategories;

        #region DisplayMethods
        // Header label for the control
        private string _listLabel;
        public string ListLabel
        {
            get
            {
                return _listLabel;
            }
            set
            {
                _listLabel = value;
                RaisePropertyChanged("ListLabel");
            }
        }

        // Text display on the Clear Button
        private string _clearLabel;
        public string ClearLabel
        {
            get
            {
                return _clearLabel;
            }
            set
            {
                _clearLabel = value;
                RaisePropertyChanged("ClearLabel");
            }
        }

        // Tool Tip message on the Category drop down
        private string _categoryToolTip;
        public string CategoryToolTip
        {
            get
            {
                return _categoryToolTip;
            }
            set
            {
                _categoryToolTip = value;
                RaisePropertyChanged("CategoryToolTip");
            }
        }
        #endregion

        #region ListMethods
        // Store the data type of the list
        private Type _listType;
        private Type _itemType;

        // List of items for display
        private ObservableCollection<WizardSelectorItem> _itemsList;
        public ObservableCollection<WizardSelectorItem> ItemsList
        {
            get
            {
                // Check if using the Category drop down to filter the list
                if (SelectedCategoryItem != null && UseCategories == true && _itemsList != null)
                {
                    // Get the filtered list from the selected Category
                    return new ObservableCollection<WizardSelectorItem> (_itemsList.Where(c => c.Category == SelectedCategoryItem.Category).ToList());
                }
                else
                {
                    // When no category is selected display an empty list
                    if (UseCategories == true)
                    {
                        return null;
                    }
                    else
                    {
                        return _itemsList;
                    }
                }
            }

            set
            {
                _itemsList = value;
            }
        }

        // Get a list of selected items
        public List<T> GetList<T>()
        {
            // Check for empty list
            if(ItemsList != null)
            {
                // Get only selected items
                var selectedItems = ItemsList.Where(i => i.IsSelected == true);
                if (selectedItems != null)
                {
                    List<T> newList = new List<T>();

                    // Loop through selected items
                    foreach (var item in selectedItems)
                    {
                        // Convert the value string to given type
                        var newItem = item.Value.Convert<T>();

                        // Add new item to the list
                        newList.Add(newItem);
                    }

                    return newList;
                }
                else
                {
                    // If there are no selected items return null
                    return null;
                }
            }
            else
            {
                // If the list is empty return null
                return null;
            }
        }

        // Convert the given list into a workable object for display
        public void LoadList<T>(List<T> listData)
        {
            // Get the data type of the list
            _listType = listData.GetType();
            _itemType = typeof(T);

            //// FACTORY METHOD
            //// Create new factory
            //WizardSelectorFactory factory = new WizardSelectorFactory();

            //// Convert list of given type
            //_itemsList = new ObservableCollection<WizardSelectorItem>(factory.Create<T>(listData));

            // EXTENSION METHOD
            // Convert list of given type
            _itemsList = new ObservableCollection<WizardSelectorItem>(listData.ToWizardSelectors<T>());
        }

        // Load and set lists of ints
        public void LoadList<T>(List<T> listData, List<int> items)
        {
            LoadList<T>(listData);

            SetSelectedItems<int>(items);
        }

        // Load and set lists of strings
        public void LoadList<T>(List<T> listData, List<string> items)
        {
            LoadList<T>(listData);

            SetSelectedItems<string>(items);
        }

        // Set selected items in the list
        public void SetSelectedItems<T>(List<T> items)
        {
            // Convert generic list to a list of strings
            List<string> selectedList = items.ConvertAll<string>(delegate (T i) { return i.ToString(); });
            
            // Set IsSelected in all main list items from the given list
            _itemsList.Where(i => selectedList.Contains(i.Value)).ToList().ForEach(i => i.IsSelected = true);
        }

        // Manage the selected item
        private WizardSelectorItem _selectedItem;
        public WizardSelectorItem SelectedItem
        {
            get
            {
                return _selectedItem;
            }

            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        // Manage the selection changed event
        public RelayCommand _selectionChangedCommand;
        public ICommand SelectionChangedCommand
        {
            get
            {
                if (_selectionChangedCommand == null)
                {
                    _selectionChangedCommand = new RelayCommand(param => this.SelectionChangedClick());
                }
                return _selectionChangedCommand;
            }
        }

        // Force the list display to update
        private void SelectionChangedClick()
        {
            RaisePropertyChanged("ItemsList");
        }
        #endregion

        #region ClearCommand
        // Bound commad for clearing the displayed list
        public RelayCommand _clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                if (_clearCommand == null)
                {
                    _clearCommand = new RelayCommand(param => this.ClearClick());
                }
                return _clearCommand;
            }
        }

        // Clear the displayed list
        private void ClearClick()
        {
            if(_itemsList != null)
            {
                _itemsList.ToList().ForEach(i => i.IsSelected = false);
            }
            RaisePropertyChanged("ItemsList");

            SelectedItem = null;
            RaisePropertyChanged("SelectedItem");
        }
        #endregion

        #region Categories
        // List of categories for display
        private ObservableCollection<WizardSelectorCategory> _categoryList;
        public ObservableCollection<WizardSelectorCategory> CategoryList
        {
            get
            {
                // Check for an empty list
                if(_categoryList == null && _itemsList != null)
                {
                    // Get all the distinct categories
                    var categories = _itemsList.Select(c => new
                    {
                        c.Category
                    }).Distinct().ToList();

                    if (categories != null)
                    {
                        // Convert the list of distinct categories to an Observable Collection
                        _categoryList = new ObservableCollection<WizardSelectorCategory>(
                                        categories
                                        .Select(c => new WizardSelectorCategory { Category = c.Category.ToUpper() })
                                        .OrderBy(c => c.Category)
                                        .ToList());
                    }
                }
                return _categoryList;
            }

            private set
            {
                _categoryList = value;
                RaisePropertyChanged("CategoryList");
            }
        }

        // Manage the selected category
        private WizardSelectorCategory _selectedCategoryItem;
        public WizardSelectorCategory SelectedCategoryItem
        {
            get
            {
                // Get selected category
                if (_categoryList != null)
                {
                    var category = _itemsList.Where(i => i.IsSelected == true).FirstOrDefault();
                    if (category != null)
                    {
                        _selectedCategoryItem = _categoryList.Where(c => c.Category == category.Category).FirstOrDefault();
                    }
                }
                return _selectedCategoryItem;
            }

            set
            {
                _selectedCategoryItem = value;
                //RaisePropertyChanged("SelectedCategoryItem");
                RaisePropertyChanged("ItemsList");
            }
        }

        // Manage the selection changed event
        public RelayCommand _selectedCategoryChangedCommand;
        public ICommand SelectedCategoryChangedCommand
        {
            get
            {
                if (_selectedCategoryChangedCommand == null)
                {
                    _selectedCategoryChangedCommand = new RelayCommand(param => this.SelectedCategoryChangedClick());
                }
                return _selectedCategoryChangedCommand;
            }
        }

        // Enable or Disable the entire control
        private void SelectedCategoryChangedClick()
        {
            RaisePropertyChanged("ItemsList");
        }
        #endregion

        #region IsEnabledMethods
        private bool _isEnabled;
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                RaisePropertyChanged("IsEnabled");
            }
        }
        #endregion
    }
}
