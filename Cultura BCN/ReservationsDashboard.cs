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
                foreach(Cultura_BCN.Model.reservas_entradas  reserva in list){
                    string nombre = context.usuarios
                           .Where(u => u.id_usuario == reserva.id_usuario)
                           .Select(u => u.correo)
                           .FirstOrDefault();
                    int numero = context.asientos.Where(a => a.id_asiento == reserva.id_asiento ).Select(a  => a.numero).FirstOrDefault();
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
    }
}
