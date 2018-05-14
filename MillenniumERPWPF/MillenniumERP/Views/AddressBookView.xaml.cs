using System.Windows.Controls;

namespace MillenniumERP.Views
{
    /// <summary>
    /// Interaction logic for AddressBookView.xaml
    /// </summary>
    public partial class AddressBookView : UserControl
    {
        public AddressBookView()
        {
            InitializeComponent();

            System.Collections.ObjectModel.ObservableCollection<object> list = new System.Collections.ObjectModel.ObservableCollection<object>();
            dynamic data = null;

            data = new { Key=1, Name = "Hello World", State="Idaho" }; list.Add(data);

            dataGridAddressBook.AutoGenerateColumns = false;
            dataGridAddressBook.ItemsSource = list;
        }
    }
}
