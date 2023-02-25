using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary;
using ClassLibrary.Models;


namespace WinForms_SQLiteDB {
    public partial class Form1 : Form {
        List<PersonModel> people = new List<PersonModel>();
        public Form1() {
            InitializeComponent();
            LoadPeopleList();
        }

        public void LoadPeopleList() {
            people = SQLiteDataAccess.LoadPeople();
            WireUpPeopleList();
        }

        public void WireUpPeopleList() {
            listBoxPopulatePeople.DataSource = null;
            listBoxPopulatePeople.DataSource = people;
            listBoxPopulatePeople.DisplayMember = "FullName";
        }

        private void btnAddPerson_Click(object sender, EventArgs e) {
            PersonModel p = new PersonModel();
            p.FirstName = txtBoxFirstName.Text;
            p.LastName = txtBoxLastName.Text;

            SQLiteDataAccess.SavePerson(p);

            txtBoxFirstName.Text = "";
            txtBoxLastName.Text = "";

            LoadPeopleList();
        }

        private void btnRefreshList_Click(object sender, EventArgs e) {
            LoadPeopleList();
        }

        private void btnDeleteAll_Click(object sender, EventArgs e) {
            Console.Beep();
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the entire database?", "WARNING!!", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes) {
                SQLiteDataAccess.DeleteAll();
                LoadPeopleList();
            }
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e) {
            listBoxPopulatePeople.Items.Remove(listBoxPopulatePeople.SelectedIndex);            
        }

        private void btnExit_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
