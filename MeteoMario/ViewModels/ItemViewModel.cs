using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MeteoMario {
	public class ItemViewModel : INotifyPropertyChanged {
		private string _lineOne;
		/// <summary>
		/// Sample ViewModel property; this property is used in the view to display its value using a Binding.
		/// </summary>
		/// <returns></returns>
		public string LineOne {
			get {
				return _lineOne;
			}
			set {
				if (value != _lineOne) {
					_lineOne = value;
					NotifyPropertyChanged("LineOne");
				}
			}
		}

		private string _fetchUrl;
		/// <summary>
		/// Sample ViewModel property; this property is used in the view to display its value using a Binding.
		/// </summary>
		/// <returns></returns>
		public string FetchUrl {
			get {
				return _fetchUrl;
			}
			set {
				if (value != _fetchUrl) {
					_fetchUrl = value;
					NotifyPropertyChanged("FetchUrl");
				}
			}
		}

		private BitmapImage _img;
		public BitmapImage Img {
			get {
				return _img;
			}
			set {
				_img = value;
				NotifyPropertyChanged("Img");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged(String propertyName) {
			PropertyChangedEventHandler handler = PropertyChanged;
			if (null != handler) {
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}