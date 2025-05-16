using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cultura_BCN.Model;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Cultura_BCN
{
    public partial class CreateEvent : Form
    {
        private bool image = false;
        private bool newUser = true;
        private eventos even;
        public CreateEvent()
        {
            InitializeComponent();
            prepareComponents();
            
        }
        private void prepareComponents()
        {
            using (var context = new CulturaBCNEntities())
            {
                var list = context.salas.ToList();
                comboBoxPlace.DataSource = list;
                comboBoxPlace.DisplayMember = "nombre";
                comboBoxPlace.ValueMember = "id_sala";
            }
            List<Pegi> listAge = new List<Pegi> { new Pegi(3, "3"), new Pegi(7, "7"), new Pegi(12, "12"), new Pegi(16, "16"), new Pegi(18, "+18") };
            comboBoxAge.DataSource = listAge;
            comboBoxAge.DisplayMember = "name";
            comboBoxAge.ValueMember = "value";
            dateTimePickerStart.Format = DateTimePickerFormat.Custom;
            dateTimePickerStart.CustomFormat = "HH:mm";
            dateTimePickerStart.ShowUpDown = true;
            dateTimePickerEnd.Format = DateTimePickerFormat.Custom;
            dateTimePickerEnd.CustomFormat = "HH:mm";
            dateTimePickerEnd.ShowUpDown = true;
        }
        public CreateEvent(eventos e)
        {
            InitializeComponent();
            newUser = false;
            image = true;
            checkBoxEnumerated.Visible = false;
            textBoxCapacity.Visible = false;
            textBoxColumns.Visible = false;
            textBoxRows.Visible = false;
            even = e;
            this.BackgroundImage = Properties.Resources.Frame_4_ocult_all;
            prepareComponents();
            putDataIn();


        }
        private void putDataIn()
        {
            textBoxName.Text = even.nombre;
            textBoxDescription.Text = even.descripcion;
            comboBoxPlace.SelectedValue = even.id_sala;
            comboBoxAge.SelectedValue = even.edad_minima;
            dateTimePickerDate.Value = even.fecha;
            dateTimePickerStart.Value = DateTime.Today.Add(even.hora_inicio);
            dateTimePickerEnd.Value = DateTime.Today.Add(even.hora_fin);
            textBoxPrice.Text = even.precio.ToString();
            
            APICalls.GETImage(even.foto_url,pictureBoxEvent);
            buttonCreate.Text = "Actualitza";
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

        private void button1_Click(object sender, EventArgs e)
        {
            // Crear un cuadro de diálogo para seleccionar archivos
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Selecciona l'imatge";
            openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBoxEvent.Image = System.Drawing.Image.FromFile(openFileDialog.FileName);
                pictureBoxEvent.SizeMode = PictureBoxSizeMode.StretchImage;
                image = true;
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            string error = checkError();

            if (error != "Error:\n")
            {
                MessageBox.Show(error, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (newUser)
            {
                eventos evento = new eventos();

                evento.nombre = textBoxName.Text;
                evento.descripcion = textBoxDescription.Text;
                evento.precio = decimal.Parse(textBoxPrice.Text, new CultureInfo("es-ES"));
                evento.id_sala = int.Parse(comboBoxPlace.SelectedValue.ToString());
                evento.lugar = comboBoxPlace.Text;
                evento.edad_minima = int.Parse(comboBoxAge.SelectedValue.ToString());
                evento.enumerado = checkBoxEnumerated.Checked;
                evento.hora_inicio = new TimeSpan(dateTimePickerStart.Value.Hour, dateTimePickerStart.Value.Minute, 0);
                evento.hora_fin = new TimeSpan(dateTimePickerEnd.Value.Hour, dateTimePickerEnd.Value.Minute, 0);
                evento.fecha = dateTimePickerDate.Value;

                APICalls.POSTevent(evento, pictureBoxEvent.Image);
                System.Threading.Thread.Sleep(1000);
                using (var context = new CulturaBCNEntities()){
                    
                    evento = context.eventos.OrderByDescending(u => u.id_evento).FirstOrDefault();
                }

                if (checkBoxEnumerated.Checked)
                {
                    int columns = int.Parse(textBoxColumns.Text);
                    int rows = int.Parse(textBoxRows.Text);
                    using (var context = new CulturaBCNEntities())
                    {
                        for (int i = 0; i < rows; i++)
                        {
                            for (int j = 0; j < columns; j++)
                            {
                                context.asientos.Add(new asientos(i + "-" + j, evento.id_evento));
                            }
                        }
                        context.SaveChanges();
                    }
                }
                else
                {
                    using (var context = new CulturaBCNEntities())
                    {
                        for (int i = 0; i < int.Parse(textBoxCapacity.Text); i++)
                        {
                            context.asientos.Add(new asientos(i.ToString(), evento.id_evento));
                        }
                        context.SaveChanges();
                    }
                }
                MessageBox.Show("L'event ha sigut creat de forma exitosa.", "Éxit", MessageBoxButtons.OK, MessageBoxIcon.Information);

                EventsDashboard eventsDashboard = new EventsDashboard();
                eventsDashboard.Show();
                this.Hide();
            }
            else
            {
                eventos evento = new eventos();

                evento.id_evento = even.id_evento;
                evento.foto_url = even.foto_url;
                evento.enumerado = even.enumerado;
                evento.nombre = textBoxName.Text;
                evento.descripcion = textBoxDescription.Text;
                string txt = textBoxPrice.Text.Replace(",", ".");
                decimal precio = decimal.Parse(txt, CultureInfo.InvariantCulture);
                MessageBox.Show("Precio que se va a guardar: " + precio);
                evento.precio = precio;
                evento.id_sala = int.Parse(comboBoxPlace.SelectedValue.ToString());
                evento.lugar = comboBoxPlace.Text;
                evento.edad_minima = int.Parse(comboBoxAge.SelectedValue.ToString());
                evento.hora_inicio = new TimeSpan(dateTimePickerStart.Value.Hour, dateTimePickerStart.Value.Minute, 0);
                evento.hora_fin = new TimeSpan(dateTimePickerEnd.Value.Hour, dateTimePickerEnd.Value.Minute, 0);
                evento.fecha = dateTimePickerDate.Value;

                APICalls.PUTevent(evento, pictureBoxEvent.Image);
                
                
                EventsDashboard eventsDashboard = new EventsDashboard();
                eventsDashboard.Show();
                this.Hide();
            }
        }
        private string checkError()
        {
            string error = "Error:\n";

            if (textBoxName.Text == "") {
                error += "Has d'afegir un nom per l'event.\n";
            }
            if (textBoxDescription.Text == "")
            {
                error += "Has d'afegir una descripció per l'event.\n";
            }
            DateTime horaInicio = dateTimePickerStart.Value;
            DateTime horaFin = dateTimePickerEnd.Value;
            if (horaInicio > horaFin)
            {
                error += "L'hora d'inici no pot ser posterior a l'hora de fi.\n";
            }
            if (textBoxPrice.Text == "")
            {
                error += "Has d'afegir un preu per l'entrada a l'event.\n";
            }
            if(!image)
            {
                error += "Has d'afegir una imatge per l'event.\n";
            }
            if (newUser)
            {
                if (checkBoxEnumerated.Checked)
                {
                    if (textBoxColumns.Text == "")
                    {
                        error += "Has d'afegir columnes de sellents per l'event.\n";
                    }
                    if (textBoxRows.Text == "")
                    {
                        error += "Has d'afegir files de sellents per l'event.\n";
                    }
                }
                else
                {
                    if (textBoxCapacity.Text == "")
                    {
                        error += "Has d'afegir un aforament per l'event.\n";
                    }
                }
            }
            
            return error;
        }

        private void textBoxPrice_Leave(object sender, EventArgs e)
        {
            if (decimal.TryParse(textBoxPrice.Text, NumberStyles.Number, new CultureInfo("es-ES"), out decimal precio))
            {
                textBoxPrice.Text = precio.ToString("N2", new CultureInfo("es-ES"));
            }
        }
    }
}
