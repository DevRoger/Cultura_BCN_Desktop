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
    public partial class ReservationsDashboard : Form
    {
        public ReservationsDashboard()
        {
            InitializeComponent();
            using (var context = new CulturaBCNEntities())
            {
                var listFinal = new List<DTOReservations>();
                var list = context.reservas_entradas.ToList();
                foreach(reservas_entradas  reserva in list){
                    string nombre = context.usuarios
                           .Where(u => u.id_usuario == reserva.id_usuario)
                           .Select(u => u.correo)
                           .FirstOrDefault();
                    string numero = context.asientos.Where(a => a.id_asiento == reserva.id_asiento ).Select(a  => a.numero).FirstOrDefault();
                    listFinal.Add(new DTOReservations(reserva.id_reserva,nombre,numero,reserva.fecha_reserva));
                }
                dataGridViewUsers.DataSource = listFinal;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
        }

        private void buttonUsaurios_Click(object sender, EventArgs e)
        {
            UsersDashboard usersDashboard = new UsersDashboard();
            usersDashboard.Show();
            this.Hide();
        }

        private void buttonSalas_Click(object sender, EventArgs e)
        {
            SalasDashboards dashboards = new SalasDashboards(); 
            dashboards.Show();
            this.Hide();
        }

        private void buttonEvents_Click(object sender, EventArgs e)
        {
            EventsDashboard eventsDashboard = new EventsDashboard();
            eventsDashboard.Show();
            this.Hide();
        }

        private void buttonBockings_Click(object sender, EventArgs e)
        {

        }

        private void buttonOff_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReservationsDashboard_Load(object sender, EventArgs e)
        {

        }

        private void deleteReservations_Click(object sender, EventArgs e)
        {
            List<reservas_entradas> reservasSeleccionados = new List<reservas_entradas>();

            foreach (DataGridViewRow row in dataGridViewUsers.SelectedRows)
            {

                using (var context = new CulturaBCNEntities())
                {
                    reservasSeleccionados.Add(context.reservas_entradas.Find(row.Cells[0].Value));
                }
            }
                if (reservasSeleccionados.Count()> 0)
                {
                    if (MessageBox.Show("Vols eliminar les fileres seleccionades?.", "Atenció", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        using (var context = new CulturaBCNEntities())
                        {
                            foreach (reservas_entradas res in reservasSeleccionados)
                            {
                                context.reservas_entradas.Remove(context.reservas_entradas.Find(res.id_reserva));
                            }
                            context.SaveChanges();
                        }
                        MessageBox.Show("Les reserves han sigut eliminades de forma exitosa.", "Éxit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                    }
                                 
                }
                else
                {
                    MessageBox.Show("Has de seleccionar com a minim una reserva per poder eliminar.", "Atenció", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
        }
    }
}
