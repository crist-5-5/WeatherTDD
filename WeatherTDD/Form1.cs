using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherTDD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            while (true)
            {
                try
                {
                    panel2.Controls.RemoveAt(0);
                }
                catch (Exception exce)
                {
                    break;
                }
            }

            string searchString = textBox1.Text;
            
            int count = 0;
            foreach (var item in GetWeather.Search(searchString))
            {
                count++;
                Button newButton = new Button();
                newButton.Location = new Point(13,25*count+13);
                newButton.Text = item.title;
                newButton.Click += (s, EventArgs) => { dynamicButton_click(s, EventArgs, item.woeid); };

                panel2.Controls.Add(newButton);
                if (count == 5)
                    break;
            }

        }
        private void dynamicButton_click(object sender, EventArgs e, int par=0)
        {
            var res = GetWeather.GetLocation(par);
            label1.Text = res.parent.title;
            label2.Text = res.title;
            label3.Text = res.location_type;
            int count = 0;
            while (true)
            {
                try
                { 
                    panel1.Controls.RemoveAt(0);
                }
                catch (Exception exce)
                {
                    break;
                }
            }
            foreach (var location in res.consolidated_weather)
            {
                var lb = new Label();
                lb.Width = 300;
                lb.Text = $"{location.applicable_date}:Minima:{location.min_temp},Maxima: {location.max_temp}";
                lb.Location = new Point(13, 25 * count + 13);
                count++;
                panel1.Controls.Add(lb);
            }

        }
    }
}
