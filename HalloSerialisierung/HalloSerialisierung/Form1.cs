using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace HalloSerialisierung
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
    }
}
