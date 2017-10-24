using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Trackmatic.Rest.Core;
using Trackmatic.Rest.Planning.Requests;

namespace ZoneCheck
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Clear();
            try
            {
                double latitude;
                double longitude;
                double.TryParse(Latitude.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out latitude);
                double.TryParse(Longitude.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out longitude);
                if (latitude == 0 || longitude == 0)
                {
                    throw new Exception("Please check Latitude or Longitude values");
                }
                var api = Login("00000000000404", "tb!AEs8B", "404");
                var GetZone = api.ExecuteRequest(new LoadZonesByLatLon(api.Context, latitude, longitude)).Data;
                foreach (var zone in GetZone)
                {
                    listView1.ForeColor = Color.Black;
                    listView1.View = View.List;
                    listView1.Items.Add(zone.Name);
                    listView1.Items.Add("Done");
                }
            }
            catch (Exception ex)
            {
                listView1.ForeColor = Color.Red;
                listView1.View = View.List;
                listView1.Items.Add(ex.Message);
            }

        }
        private static Api Login(string user, string password, string clientId)
        {
            var api = new Api("https://rest.trackmatic.co.za/api/v1", clientId, user);
            api.Authenticate(password);
            return api;
        }
    }
}
