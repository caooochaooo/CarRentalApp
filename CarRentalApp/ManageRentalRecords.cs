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
    public partial class ManageRentalRecords : Form
    {

        private readonly CarRentalEntities2 _db;
        public ManageRentalRecords()
        {
            InitializeComponent();
            _db = new CarRentalEntities2();
        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            if (!Utils.FormIsOpen("AddEditRentalRecord"))
            {
                var addRentalRecord = new AddEditRentalRecord();
                addRentalRecord.MdiParent = this.MdiParent;
                addRentalRecord.Show();
            }
                
        }

        private void btnEditRecord_Click(object sender, EventArgs e)
        {
            if (!Utils.FormIsOpen("AddEditRentalRecord"))
            {
                try
                {
                    var id = (int)gvRecordList.SelectedRows[0].Cells["id"].Value;
                    var record = _db.CarRentalRecords.FirstOrDefault(q => q.id == id);
                    var addEditRentalRecord = new AddEditRentalRecord(record);
                    addEditRentalRecord.MdiParent = this.MdiParent;
                    addEditRentalRecord.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
                
        }

        private void btnDeleteRecord_Click(object sender, EventArgs e)
        {
            try
            {
                var id = (int)gvRecordList.SelectedRows[0].Cells["id"].Value;
                var record = _db.CarRentalRecords.FirstOrDefault(q => q.id == id);
                _db.CarRentalRecords.Remove(record);
                _db.SaveChanges();
                PopulateGird() ;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void ManageRentalRecords_Load(object sender, EventArgs e)
        {
            try
            {
                PopulateGird();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private void PopulateGird()
        {
            var records = _db.CarRentalRecords.Select(q => new
            {
                Customer = q.CustomerName,
                DateOut = q.DateRented,
                DateIn = q.DateReturned,
                Id = q.id,
                q.Cost,
                Car = q.TypeOfCar.Make + " " + q.TypeOfCar.Model
            }).ToList();
            gvRecordList.DataSource = records;
            gvRecordList.Columns["DateIn"].HeaderText = "DateIn";
            gvRecordList.Columns["DateOut"].HeaderText = "DateOut";
            gvRecordList.Columns["Id"].Visible = false;
        }
    }
}
