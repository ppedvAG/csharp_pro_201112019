using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace HalloSerialisierung
{
    public delegate void RoundHouseClickEventHandler(object sender, int schaden);

    public partial class Form1 : Form
    {
        [Browsable(true)]
        public event RoundHouseClickEventHandler RoundHouseClick;

        public Form1()
        {
            InitializeComponent();

            this.SizeChanged += (o, e) => MessageBox.Show("Nö");

            RoundHouseClick += (s, e) => MessageBox.Show($"RoundhouseClick: {e}");

            //this.SizeChanged += Form1_SizeChanged;
            //this.SizeChanged += Form1_SizeChanged;
            //this.SizeChanged += Form1_SizeChanged;
            //this.SizeChanged += Form1_SizeChanged;
            //this.SizeChanged -= Form1_SizeChanged;
            //this.SizeChanged -= Form1_SizeChanged;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Nein!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var url = $"https://www.googleapis.com/books/v1/volumes?q={textBox1.Text}";

            var web = new WebClient() { Encoding = Encoding.UTF8 };
            var json = web.DownloadString(url);

            //MessageBox.Show(json);

            var result = JsonConvert.DeserializeObject<BooksResults>(json);

            dataGridView1.DataSource = result.items.Select(x => x.volumeInfo).ToList();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var filename = "books.xml";

            using (var sw = new StreamWriter(filename))
            {
                var serial = new XmlSerializer(typeof(List<Volumeinfo>));
                serial.Serialize(sw, dataGridView1.DataSource);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var filename = "books.xml";

            using (var sr = new StreamReader(filename))
            {
                var serial = new XmlSerializer(typeof(List<Volumeinfo>));
                dataGridView1.DataSource = serial.Deserialize(sr);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var daten = (List<Volumeinfo>)dataGridView1.DataSource;

            var query = from x in daten
                        where x.pageCount > 100
                        orderby x.ratingsCount, x.title descending
                        select x;
            //anonymer datentyp
            //select new { title = x.title, pc = x.pageCount };

            dataGridView1.DataSource = query.ToList();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var daten = (List<Volumeinfo>)dataGridView1.DataSource;

            dataGridView1.DataSource = daten.Where(x => x.pageCount > 100)
                                            .OrderBy(x => x.ratingsCount)
                                            .ThenByDescending(x => x.title)
                                            .ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var daten = (List<Volumeinfo>)dataGridView1.DataSource;
            var result = daten.FirstOrDefault(x => x.pageCount > 500);
            if (result != null)
                MessageBox.Show(result.title);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var daten = (List<Volumeinfo>)dataGridView1.DataSource;
            var result = daten.Average(x => x.pageCount);
            MessageBox.Show($"{result} Seiten");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var daten = (List<Volumeinfo>)dataGridView1.DataSource;
            var grps = daten.GroupBy(x => x.language).ToDictionary(x => x.Key, x => x.Sum(y => y.pageCount));
        }


        int rCount = 2;
        int lCount = 2;
        private void button9_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                rCount++;

            if (e.Button == MouseButtons.Left)
                lCount++;

            if (rCount % 2 == 0 && lCount % 2 == 0)
                RoundHouseClick(this, rCount + lCount);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var fi = new FileInfo("books.xlsx");

            using (var pack = new ExcelPackage(fi))
            {
                var ws = pack.Workbook.Worksheets.FirstOrDefault(x => x.Name == "Books");
                if (ws == null)
                    ws = pack.Workbook.Worksheets.Add("Books");

                var rsult = ws.Cells["C2:C10"].Sum(x => int.Parse(x.Value.ToString()));
                MessageBox.Show(rsult.ToString());

                //     ws.Cells[1, 1].Value = "Hallo";
                //     ws.Cells["B2"].Value = "75,83";
                //     ws.Cells["A1:A5"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                //     ws.Cells["A1:A5"].Style.Fill.BackgroundColor.SetColor(Color.YellowGreen);
                //     var daten = (List<Volumeinfo>)dataGridView1.DataSource;
                //     for (int i = 0; i < daten.Count; i++)
                //     {
                //         ws.Cells[i + 2, 1].Value = daten.ElementAt(i).title;
                //         ws.Cells[i + 2, 2].Value = string.Join(", ", daten.ElementAt(i).authors);
                //         ws.Cells[i + 2, 3].Value = daten.ElementAt(i).pageCount;
                //     }
                //     ws.Column(1).AutoFit();
                //     ws.Column(2).AutoFit();
                //     ws.Column(3).AutoFit();
                //

                pack.Save();
            }
            Process.Start("books.xlsx");
        }
    }
}
