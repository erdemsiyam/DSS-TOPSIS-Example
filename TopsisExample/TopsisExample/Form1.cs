using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TopsisExample
{
        public partial class Form1 : Form
        {
            public Form1()
            {
                InitializeComponent();
            }

            private void btnSatirEkle_Click(object sender, EventArgs e)
            {
                int toplam_en = flowLayoutPanel1.Width + 84;
                for (int i = 0; i < 6; i++)
                {
                    flowLayoutPanel1.Controls.Add(new TextBox() { Width = toplam_en / 7, Text = new Random().Next(10).ToString() });
                }
                flowLayoutPanel1.Height += 27;
            }

            private void btnSatirSil_Click(object sender, EventArgs e)
            {
                try
                {
                    List<TextBox> txts = flowLayoutPanel1.Controls.OfType<TextBox>().ToList();
                    txts.Reverse();
                    for (int i = 0; i < 6; i++)
                    {
                        flowLayoutPanel1.Controls.Remove(txts[i]);
                    }
                    flowLayoutPanel1.Height -= 27;
                }
                catch (Exception)
                {
                    MessageBox.Show("Satır Kalmadı.");
                }
            }

            private void btnHesapla_Click(object sender, EventArgs e)
            {
                try
                {

                    List<TextBox> txtBoxlar = flowLayoutPanel1.Controls.OfType<TextBox>().ToList();

                    Topsis topsis = new Topsis();
                    topsis.satirSayisi = txtBoxlar.Count / 6;
                    topsis.matrisHazirla(txtBoxlar);
                    topsis.normalizeMatrisHazirla();
                    lblNormalizeMatris.Text = topsis.normalizeMatrisGöster();
                    double[] agirliklar = new double[5] { Convert.ToDouble(txtW1.Text), Convert.ToDouble(txtW2.Text), Convert.ToDouble(txtW3.Text), Convert.ToDouble(txtW4.Text), Convert.ToDouble(txtW5.Text) };
                    topsis.agirlikliNormalizeMatrisHazirla(agirliklar);
                    lblAgirlikliMatris.Text = topsis.agirlikliNormalizeMatrisGöster();
                    topsis.idealVeNegatifİdealÇözümDeğerleriHesapla();
                    lblidealnegatifcozum.Text = topsis.idealVeNegatifİdealÇözümDeğerleriGöster();
                    topsis.uzakliklarinHesabi();
                    lblidealnegatifuzaklik.Text = topsis.uzakliklarinHesabiGöster();
                    lblCevap.Text = topsis.yakınlıkHesabı();
                }
                catch (Exception)
                {
                    MessageBox.Show("Hata, Verileri Kontrol Ediniz.\n1-Ağırlıkları Giriniz\n2-Satır Verilerinin İsimlerinin Farklı Olduğuna Emin Olunuz.");
                }


            }

            private void lblAgirlikliMatris_Click(object sender, EventArgs e)
            {

            }
    }
}
