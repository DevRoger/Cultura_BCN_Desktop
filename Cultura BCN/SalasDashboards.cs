using Cultura_BCN.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cultura_BCN
{
    public partial class SalasDashboards : Form
    {
        public SalasDashboards()
        {
            InitializeComponent();
            using (var context = new CulturaBCNEntities())
            {
                var list = context.salas.ToList();
                dataGridViewSalas.DataSource = list;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
            
        }

        private void buttonEvents_Click(object sender, EventArgs e)
        {
            EventsDashboard f = new EventsDashboard();
            f.Show();
            this.Hide();
        }

        private void dataGridViewSala_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonUsaurios_Click(object sender, EventArgs e)
        {
            UsersDashboard f = new UsersDashboard();
            f.Show();
            this.Hide();
        }

        private void buttonSalas_Click(object sender, EventArgs e)
        {
            SalasDashboards f = new SalasDashboards();
            f.Show();
            this.Hide();
        }

        private void buttonBockings_Click(object sender, EventArgs e)
        {
            ReservationsDashboard f = new ReservationsDashboard();
            f.Show();
            this.Hide();
        }

        private void buttonOff_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SalasDashboards_Load(object sender, EventArgs e)
        {

        }

        private void createSalas_Click(object sender, EventArgs e)
        {
            CreateSala createSala = new CreateSala();
            createSala.Show();
            this.Hide();
        }
    }
}
