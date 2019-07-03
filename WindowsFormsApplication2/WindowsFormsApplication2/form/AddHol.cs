using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class AddHol : Form
    {
        int Day;
        int _Day;
        int Daysecond;
        public AddHol(int _day)
        {
            Day = _day;
            _Day = _day;
            InitializeComponent();
            dateTimePicker1.ValueChanged += Limited;
            dateTimePicker2.MaxDate = dateTimePicker1.Value.AddDays(Day);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
        private void Limited(object sender,EventArgs e) {
            DateTime CountDate = dateTimePicker1.Value;
            for (int i = 0; i <= Day; i++) {
                if ((int)CountDate.DayOfWeek == 0 || (int)CountDate.DayOfWeek == 6) {
                    Day++;
                   CountDate= CountDate.AddDays(1);
                }
       
            }
            dateTimePicker2.MaxDate = dateTimePicker1.Value.AddDays(Day);
                Day = _Day;
        }
        private void AddHol_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
