using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cultura_BCN.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Cultura_BCN
{
    public partial class CreateUser : Form
    {
        private static string clave = "1234567890123456";
        private bool image = false;
        private bool newUser = true;
        private usuarios user;
        private usuarios u;
        private string originalPassword = null;
        public CreateUser(usuarios user)
        {
            InitializeComponent();
            using (var context = new CulturaBCNEntities())
            {
                var listaRols = context.roles.ToList();
                comboBoxRol.DataSource = listaRols;
                comboBoxRol.DisplayMember = "nombre";
                comboBoxRol.ValueMember = "id_rol";
            }

            this.user = user;
        }
        public CreateUser(usuarios user,usuarios user1) { 
            InitializeComponent();
            using (var context = new CulturaBCNEntities())
            {
                var listaRols = context.roles.ToList();
                comboBoxRol.DataSource = listaRols;
                comboBoxRol.DisplayMember = "nombre";
                comboBoxRol.ValueMember = "id_rol";
            }
            textBoxName.Text = user.nombre;
            textBoxSurname.Text = user.apellidos;
            textBoxEmail.Text = user.correo;
            textBoxPhone.Text = user.telefono;
            textBoxPassword.Text = Desencriptar(user.contrasena_hash);
            originalPassword = textBoxPassword.Text;
            dateTimePickerDateBirth.Value = user.fecha_nacimiento;
            image = true;
            newUser = false;
            u = user;
            buttonCreate.Text = "Actualitzar";
            APICalls.GETImage(user.foto_url, pictureBoxAvatar);
            this.user = user1;
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

        private void buttonAddImage_Click(object sender, EventArgs e)
        {
            // Crear un cuadro de diálogo para seleccionar archivos
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Selecciona l'imatge";
            openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBoxAvatar.Image = Image.FromFile(openFileDialog.FileName);
                pictureBoxAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
                image = true;
            }
        }
        private static string Encriptar(string textoPlano)
        {
            byte[] iv = new byte[16]; // IV de 16 bytes en cero (para demo)
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(clave); // clave debe tener 16, 24 o 32 caracteres
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(textoPlano);
                    sw.Close();
                    array = ms.ToArray();
                }
            }

            return Convert.ToBase64String(array); // texto cifrado
        }

        private static string Desencriptar(string textoCifrado)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(textoCifrado);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(clave);
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(buffer))
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (StreamReader reader = new StreamReader(cs))
                {
                    return reader.ReadToEnd(); // texto plano
                }
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            string error = checkedError();
            if (error != "Error :\n")
            {
                MessageBox.Show(error, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(newUser)
            {

                usuarios user = new usuarios();
                user.nombre = textBoxName.Text;
                user.apellidos = textBoxSurname.Text;
                user.contrasena_hash = Encriptar(textBoxPassword.Text);
                user.correo = textBoxEmail.Text;
                user.fecha_nacimiento = dateTimePickerDateBirth.Value;
                user.telefono = textBoxPhone.Text;
                user.id_rol = int.Parse(comboBoxRol.SelectedValue.ToString());

                APICalls.POSTuser(user, pictureBoxAvatar.Image);

                MessageBox.Show("El usuari ha sigut creat de forma exitosa.", "Éxit", MessageBoxButtons.OK, MessageBoxIcon.Information);

                UsersDashboard usersDashboard = new UsersDashboard(this.user);
                usersDashboard.Show();
                this.Hide();

            }
            else
            {
                u.nombre = textBoxName.Text;
                u.apellidos = textBoxSurname.Text;
                u.contrasena_hash = Encriptar(textBoxPassword.Text);
                u.correo = textBoxEmail.Text;
                u.fecha_nacimiento = dateTimePickerDateBirth.Value;
                u.telefono = textBoxPhone.Text;
                u.id_rol = int.Parse(comboBoxRol.SelectedValue.ToString());

                APICalls.PUTuser(u, pictureBoxAvatar.Image);

                MessageBox.Show("El usuari ha sigut editat de forma exitosa.", "Éxit", MessageBoxButtons.OK, MessageBoxIcon.Information);

                UsersDashboard usersDashboard = new UsersDashboard(this.user);
                usersDashboard.Show();
                this.Hide();
            }
            
        }
        private string checkedError()
        {
            string error = "Error :\n";
            if (textBoxName.Text == "")
            {
                error += "Has de afegir el nom de l'usuari.\n";
            }
            if (textBoxSurname.Text == "")
            {
                error += "Has de afegir el cognom de l'usuari.\n";
            }
            if (textBoxEmail.Text == "")
            {
                error += "Has de afegir el correu de l'usuari.\n";
            }
            if (textBoxPassword.Text == "")
            {
                error += "Has de afegir el contrasenya de l'usuari.\n";
            }
            if (textBoxPhone.Text == "")
            {
                error += "Has de afegir el telefon de l'usuari.\n";
            }
            if (!image)
            {
                error += "Has de afegir una imatge per l'usuari.\n";
            }
            if(originalPassword == null || originalPassword != textBoxPassword.Text)
            {
                using (var context = new CulturaBCNEntities())
                {
                    var objec = context.usuarios.Where(u => u.correo == textBoxEmail.Text).ToList();
                    if (objec.Count() != 0)
                    {
                        error += "Aquest correu ja existeix.\n";
                    }
                }
            }
            
            return error;
        }
    }
}
