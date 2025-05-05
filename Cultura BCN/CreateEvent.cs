using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cultura_BCN
{
    public partial class CreateEvent : Form
    {
        public CreateEvent()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CreateEvent_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonEvents_Click(object sender, EventArgs e)
        {
            EventsDashboard events = new EventsDashboard();
            events.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
        }

        private void buttonUsaurios_Click(object sender, EventArgs e)
        {
            UsersDashboard users = new UsersDashboard();
            users.Show();
            this.Hide();
        }

        private void buttonSalas_Click(object sender, EventArgs e)
        {
            SalasDashboards dashboards = new SalasDashboards();
            dashboards.Show();
            this.Hide();
        }

        private void buttonBockings_Click(object sender, EventArgs e)
        {
            ReservationsDashboard reservations = new ReservationsDashboard();
            reservations.Show();
            this.Hide();
        }

        private void buttonOff_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBoxEnumerated_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEnumerated.Checked)
            {
                this.BackgroundImage = Properties.Resources.Frame_4_with_enum;
                textBoxColumns.Show();
                textBoxRows.Show();
                textBoxCapacity.Hide();
            }
            else
            {
                this.BackgroundImage = Properties.Resources.Frame_4_not_enum;
                textBoxColumns.Hide();
                textBoxRows.Hide();
                textBoxCapacity.Show();
            }
            
        }
    }
}
