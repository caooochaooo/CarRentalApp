using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public partial class AddEditVehicle : Form
    {
        private bool isEditMode;
        private ManageVehiculeListing _manageVehiculeListing;
        private readonly CarRentalEntities2 _db;
        public AddEditVehicle(ManageVehiculeListing manageVehiculeListing=null)
        {
            InitializeComponent();
            lblTitle.Text="Add New Vehicle";
            isEditMode = false;
            _manageVehiculeListing= manageVehiculeListing;
            _db = new CarRentalEntities2();
        }
        public AddEditVehicle(TypeOfCar carToEdit, ManageVehiculeListing manageVehiculeListing = null)
        {
            InitializeComponent();
            lblTitle.Text = "Edit Vehicle";
            _manageVehiculeListing = manageVehiculeListing;
            isEditMode =true;
            _db = new CarRentalEntities2();
            PopulateFields(carToEdit);
        }
        private void PopulateFields(TypeOfCar car)
        {
            lblId.Text = car.id.ToString();
            tbMake.Text = car.Make;
            tbModel.Text = car.Model;
            tbVIN.Text = car.VIN;
            tbYear.Text = car.Year.ToString();
            tbLicenseNum.Text = car.LicensePlateNumber;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (isEditMode)
                {
                    var id = int.Parse(lblId.Text);
                    var car = _db.TypeOfCars.FirstOrDefault(q => q.id == id);
                    car.Model = tbModel.Text;
                    car.Make = tbMake.Text;
                    car.VIN = tbVIN.Text;
                    car.Year = int.Parse(tbYear.Text);
                    car.LicensePlateNumber = tbLicenseNum.Text;

                }
                else
                {
                    var newCar = new TypeOfCar
                    {
                        LicensePlateNumber = tbLicenseNum.Text,
                        Make = tbMake.Text,
                        Model = tbModel.Text,
                        VIN = tbVIN.Text,
                        Year = int.Parse(tbYear.Text),

                    };
                    _db.TypeOfCars.Add(newCar);
                   
                }
                _db.SaveChanges();
                _manageVehiculeListing.PopulateGrid();
                MessageBox.Show("Insert Operation Completed.Refresh Grid To see Changes");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
