using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cultura_BCN.Model;

namespace Cultura_BCN
{
    public class PanelEvents
    {
        public PictureBox panel {  get; set; }
        public PictureBox avatar { get; set; }
        public Label titleEventName { get; set; }
        public Label titleTime { get; set; }
        public Label valueTime { get; set; }
        public Label titleDate { get; set; }
        public Label valueDate { get; set; }
        public Label titleEntrades { get; set; }
        public Label valueEntrades { get; set; }

        public PanelEvents(PictureBox panel, PictureBox avatar, Label titleEventName, Label titleTime, Label valueTime, Label titleDate, Label valueDate, Label titleEntrades, Label valueEntrades) { 
            this.panel = panel;
            this.avatar = avatar;
            this.titleEventName = titleEventName;
            this.titleTime = titleTime;
            this.valueTime = valueTime;
            this.titleDate = titleDate;
            this.valueDate = valueDate;
            this.titleEntrades = titleEntrades;
            this.valueEntrades = valueEntrades;
        }
        public void Hide()
        {
            panel.Visible = false;
            avatar.Visible = false;
            titleEventName.Visible = false;
            titleEntrades.Visible = false;
            valueEntrades.Visible = false;
            titleDate.Visible = false;
            valueDate.Visible = false;
            titleTime.Visible = false;
            valueTime.Visible = false;
        }
        public void Show() {
            panel.Visible = true;
            avatar.Visible = true;
            titleEventName.Visible = true;
            titleEntrades.Visible = true;
            valueEntrades.Visible = true;
            titleDate.Visible = true;
            valueDate.Visible = true;
            titleTime.Visible = true;
            valueTime.Visible = true;
        }
        public void Update(eventos evento)
        {
            APICalls.GETImage(evento.foto_url, this.avatar);
            titleEventName.Text = evento.nombre;
            valueEntrades.Text = generateTotalEntradas(evento);
            valueDate.Text = evento.fecha.ToString();
            valueTime.Text = evento.hora_inicio.ToString() + " - " + evento.hora_fin.ToString();
        }
        public string generateTotalEntradas(eventos evento)
        {
            string txt = "";
            using (var context = new CulturaBCNEntities())
            {
                var even = context.eventos.Find(evento.id_evento);

                var totalAsientos = context.asientos.Where(a => a.id_evento == even.id_evento).ToList();

                var totalSold = context.asientos.Where(a => a.id_evento == even.id_evento || a.disponible == false).ToList();

                txt = totalSold.Count() + "/" + totalAsientos.Count();
            }
            return txt;
        }
    }
}
