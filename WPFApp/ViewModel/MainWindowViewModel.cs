using DBLibrary.EF.Context;
using DBLibrary.EF.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFApp.Infrastructure;
using WPFApp.Model;
using WPFApp.UIElements;
using System.Linq;
using System;

namespace WPFApp.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<Phone> myList = new ObservableCollection<Phone>();

        #region Binding to XAML
        private StackPanel company;
        public StackPanel Company
        {
            get
            {
                if (company == null)
                    company = AddExpanders.CreateCompanyCheckBox();
                return company;
            }
        }

        private StackPanel name;
        public StackPanel Name
        {
            get
            {
                if (name == null)
                    name = AddExpanders.CreateNameCheckBox();
                return name;
            }
            set
            {
                name = value;
            }
        }

        //private StackPanel price;
        //public StackPanel Price
        //{
        //    get
        //    {
        //        if (price == null)
        //            price = AddExpanders.CreatePriceCheckBox();
        //        return price;
        //    }
        //}

        private ObservableCollection<Phone> phones;
        public ObservableCollection<Phone> Phones
        {
            get
            {
                if (phones == null)
                    phones = new PhonesRepository().AllPhones;
                return phones;
            }
            set
            {
                phones = value;
            }
        }
        #endregion

        #region Command
        RelayCommand filter;
        public ICommand Filter
        {
            get
            {
                if (filter == null)
                    filter = new RelayCommand(ExecuteFilterCommand, CanExecuteFilterCommand);
                return filter;
            }
        }

        public bool CanExecuteFilterCommand(object parameter)
        {
            return true;
        }

        public void ExecuteFilterCommand(object parameter)
        {
            MainWindow mw = (MainWindow)Application.Current.MainWindow;

            if (AddExpanders.ifCompanyChecked)
            {
                CompanyFilterMethod(AddExpanders.whatCompanyChecked);
                ModelFilterMethod(AddExpanders.whatModelChecked);

                if (AddExpanders.whatCompanyChecked.Count > 0)
                {
                    mw.expanderListName.Content = UpdateModelCheckboxes(AddExpanders.whatCompanyChecked);
                }
                mw.phonesList.ItemsSource = myList;
            }
            else
            {
                if (AddExpanders.whatCompanyChecked.Count > 0)
                {
                    CompanyFilterMethod(AddExpanders.whatCompanyChecked);
                    mw.expanderListName.Content = UpdateModelCheckboxes(AddExpanders.whatCompanyChecked);
                    mw.phonesList.ItemsSource = myList;
                }
                else if (AddExpanders.whatCompanyChecked.Count == 0)
                {
                    if(AddExpanders.whatModelChecked.Count > 0)
                    {
                        ModelFilterMethod(AddExpanders.whatModelChecked);
                        mw.phonesList.ItemsSource = myList;
                    }
                    else
                    {
                        mw.phonesList.ItemsSource = Phones;
                        mw.expanderListName.Content = Name;
                    }
                }
                else
                {
                    mw.phonesList.ItemsSource = Phones;
                    mw.expanderListName.Content = Name;
                }
            }
        }
        #endregion

        #region Update checkboxes with models, depends on checked company
        private StackPanel UpdateModelCheckboxes(List<int> whatCompanyChecked)
        {
            StackPanel expanderPanel = new StackPanel();
            List<Phone> list = new List<Phone>();
            using (ShopFilterEntity db = new ShopFilterEntity())
            {
                foreach (var item in whatCompanyChecked)
                {
                    var ph = db.Phones.Where(c => c.CompanyId == item).ToList();
                    foreach(var p in ph)
                    {
                        list.Add(p);
                    }
                }
                
                foreach(var item in list)
                {
                    CheckBox ch = new CheckBox();
                    ch.Content = item.Name;
                    ch.Margin = new Thickness(5);
                    expanderPanel.Children.Add(ch);
                }
            }
            return expanderPanel;
        }
        #endregion

        #region Command logic
        private void CompanyFilterMethod(List<int> whatCompanyChecked)
        {
            using (ShopFilterEntity db = new ShopFilterEntity())
            {
                List<Phone> allPhonesChecked = new List<Phone>();

                foreach(var t in whatCompanyChecked)
                {
                    var p = db.Phones.Where(c => c.CompanyId == t).ToList();
                    foreach(var ph in p)
                    {
                        allPhonesChecked.Add(ph);
                    }
                }

                myList.Clear();
                foreach (var n in allPhonesChecked)
                {
                    string path = Environment.CurrentDirectory + n.ImagePath;
                    n.ImagePath = path;
                    myList.Add(n);
                }
            }
        }

        private void ModelFilterMethod(List<string> whatModelChecked)
        {
            using (ShopFilterEntity db = new ShopFilterEntity())
            {
                List<Phone> allPhonesChecked = new List<Phone>();

                foreach (var t in whatModelChecked)
                {
                    var p = db.Phones.Where(c => c.Name == t).ToList();
                    foreach (var ph in p)
                    {
                        allPhonesChecked.Add(ph);
                    }
                }

                if (whatModelChecked.Count > 0)
                {
                    myList.Clear();
                }
                foreach (var n in allPhonesChecked)
                {
                    string path = Environment.CurrentDirectory + n.ImagePath;
                    n.ImagePath = path;
                    myList.Add(n);
                }
            }
        }
        #endregion
    }
}
