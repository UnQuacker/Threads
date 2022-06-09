using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.IO;
using System.Text;

namespace Threads
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        async private void button1_Click(object sender, EventArgs e)
        {
            //writeToFile1();
            //writeToFile2();
            //Thread thread1 = new Thread(writeToFileThread1);
            //Thread thread2 = new Thread(writeToFileThread2);
            //thread1.Start();
            //thread2.Start();
            //writetoFileAsync1();
            //writetoFileAsync2();
        }

        private void writeToFile1()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Thread1.txt";
            using (StreamWriter sw = File.AppendText(path))
                for (int i = 0; i < 10000000; i++)
                {
                    sw.WriteLine(i.ToString());
                }
        }

        private void writeToFile2()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Thread2.txt";
            using (StreamWriter sw = File.AppendText(path))
                for (int i = 0; i < 10000000; i++)
                {
                    sw.WriteLine(i.ToString());
                }
        }

        async private void writetoFileAsync1()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Thread1.txt";
            await Task.Run(async () =>
              {
                  using (StreamWriter sw = File.AppendText(path))
                      for (int i = 0; i < 40000000; i++)
                      {
                          sw.WriteLine(i.ToString());
                          //await Task.Delay(10);
                      }
              });
          
        }

        async private void writetoFileAsync2()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Thread2.txt";
            await Task.Run(async () =>
            {
                using (StreamWriter sw = File.AppendText(path))
                    for (int i = 0; i < 40000000; i++)
                    {
                        sw.WriteLine(i.ToString());
                        //await Task.Delay(10);
                    }
            });

        }

        private void writeToFileThread1()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Thread1.txt";
            using (StreamWriter sw = File.AppendText(path))
                for (int i = 0; i <100; i++)
                {
                    sw.WriteLine(i.ToString());
                    Thread.Sleep(50);
                }
        }

        private void writeToFileThread2()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Thread2.txt";
            using (StreamWriter sw = File.AppendText(path))
                for (int i = 0; i < 100; i++)
                {
                    sw.WriteLine(i.ToString());
                    Thread.Sleep(50);
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "Quack";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label2.Text = "Quack";
        }
    }
}
