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
        UnitOfWork unitOfWork = new UnitOfWork();
        IQueryable<AddressBook> dataContext=null;
        /// <summary>
        /// Do the searching
        /// </summary>
        public void Search()
        {


            dataContext = unitOfWork.addressBookRepository.GetObjectsAsync(a => a.Name.Contains("David"));

           
        }
    }
}
