using AddressBook.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddressBook
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

            Task<List<EntityFramework.AddressBook>> resultListTask = Task.Run <List<EntityFramework.AddressBook>>(async() =>await unitOfWork.addressBookRepository.GetAddressBooks("customer"));

            foreach (var item in resultListTask.Result)
            {
                Console.WriteLine($"{item.Name}");
            }

            MessageBox.Show("reached");
        }
    }
}
