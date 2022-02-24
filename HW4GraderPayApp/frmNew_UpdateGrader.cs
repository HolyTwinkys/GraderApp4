using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW4GraderPayApp
{
    public partial class frmNew_UpdateGrader : Form
    {
        List<Department> departments;
        Grader gr;
        Boolean isModification;

        public Grader Gr { get => gr; set => gr = value; }
        public bool IsModification { get => isModification; set => isModification = value; }

        public frmNew_UpdateGrader()
        {
            InitializeComponent();
        }

        private void frmNew_UpdateGrader_Load(object sender, EventArgs e)
        {
            using (var container = new GraderEntities())
            {
                departments = container.Departments.ToList();
            }
            cbxDept.DataSource = departments;
            cbxDept.DisplayMember = "DepartmentName";
            cbxDept.ValueMember = "DepartmentCode";
            if (isModification)
            {
                txtGraderID.Text = Gr.GraderID.ToString();
                txtFName.Text = Gr.FirstName;
                txtLName.Text = Gr.LastName;
                cbxDept.SelectedValue = Gr.Department;
                txtHourlyPay.Text = Gr.HourlyPay.ToString();
                txtHours.Text = Gr.Hours.ToString();
                txtStipend.Text = Gr.Stipend.ToString();
                txtGraderID.ReadOnly = true;
            }
        }

     

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isModification)
            {
                txtFName.Text = Gr.FirstName;
                txtLName.Text = Gr.LastName;
                cbxDept.SelectedValue = Gr.Department;
                txtHourlyPay.Text = Gr.HourlyPay.ToString();
                txtHours.Text = Gr.Hours.ToString();
                txtStipend.Text = Gr.Stipend.ToString();
                MessageBox.Show(Gr.GetPayment().ToString("c"));
                try
                {
                    using (var container = new GraderEntities())
                    {
                        container.Graders.Add(Gr);
                        container.Entry(Gr).State = System.Data.Entity.EntityState.Modified;
                        container.SaveChanges();
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                int id;
                try
                {
                    id = int.Parse(txtGraderID.Text);
                }
                catch
                {
                    MessageBox.Show("Grader ID must be a number!");
                    return;
                }
                Grader g = new Grader
                {
                    GraderID = id,
                    FirstName = txtFName.Text,
                    LastName = txtLName.Text,
                    Department = cbxDept.SelectedValue.ToString(),
                    HourlyPay = decimal.Parse(txtHourlyPay.Text),
                    Hours = int.Parse(txtHours.Text),
                    Stipend = decimal.Parse(txtStipend.Text)
                };
                try
                {
                    using (var container = new GraderEntities())
                    {
                        container.Graders.Add(g);
                        container.SaveChanges();
                    }
                    MessageBox.Show("It was Successfully entered!");
                    MessageBox.Show(Gr.GetPayment().ToString("c"));

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
