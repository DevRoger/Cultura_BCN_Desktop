using Cultura_BCN.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cultura_BCN
{
    public partial class CreateSala : Form
    {
        public CreateSala()
        {
            InitializeComponent();
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
            ReservationsDashboard reservationsDashboard = new ReservationsDashboard();
            reservationsDashboard.Show();
            this.Hide();
        }

        private void buttonOff_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            string error = errorChecker();
            if (error != "Error:\n")
            {
                MessageBox.Show(error,"ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                using (var context = new CulturaBCNEntities())
                {
                    int total = int.Parse(textBoxTotalPeople.Text);
                    context.salas.Add(new salas(textBoxName.Text,textBoxAddress.Text,total));
                    context.SaveChanges();
                    MessageBox.Show("La sala ha sigut creada de forma exitosa.","Éxit",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                textBoxAddress.Clear();
                textBoxName.Clear();
                textBoxTotalPeople.Clear();
                SalasDashboards salasDashboards = new SalasDashboards();
                salasDashboards.Show();
                this.Hide();
            }
        }
        private string errorChecker()
        {
            string error = "Error:\n";
            if (textBoxName.Text == "")
            {
                error += "Ha de afegir un nom per a la sala.\n";
            }
            if (textBoxAddress.Text == "")
            {
                error += "Ha de afegir una direcció per a la sala.\n";
            }
            if (textBoxTotalPeople.Text == "")
            {
                error += "Ha de afegir un aforament per a la sala.\n";
            }
            if (Regex.IsMatch(textBoxTotalPeople.Text, "[a-zA-Z]"))
            {
                error += "El camp de aforament només accepta numeros.\n";
            }
            return error;
        }
    }
}
