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
            progressBar1.Value = 0;

            string source = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Thread1.txt";
            string destination = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Thread3.txt";

            CustomFileCopier customFileCopier = new CustomFileCopier(source,destination, ref progressBar1);

            customFileCopier.Copy();


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
                for (int i = 0; i < 100; i++)
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

        public delegate void ProgressChangeDelegate(double Percentage);
        public delegate void Completedelegate();


        class CustomFileCopier
        {
            public CustomFileCopier(string Source, string Dest, ref ProgressBar bar)
            {
                this.SourceFilePath = Source;
                this.DestFilePath = Dest;
                this.progressBar = bar;

                OnProgressChanged += delegate { };
                OnComplete += delegate { };
            }

            public void Copy()
            {
                byte[] buffer = new byte[1024 * 1024]; 

                if (File.Exists(DestFilePath))
                {
                    File.Delete(DestFilePath);
                }

                using (FileStream source = new FileStream(SourceFilePath, FileMode.Open, FileAccess.Read))
                {
                    long fileLength = source.Length;
                    using (FileStream dest = new FileStream(DestFilePath, FileMode.CreateNew, FileAccess.Write))
                    {
                        long totalBytes = 0;
                        int currentBlockSize = 0;

                        while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            totalBytes += currentBlockSize;
                            double percentage = (double)totalBytes * 100.0 / fileLength;

                            dest.Write(buffer, 0, currentBlockSize);

                            OnProgressChanged(percentage);
                            MethodInvoker methodInvoker = new MethodInvoker(() =>
                            {

                                if (progressBar.Value + (int)percentage > 100)
                                {
                                    progressBar.Value = 100;
                                }
                                else
                                {
                                    progressBar.Value += (int)percentage;
                                }
                            });
                            try {
                                if (progressBar.InvokeRequired)
                                {
                                    progressBar.Invoke(methodInvoker);
                                }
                                else
                                {
                                    methodInvoker.Invoke();
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            
                        }
                    }
                }

                OnComplete();
            }

            public string SourceFilePath { get; set; }
            public string DestFilePath { get; set; }

            public ProgressBar progressBar { get; set; }

            public event ProgressChangeDelegate OnProgressChanged;
            public event Completedelegate OnComplete;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            writeToFile1();
            writeToFile2();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Thread thread1 = new Thread(writeToFileThread1);
            Thread thread2 = new Thread(writeToFileThread2);
            thread1.Start();
            thread2.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            writetoFileAsync1();
            writetoFileAsync2();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;

            string source = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Thread1.txt";
            string destination = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Thread3.txt";

            CustomFileCopier customFileCopier = new CustomFileCopier(source, destination, ref progressBar1);

            Thread thread3 = new Thread(customFileCopier.Copy);
            thread3.Start();

        }
    }
}
