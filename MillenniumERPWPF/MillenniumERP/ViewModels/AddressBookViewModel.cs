//-----------------------------------------------------------------------
// <copyright file="AddressBookViewModel.cs" company="Listensoftware">
//     Copyright (c) Listensoftware Pte. Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace MillenniumERP.ViewModels
{
    using Caliburn.Micro;
    using MillenniumERP.EntityFramework;
    using MillenniumERP.Services;
    using System.Linq;

    /// <summary>
    /// Interaction logic for AddressBookViewModel
    /// </summary>
    /// <seealso cref="Caliburn.Micro.Screen" />
    public class AddressBookViewModel : Screen
    {
        private BindableCollection<AddressBook> _addressBooks = new BindableCollection<AddressBook>();

        UnitOfWork unitOfWork = new UnitOfWork();

        public void Search()
        {

            IQueryable <AddressBook> query = unitOfWork.addressBookRepository.GetObjectsAsync(a => a.Name.Contains("David"));
            foreach (var item in query)
            {
                AddressBooks.Add(item);
            }

        }
        public BindableCollection<AddressBook> AddressBooks
        {
            get { return _addressBooks; }
            set { _addressBooks = value; }
        }

    }
}
