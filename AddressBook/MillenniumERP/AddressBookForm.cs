using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MillenniumERP.EntityFramework;
using MillenniumERP.Services;

namespace MillenniumERP
{
    public partial class AddressBookForm : Form
    {
        public AddressBookForm()
        {
            InitializeComponent();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
           UnitOfWork unitOfWork = new UnitOfWork();

            IQueryable<AddressBook> query = unitOfWork.addressBookRepository.GetObjectsAsync(a => a.Name.Contains(txtSearchName.Text.ToString()));

            dataGridViewAddressBook.AutoGenerateColumns = false;
            dataGridViewAddressBook.DataSource = query.ToList<AddressBook>();


        }
    }
}
