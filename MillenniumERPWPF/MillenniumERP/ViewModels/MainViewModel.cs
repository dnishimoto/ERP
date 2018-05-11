//-----------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="Listensoftware">
//     Copyright (c) Listensoftware Pte. Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace MillenniumERP.ViewModels
{
    using Caliburn.Micro;
    /// <summary>
    /// Interaction logic for MainViewModel
    /// </summary>
    /// <seealso cref="Caliburn.Micro.Screen" />
    public class MainViewModel : Conductor<object>
    {
        /// <summary>
        /// Loads the address book.
        /// </summary>
        public void LoadAddressBook()
        {
            ActivateItem(new AddressBookViewModel());
        }
    }
}
