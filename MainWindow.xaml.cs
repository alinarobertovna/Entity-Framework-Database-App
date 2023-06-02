using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab4_301249589_Fadeeva_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            /*string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);*/

            /*var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var databasePath = currentDirectory + "Customers";
            AppDomain.CurrentDomain.SetData("DataDirectory", databasePath);
            var dataDirectory = AppDomain.CurrentDomain.GetData("DataDirectory");
            SqlConnection conn = new SqlConnection(@"Data Source=.;AttachDbFilename=|DataDirectory|\Customer.mdf;Integrated Security=True");

            conn.Open();*/

            InitializeComponent();            
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            CustomerEntities db = new CustomerEntities();

            Customer customerObject = new Customer()
            {
                Id = Convert.ToInt32(cid.Text),
                NameStyle = nstyle.Text,
                Title = title.Text,
                FirstName = fname.Text,
                MiddleName = mname.Text,
                LastName = lname.Text,
                CompanyName = company.Text,
                SalesPerson = sperson.Text,
                EmailAddress = email.Text,
                Phone = phone.Text,
                Password = password.Text
            };

            db.Customers.Add(customerObject);
            db.SaveChangesAsync();

            MessageBox.Show("Successfully Added!");

            cid.Text = "";
            nstyle.Text = "";
            title.Text = "";
            fname.Text = "";
            mname.Text = "";
            lname.Text = "";
            company.Text = "";
            sperson.Text = "";
            email.Text = "";
            phone.Text = "";
            password.Text = "";
            
            var result = from d in db.Customers
                         select new
                         {
                             d.Id,
                             d.Title,
                             d.NameStyle,
                             d.FirstName,
                             d.MiddleName,
                             d.LastName,
                             d.CompanyName,
                             d.SalesPerson,
                             d.EmailAddress,
                             d.Phone,
                             d.Password
                         };
            this.CustomersGrid.ItemsSource = result.ToList();
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            CustomerEntities db = new CustomerEntities();
            int Id = Convert.ToInt32(find.Text);

            var customer = from d in db.Customers where d.Id == Id
                           select new
                           {
                               d.Id,
                               d.Title,
                               d.NameStyle,
                               d.FirstName,
                               d.MiddleName,
                               d.LastName,
                               d.CompanyName,
                               d.SalesPerson,
                               d.EmailAddress,
                               d.Phone,
                               d.Password
                           };
            
            this.CustomersGrid.ItemsSource = customer.ToList();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            CustomerEntities db = new CustomerEntities();

            Customer customerObject = new Customer()
            {
                Id = Convert.ToInt32(cid.Text),
                NameStyle = nstyle.Text,
                Title = title.Text,
                FirstName = fname.Text,
                MiddleName = mname.Text,
                LastName = lname.Text,
                CompanyName = company.Text,
                SalesPerson = sperson.Text,
                EmailAddress = email.Text,
                Phone = phone.Text,
                Password = password.Text
            };

            int Id = Convert.ToInt32(cid.Text);
            var obj = db.Customers.Find(Id);
            db.Entry(obj).CurrentValues.SetValues(customerObject);
            db.SaveChangesAsync();

            MessageBox.Show("Successfully Edited!");

            cid.Text = "";
            nstyle.Text = "";
            title.Text = "";
            fname.Text = "";
            mname.Text = "";
            lname.Text = "";
            company.Text = "";
            sperson.Text = "";
            email.Text = "";
            phone.Text = "";
            password.Text = "";

            var result = from d in db.Customers
                         select new
                         {
                             d.Id,
                             d.Title,
                             d.NameStyle,
                             d.FirstName,
                             d.MiddleName,
                             d.LastName,
                             d.CompanyName,
                             d.SalesPerson,
                             d.EmailAddress,
                             d.Phone,
                             d.Password
                         };
            this.CustomersGrid.ItemsSource = result.ToList();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            CustomerEntities db = new CustomerEntities();
            int Id = Convert.ToInt32(delete.Text);

            var obj = db.Customers.Find(Id);
            db.Customers.Remove(obj);
            db.SaveChangesAsync();

            MessageBox.Show("Successfully Deleted!");
            var result = from d in db.Customers
                         select new
                         {
                             d.Id,
                             d.Title,
                             d.NameStyle,
                             d.FirstName,
                             d.MiddleName,
                             d.LastName,
                             d.CompanyName,
                             d.SalesPerson,
                             d.EmailAddress,
                             d.Phone,
                             d.Password
                         };
            this.CustomersGrid.ItemsSource = result.ToList();
        }

        private void view(object sender, RoutedEventArgs e)
        {
            Button _myButton = (Button)sender;
            string value = _myButton.CommandParameter.ToString();

            CustomerEntities db = new CustomerEntities();

            var result = from d in db.Customers
                         select new
                         {
                             d.Id,
                             d.Title,
                             d.NameStyle,
                             d.FirstName,
                             d.MiddleName,
                             d.LastName,
                             d.CompanyName,
                             d.SalesPerson,
                             d.EmailAddress,
                             d.Phone,
                             d.Password,
                             d.CustomerAddress.Address.AddressLine1,
                             d.CustomerAddress.Address.AddressLine2,
                             d.CustomerAddress.Address.City,
                             d.CustomerAddress.Address.CountryRegion
                         };

            if (value == "l1")
            {
                result = from d in db.Customers
                             select new
                             {
                                 d.Id,
                                 d.Title,
                                 d.NameStyle,
                                 d.FirstName,
                                 d.MiddleName,
                                 d.LastName,
                                 d.CompanyName,
                                 d.SalesPerson,
                                 d.EmailAddress,
                                 d.Phone,
                                 d.Password,                                 
                                 d.CustomerAddress.Address.AddressLine1,
                                 d.CustomerAddress.Address.AddressLine2,
                                 d.CustomerAddress.Address.City,
                                 d.CustomerAddress.Address.CountryRegion
                             };
            }
            else if (value == "s1")
            {
                result = from d in db.Customers
                             orderby d.FirstName
                             select new
                             {
                                 d.Id,
                                 d.Title,
                                 d.NameStyle,
                                 d.FirstName,
                                 d.MiddleName,
                                 d.LastName,
                                 d.CompanyName,
                                 d.SalesPerson,
                                 d.EmailAddress,
                                 d.Phone,
                                 d.Password,
                                 d.CustomerAddress.Address.AddressLine1,
                                 d.CustomerAddress.Address.AddressLine2,
                                 d.CustomerAddress.Address.City,
                                 d.CustomerAddress.Address.CountryRegion
                             };
            }
            else if (value == "l2")
            {
                result = from d in db.Customers
                             where d.CustomerAddress.Address.CountryRegion == "Canada"
                             select new
                             {
                                 d.Id,
                                 d.Title,
                                 d.NameStyle,
                                 d.FirstName,
                                 d.MiddleName,
                                 d.LastName,
                                 d.CompanyName,
                                 d.SalesPerson,
                                 d.EmailAddress,
                                 d.Phone,
                                 d.Password,
                                 d.CustomerAddress.Address.AddressLine1,
                                 d.CustomerAddress.Address.AddressLine2,
                                 d.CustomerAddress.Address.City,
                                 d.CustomerAddress.Address.CountryRegion
                             };
            }
            else if (value == "s2")
            {
                result = from d in db.Customers
                             orderby d.CompanyName
                             select new
                             {
                                 d.Id,
                                 d.Title,
                                 d.NameStyle,
                                 d.FirstName,
                                 d.MiddleName,
                                 d.LastName,
                                 d.CompanyName,
                                 d.SalesPerson,
                                 d.EmailAddress,
                                 d.Phone,
                                 d.Password,
                                 d.CustomerAddress.Address.AddressLine1,
                                 d.CustomerAddress.Address.AddressLine2,
                                 d.CustomerAddress.Address.City,
                                 d.CustomerAddress.Address.CountryRegion
                             };
            }

            this.CustomersGrid.ItemsSource = result.ToList();
        }       

    }
}
