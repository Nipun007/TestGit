﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UniversityManagementApp.BLL;

namespace UniversityManagementApp
{
    public partial class StudentEntryUI : Form
    {
        StudentManager manager = new StudentManager();
        public StudentEntryUI()
        {
            InitializeComponent();
        }


        List<Student> students = new List<Student>();

        private int studentId = 0;

        private bool isUpdateMode = false;


        private void StudentEntryUI_Load(object sender, EventArgs e)
        {        

            LoadAllStudents();
        }


        

        

        private void saveButton_Click(object sender, EventArgs e)
        {
            string name = nameTextBox.Text;
            string regNo = regNoTextBox.Text;
            string address = addressTextBox.Text;

            Student student = new Student();
            student.Name = name;
            student.Address = address;
            student.RegNo = regNo;
            student.Id = studentId;

          

            bool isSuccess = false;
            if (isUpdateMode)
            {
                isSuccess = manager.Update(student);
            }
            else
            {
                isSuccess = manager.Insert(student);
            }


            if (isSuccess)
            {
                if (isUpdateMode)
                {
                    MessageBox.Show("Updated Successfully!");
                    saveButton.Text = "Save";
                    isUpdateMode = false;
                }
                else
                {
                    MessageBox.Show("Inserted Successfully!");
                }
                ClearTextBox();
                LoadAllStudents();
            }
            else
            {
                if (isUpdateMode)
                {
                    MessageBox.Show("Update failed!");
                }
                else
                {
                    MessageBox.Show("Insertion Failed!");
                }
            }





        }

        

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void LoadAllStudents()
        {
            students = manager.GetAllStudents();
            // show students to list view 
            studentsListView.Items.Clear();
            foreach (var student in students)
            {
                ListViewItem item = new ListViewItem(student.Id.ToString());
                item.SubItems.Add(student.Name);
                item.SubItems.Add(student.RegNo);
                item.SubItems.Add(student.Address);
                studentsListView.Items.Add(item);

            }

           
           
            
        }

        private void studentDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void studentsListView_DoubleClick(object sender, EventArgs e)
        {
            // get selected item 

            if (studentsListView.SelectedItems.Count>0)
            {
                ListViewItem firstSelectedItem = studentsListView.SelectedItems[0];

                int studentId = int.Parse(firstSelectedItem.Text);

                Student student = manager.GetStudentById(studentId);

                nameTextBox.Text = student.Name;
                regNoTextBox.Text = student.RegNo;
                addressTextBox.Text = student.Address;

                isUpdateMode = true;

                this.studentId = student.Id;

                saveButton.Text = "Update";

            }


        }

        

        public void ClearTextBox()
        {
            studentId = 0;
            nameTextBox.Clear();
            addressTextBox.Clear();
            regNoTextBox.Clear();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            Student student = new Student();

            student.Id = studentId;
            student.Name = nameTextBox.Text;
            student.RegNo = regNoTextBox.Text;
            student.Address = addressTextBox.Text;

            bool isSuccess = manager.Delete(student);

            if (isSuccess)
            {
                MessageBox.Show("Deleted Successfully!");
                LoadAllStudents();
                ClearTextBox();
            }
            else
            {
                MessageBox.Show("Could not delete!");
            }
        }
    }
}
