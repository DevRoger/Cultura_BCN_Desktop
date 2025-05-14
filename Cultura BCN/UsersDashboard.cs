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
    public partial class UsersDashboard : Form
    {
        public UsersDashboard()
        {
            InitializeComponent();
            using (var context = new CulturaBCNEntities())
            {
                var list = context.usuarios.ToList();
                dataGridViewUsers.DataSource = list;
            }
            dataGridViewUsers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        private void Users_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
        }

        private void buttonUsaurios_Click(object sender, EventArgs e)
        {
            UsersDashboard dashboard = new UsersDashboard();
            dashboard.Show();
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
            EventsDashboard dashboard = new EventsDashboard();
            dashboard.Show();
            this.Hide();
        }

        private void buttonBockings_Click(object sender, EventArgs e)
        {
            ReservationsDashboard dashboard = new ReservationsDashboard();
            dashboard.Show();
            this.Hide();
        }

        private void buttonOff_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void createUsers_Click(object sender, EventArgs e)
        {
            CreateUser createUser = new CreateUser();
            createUser.Show();
            this.Hide();
        }

        private void dataGridViewUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void editUsers_Click(object sender, EventArgs e)
        {
            List<usuarios> usuariosSeleccionados = new List<usuarios>();

            foreach (DataGridViewRow row in dataGridViewUsers.SelectedRows)
            {
                if (row.DataBoundItem is usuarios usuario)
                {
                    usuariosSeleccionados.Add(usuario);
                }
            }
            if (usuariosSeleccionados.Count()==0) {
                MessageBox.Show("Has de seleccionar un usuari per poder editar.", "Atenció", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }else if (usuariosSeleccionados.Count() > 1)
            {
                MessageBox.Show("No pots seleccionar més de un usuari per editar.", "Atenció", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CreateUser editUser = new CreateUser(usuariosSeleccionados[0]);
                editUser.Show();
                this.Hide();
            }
        }

        private void deleteUsers_Click(object sender, EventArgs e)
        {
            List<usuarios> usuariosSeleccionados = new List<usuarios>();

            foreach (DataGridViewRow row in dataGridViewUsers.SelectedRows)
            {
                if (row.DataBoundItem is usuarios usuario)
                {
                    usuariosSeleccionados.Add(usuario);
                }
            }
            if (usuariosSeleccionados.Count() > 0)
            {
               if(MessageBox.Show("Vols eliminar les fileres seleccionades?.", "Atenció", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK){
                    using (var context = new CulturaBCNEntities())
                    {
                        
                        foreach (usuarios user in usuariosSeleccionados)
                        {
                            var listReserv = context.reservas_entradas.Where(r => r.id_usuario == user.id_usuario).ToList();
                            foreach (reservas_entradas res in listReserv)
                            {
                                context.reservas_entradas.Remove(res);
                            }
                            var listChats = context.chats.Where(chat => chat.id_usuario_1 == user.id_usuario || chat.id_usuario_2 == user.id_usuario).ToList();

                            foreach (chats chat in listChats)
                            {
                                var listMessages = context.mensajes.Where(mes => mes.id_chat == chat.id_chat).ToList();
                                foreach (mensajes mensaje in listMessages)
                                {
                                    context.mensajes.Remove(mensaje);
                                }
                                context.chats.Remove(chat);
                            }
                            context.usuarios.Remove(context.usuarios.Find(user.id_usuario));
                        }
                        context.SaveChanges();
                        var list = context.usuarios.ToList();
                        dataGridViewUsers.DataSource = list;
                        MessageBox.Show("El usuari ha sigut eliminat de forma exitosa.", "Éxit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else if (usuariosSeleccionados.Count() == 0)
            {
                MessageBox.Show("Has de seleccionar com a minim un usuari per poder eliminar.", "Atenció", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
