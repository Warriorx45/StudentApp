using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls.PlatformConfiguration;
using TestApp.ViewModels;
using TestApp.Views;

namespace TestApp;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
        container.Content = new MainViewPage(new DataViewModel());
    }

    

}

