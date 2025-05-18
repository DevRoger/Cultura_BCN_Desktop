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
        private usuarios users;
        private List<PanelUsers> listPanelUsers = new List<PanelUsers>();
        private List<PanelEvents> listPanelEvents = new List<PanelEvents>();
        private int pageEvents = 1;
        private Thread threadEvents;
        private bool stopThread = false;    
        public Dashboard(usuarios user)
        {
            InitializeComponent();
            this.users = user;
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
                        if (timeClock.IsHandleCreated && !timeClock.IsDisposed)
                        {
                            timeClock.BeginInvoke(new Action(() =>
                            {
                                timeClock.Text = timeHoure;
                            }));
                        }
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
            UsersDashboard usersDashboard = new UsersDashboard(this.users);
            usersDashboard.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            ReservationsDashboard reservationsDashboard = new ReservationsDashboard(this.users);
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
            EventsDashboard f = new EventsDashboard(this.users);
            f.Show();
            this.Hide();
        }

        private void buttonSalas_Click(object sender, EventArgs e)
        {
            SalasDashboards salasDashboards = new SalasDashboards(this.users);
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
            DateTime dateTime = monthCalendar.SelectionStart;
            using (var context = new CulturaBCNEntities())
            {
                listEvents = context.eventos.OrderByDescending(u => u.id_evento).Where(ev =>
                                                                                            ev.fecha.Day == dateTime.Day &&
                                                                                            ev.fecha.Month == dateTime.Month &&
                                                                                            ev.fecha.Year == dateTime.Year).ToList();
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
            labelNotEvents.Visible = false;
            for (int i = 0; i < listPanelUsers.Count; i++)
            {
                listPanelUsers[i].setData(listUsers[i]);
            }
            if(listEvents.Count == 0)
            {
                listPanelEvents[0].Hide();
                listPanelEvents[1].Hide();
                listPanelEvents[2].Hide();
                labelNotEvents.Visible = true;
            }
            else
            {
                listPanelEvents[0].Show();
                listPanelEvents[1].Show();
                listPanelEvents[2].Show();
                labelNotEvents.Visible = false;
            }
            if (listEvents.Count <= 3)
            {
                int index= 0;
                for (int i = 0; i< listEvents.Count; i++)
                {
                    listPanelEvents[i].Update(listEvents[i]);
                    index = i;
                }
                while (index < 3)
                {
                    listPanelEvents[index].Hide();
                    index++;
                }
            }
            else
            {
                threadEvents = new Thread(() =>
                {
                    List<eventos> list = new List<eventos>();
                    while (!this.stopThread)
                    {
                        listPanelEvents[0].Show();
                        listPanelEvents[1].Show();
                        listPanelEvents[2].Show();
                        DateTime dateSelected = monthCalendar.SelectionStart;
                        using (var context = new CulturaBCNEntities())
                        {
                            list = context.eventos.OrderByDescending(u => u.id_evento).Where(ev =>
                                                                                                    ev.fecha.Day == dateTime.Day &&
                                                                                                    ev.fecha.Month == dateTime.Month &&
                                                                                                    ev.fecha.Year == dateTime.Year).ToList();
                        }

                        if (pageEvents * 3 <= list.Count)
                        {
                            int index = 0;
                            for (int i = pageEvents * 3 - 3; i < pageEvents * 3; i++)
                            {
                                if (i >= list.Count)
                                {
                                    listPanelEvents[index].Hide();
                                }
                                else
                                {
                                    listPanelEvents[index].Update(list[i]);
                                }
                                index++;
                            }
                        }
                        else
                        {
                            pageEvents = 1;
                            for (int i = pageEvents * 3 - 3; i < pageEvents * 3; i++)
                            {
                                if (i >= listEvents.Count)
                                {
                                    listPanelEvents[i].Hide();
                                }
                                else
                                {
                                    listPanelEvents[i].Update(listEvents[i]);
                                }
                            }
                        }
                        Thread.Sleep(6000);
                    }
                    
                });
                this.threadEvents.Start();
            }
        }

        private void Dashboard_Leave(object sender, EventArgs e)
        {
            
        }

        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {

            if (threadEvents != null && threadEvents.IsAlive)
            {
                stopThread = true;
                threadEvents.Abort();
            }
            this.pageEvents = 1;
            this.stopThread = false;
            DateTime dateTime = monthCalendar.SelectionStart;
            List<eventos> listEvents = new List<eventos>();

            using (var context = new CulturaBCNEntities())
            {
                listEvents = context.eventos.OrderByDescending(u => u.id_evento).Where(ev =>
                                                                                                ev.fecha.Day == dateTime.Day &&
                                                                                                ev.fecha.Month == dateTime.Month &&
                                                                                                ev.fecha.Year == dateTime.Year).ToList();
            }

            if (listEvents.Count == 0)
            {
                listPanelEvents[0].Hide();
                listPanelEvents[1].Hide();
                listPanelEvents[2].Hide();
                labelNotEvents.Visible = true;
            }
            else
            {
                listPanelEvents[0].Show();
                listPanelEvents[1].Show();
                listPanelEvents[2].Show();
                labelNotEvents.Visible = false;
            }
            if (listEvents.Count <= 3)
            {
                int index = 0;
                for (int i = 0; i < listEvents.Count; i++)
                {
                    listPanelEvents[i].Update(listEvents[i]);
                    index = i;
                }
                while (index < 3)
                {
                    listPanelEvents[index].Hide();
                    index++;
                }
            }
            else
            {
                threadEvents = new Thread(() =>
                {
                    List<eventos> list = new List<eventos>();
                    while (!this.stopThread)
                    {
                        listPanelEvents[0].Show();
                        listPanelEvents[1].Show();
                        listPanelEvents[2].Show();
                        DateTime dateSelected = monthCalendar.SelectionStart;
                        using (var context = new CulturaBCNEntities())
                        {
                            list = context.eventos.OrderByDescending(u => u.id_evento).Where(ev =>
                                                                                                    ev.fecha.Day == dateTime.Day &&
                                                                                                    ev.fecha.Month == dateTime.Month &&
                                                                                                    ev.fecha.Year == dateTime.Year).ToList();
                        }

                        if (pageEvents <= (int)Math.Ceiling((double)list.Count / 3))
                        {
                            int index = 0;
                            for (int i = pageEvents * 3 - 3; i < pageEvents * 3; i++)
                            {
                                if (i >= list.Count)
                                {
                                    listPanelEvents[index].Hide();
                                }
                                else
                                {
                                    listPanelEvents[index].Update(list[i]);
                                }
                                index++;
                            }
                        }
                        else
                        {
                            pageEvents = 1;
                            for (int i = pageEvents * 3 - 3; i < pageEvents * 3; i++)
                            {
                                if (i >= listEvents.Count)
                                {
                                    listPanelEvents[i].Hide();
                                }
                                else
                                {
                                    listPanelEvents[i].Update(listEvents[i]);
                                }
                            }
                        }
                        pageEvents++;
                        Thread.Sleep(6000);
                    }

                });
                this.threadEvents.Start();

            }
        }
    }
}
