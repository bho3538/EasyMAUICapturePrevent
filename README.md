# EasyMAUICapturePrevent
Easy Screen Capture &amp; Recording Protection in .NET MAUI

# ScreenShot
![maui_prevent_capture](https://github.com/bho3538/EasyMAUICapturePrevent/assets/12496720/e79fe6cc-1eb2-46c6-a9a0-3760afab8c79)

# Supported Platform
Android (tested on Android 13)\
iOS (require 13+) (tested on iOS 16.4)\
Windows (require Windows 10 20h1+) (tested on Windows 11)\

# How to use
Just copy this single file at your project.
https://github.com/bho3538/EasyMAUICapturePrevent/blob/master/MAUICapturePreventLib/PreventCapture.cs\

After copy and include it, just call like this at secure page code in your app.
```
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
        // unprotect page from screen capture
        _capture.SetPageAllowCapture(this);
    }

    private void SecureWebPage_Loaded(object sender, EventArgs e)
    {
        // protect page from screen capture
        _capture.SetPagePreventCapture(this);
    }
}

```

'MAUICapturePrevent' is sample project include secure webview page.

# How does it work
In Windows platform, Call 'SetWindowDisplayAffinity' WinAPI to prevent capture.\
In Android platform, Set 'FLAG_SECURE' at current app window.\
In iOS platform, Create dummy 'Secure TextField' at current view.\
(Credits : orakull's answer from stackoverflow 'https://stackoverflow.com/a/67054892')

# License
License : MIT

