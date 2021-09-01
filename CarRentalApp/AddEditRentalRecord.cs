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
    public partial class AddEditRentalRecord : Form
    {
        private bool isEditMode;
        private readonly CarRentalEntities2 _db;
        public AddEditRentalRecord()
        {
            InitializeComponent();
            lblTitle.Text = "Add New Rental";
            this.Text = "Add New Rental";
            isEditMode = false;
            _db = new CarRentalEntities2();
        }
        public AddEditRentalRecord(CarRentalRecord recordToEdit)
        {
            InitializeComponent();
            lblTitle.Text = "Edit Rental Record";
            this.Text = "Edit Rental Record";
            if(recordToEdit==null)
            {
                MessageBox.Show($"Please ensure that you selected record to Edit");
                    Close();
            }
            else
            {

                isEditMode = true;
                _db = new CarRentalEntities2();
                PopulateFields(recordToEdit);
            }
        }
        private void PopulateFields(CarRentalRecord recordToEdit)
        {
            tbcustomername.Text= recordToEdit.CustomerName;
            dtRented.Value= (DateTime)recordToEdit.DateRented;
            dtReturned.Value=(DateTime)recordToEdit.DateReturned;
            tbcost.Text= recordToEdit.Cost.ToString();
            lblRecordId.Text = recordToEdit.id.ToString(); 
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string customername = tbcustomername.Text;
                var dateOut = dtRented.Value;
                var dateIn = dtReturned.Value;
                double cost = Convert.ToDouble(tbcost.Text);
                var errormessage = "";
                var carType = cbTypeOfCar.Text;
                var isvalid = true;
                if (string.IsNullOrWhiteSpace(customername) || string.IsNullOrWhiteSpace(carType)) 
                {
                    isvalid = false;
                    errormessage += "Error: Please enter missing data.\n\r";
                }
                if (dateOut > dateIn)
                {
                    isvalid = false;
                    errormessage += "Error: Illegal date selection\n\r";
                }
                
                
                //if(isvalid)
                if(isvalid)
                {
                    var rentalRecord = new CarRentalRecord();
                    if (isEditMode)
                    {
                        var id = int.Parse(lblRecordId.Text);
                        rentalRecord = _db.CarRentalRecords.FirstOrDefault(q => q.id == id);
                    }
                    rentalRecord.CustomerName = customername;
                    rentalRecord.DateRented = dateOut;
                    rentalRecord.DateReturned = dateIn;
                    rentalRecord.Cost = (decimal)cost;
                    rentalRecord.TypeOfCarID = (int)cbTypeOfCar.SelectedValue;
                    if(!isEditMode)
                        _db.CarRentalRecords.Add(rentalRecord);

                        
                    _db.SaveChanges();
                       MessageBox.Show($"Customer Name: {customername}\n\r" +
                        $"Date Rented: {dateOut}\n\r"
                        + $"Date Returned: {dateIn}\n\r" +
                        $"cost : {cost}\n\r" +
                        $"Car Type: {carType}\n\r" +
                        $"Thank you for your Business");
                    
                    Close();
                }
                else
                {
                    MessageBox.Show(errormessage);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void cbTypeOfCar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var cars = carRentalEntities.TypeOfCars.ToList();
            var cars = _db.TypeOfCars.Select(q => new { Id = q.id, Name = q.Make + " " + q.Model }).ToList();
            cbTypeOfCar.DisplayMember = "Name";
            cbTypeOfCar.ValueMember = "id";
            cbTypeOfCar.DataSource = cars;
        }

    }
}
