using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
namespace WindowsFormsApplication2
{
    public partial class Sort : Form
    {
        People peopl;
        WorkerContext _db;
        int YearIndex;
        public Sort()
        {
            InitializeComponent();
            _db = new WorkerContext();
            _db.Peoples.Load();
            YearIndex = 2018;
            if (_db.Peoples.Count(i => i.Year == YearIndex)<1) {
                    MessageBox.Show("Don`t booked");
               
            }
            else {

                BindingSource DatedbOne = new BindingSource();
                var DatedbOnek = from w in _db.Peoples.Local
                                where w.Year == YearIndex
                                select w;
                var qieryAsList = new BindingList<People>(DatedbOnek.ToList());
                DatedbOne.DataSource = qieryAsList;
                dataGridView1.DataSource = DatedbOne;

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            YearIndex = (int)numericUpDown1.Value;
            if (_db.Peoples.Count(i => i.Year == YearIndex) < 1)
            {
                MessageBox.Show("Don`t booked");

            }
            else
            {

                BindingSource DatedbOne = new BindingSource();
                var DatedbOnek = from w in _db.Peoples.Local
                                 where w.Year == YearIndex
                                 select w;
                var qieryAsList = new BindingList<People>(DatedbOnek.ToList());
                DatedbOne.DataSource = qieryAsList;
                dataGridView1.DataSource = DatedbOne;

            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {


                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;

                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                date addHolForm = new date(id);
                DialogResult result = addHolForm.ShowDialog(this);

                if (result == DialogResult.OK)
                    return;

            }
        }

        private void Sort_Load(object sender, EventArgs e)
        {

        }
    }
}
