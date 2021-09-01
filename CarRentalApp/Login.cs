using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class Login : Form
    {
        private readonly CarRentalEntities2 _db;
        public Login()
        {
            InitializeComponent();
            _db =new CarRentalEntities2();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                SHA256 sha = SHA256.Create();

                var username=tbUsername.Text.Trim();
                var password = tbPassword.Text;
                var hashed_password = Utils.HashPassword(password);
                var user = _db.Users.FirstOrDefault(q => q.username == username && q.password == hashed_password && q.isActive==true);
                if(user==null)
                {
                    MessageBox.Show("Please provide valid credentials");
                }
                else
                {
                    var role = user.UserRoles.FirstOrDefault();
                    var roleShortName = role.Role.shortname;
                    var mainWindow = new MainWindow(this,user);
                    mainWindow.Show();
                    Hide();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
