using Cultura_BCN.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Cultura_BCN
{
    public partial class Dashboard : Form
    {
        private string timeHoure;
        private List<PanelUsers> listPanelUsers = new List<PanelUsers>();
        private List<PanelEvents> listPanelEvents = new List<PanelEvents>();
        private int pageUser = 0;
        private int pageEvents = 0;
        public Dashboard()
        {
            InitializeComponent();
            string date = DateTime.Now.ToString("d MMMM yyyy", new CultureInfo("ca-ES"));
            labelDate.Text = date;
            timeHoure = DateTime.Now.ToString("HH:mm");
            timeClock.Text = timeHoure;
            Thread threadTime = new Thread(() =>
            {
                while (true)
                {
                    string newDate = DateTime.Now.ToString("HH:mm");
                    if (newDate != timeHoure) { 
                        timeHoure = newDate;
                        timeClock.Invoke(new Action(() =>
                        {
                            timeClock.Text = timeHoure;
                        }));
                    }
                    Thread.Sleep(1000); 
                }
            });

            threadTime.IsBackground = true;

            using (var context = new CulturaBCNEntities()) {
                labelTotalClients.Text = context.usuarios.Count().ToString(); }

            List<DayResults> listData = generateListData();
            setDataInChart(listData);
            var area = chartReservation.ChartAreas[0];
            area.BackColor = Color.Transparent;
            area.AxisX.MajorGrid.LineWidth = 0;
            area.AxisY.MajorGrid.LineWidth = 0;
            
            
            threadTime.Start();


        }
        public void Form1_Shown(object sender, EventArgs e)
        {
            
        }
       
        private void setDataInChart(List<DayResults> list)
        {
            chartReservation.Series.Clear();
            Series serie = new Series("Ventas");
            serie.ChartType = SeriesChartType.SplineArea;
            serie.Color = Color.FromArgb(100, Color.Red); // Rojo transparente debajo
            serie.BorderColor = Color.Red;                // Línea roja
            serie.BorderWidth = 2;
            serie.IsValueShownAsLabel = false;

            chartReservation.Series.Clear();
            chartReservation.ChartAreas.Clear();
            chartReservation.Titles.Clear();
            chartReservation.Legends.Clear();

            // Crear área del gráfico
            ChartArea area = new ChartArea();
            area.BackColor = Color.Transparent;
            area.BorderWidth = 0;
            area.AxisX.MajorGrid.LineWidth = 0;
            area.AxisY.MajorGrid.LineWidth = 0;
            area.AxisX.LineWidth = 0;
            area.AxisY.LineWidth = 0;
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 9);
            area.AxisY.LabelStyle.Enabled = false; // Opcional: Ocultar etiquetas Y
            area.AxisX.MajorTickMark.Enabled = false;
            area.AxisY.MajorTickMark.Enabled = false;

            chartReservation.ChartAreas.Add(area);

            foreach (DayResults day in list)
            {
                serie.Points.AddXY(day.date, day.sales);
            }
            chartReservation.Series.Add(serie);

            

        }
        private List<DayResults> generateListData()
        {
            List<DayResults> list = new List<DayResults>();
            using (var context = new CulturaBCNEntities())
            {
                for (int i = 30; i >= 0; i--)
                {
                    DateTime targetDate = DateTime.Now.Date.AddDays(-i); // calcula la fecha fuera del LINQ

                    var listSales = context.reservas_entradas
                        .Where(r => DbFunctions.TruncateTime(r.fecha_reserva) == targetDate)
                        .ToList();

                    list.Add(new DayResults(targetDate.ToString("dd/MM"), listSales.Count));
                }

            }

            return list;
        }
                

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            UsersDashboard usersDashboard = new UsersDashboard();
            usersDashboard.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            ReservationsDashboard reservationsDashboard = new ReservationsDashboard();
            reservationsDashboard.Show();
            this.Hide();
        }

        private void buttonOff_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonEvents_Click(object sender, EventArgs e)
        {
            EventsDashboard f = new EventsDashboard();
            f.Show();
            this.Hide();
        }

        private void buttonSalas_Click(object sender, EventArgs e)
        {
            SalasDashboards salasDashboards = new SalasDashboards();
            salasDashboards.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Dashboard_Shown(object sender, EventArgs e)
        {
            List<usuarios> listUsers = new List<usuarios>();
            List<eventos> listEvents = new List<eventos>();
            DateTime dateTime = DateTime.Now;
            using (var context = new CulturaBCNEntities())
            {
                listEvents = context.eventos.OrderByDescending(u => u.id_evento).Take(3).ToList();
                listUsers = context.usuarios.OrderByDescending(u => u.id_usuario).Take(3).ToList();
            }

            PanelEvents p1 = new PanelEvents(pictureBoxEvent1, pictureBoxEventFoto1, labelNomEvent1, labelTitleHora1, labelHora1, labelTitleData1, labelData1, labelTitleEntrades1, labelEntrades1);
            PanelEvents p2 = new PanelEvents(pictureBoxEvent2, pictureBoxEventFoto2, labelNomEvent2, labelTitleHora2, labelHora2, labelTitleData2, labelData2, labelTitleEntrades2, labelEntrades2);
            PanelEvents p3 = new PanelEvents(pictureBoxEvent3, pictureBoxEventFoto3, labelNomEvent3, labelTitleHora3, labelHora3, labelTitleData3, labelData3, labelTitleEntrades3, labelEntrades3);

            listPanelEvents.Add(p1);
            listPanelEvents.Add(p2);
            listPanelEvents.Add(p3);

            PanelUsers u1 = new PanelUsers(pictureBoxUsuaris1, pictureBoxUsuarisFoto1, labelNomUsuari1, labelTilteCorreu1, labelCorreu1, labelTitleTelefon1, labelTelefon1, labelTitleEdad1, labelEdad1);
            PanelUsers u2 = new PanelUsers( pictureBoxUsuaris2, pictureBoxUsuarisFoto2, labelNomUsuari2,
                                                labelTilteCorreu2, labelCorreu2,
                                                labelTitleTelefon2, labelTelefon2,
                                                labelTitleEdad2, labelEdad2);
            PanelUsers u3 = new PanelUsers(pictureBoxUsuaris3, pictureBoxUsuarisFoto3, labelNomUsuari3,
                                                            labelTilteCorreu3, labelCorreu3,
                                                            labelTitleTelefon3, labelTelefon3,
                                                            labelTitleEdad3, labelEdad3);
            listPanelUsers.Add(u1);
            listPanelUsers.Add(u2);

            listPanelUsers.Add(u3);

            for (int i = 0; i < listPanelUsers.Count; i++)
            {
                listPanelUsers[i].setData(listUsers[i]);
            }
            for (int i = 0; i < listPanelEvents.Count; i++)
            {
                listPanelEvents[i].Update(listEvents[i]);
            }
        }

        private void Dashboard_Leave(object sender, EventArgs e)
        {
            
        }
    }
}
