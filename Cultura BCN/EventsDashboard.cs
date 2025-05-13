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
    public partial class EventsDashboard : Form
    {
        public EventsDashboard()
        {
            InitializeComponent();
            using (var context = new CulturaBCNEntities())
            {
                var list = context.eventos.ToList();
                dataGridViewEvents.DataSource = list;
            }
            dataGridViewEvents.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Dashboard d = new Dashboard();
            d.Show();
            this.Hide();
        }

        private void buttonOff_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            CreateEvent c = new CreateEvent();
            c.Show();
            this.Hide();
        }

        private void buttonUsaurios_Click(object sender, EventArgs e)
        {
            UsersDashboard d = new UsersDashboard();
            d.Show();
            this.Hide();
        }

        private void buttonSalas_Click(object sender, EventArgs e)
        {
            SalasDashboards d = new SalasDashboards();
            d.Show();
            this.Hide();
        }

        private void buttonEvents_Click(object sender, EventArgs e)
        {
            EventsDashboard d = new EventsDashboard();
            d.Show();
            this.Hide();
        }

        private void buttonBockings_Click(object sender, EventArgs e)
        {
            ReservationsDashboard d = new ReservationsDashboard();
            d.Show();
            this.Hide();
        }

        private void EventsDashboard_Load(object sender, EventArgs e)
        {

        }

        private void deleteEvents_Click(object sender, EventArgs e)
        {

        }

        private void editEvents_Click(object sender, EventArgs e)
        {
            List<eventos> eventosSeleccionados = new List<eventos>();

            foreach (DataGridViewRow row in dataGridViewEvents.SelectedRows)
            {
                if (row.DataBoundItem is eventos evento)
                {
                    eventosSeleccionados.Add(evento);
                }
            }
            if (eventosSeleccionados.Count() == 0)
            {
                MessageBox.Show("Has de seleccionar un event per poder editar.", "Atenció", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (eventosSeleccionados.Count() > 1)
            {
                MessageBox.Show("No pots seleccionar més de un event per editar.", "Atenció", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CreateEvent editEvent = new CreateEvent(eventosSeleccionados[0]);
                editEvent.Show();
                this.Hide();
            }
        }
    }
}
