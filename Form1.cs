using System;
using System.Windows.Forms;

namespace Lab03_03
{
    public partial class FormStudent : Form
    {
        private FormMain.AddStudentDelegate addStudentCallback;

        public FormStudent(FormMain.AddStudentDelegate callback)
        {
            InitializeComponent();
            addStudentCallback = callback;
        }

        private void FormStudent_Load(object sender, EventArgs e)
        {
            comboBoxDepartment.Items.Add("Công nghệ thông tin");
            comboBoxDepartment.Items.Add("Ngôn ngữ Anh");
            comboBoxDepartment.Items.Add("Quản trị kinh doanh");
            comboBoxDepartment.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string studentID = txtStudentID.Text;
            string studentName = txtStudentName.Text;
            string department = comboBoxDepartment.SelectedItem?.ToString();
            string score = txtScore.Text;

            if (string.IsNullOrWhiteSpace(studentID) || string.IsNullOrWhiteSpace(studentName) || string.IsNullOrWhiteSpace(score) || department == null)
            {
                MessageBox.Show("Các thông tin bắt buộc phải nhập liệu (Mã số, Tên Sinh Viên, Khoa, Điểm)", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (addStudentCallback != null && addStudentCallback.Method.Name == "CheckDuplicateStudentID" && addStudentCallback.Target is FormMain mainForm && mainForm.CheckDuplicateStudentID(studentID))
            {
                MessageBox.Show("Mã số sinh viên bị trùng!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(score, out int parsedScore) || parsedScore < 0 || parsedScore > 10)
            {
                MessageBox.Show("Điểm phải trong phạm vi từ 0 đến 10!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            addStudentCallback?.Invoke(studentID, studentName, department, score);
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
