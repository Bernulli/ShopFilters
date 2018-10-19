using DBLibrary.EF.Context;
using DBLibrary.EF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WPFApp.UIElements;

namespace WPFApp.Model
{
    class PhonesRepository
    {
        private ObservableCollection<Phone> phones;

        public ObservableCollection<Phone> AllPhones
        {
            get
            {
                if (phones == null)
                    phones = GetPhones();
                return phones;
            }
        }

        private ObservableCollection<Phone> GetPhones()
        {
            phones = new ObservableCollection<Phone>();

            using (ShopFilterEntity db = new ShopFilterEntity())
            {
                var list = db.Phones.ToList();

                foreach (Phone p in list)
                {
                    string path = Environment.CurrentDirectory + p.ImagePath;
                    p.ImagePath = path;
                    phones.Add(p);
                }
            }
            return phones;
        }
    }
}
