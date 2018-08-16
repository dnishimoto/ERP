//-----------------------------------------------------------------------
// <copyright file="AddressBookViewModel.cs" company="Listensoftware">
//     Copyright (c) Listensoftware Pte. Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace MillenniumERP.ViewModels
{
    using Caliburn.Micro;
    using ERP_Core2.EntityFramework;
    using MillenniumERP.Services;
    using System.Linq;

    /// <summary>
    /// Interaction logic for AddressBookViewModel
    /// </summary>
    /// <seealso cref="Caliburn.Micro.Screen" />
    public class AddressBookViewModel : Screen
    {
        private BindableCollection<AddressBook> _addressBooks = new BindableCollection<AddressBook>();
        private string _searchName = "";
        UnitOfWork unitOfWork = new UnitOfWork();

        public void Search()
        {
            AddressBooks.Clear();
            IQueryable <AddressBook> query = unitOfWork.addressBookRepository.GetObjectsQueryable(a => a.Name.Contains(SearchName));
            foreach (var item in query)
            {
                AddressBooks.Add(item);
            }

        }
        public string SearchName
        {
            get { return _searchName; }
            set
            {
                _searchName = value;
       
            }
        }

        public BindableCollection<AddressBook> AddressBooks
        {
            get { return _addressBooks; }
            set { _addressBooks = value; }
        }

    }
}
