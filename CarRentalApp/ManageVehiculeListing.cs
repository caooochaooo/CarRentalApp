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
    public partial class ManageVehiculeListing : Form
    {
        private readonly CarRentalEntities2 _db;
        public ManageVehiculeListing()
        {
            InitializeComponent();
            _db = new CarRentalEntities2();
        }
        public void PopulateGrid()
        {
            //var cars = _db.TypeOfCars.ToList();
            /*   var cars = _db.TypeOfCars
                .Select(q => new { CarId=q.id,CarName=q.Make})
                .ToList();*/
            var cars = _db.TypeOfCars.Select(q => new { Make = q.Make, Model = q.Model, VIN = q.VIN, Year = q.Year, LicencePlateNumber = q.LicensePlateNumber, q.id }).ToList();
            gvVehiculeList.DataSource = cars;
            gvVehiculeList.Columns[4].HeaderText = "License Plate Number";
            gvVehiculeList.Columns[5].Visible = false;
            // gvVehiculeList.Columns[1].HeaderText = "NAME";*/

        }
        private void ManageVehiculeListing_Load(object sender, EventArgs e)
        {
            try
            {
                //var cars = _db.TypeOfCars.ToList();
                /*   var cars = _db.TypeOfCars
                    .Select(q => new { CarId=q.id,CarName=q.Make})
                    .ToList();*/
                var cars = _db.TypeOfCars.Select(q => new { Make = q.Make, Model = q.Model, VIN = q.VIN, Year = q.Year, LicencePlateNumber = q.LicensePlateNumber, q.id }).ToList();
                gvVehiculeList.DataSource = cars;
                gvVehiculeList.Columns[4].HeaderText = "License Plate Number";
                gvVehiculeList.Columns[5].Visible = false;
                // gvVehiculeList.Columns[1].HeaderText = "NAME";*/
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            if (!Utils.FormIsOpen("AddEditVehicle"))
            {
                var addEditVehicle = new AddEditVehicle(this);
                addEditVehicle.ShowDialog();
                addEditVehicle.MdiParent = this.MdiParent;
            }
                
        }

        private void btnEditCar_Click(object sender, EventArgs e)
        {
            if (!Utils.FormIsOpen("AddEditVehicle"))
            {
                try
                {
                    var id = (int)gvVehiculeList.SelectedRows[0].Cells["id"].Value;
                    var car = _db.TypeOfCars.FirstOrDefault(q => q.id == id);
                    var addEditVehicle = new AddEditVehicle(car);
                    addEditVehicle.MdiParent = this.MdiParent;
                    addEditVehicle.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
                
        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {
            
            try
            {
                var id = (int)gvVehiculeList.SelectedRows[0].Cells["id"].Value;
                var car = _db.TypeOfCars.FirstOrDefault(q => q.id == id);
                DialogResult dr=MessageBox.Show("Are you Sure You Want to Delete This Record?","Delete",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Warning);
                if(dr==DialogResult.Yes)
                {
                    _db.TypeOfCars.Remove(car);
                    _db.SaveChanges();
                }
                PopulateGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                var cars = _db.TypeOfCars.Select(q => new { Make = q.Make, Model = q.Model, VIN = q.VIN, Year = q.Year, LicencePlateNumber = q.LicensePlateNumber, q.id }).ToList();
                gvVehiculeList.DataSource = cars;
                gvVehiculeList.Columns[4].HeaderText = "License Plate Number";
                gvVehiculeList.Columns[5].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

    }
}
