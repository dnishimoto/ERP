using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MillenniumERP.Interfaces
{
    public interface IERPModule
    {
       
            string DisplayName { get; }
            Form MainForm { get; }
            Type MainFormType { get; }
            Image Image { get; }
   
    }
}
