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

        private void editSalas_Click(object sender, EventArgs e)
        {
            List<salas> salasSeleccionados = new List<salas>();

            foreach (DataGridViewRow row in dataGridViewSalas.SelectedRows)
            {
                if (row.DataBoundItem is salas sala)
                {
                    salasSeleccionados.Add(sala);
                }
            }
            if (salasSeleccionados.Count() == 0)
            {
                MessageBox.Show("Has de seleccionar una sala per poder editar.", "Atenció", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (salasSeleccionados.Count() > 1)
            {
                MessageBox.Show("No pots seleccionar més d'una sala per editar.", "Atenció", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CreateSala editEvent = new CreateSala(salasSeleccionados[0]);
                editEvent.Show();
                this.Hide();
            }
        }

        private void deleteSalas_Click(object sender, EventArgs e)
        {
            List<salas> salasSeleccionados = new List<salas>();

            foreach (DataGridViewRow row in dataGridViewSalas.SelectedRows)
            {
                if (row.DataBoundItem is salas sala)
                {
                    salasSeleccionados.Add(sala);
                }
            }
            if (salasSeleccionados.Count() == 0)
            {
                MessageBox.Show("Has de seleccionar com a minim una sala per poder eliminar.", "Atenció", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else 
            {
                
                using (var context = new CulturaBCNEntities())
                {
                    foreach (salas sala in salasSeleccionados)
                    {
                        var listEvents = context.eventos.Where(sal => sal.id_sala == sala.id_sala).ToList();

                        if (listEvents.Count() > 0)
                        {
                            if (MessageBox.Show("Vols eliminar la sala " + sala.nombre+ "? Això eliminara els events on tenen lloc a aquestes sales.", "Atenció", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                foreach(eventos events in listEvents)
                                {
                                    context.eventos.Remove(events);
                                }
                                context.salas.Remove(context.salas.Find(sala.id_sala));
                            }
                        }
                        else
                        {
                            if (MessageBox.Show("Vols eliminar la sala " + sala.nombre+ "?", "Atenció", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                context.salas.Remove(context.salas.Find(sala.id_sala));
                            }
                        }
                    }
                    context.SaveChanges();
                    
                }
                    MessageBox.Show("No pots seleccionar més de un event per editar.", "Atenció", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }
    }
}
