using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TopsisExample
{
    class Topsis
    {
        public double[,] matris;
        public string[] isimler;
        public int satirSayisi;

        public void matrisHazirla(List<TextBox> txtBoxlar)
        {
            isimler = new string[satirSayisi];
            matris = new double[satirSayisi, 5];

            int satir = -1, sutun = 0, sayac = 0;
            foreach (TextBox item in txtBoxlar)
            {
                if (sayac % 6 == 0) // satır bittiyse satır sayacını bir artır , sütun sayacını da sıfırla
                {
                    satir++;
                    sutun = 0;
                    sayac++;
                    isimler[satir] = item.Text.ToString();
                    continue;
                }
                else
                {
                    matris[satir, sutun] = Convert.ToDouble(item.Text.ToString());
                }
                sutun++;
                sayac++;
            }
        }

        ///////////////////////////////// Normalize Matris 
        public double[,] normalizeMatris;
        public void normalizeMatrisHazirla()
        {
            normalizeMatris = new double[satirSayisi, 5];
            //her sütun için
            double r = 0;
            for (int j = 0; j < 5; j++) // her sütun için normalize hesabı yapılır
            {
                r = 0;
                for (int i = 0; i < satirSayisi; i++) // sıradaki sütun için satırlarda gezip R bulduk
                {
                    r += Math.Pow(matris[i, j], 2);
                }
                r = Math.Sqrt(r);
                for (int i = 0; i < satirSayisi; i++) // sıradaki sütun için satırlarda gezip Aij / R yaptık
                {
                    normalizeMatris[i, j] = matris[i, j] / r;
                }
            }
        }
        public string normalizeMatrisGöster()
        {
            string sonuc = "1.Adım : Normalize Matris\n";
            for (int i = 0; i < satirSayisi; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (normalizeMatris[i, j].ToString().Length < 6)
                    {
                        sonuc += normalizeMatris[i, j].ToString() + "   ";
                    }
                    else
                    {
                        sonuc += normalizeMatris[i, j].ToString().Substring(0, 5) + "   ";
                    }
                    
                }
                sonuc += "\n";
            }
            return sonuc;
        }

        ///////////////////////////////// Ağırlıklı Normalize Matris
        public double[,] agirlikliNormalizeMatris;
        public void agirlikliNormalizeMatrisHazirla(double[] agirliklar)
        {
            agirlikliNormalizeMatris = new double[satirSayisi, 5];
            for (int j = 0; j < satirSayisi; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    agirlikliNormalizeMatris[j, i] = normalizeMatris[j, i] * agirliklar[i];
                }
            }

        }
        public string agirlikliNormalizeMatrisGöster()
        {
            string sonuc = "2.Adım : Ağırlıklı Normalize Matris\n";
            for (int i = 0; i < satirSayisi; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (agirlikliNormalizeMatris[i, j].ToString().Length < 6)
                    {
                        sonuc += agirlikliNormalizeMatris[i, j].ToString() + "   ";
                    }
                    else
                    {
                        sonuc += agirlikliNormalizeMatris[i, j].ToString().Substring(0, 5) + "   ";
                    }
                }
                sonuc += "\n";
            }
            return sonuc;
        }

        ///////////////////////////////// ideal çözüm ve negatif ideal çözüm değerleri (min ve max değerlerin bulunması)
        public double[] idealÇözümDeğerleri;
        public double[] negatifİdealÇözümDeğerleri;

        public void idealVeNegatifİdealÇözümDeğerleriHesapla()
        {
            idealÇözümDeğerleri = new double[5];
            negatifİdealÇözümDeğerleri = new double[5];
            double max, min;
            for (int j = 0; j < 5; j++) // sırayla tüm sütunlar döner
            {
                max = min = agirlikliNormalizeMatris[0, j];
                if (j == 3 || j == 4) // Kira ve Çevredeki Market Sayıları SÜTUNLARI için ters değerleri alacağız. çünkü bunların az olmasını istiyoruz
                {
                    for (int i = 0; i < satirSayisi; i++) // her sütun için satırlar arasında , min ve max bulma
                    {
                        if (agirlikliNormalizeMatris[i, j] > max)
                            max = agirlikliNormalizeMatris[i, j];
                        if (agirlikliNormalizeMatris[i, j] < min)
                            min = agirlikliNormalizeMatris[i, j];
                        idealÇözümDeğerleri[j] = min;
                        negatifİdealÇözümDeğerleri[j] = max;
                    }
                }
                else {
                    for (int i = 0; i < satirSayisi; i++) // her sütun için satırlar arasında , min ve max bulma
                    {
                        if (agirlikliNormalizeMatris[i, j] > max)
                            max = agirlikliNormalizeMatris[i, j];
                        if (agirlikliNormalizeMatris[i, j] < min)
                            min = agirlikliNormalizeMatris[i, j];
                        idealÇözümDeğerleri[j] = max;
                        negatifİdealÇözümDeğerleri[j] = min;
                    }
                }
            }


        }
        public string idealVeNegatifİdealÇözümDeğerleriGöster()
        {
            string sonuc = "3.Adım  ideal ve Negatif İdeal Çözüm Değerleri\n";
            sonuc += "ideal :\n";
            for (int i = 0; i < 5; i++)
            {
                if (idealÇözümDeğerleri[i].ToString().Length < 6)
                {
                    sonuc += idealÇözümDeğerleri[i].ToString() + "  ";
                }
                else
                {
                    sonuc += idealÇözümDeğerleri[i].ToString().Substring(0, 5) + "  ";
                }

            }
            sonuc += "\nNegatif İdeal :\n";
            for (int i = 0; i < 5; i++)
            {
                if (negatifİdealÇözümDeğerleri[i].ToString().Length < 6)
                {
                    sonuc += negatifİdealÇözümDeğerleri[i].ToString() + "  ";
                }
                else
                {
                    sonuc += negatifİdealÇözümDeğerleri[i].ToString().Substring(0, 5) + "  ";
                }
            }
            return sonuc;
        }

        ///////////////////////////////// ideal ve negatif ideal noktalara olan uzaklık
        public double[] idealUzaklik;
        public double[] negatifİdealUzaklik;

        public void uzakliklarinHesabi()
        {
            idealUzaklik = new double[satirSayisi]; // her satır için yaptığımız için satır kadar uzunlukta olacak
            negatifİdealUzaklik = new double[satirSayisi];
            double sayi = 0;

            for (int j = 0; j < satirSayisi; j++)
            {
                //pozitif
                sayi = 0;
                for (int i = 0; i < 5; i++)
                {
                    sayi += Math.Pow( agirlikliNormalizeMatris[j, i] - idealÇözümDeğerleri[i],2);
                }
                sayi = Math.Sqrt(sayi);
                idealUzaklik[j] = sayi;
                //negatif
                sayi = 0;
                for (int i = 0; i < 5; i++)
                {
                    sayi += Math.Pow(agirlikliNormalizeMatris[j, i] - negatifİdealÇözümDeğerleri[i], 2);
                }
                sayi = Math.Sqrt(sayi);
                negatifİdealUzaklik[j] = sayi;
            }

        }
        public string uzakliklarinHesabiGöster()
        {
            string sonuc = "4.Adım  ideal Uzaklık ve Negatif İdeal Uzaklık\n";
            sonuc += "ideal Uzaklık :\n";
            for (int i = 0; i < satirSayisi; i++)
            {
                if (idealUzaklik[i].ToString().Length < 6)
                {
                    sonuc += idealUzaklik[i].ToString() + "  ";
                }
                else
                {
                    sonuc += idealUzaklik[i].ToString().Substring(0, 5) + "  ";
                }
            }
            sonuc += "\nNegatif İdeal Uzaklık :\n";
            for (int i = 0; i < satirSayisi; i++)
            {
                if (negatifİdealUzaklik[i].ToString().Length < 6)
                {
                    sonuc += negatifİdealUzaklik[i].ToString() + "  ";
                }
                else
                {
                    sonuc += negatifİdealUzaklik[i].ToString().Substring(0, 5) + "  ";
                }
            }
            return sonuc;
        }

        ///////////////////////////////// Yakınlık Hesabı
        public Dictionary<string, double> sonuc;
        public string yakınlıkHesabı()
        {
            sonuc = new Dictionary<string, double>();
            string son = "";
            for (int i = 0; i < satirSayisi; i++)
            {
                double cevap = negatifİdealUzaklik[i] / (negatifİdealUzaklik[i] + idealUzaklik[i]);
                sonuc.Add(isimler[i], cevap);
            }

            var items = from pair in sonuc
                        orderby pair.Value descending
                    select pair;
            int siralama = 1;
            foreach (KeyValuePair<string, double> pair in items)
            {
                son += siralama.ToString()+".nci  :  "+pair.Key+" ("+ pair.Value + ")\n";
                siralama++;
            }
            return son;
        }
    }
}
