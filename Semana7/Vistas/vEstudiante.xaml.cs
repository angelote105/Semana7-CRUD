using Newtonsoft.Json;
using Semana7.Modelos;

using System.Collections.ObjectModel;



namespace Semana7.Vistas;

public partial class vEstudiante : ContentPage
{
    private const string url = "http://10.90.9.92:81/Semana6/estudiantews.php";
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<Estudiante> estu;
    public vEstudiante()
	{
		InitializeComponent();
        obtener();
    }
    public async void obtener()
    {
        var content = await cliente.GetStringAsync(url);
        List<Estudiante> mostrarEst = JsonConvert.DeserializeObject<List<Modelos.Estudiante>>(content);
        estu = new ObservableCollection<Estudiante>(mostrarEst);

        listaEstudiantes.ItemsSource = estu;
    }

    private void btnagregar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Vistas.vAgregarEstudiante());
    }

    private async void btnmantenimiento_Clicked(object sender, EventArgs e)
    {
        var estuSeleccionado = (Estudiante)((Button)sender).BindingContext;
        await Navigation.PushAsync(new vEditarEstudiante(estuSeleccionado));
    }
}