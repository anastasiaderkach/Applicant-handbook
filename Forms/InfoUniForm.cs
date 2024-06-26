﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.Forms
{
    public partial class InfoUniForm : Form
    {
        Form _previousForm;
        University university;
        MainForm _mainForm;
        public InfoUniForm(Form previousForm, University uni, MainForm mainForm)
        {
            InitializeComponent();
            _previousForm = previousForm;
            university = uni;
            _mainForm = mainForm;

        }

        public void FillInfoUniForm(University uni)
        {
            NameLabel.Text = uni.Name;
            IdLabel.Text = uni.Id;
            HeadLabel.Text = uni.Head;
            AdressLabel.Text = uni.Adress;
            TelLabel.Text = uni.PhoneNum;
            EmailLabel.Text = uni.Email;

            SpeciDataGridView.DataSource = uni.Specialities;
        }

        private void GoBackButton_Click(object sender, EventArgs e)
        {
            if(_previousForm is SavedForm)
            {
                SavedForm form = new SavedForm(_mainForm);
                form.Show();
            }
            else
            {
                _previousForm.Show();
            }
            this.Close();
        }

        private void InfoUniForm_Load(object sender, EventArgs e)
        {
            SpeciDataGridView.AutoGenerateColumns = false;
            SpeciDataGridView.EnableHeadersVisualStyles = false;
            SpeciDataGridView.Columns.Add("NameColumn", "Назва спеціальності");
            SpeciDataGridView.Columns.Add("EduColumn", "Форма навчання");

            SpeciDataGridView.Columns["NameColumn"].DataPropertyName = "Name";
            SpeciDataGridView.Columns["EduColumn"].DataPropertyName = "EducationForm";
            foreach (DataGridViewColumn column in SpeciDataGridView.Columns)
            {
                if (column.Name != "NameColumn" && column.Name != "EduColumn")
                {
                    column.Visible = false;
                }
            }

            SpeciDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SpeciDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            this.Hide();
            Speciality spec = (Speciality)SpeciDataGridView.Rows[e.RowIndex].DataBoundItem;
            UniSpecForm form = new UniSpecForm(this, _mainForm, spec);
            form.Show();
            form.FillUniSpecForm(spec);
        }

        private void SpeciDataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                SpeciDataGridView.Cursor = Cursors.Hand;
            }
        }

        private void SpeciDataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            SpeciDataGridView.Cursor = Cursors.Default;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (_previousForm is SavedForm || _mainForm.directory.ContainsUni(university))
            {
                SavedLabel.Text = "Заклад вищої освіти збережений.";
                return;
            }
            _mainForm.directory.AddToSavedUnis(university);
            SavedLabel.Text = "Заклад вищої освіти збережений.";
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            AboutForm form = new AboutForm(this);
            form.Show();
        }
    }
}
