using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cultura_BCN.Model;

namespace Cultura_BCN
{
    public class PanelUsers
    {
        public PictureBox panel {  get; set; }
        public PictureBox avatar { get; set; }
        public Label titleUserName { get; set; }
        public Label titleEmail {  get; set; }
        public Label valueEmail { get; set; }
        public Label titleTele {  get; set; }
        public Label valueTele { get; set; }
        public Label titleAge { get; set; }
        public Label valueAge { get; set; }

        public PanelUsers(PictureBox panel, PictureBox avatar, Label titleUserName, Label titleEmail, Label valueEmail, Label titleTele, Label valueTele, Label titleAge, Label valueAge) { 
            this.panel = panel;
            this.avatar = avatar;
            this.titleUserName = titleUserName;
            this.titleEmail = titleEmail;
            this.valueEmail = valueEmail;
            this.titleTele = titleTele;
            this.valueTele = valueTele;
            this.titleAge = titleAge;
            this.valueAge = valueAge;
        }

        public void Hide()
        {
            panel.Visible = false;
            avatar.Visible = false;
            titleUserName.Visible = false;
            titleEmail.Visible = false;
            valueEmail.Visible = false;
            titleTele.Visible = false;
            valueTele.Visible = false;
            titleAge.Visible = false;
            valueAge.Visible = false;
        }
        public void Show()
        {
            panel.Visible = true;
            avatar.Visible = true;
            titleUserName.Visible = true;
            titleEmail.Visible = true;
            valueEmail.Visible = true;
            titleTele.Visible = true;
            valueTele.Visible = true;
            titleAge.Visible = true;
            valueAge.Visible = true;
        }
        public void setData(usuarios users)
        {
            APICalls.GETImage(users.foto_url,this.avatar);
            titleUserName.Text = users.nombre + " " + users.apellidos;
            valueEmail.Text = users.correo;
            valueTele.Text = users.telefono;
            int edad = DateTime.Now.Year - users.fecha_nacimiento.Year;
            valueAge.Text = edad.ToString();
        }

    }
}
