using Agent.Csv;
using Agent.Files;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RichCSVDataManipulator
{
    public partial class Form1 : Form
    {
        public string FileName { get; set; }
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            if (checkedListBox2.SelectedIndex > -1)
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string setting = Properties.Settings.Default.Vehicles;
            string[] comboRows = setting.Split('|');
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = @"C:\Users\YaseenH\Desktop";
                openFileDialog1.Filter = "csv files (*.csv)|*.csv";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    // Insert code to read the stream here.
                    var reader = new CsvReader();
                    FileName = openFileDialog1.FileName;
                    var content = File.ReadAllText(FileName);
                    var lines = reader.Read(content);
                    var uploads = lines.Where(p => p.Data != null).Select(p => new ActionLine(p)).ToList();
                    var groupNums = uploads.Select(x => x.GetGroupNo()).ToList().Distinct();
                    foreach (var groupNum in groupNums)
                    {
                        checkedListBox1.Items.Add(groupNum.ToString());
                    }

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> checkedItems = new List<string>();

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    string str = (string)checkedListBox1.Items[i];
                    checkedItems.Add(str);
                }
            }
            var comboSelected = comboBox1.SelectedItem.ToString();


            foreach (var checkedItem in checkedItems)
            {
                checkedListBox2.Items.Add(checkedItem.ToString()+"-"+comboSelected);
                checkedListBox1.Items.Remove(checkedItem.ToString());
            }
            if (checkedListBox1.Items.Count == 0)
            {
                button4.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> checkedItems = new List<string>();

            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i))
                {
                    string str = (string)checkedListBox2.Items[i];
                    checkedItems.Add(str);
                }
            }

            foreach (var checkedItem in checkedItems)
            {
                var groupNum = checkedItem.Split('-')[0];
                checkedListBox1.Items.Add(groupNum);
                checkedListBox2.Items.Remove(checkedItem.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int count = checkedListBox2.SelectedItems.Count;
            List<string> allItems = new List<string>();


            for (int i = 0; i < checkedListBox2.SelectedItems.Count; i++)
            {
                allItems.Add(checkedListBox2.SelectedItems[i].ToString());
            }
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (!checkedListBox2.SelectedItems.Contains(checkedListBox2.Items[i]))
                {
                    allItems.Add(checkedListBox2.Items[i].ToString());
                }
            }

            var reader = new CsvReader();
            var content = File.ReadAllText(FileName);
            var lines = reader.Read(content);
            var actionLine = lines.Where(p => p.Data != null).Select(p => new ActionLine(p)).ToList();


            var groupNums = actionLine.Select(x => x.GetGroupNo().ToString()).ToList();

            var newFileName = FileName.Split('.')[0]+"Updated.csv";
            var header = "DeliveryNo,DeliveryItem,DeliveryDate,Route,GroupNo,MaterialNo,MaterialDescription,Qty,UoM,Weight,Volume,CustomerNo,CustomerName,CustomerReference,StreetNumber,StreetName,Suburb,City,Province,PostalCode,TelephoneNo,FaxNo,StoreName,CentrumName,TransportationZone,Truck Reg" + Environment.NewLine;
            File.AppendAllText(newFileName, header);
            foreach (var item in allItems)
            {
                foreach (var line in actionLine)
                 {

                    var groupNum = item.Split('-')[0];
                    var registration = item.Split('-')[1];
                    var lineGroupNum = line.GetGroupNo().ToString();
                    if (groupNum == lineGroupNum)
                    {
                        var date = $"{line.GetDeliveryDate().Year}{line.GetDeliveryDate().Month}{line.GetDeliveryDate().Day}";
                        var newLine = $"{line.GetDeliveryNo()},{line.GetDeliveryItem()},{date},{line.GetRoute()},{line.GetGroupNo()},{line.GetMaterialNo()},{line.GetMaterialDescription()},{line.GetQty()},{line.GetUoM()},{line.GetWeight()},{line.GetVolume()},{line.GetCustomerNo()},{line.GetCustomerName()},{line.GetCustomerReference()},{line.GetStreetNumber()},{line.GetStreetName()},{line.GetSuburb()},{line.GetCity()},{line.GetProvince()},{line.GetPostalCode()},{line.GetTelephoneNo()},{line.GetFaxNo()},{line.GetStoreName()},{line.GetCentrumName()},{line.GetTransportationZone()},{registration}" + Environment.NewLine;
                        File.AppendAllText(newFileName, newLine);
                    }
                }
            }
            checkedListBox2.Items.Clear();
            MessageBox.Show("New file has been created");
            button4.Enabled = false;
        }

        private void addVehicleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please inform Trackmatic of the addition of your vehicle!");
            string newRegistration = Microsoft.VisualBasic.Interaction.InputBox("Please enter the registration below: ", "Add Vehicle Registration", "").ToUpper();
            //comboBox1.Items.Add(newRegistration);
            Properties.Settings.Default.Vehicles = newRegistration;
            Properties.Settings.Default.Save();
        }
    }
}
