using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using HtmlAgilityPack;

namespace MeteoMario {
	public class MainViewModel : INotifyPropertyChanged {
		public MainViewModel() {
			this.Items = new ObservableCollection<ItemViewModel>();
		}

		/// <summary>
		/// A collection for ItemViewModel objects.
		/// </summary>
		public ObservableCollection<ItemViewModel> Items { get; private set; }

		private string _meteoOggi = "Sto caricando le previsioni del tempo...";
		public string MeteoOggi {
			get {
				return _meteoOggi;
			}
			set {
				if (value != _meteoOggi) {
					_meteoOggi = value;
					NotifyPropertyChanged("MeteoOggi");
				}
			}
		}

		private string _meteoDomani = "Sto caricando le previsioni del tempo...";
		public string MeteoDomani {
			get {
				return _meteoDomani;
			}
			set {
				if (value != _meteoDomani) {
					_meteoDomani = value;
					NotifyPropertyChanged("MeteoDomani");
				}
			}
		}

		public bool IsDataLoaded {
			get;
			private set;
		}

		/// <summary>
		/// Creates and adds a few ItemViewModel objects into the Items collection.
		/// </summary>
		public void LoadData() {
			this.Items.Add(new ItemViewModel() { LineOne = "Monte Lussari", FetchUrl = "http://promotur.digitalwebland.com/webcam/lussari.jpg.ashx?maxheight=768&maxwidth=3000" });
			this.Items.Add(new ItemViewModel() { LineOne = "Monte Acomizza", FetchUrl = "http://www.crs.inogs.it/ipcam/acom/acom.jpg" });
			this.Items.Add(new ItemViewModel() { LineOne = "Sella Nevea", FetchUrl = "http://promotur.digitalwebland.com/webcam/sellaseggiovia.jpg.ashx?maxheight=768&maxwidth=3000" });
			this.Items.Add(new ItemViewModel() { LineOne = "Kötschach-Mauthen", FetchUrl = "http://www.koemau.at/webcam/upload/big_current1.jpg" });
			this.Items.Add(new ItemViewModel() { LineOne = "Faakersee", FetchUrl = "http://faakersee.it-wms.com/big_current3.jpg" });
			this.Items.Add(new ItemViewModel() { LineOne = "Kredarica", FetchUrl = "http://meteo.arso.gov.si/uploads/probase/www/observ/webcam/KREDA-ICA_dir/siwc_KREDA-ICA_e_latest.jpg" });
			this.Items.Add(new ItemViewModel() { LineOne = "Piancavallo", FetchUrl = "http://promotur.digitalwebland.com/webcam/labusa.jpg.ashx?maxheight=768&maxwidth=3000" });
			this.Items.Add(new ItemViewModel() { LineOne = "Tarcento", FetchUrl = "http://79.38.107.56/record/current.jpg?rand=6990265" });
			this.Items.Add(new ItemViewModel() { LineOne = "Cividale", FetchUrl = "http://webcam.cividale.net/cividale.jpg" });
			this.Items.Add(new ItemViewModel() { LineOne = "Udine", FetchUrl = "http://www.comune.udine.it/opencms/castello/castello.jpg" });
			this.Items.Add(new ItemViewModel() { LineOne = "Lubiana", FetchUrl = "http://meteo.arso.gov.si/uploads/probase/www/observ/webcam/siwc_ljubljana-bezigrad_s-zoom_48.jpg" });
			this.Items.Add(new ItemViewModel() { LineOne = "Portorose", FetchUrl = "http://www.portoroz.si/imagelib/full/webcams/webcam1.jpg" });
			this.Items.Add(new ItemViewModel() { LineOne = "Grado", FetchUrl = "http://www.osmer.fvg.it/~www/COMMON/WEBCAM/WebcamGradoMare.jpg" });
			this.Items.Add(new ItemViewModel() { LineOne = "Lignano", FetchUrl = "http://www.osmer.fvg.it/COMMON/WEBCAM/WebcamLignano.jpg" });
			this.Items.Add(new ItemViewModel() { LineOne = "Trieste", FetchUrl = "http://www.osmer.fvg.it/COMMON/WEBCAM/WebcamTrieste.jpg" });
			this.Items.Add(new ItemViewModel() { LineOne = "Sauris", FetchUrl = "http://www.osmer.fvg.it/COMMON/WEBCAM/WebcamSauris.jpg" });

			WebClient wc = new WebClient();
			wc.OpenReadCompleted += OnWebClientOpenReadCompleted;
			wc.OpenReadAsync(new Uri("http://www.osmer.fvg.it"));
			this.IsDataLoaded = true;
		}

		void OnWebClientOpenReadCompleted(object sender, OpenReadCompletedEventArgs e) {
			var bOk = false;
			try {
				if (!e.Cancelled && e.Error == null) {
					HtmlDocument doc = new HtmlDocument();
					doc.Load(e.Result);
					var tr = doc.DocumentNode.SelectNodes("//table[count(tr)=10]")[0].ChildNodes[9];
					var tdOggi = tr.ChildNodes[1];
					this.MeteoOggi = HttpUtility.HtmlDecode(tdOggi.InnerText);
					var tdDomani = tr.ChildNodes[3];
					this.MeteoDomani = HttpUtility.HtmlDecode(tdDomani.InnerText);
					bOk = true;
				}
			} catch (Exception) {
				if (System.Diagnostics.Debugger.IsAttached)
					System.Diagnostics.Debugger.Break();
			}
			if (!bOk)
				this.MeteoOggi = this.MeteoDomani = "Informazioni meteo non disponibili";
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