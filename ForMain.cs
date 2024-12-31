using System;
using System.Linq;
using System.Windows.Forms;

namespace Lab03_03
{
    public partial class FormMain : Form
    {
        public delegate void AddStudentDelegate(string studentID, string studentName, string department, string score);
        public AddStudentDelegate OnAddStudent;

        public FormMain()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormMain_Load);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Thiết lập cho DataGridView
            
            dataGridViewStudents.Columns.Add("StudentID", "MSSV");
            dataGridViewStudents.Columns.Add("StudentName", "Tên SV");
            dataGridViewStudents.Columns.Add("Department", "Khoa");
            dataGridViewStudents.Columns.Add("Score", "ĐTB");

            // Thiết lập TextBox tìm kiếm
            txtSearch.TextChanged += TxtSearch_TextChanged;

            OnAddStudent = new AddStudentDelegate(AddStudentToGrid);
        }

        private void thêmMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowStudentForm();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            ShowStudentForm();
        }

        private void ShowStudentForm()
        {
            FormStudent formStudent = new FormStudent(OnAddStudent);
            formStudent.Show();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if (dataGridViewStudents.IsCurrentRowDirty)
            {
                dataGridViewStudents.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            string searchValue = txtSearch.Text.ToLower();
            foreach (DataGridViewRow row in dataGridViewStudents.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (row.Cells["StudentName"].Value != null && row.Cells["StudentName"].Value.ToString().ToLower().Contains(searchValue))
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }
                }
            }
        }

        public bool CheckDuplicateStudentID(string studentID)
        {
            foreach (DataGridViewRow row in dataGridViewStudents.Rows)
            {
                if (row.Cells["StudentID"].Value != null && row.Cells["StudentID"].Value.ToString() == studentID)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddStudentToGrid(string studentID, string studentName, string department, string score)
        {
            dataGridViewStudents.Rows.Add(studentID, studentName, department, score);
        }

        private void dataGridViewStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridViewStudents_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
