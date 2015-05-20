﻿using App1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml.Serialization;
using System.ComponentModel;

namespace App1.ViewModel
{
    class MainViewModel:INotifyPropertyChanged
    {
        private Rss _rss;

        public Rss Rss {
            get {
                return this._rss;
            }

            set {
                this._rss = value;
                NotifyPropertyChanged("Rss");
            } 
        }
        public bool IsLoaded { get; set; }
        public async void LoadRss(string url) {
            HttpClient client = new HttpClient();
            using (var Stream = await client.GetStreamAsync(url)) {
                XmlSerializer Serializer = new XmlSerializer(typeof(Rss));
                this.Rss = Serializer.Deserialize(Stream) as Rss;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string property) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
