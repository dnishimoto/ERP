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
            List<EntityFramework.AddressBook> resultList = new List<EntityFramework.AddressBook>();
            //UDCRepository udcRepository = new UDCRepository();
            //long xRefId = udcRepository.GetUdcByKeyCode(keyCode);
            using (var db = new EntityFramework.listensoftwareDBEntities())
            {
                var query = from b in db.AddressBooks
                            //.Where(b => b.PeopleXrefId == xRefId)
                            select b;

                query = query.OrderBy(s => s.Name);


                foreach (var item in query)
                {
                    resultList.Add(item);
                }


            }
            
        }
    }
}
