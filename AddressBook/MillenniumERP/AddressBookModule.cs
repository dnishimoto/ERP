using MillenniumERP.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MillenniumERP
{
    public class AddressBookModule : IERPModule
    {
        private Form _Form;
        public AddressBookModule()
        {
            _Form = new MillenniumERP.AddressBookForm();
        }
        public string DisplayName => "Address Book";

        public Form MainForm => _Form;

        public Type MainFormType => typeof(MillenniumERP.AddressBookForm);

        public Image Image => Properties.Resources.newdocument32;
    }
}
