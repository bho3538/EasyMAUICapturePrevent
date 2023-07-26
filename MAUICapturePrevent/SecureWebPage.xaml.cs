namespace MAUICapturePrevent;

public partial class SecureWebPage : ContentPage
{
    private MAUICapturePreventLib.PreventCapture _capture = new MAUICapturePreventLib.PreventCapture();

    public SecureWebPage()
	{
		InitializeComponent();
        this.Loaded += SecureWebPage_Loaded;
        this.Unloaded += SecureWebPage_Unloaded;
    }

    private void SecureWebPage_Unloaded(object sender, EventArgs e)
    {
        _capture.SetPageAllowCapture(this);
    }

    private void SecureWebPage_Loaded(object sender, EventArgs e)
    {
        _capture.SetPagePreventCapture(this);
    }


}