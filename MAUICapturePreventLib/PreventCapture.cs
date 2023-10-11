using Microsoft.Maui.Controls;
using System.Runtime.InteropServices;

namespace MAUICapturePreventLib
{
    // Just Copy this file.
    // Check Update : 
    public class PreventCapture
    {
#if IOS
        private UIKit.UITextField _dummyField = null;
#endif

#if WINDOWS
        [DllImport("User32.dll", SetLastError = true)]
        private static extern bool SetWindowDisplayAffinity(IntPtr hWnd, uint dwAffinity);
        private const int WDA_EXCLUDEFROMCAPTURE = 0x00000011;

#endif

        public bool SetPagePreventCapture(ContentPage page)
        {
#if WINDOWS
            if(Application.Current.Windows.Count == 0)
            {
                return false;
            }

            var hwnd = ((MauiWinUIWindow)Application.Current.Windows[0].Handler.PlatformView).WindowHandle;
            if(hwnd == IntPtr.Zero)
            {
                return false;
            }

            return SetWindowDisplayAffinity(hwnd, WDA_EXCLUDEFROMCAPTURE);
#else
            if (page.Handler.PlatformView != null)
            {
#if ANDROID
			    Android.Views.View view = (Android.Views.View)page.Handler.PlatformView;
			    if(view != null)
			    {
				    Android.App.Activity activity = (Android.App.Activity)view.Context;
				    if(activity != null)
				    {
					    Android.Views.Window window = activity.Window;
					    if(window != null)
					    {
						    window.SetFlags(Android.Views.WindowManagerFlags.Secure, Android.Views.WindowManagerFlags.Secure);
                            return true;
					    }
				    }
			    }
#elif IOS
                UIKit.UIView view = (UIKit.UIView)page.Handler.PlatformView;
                if (view != null)
                {
                    var dummyField = new UIKit.UITextField();
                    dummyField.SecureTextEntry = true;
                    dummyField.Frame = new CoreGraphics.CGRect(0, 0, 0, 0);
                    view.AddSubview(dummyField);
                    view.Layer.SuperLayer.AddSublayer(dummyField.Layer);
                    int len = dummyField.Layer.Sublayers.Length;
                    if (len > 0)
                    {
                        dummyField.Layer.Sublayers[len-1].AddSublayer(view.Layer);
                        return true;
                    }

                }
#endif
            }
            return false;
#endif
        }

        public bool SetPageAllowCapture(ContentPage page)
        {
#if WINDOWS
            if (Application.Current.Windows.Count == 0)
            {
                return false;
            }

            var hwnd = ((MauiWinUIWindow)Application.Current.Windows[0].Handler.PlatformView).WindowHandle;
            if (hwnd == IntPtr.Zero)
            {
                return false;
            }

            return SetWindowDisplayAffinity(hwnd, 0);

#else
            if (page.Handler.PlatformView != null)
            {
#if ANDROID
			    Android.Views.View view = (Android.Views.View)page.Handler.PlatformView;
			    if(view != null)
			    {
				    Android.App.Activity activity = (Android.App.Activity)view.Context;
				    if(activity != null)
				    {
					    Android.Views.Window window = activity.Window;
					    if(window != null)
					    {
                            window.ClearFlags(Android.Views.WindowManagerFlags.Secure);
                            return true;
					    }
				    }
			    }
#elif IOS
                if(_dummyField != null)
                {
                    _dummyField.SecureTextEntry = false;
                    return true;
                }
#endif
            }
            return false;
#endif
        }
    }
}