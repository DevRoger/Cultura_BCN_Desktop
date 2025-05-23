﻿using Cultura_BCN.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
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
        private salas sala;
        private usuarios user;
        public CreateSala(usuarios usuarios)
        {
            InitializeComponent();
            this.user = usuarios;
        }
        public CreateSala(salas sala,usuarios user)
        {
            InitializeComponent();
            this.sala = sala;
            textBoxAddress.Text = sala.direccion;
            textBoxTotalPeople.Text = sala.aforo.ToString();
            textBoxName.Text = sala.nombre;
            buttonCreate.Text = "Actualitzar";
            this.user = user;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard(this.user);
            dashboard.Show();
            this.Hide();
        }

        private void buttonUsaurios_Click(object sender, EventArgs e)
        {
            UsersDashboard usersDashboard = new UsersDashboard(this.user);
            usersDashboard.Show();
            this.Hide();
        }

        private void buttonSalas_Click(object sender, EventArgs e)
        {
            SalasDashboards dashboards = new SalasDashboards(this.user);
            dashboards.Show();
            this.Hide();
        }

        private void buttonEvents_Click(object sender, EventArgs e)
        {
            EventsDashboard eventsDashboard = new EventsDashboard(this.user);
            eventsDashboard.Show();
            this.Hide();
        }

        private void buttonBockings_Click(object sender, EventArgs e)
        {
            ReservationsDashboard reservationsDashboard = new ReservationsDashboard(this.user);
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
            else if (sala == null)
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
                SalasDashboards salasDashboards = new SalasDashboards(this.user);
                salasDashboards.Show();
                this.Hide();
            }
            else
            {
                using (var context = new CulturaBCNEntities())
                {
                    var salaOriginal = context.salas.Find(sala.id_sala);
                    salaOriginal.nombre = textBoxName.Text;
                    salaOriginal.direccion = textBoxAddress.Text;
                    salaOriginal.aforo = int.Parse(textBoxTotalPeople.Text);
                    context.Entry(salaOriginal).State = EntityState.Modified;
                    context.SaveChangesAsync();
                }
                MessageBox.Show("La sala ha sigut actualitzada de forma exitosa.", "Éxit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SalasDashboards salasDashboards = new SalasDashboards(this.user);
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
