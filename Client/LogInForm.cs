using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using csharp_project.model;
using csharp_project.service;

namespace csharp_project
{
    public partial class LogInForm : Form
    {
        private static IService _service;
        public LogInForm(IService service)
        {
            InitializeComponent();
            _service = service;
        }

        private static string HashPassword(string password)
        {
            try
            {
                using (var sha256 = SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return Convert.ToBase64String(hashedBytes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while hashing password: " + ex.Message);
                return null;
            }
        }


        private void HandleLogIn(object sender, EventArgs e)
        {
            var username = usernameTextBox.Text;
            var password = passwordTextBox.Text;
            var hashedPassword = HashPassword(password);
            
            Console.WriteLine(username);
            Console.WriteLine(password);
            Console.WriteLine(hashedPassword);

            /*var employee = _service.FindOneByUsernameAndPassword(username, hashedPassword);*/
            var employee = new Employee(1, "", "", username, hashedPassword);
            var accountForm = new AccountForm(_service, employee);
            var connectedEmployee = _service.Login(employee, accountForm);

            if (connectedEmployee != null)
            {
                /*ShowAccountDialog(employee);*/
                accountForm.SetConnectedEmployee(connectedEmployee);
                Hide();
                accountForm.Text = connectedEmployee.GetUsername();
                accountForm.ShowDialog();
            }
            else
            {
                MessageAlert.ShowErrorMessage(null, "Wrong username or password!");
            }
        }
        
        private static void ShowAccountDialog(Employee employee)
        {
            try
            {
                var accountForm = new AccountForm(_service, employee);
                accountForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while showing account dialog: " + ex.Message);
            }
        }
    }
}