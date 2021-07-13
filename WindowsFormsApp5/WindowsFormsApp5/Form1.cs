using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.IO.Compression;
using Ionic.Zip;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public async void selectFile()
        {
            //OpenFileDialog path = new OpenFileDialog();
            //path.Title = "Dosya yolu seç";

            //if (path.ShowDialog() == DialogResult.OK)
            //{
            //    FileInfo ff = new System.IO.FileInfo(path.FileName);
            //    string DosyaUzantisi = ff.;
            //    //label13.Text = DosyaUzantisi;
            //    label1.Text = path.FileName;
            //}

            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    System.Windows.Forms.MessageBox.Show($"Seçilen klasör: {fbd.SelectedPath}");
                label1.Text = fbd.SelectedPath;            }
        }




        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //string baslangic = @"C:\Users\merve\OneDrive\Masaüstü\EDEFTER"; // zip dosyasının başlangıç yolu.
                //string[] folderName = Directory.GetDirectories($@"C:\Users\merve\OneDrive\Masaüstü\EDEFTER");
                string[] folderName = Directory.GetDirectories(label1.Text);
                
                foreach (var name in folderName) // foreach döngüsü ile  klasörleri geziyoruz.
                {
                    string[] folname2 = Directory.GetDirectories(name);

                    //textBox1.Text = label1.Text;



                    if (name.Contains(textBox1.Text))
                    {
                        string[] folname = Directory.GetDirectories(name);
                        foreach (var name2 in folname)
                        {
                            //string abc = new DirectoryInfo(name2).Name; // dosyaların isim bilgisini aldık.
                            //  listBox1.Items.Add(abc);

                            //bool isIntString = abc.Any(char.IsDigit); // isim bilgisi sayısal değer ise içine ata.

                            string[] folname1 = Directory.GetDirectories(name2);

                            foreach (var name3 in folname1)
                            {
                                string def = new DirectoryInfo(name3).Name;
                                bool IntString = def.Any(char.IsDigit);

                                if (IntString == true) // eğer sayısal değer ise listbox a veriyi yaz.
                                {

                                    listBox1.Items.Add(def); // listbox a  yazdırma işlemi
                                    string[] dosyaadi = Directory.GetFiles(name2 + "\\" + def); // array list başlangıç yoluna bakıyor ve şarta ait klasörün bilgisini alıyor.
                                                                                                // listBox1.Items.Add(dosyaadi[i].ToString()); // şarta ait dosyaların yolu ve adı lixtbox aracına yazdırılıyor...
                                    foreach (var zip in dosyaadi) // dosyaadi değişkeninin içindeki dosyaları zip e ata .
                                    {
                                        if (zip.Contains(".xml"))
                                        {
                                            File.Delete(zip);
                                        }

                                        if (zip.Contains(".zip")) // eğer dosya uzantısı " rar " ise dosyayı çıkar. 
                                        {
                                            Process p = new Process();
                                            //p.StartInfo.UseShellExecute = false;
                                            p.StartInfo = new ProcessStartInfo("Winrar.exe", "e " + zip + " " + name3);
                                            p.Start();
                                            p.WaitForExit();
                                        }
                                    }

                                }
                            }
                            //if (isIntString == true) // eğer sayısal değer ise listbox a veriyi yaz.
                            //{

                            //    listBox1.Items.Add(abc); // listbox a  yazdırma işlemi
                            //    string[] dosyaadi = Directory.GetFiles(name + "\\" + abc); // array list başlangıç yoluna bakıyor ve şarta ait klasörün bilgisini alıyor.
                            //                                                               // listBox1.Items.Add(dosyaadi[i].ToString()); // şarta ait dosyaların yolu ve adı lixtbox aracına yazdırılıyor...
                            //    foreach (var zip in dosyaadi) // dosyaadi değişkeninin içindeki dosyaları zip e ata .
                            //    {
                            //        if (zip.Contains(".xml"))
                            //        {
                            //                    File.Delete(zip);
                            //        }

                            //            if (zip.Contains(".zip")) // eğer dosya uzantısı " rar " ise dosyayı çıkar. 
                            //            {
                            //            Process p = new Process();
                            //            p.StartInfo = new ProcessStartInfo("Winrar.exe", "e " + zip + " " + baslangic + @"\2021\" + abc);
                            //            p.Start();
                            //            p.WaitForExit();
                            //            }
                            //    }

                            //}

                        }


                    }



                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("hata mesajı !" + ex.StackTrace);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selectFile();
        }


    }
}
