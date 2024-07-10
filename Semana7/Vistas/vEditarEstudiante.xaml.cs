using Semana7.Modelos;
using System.Net;
using System.Text;

namespace Semana7.Vistas;

public partial class vEditarEstudiante : ContentPage
{
    public Estudiante Estudiante { get; set; }

    public vEditarEstudiante( Estudiante estudiante)
	{
		InitializeComponent();
        Estudiante = estudiante;
        BindingContext = estudiante;
    }

    private async void btnEditar_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Crear un objeto WebClient
            WebClient cliente = new WebClient();

            // Crear los parámetros como un diccionario
            var parametros = new System.Collections.Specialized.NameValueCollection
        {
            { "codigo", txtCodigo.Text },
            { "nombre", txtNombre.Text },
            { "apellido", txtApellido.Text },
            { "edad", txtEdad.Text }
        };

            // Convertir los parámetros a una cadena de consulta
            string parametrosString = string.Join("&", parametros.AllKeys.Select(key => $"{key}={parametros[key]}"));

            // Convertir la cadena de consulta a un array de bytes
            byte[] parametrosBytes = Encoding.UTF8.GetBytes(parametrosString);

            // Establecer la URL
            string url = "http://PYD07ZYD6-AECIT:81/Semana6/estudiantews.php";

            // Enviar la solicitud PUT
            cliente.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            byte[] response = cliente.UploadData(new Uri(url), "PUT", parametrosBytes);

            // Opcional: Convertir la respuesta a string si necesitas verificar la respuesta del servidor
            string responseString = Encoding.UTF8.GetString(response);
            Console.WriteLine(responseString);

            // Navegar a la nueva página
            await Navigation.PushAsync(new vEstudiante());
        }
        catch (WebException webEx)
        {
            // Manejar WebException por separado para obtener más detalles
            using (var reader = new StreamReader(webEx.Response.GetResponseStream()))
            {
                string error = reader.ReadToEnd();
                Console.WriteLine("WebException: " + error);
                await DisplayAlert("Alerta", error, "Cerrar");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
            await DisplayAlert("Alerta", ex.Message, "Cerrar");
        }

    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("codigo", txtCodigo.Text) // Asegúrate de tener el ID del estudiante
            });

                var response = await client.DeleteAsync("http://PYD07ZYD6-AECIT:81/Semana6/estudiantews.php?codigo=" + txtCodigo.Text);
                //var response = await client.DeleteAsync("?codigo=" + txtCodigo.Text);

                if (response.IsSuccessStatusCode)
                {
                    DisplayAlert("alerta", "Estudiante eliminado correctamente", "cerrar");
                    await Navigation.PushAsync(new vEstudiante());
                }
                else
                {
                    DisplayAlert("alerta", "Error al eliminar el estudiante", "cerrar");
                }
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("alerta", ex.Message, "cerrar");
        }

    }

    private void btnCacnelar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Vistas.vEstudiante());
    }
}