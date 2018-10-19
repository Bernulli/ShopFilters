using DBLibrary.EF.Context;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;
using WPFApp.ViewModel;
using System.Collections.Generic;
using DBLibrary.EF.Models;

namespace WPFApp.UIElements
{
    class AddExpanders
    {
        /// <summary>
        /// This list contains values of checkboxes which are checked at the moment.
        /// It helps us to take only checked objects from database and load them to main page.
        /// </summary>
        public static List<int> whatCompanyChecked = new List<int>();
        public static List<string> whatModelChecked = new List<string>();

        /// <summary>
        /// This state of checkbox is needed to MainWindowViewModel
        /// </summary>
        public static bool ifCompanyChecked = false;
        public static bool ifNameChecked = false;

        #region EventHandlers
        /// <summary>
        /// EventHandlers for adding or removing values of checked checkbox to List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Ch_UncheckedCompany(object sender, RoutedEventArgs e)
        {
            CheckBox ch = sender as CheckBox;
            ifCompanyChecked = false;

            if (ch.IsChecked == false)
            {
                whatCompanyChecked.Remove((int)ch.Tag);
            }
        }

        private static void Ch_CheckedCompany(object sender, RoutedEventArgs e)
        {
            CheckBox ch = sender as CheckBox;
            ifCompanyChecked = true;

            if (ch.IsChecked == true)
            {
                whatCompanyChecked.Add((int)ch.Tag);
            }
        }

        private static void Ch_UncheckedModel(object sender, RoutedEventArgs e)
        {
            CheckBox ch = sender as CheckBox;
            ifNameChecked = false;
            ifCompanyChecked = false;

            if (ch.IsChecked == false)
            {
                whatModelChecked.Remove((string)ch.Tag);
            }
        }

        private static void Ch_CheckedModel(object sender, RoutedEventArgs e)
        {
            CheckBox ch = sender as CheckBox;
            ifNameChecked = true;
            ifCompanyChecked = true;

            if (ch.IsChecked == true)
            {
                whatModelChecked.Add((string)ch.Tag);
            }
        }
        #endregion

        #region Dynamically created checkboxes. Their count depends on records in database
        public static StackPanel CreateCompanyCheckBox()
        {
            using (ShopFilterEntity db = new ShopFilterEntity())
            {
                var phone = db.Phones.Include(c => c.Company).ToList();
                var company = db.Companies.ToList();
                
                StackPanel expanderPanel = new StackPanel();

                foreach (var c in company)
                {
                    CheckBox ch = new CheckBox();
                    ch.Content = c.Name;
                    ch.Command = new MainWindowViewModel().Filter;
                    ch.Checked += Ch_CheckedCompany;
                    ch.Unchecked += Ch_UncheckedCompany;
                    ch.Tag = c.Id;
                    ch.Margin = new Thickness(5);
                    expanderPanel.Children.Add(ch);
                }

                return expanderPanel;
            }
        }

        public static StackPanel CreateNameCheckBox()
        {
            using (ShopFilterEntity db = new ShopFilterEntity())
            {
                var ph = db.Phones.ToList();
                StackPanel expanderPanel = new StackPanel();

                foreach (var c in ph)
                {
                    CheckBox ch = new CheckBox();
                    ch.Content = c.Name;
                    ch.Command = new MainWindowViewModel().Filter;
                    ch.IsChecked = false;
                    ch.Checked += Ch_CheckedModel;
                    ch.Unchecked += Ch_UncheckedModel;
                    ch.Tag = c.Name;
                    ch.Margin = new Thickness(5);
                    expanderPanel.Children.Add(ch);
                }

                return expanderPanel;
            }
        }

        //public static StackPanel CreatePriceCheckBox()
        //{
        //    using (ShopFilterEntity db = new ShopFilterEntity())
        //    {
        //        var phone = db.Phones.Select(n => n.Price).ToList();

        //        StackPanel expanderPanel = new StackPanel();

        //        foreach (var c in phone)
        //        {
        //            CheckBox ch = new CheckBox();
        //            ch.Content = c.ToString();
        //            //ch.Command = new MainWindowViewModel().Filter;
        //            ch.Margin = new Thickness(5);
        //            expanderPanel.Children.Add(ch);
        //        }

        //        return expanderPanel;
        //    }
        //}
        #endregion
    }
}
