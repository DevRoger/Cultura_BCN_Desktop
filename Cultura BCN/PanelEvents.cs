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
            SafeSetVisible(panel, false);
            SafeSetVisible(avatar, false);
            SafeSetVisible(titleEventName, false);
            SafeSetVisible(titleEntrades, false);
            SafeSetVisible(valueEntrades, false);
            SafeSetVisible(titleDate, false);
            SafeSetVisible(valueDate, false);
            SafeSetVisible(titleTime, false);
            SafeSetVisible(valueTime, false);
        }
        public void Show()
        {
            SafeSetVisible(panel, true);
            SafeSetVisible(avatar, true);
            SafeSetVisible(titleEventName, true);
            SafeSetVisible(titleEntrades, true);
            SafeSetVisible(valueEntrades, true);
            SafeSetVisible(titleDate, true);
            SafeSetVisible(valueDate, true);
            SafeSetVisible(titleTime, true);
            SafeSetVisible(valueTime, true);
        }

        // Función auxiliar
        private void SafeSetVisible(Control ctrl, bool visible)
        {
            if (ctrl.InvokeRequired)
                ctrl.Invoke(new Action(() => ctrl.Visible = visible));
            else
                ctrl.Visible = visible;
        }
        public void Update(eventos evento)
        {
            APICalls.GETImage(evento.foto_url, this.avatar);
            titleEventName.Invoke(new Action(() => titleEventName.Text = evento.nombre));
            valueEntrades.Invoke(new Action(() => valueEntrades.Text = generateTotalEntradas(evento)));
            valueDate.Invoke(new Action(() => valueDate.Text = evento.fecha.ToString("dd/MM/yyyy")));
            valueTime.Invoke(new Action(() => valueTime.Text = evento.hora_inicio.ToString(@"hh\:mm") + " - " + evento.hora_fin.ToString(@"hh\:mm")));

        }
        public string generateTotalEntradas(eventos evento)
        {
            string txt = "";
            using (var context = new CulturaBCNEntities())
            {
                var even = context.eventos.Find(evento.id_evento);

                var totalAsientos = context.asientos.Where(a => a.id_evento == even.id_evento).ToList();

                var totalSold = context.asientos.Where(a => a.id_evento == even.id_evento && a.disponible == false).ToList();

                txt = totalSold.Count() + "/" + totalAsientos.Count();
            }
            return txt;
        }
    }
}
