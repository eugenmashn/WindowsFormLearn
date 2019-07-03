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
        static WorkerContext db;
        public Form1()
        {
            db = new WorkerContext();
            //_db = new WorkerContext();
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
            addForm.numericUpDown2.Value = (int)DateTime.Now.Year;
            DialogResult result = addForm.ShowDialog(this);
     

            if (result == DialogResult.Cancel)
                return;
          
            People people = new People();
            people.Name = addForm.textBox1.Text;
            people.LastName = addForm.textBox2.Text;
            people.Day = (int)addForm.numericUpDown1.Value;
            people.Year = (int)addForm.numericUpDown2.Value;
            db.Peoples.Add(people);
            db.SaveChanges();
            dataGridView1.DataSource = null;
            dataGridView1.Update();
            db.Peoples.Load();
            dataGridView1.DataSource = db.Peoples.AsNoTracking().ToList();
            dataGridView1.Refresh();

        }

        public void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                HolyDay holydayn = new HolyDay();
                People person=new People();
                int Daysu;
                int Dayx;
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                  if (converted == false)
                    return;
                   db.Peoples.AsNoTracking().ToList();
                     db.Peoples.Load();
              
                person=db.Peoples.ToList().FirstOrDefault(i => i.Id == id);
                db.Entry(person).Reload();
                db.Entry(person).State = EntityState.Modified;
                // holydayn.People = db.Peoples.AsNoTracking().ToList().FirstOrDefault(i=>i.Id==id);
                holydayn.Peopleid = person.Id;
                AddHol addHolForm = new AddHol(person.Day);
                DateTime date = new DateTime(person.Year, 1, 1);
                if (person.Day < 1) {
                    MessageBox.Show("don`t have weekend!!!");
                    return;
                }
                  addHolForm.dateTimePicker1.Value = date;
                addHolForm.dateTimePicker2.MaxDate = addHolForm.dateTimePicker1.Value.AddDays(person.Day);
                addHolForm.dateTimePicker2.MinDate = addHolForm.dateTimePicker1.Value;
                DialogResult result = addHolForm.ShowDialog(this);
             
                holydayn.FirstDate = addHolForm.dateTimePicker1.Value;
           
                holydayn.IndexDate = false;
                addHolForm.dateTimePicker2.MaxDate= addHolForm.dateTimePicker1.Value.AddDays(person.Day);
              holydayn.SecontDate = addHolForm.dateTimePicker2.Value;
                 if (result == DialogResult.Cancel)
                    return;
                Daysu = person.Day - holydayn.SecontDate.Subtract(holydayn.FirstDate).Days;
                person.Day = Daysu;
                holydayn.Days = holydayn.SecontDate.Subtract(holydayn.FirstDate).Days;
                db.HolyDays.Add(holydayn);
                db.SaveChanges();
                dataGridView1.DataSource = null;
                dataGridView1.Update();
                db.Peoples.Load();
                dataGridView1.DataSource = db.Peoples.AsNoTracking().ToList();
                dataGridView1.Refresh();
               
           
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
                dataGridView1.DataSource = null;
                dataGridView1.Update();
                db.Peoples.Load();
                dataGridView1.DataSource = db.Peoples.AsNoTracking().ToList();
                dataGridView1.Refresh();
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

                addHolForm.onupdate += new ONupdate(evenstb);
                DialogResult result = addHolForm.ShowDialog(this);
              dataGridView1.Update();
              dataGridView1.Refresh();
                if (result == DialogResult.OK) {
                    addHolForm.Close();
                   
                    return;
                }
             
            }
            dataGridView1.DataSource = null;
            dataGridView1.Update();
            db.Peoples.Load();
            dataGridView1.DataSource = db.Peoples.AsNoTracking().ToList();
            dataGridView1.Refresh();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
             
                int id = 0;
           
                Add addForm = new Add();
                int index = 0;
                if (dataGridView1.RowCount > db.Peoples.Count())
                {
                 index = dataGridView1.RowCount - 1;
                 }
               else {
                index = dataGridView1.RowCount;
                  }
                for (int i = 0; i < index; i++) {
                    id = (int)dataGridView1[0, i].Value;
                People people = new People();
                People peopleOne = new People();
                peopleOne = db.Peoples.Find(id);
                     

                peopleOne.Day = (int)peopleOne.Day+18;
                peopleOne.Year = (int)peopleOne.Year+1;
               
               
                db.SaveChanges();
              }
                dataGridView1.DataSource = null;
                dataGridView1.Update();
                db.Peoples.Load();
                dataGridView1.DataSource = db.Peoples.AsNoTracking().ToList();
                dataGridView1.Refresh();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                People person = new People();
                person = db.Peoples.Find(id);
                Sort sort = new Sort(id,person.Name,person.LastName);
                DialogResult result = sort.ShowDialog(this);
                if (result == DialogResult.OK)
                    return;
            }
        }
        private void update(object sender, EventArgs e) {

            this.Refresh();
        }
        void evenstb()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Update();
            db.Peoples.Load();
            dataGridView1.DataSource = db.Peoples.AsNoTracking().ToList();
            dataGridView1.Refresh();
        }

        private void button7_Click(object sender, EventArgs e)
        {

            int id = 0;
            int index = 0;
            Add addForm = new Add();
            if (dataGridView1.RowCount > db.Peoples.Count())
            {
                index = dataGridView1.RowCount - 1;
            }
            else
            {
                index = dataGridView1.RowCount;
            }
            for (int i = 0; i < index; i++)
            {
                id = (int)dataGridView1[0, i].Value;
                People people = new People();
                People peopleOne = new People();
                peopleOne = db.Peoples.Find(id);

                if (peopleOne.Day < 0)
                {
                    peopleOne.Day = 0;
                    peopleOne.Year = (int)peopleOne.Year - 1;
                }
                else { 
                peopleOne.Day = (int)peopleOne.Day - 18;
                peopleOne.Year = (int)peopleOne.Year - 1;
                      }
                db.SaveChanges();
            }
            dataGridView1.DataSource = null;
            dataGridView1.Update();
            db.Peoples.Load();
            dataGridView1.DataSource = db.Peoples.AsNoTracking().ToList();
            dataGridView1.Refresh();
        }
    }
}
