using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace MeteoMario {
	public partial class WebcamPage : PhoneApplicationPage {
		public WebcamPage() {
			InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e) {
			DataContext = new { 
				LineOne = PhoneApplicationService.Current.State["webcamTitle"].ToString()
			};
			var url = PhoneApplicationService.Current.State["webcamUrl"].ToString();
			WebClient wc = new WebClient();
			wc.OpenReadCompleted += OnWebClientOpenReadCompleted;
			wc.OpenReadAsync(new Uri(url));

			base.OnNavigatedTo(e);
		}

		void OnWebClientOpenReadCompleted(object sender, OpenReadCompletedEventArgs e) {
			try {
				if (!e.Cancelled && e.Error == null) {
					BitmapImage bmp = new BitmapImage();
					bmp.SetSource(e.Result);
					this.Img.Source = bmp;
				}
			} catch (Exception) {
				if (System.Diagnostics.Debugger.IsAttached)
					System.Diagnostics.Debugger.Break();
			}
		}

		private void Image_Tap_1(object sender, System.Windows.Input.GestureEventArgs e) {
			this.NavigationService.GoBack();
		}
	}
}
