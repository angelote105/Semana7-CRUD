namespace Semana7
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Vistas.vEstudiante());
        }
    }
}
