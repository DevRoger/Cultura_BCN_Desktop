using Cultura_BCN.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cultura_BCN
{
    public class APICalls
    {
        private static string APIurl = "http://10.0.3.141/CulturaBCN/api/";


        public static async Task POSTuser(usuarios user, Image imagen)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIurl);

                using (var form = new MultipartFormDataContent())
                {
                    // Añadir campos normales
                    form.Add(new StringContent(user.nombre), "nombre");
                    form.Add(new StringContent(user.apellidos), "apellidos");
                    form.Add(new StringContent(user.correo), "correo");
                    form.Add(new StringContent(user.contrasena_hash), "contrasena_hash");
                    form.Add(new StringContent(user.fecha_nacimiento.ToString("yyyy-MM-dd")), "fecha_nacimiento");
                    form.Add(new StringContent(user.telefono), "telefono");
                    form.Add(new StringContent(user.id_rol.ToString()), "id_rol");

                    // Convertir imagen a bytes desde PictureBox.Image
                    using (var ms = new MemoryStream())
                    {
                        imagen.Save(ms, ImageFormat.Jpeg); // Usa PNG si lo prefieres
                        ms.Position = 0;

                        var fileContent = new ByteArrayContent(ms.ToArray());
                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                        form.Add(fileContent, "photo", "avatar.jpg"); // nombre simulado del archivo
                    }

                    // Enviar POST
                    var response = await client.PostAsync("usuarios", form);
                    if (!response.IsSuccessStatusCode)
                        throw new Exception($"Error al crear usuario: {response.StatusCode}");
                }
            }
        }
        public static async Task POSTevent(eventos even, Image imagen)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIurl);

                using (var form = new MultipartFormDataContent())
                {
                    // Agregar campos del evento (ajusta según los nombres reales)
                    form.Add(new StringContent(even.nombre), "nombre");
                    form.Add(new StringContent(even.descripcion), "descripcion");
                    form.Add(new StringContent(even.fecha.ToString("yyyy-MM-dd")), "fecha");
                    form.Add(new StringContent(even.hora_fin.ToString()), "hora_fin");
                    form.Add(new StringContent(even.hora_inicio.ToString()), "hora_inicio");
                    form.Add(new StringContent(even.id_sala.ToString()), "id_sala");
                    form.Add(new StringContent(even.lugar), "lugar");
                    form.Add(new StringContent(even.enumerado.ToString()), "enumerado");
                    form.Add(new StringContent(even.edad_minima.ToString()), "edad_minima");
                    form.Add(new StringContent(even.precio.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)), "precio");

                    // Convertir imagen del PictureBox a bytes
                    using (var ms = new MemoryStream())
                    {
                        imagen.Save(ms, ImageFormat.Jpeg); // O PNG si lo prefieres
                        ms.Position = 0;

                        var fileContent = new ByteArrayContent(ms.ToArray());
                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                        form.Add(fileContent, "photo", "evento.jpg"); // nombre simulado del archivo
                    }

                    // Enviar POST
                    var response = await client.PostAsync("eventos", form);
                    if (!response.IsSuccessStatusCode)
                        throw new Exception($"Error al crear evento: {response.StatusCode}");
                    
                }
            }
        }
        public static async Task PUTuser(usuarios user, Image imagen)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIurl);

                using (var form = new MultipartFormDataContent())
                {
                    // Añadir campos normales
                    form.Add(new StringContent(user.id_usuario.ToString()), "id_usuario");
                    form.Add(new StringContent(user.nombre), "nombre");
                    form.Add(new StringContent(user.apellidos), "apellidos");
                    form.Add(new StringContent(user.correo), "correo");
                    form.Add(new StringContent(user.foto_url), "foto_url");
                    form.Add(new StringContent(user.contrasena_hash), "contrasena_hash");
                    form.Add(new StringContent(user.fecha_nacimiento.ToString("yyyy-MM-dd")), "fecha_nacimiento");
                    form.Add(new StringContent(user.telefono), "telefono");
                    form.Add(new StringContent(user.id_rol.ToString()), "id_rol");

                    // Convertir imagen a bytes desde PictureBox.Image
                    using (var ms = new MemoryStream())
                    {
                        using (Bitmap bmp = new Bitmap(imagen))
                        {
                            bmp.Save(ms, ImageFormat.Png);  // O ImageFormat.Jpeg si prefieres
                            ms.Position = 0;
                            var fileContent = new ByteArrayContent(ms.ToArray());
                            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
                            form.Add(fileContent, "photo", "evento.png");
                        }
                    }

                    // Enviar PUT
                    var response = await client.PutAsync("usuarios", form);
                    if (!response.IsSuccessStatusCode)
                        throw new Exception($"Error al crear usuario: {response.StatusCode}");
                }
            }
        }
        public static async Task PUTevent(eventos even, Image imagen)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIurl);

                using (var form = new MultipartFormDataContent())
                {
                    // Agregar campos del evento (ajusta según los nombres reales)
                    form.Add(new StringContent(even.id_evento.ToString()), "id_evento");
                    form.Add(new StringContent(even.foto_url), "foto_url");
                    form.Add(new StringContent(even.nombre), "nombre");
                    form.Add(new StringContent(even.descripcion), "descripcion");
                    form.Add(new StringContent(even.fecha.ToString("yyyy-MM-dd")), "fecha");
                    form.Add(new StringContent(even.hora_fin.ToString()), "hora_fin");
                    form.Add(new StringContent(even.hora_inicio.ToString()), "hora_inicio");
                    form.Add(new StringContent(even.id_sala.ToString()), "id_sala");
                    form.Add(new StringContent(even.lugar), "lugar");
                    form.Add(new StringContent(even.enumerado.ToString()), "enumerado");
                    form.Add(new StringContent(even.edad_minima.ToString()), "edad_minima");
                    form.Add(new StringContent(even.precio.ToString("", System.Globalization.CultureInfo.InvariantCulture)), "precio");

                    // Convertir imagen del PictureBox a bytes
                    using (var ms = new MemoryStream())
                    {
                        using (Bitmap bmp = new Bitmap(imagen))
                        {
                            bmp.Save(ms, ImageFormat.Png);  // O ImageFormat.Jpeg si prefieres
                            ms.Position = 0;
                            var fileContent = new ByteArrayContent(ms.ToArray());
                            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
                            form.Add(fileContent, "photo", "evento.png");
                        }
                    }

                    // Enviar PUY
                    var response = await client.PutAsync("eventos", form);
                    if (!response.IsSuccessStatusCode)
                        throw new Exception($"Error al crear evento: {response.StatusCode}");
                }
            }
        }
        public static async Task GETImage(string path, PictureBox box)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(APIurl);

                // Enviar JSON con la ruta
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(new { foto_url = path });
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("usuarios/imagen", content); // cambia 'usuarios' por 'eventos' si es otra entidad

                if (response.IsSuccessStatusCode)
                {
                    var bytes = await response.Content.ReadAsByteArrayAsync();
                    using (var ms = new MemoryStream(bytes))
                    {
                        Image img = Image.FromStream(ms);
                        box.Image = img;
                        box.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                else
                {
                    MessageBox.Show($"Error al obtener imagen: {response.StatusCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
