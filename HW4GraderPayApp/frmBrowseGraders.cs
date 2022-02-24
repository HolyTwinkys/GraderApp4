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
    public partial class frmBrowseGrader : Form
    {
        List<Grader> list;

        public frmBrowseGrader()
        {
            InitializeComponent();
        }

        private void frmBrowseGrader_Load(object sender, EventArgs e)
        {
            using (var container = new GraderEntities())
            {
                list = container.Graders.ToList();
            }
            dgvGraders1.DataSource = list;
            dgvGraders1.Columns[0].HeaderText = "Grader ID";
            dgvGraders1.Columns[1].HeaderText = "First Name";
            dgvGraders1.Columns[2].HeaderText = "Last Name";
            DataGridViewButtonColumn modifyColumn = new DataGridViewButtonColumn();
            modifyColumn.HeaderText = "";
            modifyColumn.UseColumnTextForButtonValue = true;
            modifyColumn.Text = "Modify";
            dgvGraders1.Columns.Add(modifyColumn);

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmNew_UpdateGrader form = new frmNew_UpdateGrader();
            form.ShowDialog();
            dgvGraders1.Columns.Clear();
            try
            {
                using (var container = new GraderEntities())
                {
                    list = container.Graders.ToList();
                }
                dgvGraders1.DataSource = list;
                dgvGraders1.Columns[0].HeaderText = "Grader ID";
                dgvGraders1.Columns[1].HeaderText = "First Name";
                dgvGraders1.Columns[2].HeaderText = "Last Name";
                DataGridViewButtonColumn modifyColumn = new DataGridViewButtonColumn();
                modifyColumn.HeaderText = "";
                modifyColumn.UseColumnTextForButtonValue = true;
                modifyColumn.Text = "Modify";
                dgvGraders1.Columns.Add(modifyColumn);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dgvGraders1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                frmNew_UpdateGrader form = new frmNew_UpdateGrader();
                form.Gr = list[e.RowIndex];
                form.IsModification = true;
                form.ShowDialog();
                dgvGraders1.Columns.Clear();
                try
                {
                    using (var container = new GraderEntities())
                    {
                        list = container.Graders.ToList();
                    }
                    dgvGraders1.DataSource = list;
                    dgvGraders1.Columns[0].HeaderText = "Grader ID";
                    dgvGraders1.Columns[1].HeaderText = "First Name";
                    dgvGraders1.Columns[2].HeaderText = "Last Name";
                    DataGridViewButtonColumn modifyColumn = new DataGridViewButtonColumn();
                    modifyColumn.HeaderText = "";
                    modifyColumn.UseColumnTextForButtonValue = true;
                    modifyColumn.Text = "Modify";
                    dgvGraders1.Columns.Add(modifyColumn);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
