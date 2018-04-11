using MillenniumERP.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MillenniumERP
{
    public partial class frmERPMainForm : Form
    {
        private MillenniumERP.AddressBookModule _addressBookInterface = new MillenniumERP.AddressBookModule();

        public frmERPMainForm()
        {
            InitializeComponent();


        }

        private void addressBookToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void frmERPMainForm_Load(object sender, EventArgs e)
        {
     
            var ribbonButton = new RibbonButton();
            ribbonButton.Name = _addressBookInterface.DisplayName;
            ribbonButton.Tag = _addressBookInterface;
            ribbonButton.LargeImage = _addressBookInterface.Image;
            ribbonButton.Text = "Button: " + _addressBookInterface.DisplayName;
            ribbonButton.Click += new EventHandler(RibbonButtonAddressBookClick);
            
            var ribbonPanel = new RibbonPanel(_addressBookInterface.DisplayName);
            ribbonPanel.Items.Add(ribbonButton);

            var ribbonTab = new RibbonTab();
            ribbonTab.Text = _addressBookInterface.DisplayName;
            ribbonTab.Panels.Add(ribbonPanel);

            ribbon.Tabs.Add(ribbonTab);
            this.Controls.Add(ribbon);
        }

        private void RibbonButtonAddressBookClick(object sender, EventArgs e)
        {
            if (sender is RibbonButton)
            {
                if (((RibbonButton)sender).Tag is IERPModule)
                {
                    var moduleInterface = (IERPModule)((RibbonButton)sender).Tag;

                    foreach (Form f in this.MdiChildren)
                    {
                        if (f.GetType() == moduleInterface.MainFormType)
                        {
                            f.Activate();
                            return;
                        }
                    }
                    Form form = moduleInterface.MainForm;
                    //form.FormBorderStyle = FormBorderStyle.None;
                    form.WindowState = FormWindowState.Maximized;
                    form.ControlBox = false;
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }

        private void gridViewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
