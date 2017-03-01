using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Minesweep
{
    public class MenuItemGroupNameExtension:
        DependencyObject
    {
        private static Dictionary<string, HashSet<MenuItem>> Groups =
            new Dictionary<string, HashSet<MenuItem>>();

        public static string GetGroupName(DependencyObject obj)
        {
            return (string)obj.GetValue(GroupNameProperty);
        }

        public static void SetGroupName(DependencyObject obj, string value)
        {
            obj.SetValue(GroupNameProperty, value);
        }

        // Using a DependencyProperty as the backing store for GroupName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupNameProperty =
            DependencyProperty.RegisterAttached(
                "GroupName",
                typeof(string),
                typeof(MenuItemGroupNameExtension),
                new PropertyMetadata(string.Empty, OnGroupNameChanged));

        private static void AddMenuItemToGroup(MenuItem item, string name)
        {
            if(!string.IsNullOrEmpty(name))
            {
                HashSet<MenuItem> group;
                var success = Groups.TryGetValue(name, out group);
                if(success)
                {
                    if(!group.Add(item))
                    {
                        throw new ArgumentException("MenuItem already in group");
                    }
                }
                else
                {
                    Groups[name] = new HashSet<MenuItem>() { item };
                }
                item.Checked += MenuItemChecked;
            }
        }

        private static void RemoveMenuItemFromGroup(MenuItem item, string name)
        {
            try
            {
                var group = Groups[name];
                var success = group.Remove(item);
                if(!success)
                {
                    throw new ArgumentException("MenuItem not found in group");
                }
                if(group.Count == 0)
                {
                    Groups.Remove(name);
                }
            }
            catch(KeyNotFoundException ex)
            {
                throw new ArgumentException("Group name does not exist", "item", ex);
            }
            item.Checked -= MenuItemChecked;
        }

        

        private static void OnGroupNameChanged(DependencyObject db,
            DependencyPropertyChangedEventArgs e)
        {
            var menuItem = db as MenuItem;
            if(null != menuItem)
            {
                var oldName = e.OldValue as string;
                var newName = e.NewValue as string;
                if(oldName.Equals(newName))
                {
                    return;
                }
                if(!string.IsNullOrEmpty(oldName))
                {
                    RemoveMenuItemFromGroup(menuItem, oldName);
                }
                if(!string.IsNullOrEmpty(newName))
                {
                    AddMenuItemToGroup(menuItem, newName);
                }
            }
        }

        private static void MenuItemChecked(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var groupName = GetGroupName(menuItem);
            if(string.IsNullOrEmpty(groupName))
            {
                return;
            }
            try
            {
                foreach(var item in Groups[groupName])
                {
                    if(menuItem != item)
                    {
                        item.IsChecked = false;
                        item.IsCheckable = true;
                    }
                }
                menuItem.IsChecked = true;
                menuItem.IsCheckable = false;
            }
            catch(KeyNotFoundException ex)
            {
                throw new InvalidOperationException("Group name of menuitem not found", ex);
            }
        }
    }
}
