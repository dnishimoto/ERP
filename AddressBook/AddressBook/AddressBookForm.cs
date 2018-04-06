using Millennium.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Millennium.EntityFramework;

namespace Millennium
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

            Task<List<AddressBook>> resultListTask = Task.Run <List<AddressBook>>(async() =>await unitOfWork.addressBookRepository.GetAddressBooks("customer"));

            foreach (var item in resultListTask.Result)
            {
                Console.WriteLine($"{item.Name}");
                /*
                foreach (var item2 in item.ScheduleEvents)
                {
                    Console.WriteLine($"{item2.EventDateTime}");

                }
                */
            }
      
            Task<AddressBook> resultTask2 = Task.Run<AddressBook>(async () => await unitOfWork.addressBookRepository.GetObjectAsync(1));
            Console.WriteLine($"{resultTask2.Result.FirstName}");
            MessageBox.Show("reached");
        }
    }
}
