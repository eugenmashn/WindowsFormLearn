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

    public delegate void ONupdate();
    public partial class date : Form
    {

        public event ONupdate onupdate;
        int _Id;
        HolyDay personOfweekend;
        WorkerContext _db;
        public class HollydayTwo {
            public int Id;
            public DateTime FirstDate;
            public DateTime SecondDate;
        }
        protected virtual void Onstartevent() {
            if (onupdate != null) {
                onupdate();
            }
        }

        public date(int Id)
        {
            InitializeComponent();
            _db = new WorkerContext();

       
            _Id = Id;
            _db.Peoples.Load();
            _db.HolyDays.Load();
            if (_db.HolyDays.Count(i=>i.Peopleid==_Id)<1)
            {
               MessageBox.Show("Don`t have weekend");
            
            }
            else {
               
            BindingSource DatedbOne = new BindingSource();
            var DatedbOneK=from w in _db.HolyDays.Local
                       where (w.Peopleid== _Id)
                       select w;
             personOfweekend = _db.HolyDays.FirstOrDefault(q => q.People.Id == _Id);
            checkBox1.Checked = personOfweekend.IndexDate;
            var qieryAsList = new BindingList<HolyDay>(DatedbOneK.ToList());
            DatedbOne.DataSource = qieryAsList;
            dataGridView1.DataSource = DatedbOne;
         }
         
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void date_Load(object sender, EventArgs e)
        {

        }
   

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int DaysRegain;
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                HolyDay peoplday = _db.HolyDays.FirstOrDefault(c => c.Id == id);
                People people = _db.Peoples.FirstOrDefault(c => c.Id == peoplday.Peopleid);
                BindingSource DatedbOne = new BindingSource();
                var DatedbOneK = from w in _db.HolyDays.Local
                                 where (w.Peopleid == _Id)
                                 select w;

                if (peoplday.IndexDate == true)
                {
                    MessageBox.Show("it is used");
                }
                else
                {

                    DaysRegain = peoplday.People.Day + (peoplday.SecontDate-peoplday.FirstDate).Days;
                    people.Day = DaysRegain;
                   _db.HolyDays.Remove(peoplday);

                    _db.SaveChanges();

                    DatedbOneK = from w in _db.HolyDays.Local
                                 where (w.Peopleid == _Id)
                                 select w;
                   
                    var qieryAsList = new BindingList<HolyDay>(DatedbOneK.ToList());
                    DatedbOne.DataSource = qieryAsList;
                    dataGridView1.DataSource = DatedbOne;
            
                    dataGridView1.Update();
                    dataGridView1.Refresh();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                HolyDay peoplday = _db.HolyDays.FirstOrDefault(c => c.Id == id);
                peoplday.IndexDate = true;
                _db.SaveChanges();

             
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Onstartevent();
        }
    }
}
