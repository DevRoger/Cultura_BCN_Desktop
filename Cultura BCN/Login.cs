using Cultura_BCN.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cultura_BCN
{
    public partial class Login : Form
    {
        private static string clave = "1234567890123456";
        public Login()
        {
            InitializeComponent();
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            string email = textBoxEmail.Text;
            string passwordEncript = Encriptar(textBoxPassword.Text);
            usuarios user = new usuarios();
            using (var context = new CulturaBCNEntities())
            {
                user = context.usuarios.Where(u => u.correo == email && u.contrasena_hash == passwordEncript).FirstOrDefault();
            }
            if(user == null)
            {
                MessageBox.Show("El usuari o la contrasenya no son correctes.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (user.id_rol == 2)
            {
                MessageBox.Show("Els usuaris clients no poden accedir a la aplicació.", "Atenció", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Dashboard d = new Dashboard(user);
                d.Show();
                this.Hide();
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
    }
}
