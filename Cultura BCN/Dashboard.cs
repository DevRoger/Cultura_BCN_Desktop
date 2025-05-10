using Cultura_BCN.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cultura_BCN
{
    public partial class Dashboard : Form
    {
        private string timeHoure;
        public Dashboard()
        {
            InitializeComponent();
            string date = DateTime.Now.ToString("d MMMM yyyy", new CultureInfo("ca-ES"));
            labelDate.Text = date;
            timeHoure = DateTime.Now.ToString("HH:mm");
            timeClock.Text = timeHoure;
            Thread threadTime = new Thread(() =>
            {
                while (true)
                {
                    string newDate = DateTime.Now.ToString("HH:mm");
                    if (newDate != timeHoure) { 
                        timeHoure = newDate;
                        timeClock.Invoke(new Action(() =>
                        {
                            timeClock.Text = timeHoure;
                        }));
                    }
                    Thread.Sleep(1000); 
                }
            });

            threadTime.IsBackground = true;

            using (var context = new CulturaBCNEntities()) {
                labelTotalClients.Text = context.usuarios.Count().ToString(); }

            threadTime.Start();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            UsersDashboard usersDashboard = new UsersDashboard();
            usersDashboard.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            ReservationsDashboard reservationsDashboard = new ReservationsDashboard();
            reservationsDashboard.Show();
            this.Hide();
        }

        private void buttonOff_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void buttonEvents_Click(object sender, EventArgs e)
        {
            EventsDashboard f = new EventsDashboard();
            f.Show();
            this.Hide();
        }

        private void buttonSalas_Click(object sender, EventArgs e)
        {
            SalasDashboards salasDashboards = new SalasDashboards();
            salasDashboards.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
