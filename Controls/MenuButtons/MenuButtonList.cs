using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VoterX.Utilities.Controls
{
    public class MenuButtonList
    {
        private List<IMenuButtonViewModel> _buttonsList;

        public MenuButtonList()
        {
            _buttonsList = new List<IMenuButtonViewModel>();
        }

        public void Add(IMenuButtonViewModel ButtonModel)
        {
            _buttonsList.Add(ButtonModel);
        }

        public int? Count()
        {
            if (_buttonsList == null)
            {
                return null;
            }
            else
            {
                return _buttonsList.Count();
            }
        }

        public List<MenuButton> GetButtons()
        {
            if (_buttonsList != null)
            {
                // Create empty list
                List<MenuButton> list = new List<Controls.MenuButton>();

                //int index = 0;

                // Loop through button view models
                foreach (var item in _buttonsList)
                {
                    // Create default button
                    var button = new MenuButton();

                    // Apply the view model
                    button.DataContext = item;

                    // And a margin to all but the first button (may want to include this as part of the view model)
                    //if (index > 0)
                    //{
                    //    button.Margin = new Thickness(0, 5, 0, 0);
                    //}

                    // Dynamic button list will never be the first item
                    button.Margin = new Thickness(0, 5, 0, 0);

                    // Add the new buttont to the list
                    list.Add(button);

                    //index++;
                }
                return list;
            }
            else
            {
                return null;
            }
        }
    }
}
