using Microsoft.Maui.Controls.Handlers.Items;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Platform;

namespace MAUICapturePrevent;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

    private void MainPage_Loaded(object sender, EventArgs e)
    {
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SecureWebPage());
    }
}

