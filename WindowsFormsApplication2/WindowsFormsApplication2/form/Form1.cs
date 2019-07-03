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
    public partial class Form1 : Form
    {
        WorkerContext db;
        public Form1()
        {
            db = new WorkerContext();
            InitializeComponent();
            db.Peoples.Load();
            dataGridView1.DataSource = db.Peoples.Local.ToBindingList();
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add addForm = new Add();
            DialogResult result = addForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            People people = new People();
            people.Name = addForm.textBox1.Text;
            people.LastName = addForm.textBox2.Text;
            people.Day = (int)addForm.numericUpDown1.Value;
            db.Peoples.Add(people);
            db.SaveChanges();
            MessageBox.Show("New person");
        }

        public void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                HolyDay holydayn = new HolyDay();
              
              
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                  if (converted == false)
                    return;
                
               
                holydayn.People = db.Peoples.Find(id);
                AddHol addHolForm = new AddHol(holydayn.People.Day);
                
           
                DialogResult result = addHolForm.ShowDialog(this);
                holydayn.FirstDate = addHolForm.dateTimePicker1.Value;


                holydayn.IndexDate = false;
                addHolForm.dateTimePicker2.MaxDate= addHolForm.dateTimePicker1.Value.AddDays(holydayn.People.Day);
              holydayn.SecontDate = addHolForm.dateTimePicker2.Value;
                db.HolyDays.Add(holydayn);
                db.SaveChanges();
                if (result == DialogResult.Cancel)
                    return;
                MessageBox.Show("New date add");
            }
                
               
               
        }
      
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                People peopl = db.Peoples.FirstOrDefault(c=> c.Id==id);

                
                
                db.Peoples.Remove(peopl);
                   
                    db.SaveChanges();
                
                


                MessageBox.Show("object delete");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
    }
}
