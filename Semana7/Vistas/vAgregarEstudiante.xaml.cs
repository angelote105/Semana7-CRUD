using System.Net;

namespace Semana7.Vistas;

public partial class vAgregarEstudiante : ContentPage
{
	public vAgregarEstudiante()
	{
		InitializeComponent();
	}

    private void btnIngresar_Clicked(object sender, EventArgs e)
    {
        try
        {
            WebClient cliente = new WebClient();

            var parametros = new System.Collections.Specialized.NameValueCollection();

            parametros.Add("nombre", txtNombre.Text);
            parametros.Add("apellido", txtApellido.Text);
            parametros.Add("edad", txtEdad.Text);

            cliente.UploadValues("http://PYD07ZYD6-AECIT:81/Semana6/estudiantews.php", "post", parametros);

            Navigation.PushAsync(new vEstudiante());


        }
        catch (Exception ex)
        {

            DisplayAlert("alerta", ex.Message, "cerrar");
        }
    }
}