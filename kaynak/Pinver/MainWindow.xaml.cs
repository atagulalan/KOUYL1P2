﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Pinver {
    public partial class MainWindow : Window {
        #region Genel
        //     _____                 _ 
        //    / ____|               | |
        //   | |  __  ___ _ __   ___| |
        //   | | |_ |/ _ \ '_ \ / _ \ |
        //   | |__| |  __/ | | |  __/ |
        //    \_____|\___|_| |_|\___|_|

        public static int toplama_sayisi = 0;
        public static int carpma_sayisi = 0;
        public MainWindow() {
            InitializeComponent();
            this.Height = 550;
            this.Width = 550;
            this.MinHeight = 550;
            this.MinWidth = 550;
        }

        // Scrollviewer aşamalar arası geçişte kullanılıyor, bu yüzden mousewheel engellenmeli.
        private void tekerlegiEngelle(object sender, System.Windows.Input.MouseWheelEventArgs e) {
            e.Handled = true;
        }

        // Window resize olduğunda aşama değişmesini engelle ve büyüt
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e) {
            Grid[] asamalar = new Grid[] { a1, a2, a3, a4};
            // Tüm aşamaların yüksekliğini eşitler
            for (int i = 0; i < asamalar.Length; i++) {
                asamalar[i].Height = scroller.ActualHeight;
            }
        }
        #endregion

        #region Asama 1
        //                                         __ 
        //       /\                               /_ |
        //      /  \   ___  __ _ _ __ ___   __ _   | |
        //     / /\ \ / __|/ _` | '_ ` _ \ / _` |  | |
        //    / ____ \\__ \ (_| | | | | | | (_| |  | |
        //   /_/    \_\___/\__,_|_| |_| |_|\__,_|  |_|

        private void sayiGirisKontrolu(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^1-5]+"); // Regular Expression (regex) oluştur
            bool birinciKontrol = regex.IsMatch(e.Text); // Eğer sayı değil ise kontrolden kaldı
            bool ikinciKontrol = MBox.Text == e.Text || NBox.Text == e.Text; // Sayılar birbirine eşit olamaz
            e.Handled = birinciKontrol || ikinciKontrol; // Herhangi bir kontrolden kaldı ise durdur
        }

        private void otomatikOlusturButon_Click(object sender, RoutedEventArgs e) {
            Random r = new Random(); // Aynı random gelmesini engellemek için tek bir random açıp "Next" kullanıyoruz.
            MBox.Text = r.Next(1, 6).ToString(); // Oluştur ve M'e ata
            NBox.Text = r.Next(1, 6).ToString(); // Oluştur ve N'e ata
            while (MBox.Text == NBox.Text) { // Eğer değerler aynı çıktı ise N'e farklı bir rastgele sayı ata
                NBox.Text = r.Next(1, 6).ToString();
            }
        }

        private void degistirButon_Click(object sender, RoutedEventArgs e) {
            // Basit swap olayı
            var temp = NBox.Text;
            NBox.Text = MBox.Text;
            MBox.Text = temp;
        }

        private void olusturButon_Click(object sender, RoutedEventArgs e) {
            asama1Bitti();
        }

        private void asama1Bitti() {
            scroller.ScrollToVerticalOffset(1);
            asama2Basla();
        }

        private void MorNChanged(object sender, TextChangedEventArgs e) {
            // Verilerden herhangi birisi girilmemiş ise oluştur butonunu disabled yap
            if (MBox.Text != "" && NBox.Text != "") {
                olusturButon.IsEnabled = true;
            } else {
                olusturButon.IsEnabled = false;
            }
        }
        #endregion

        #region Asama 2
        //                                         ___  
        //       /\                               |__ \ 
        //      /  \   ___  __ _ _ __ ___   __ _     ) |
        //     / /\ \ / __|/ _` | '_ ` _ \ / _` |   / / 
        //    / ____ \\__ \ (_| | | | | | | (_| |  / /_ 
        //   /_/    \_\___/\__,_|_| |_| |_|\__,_| |____| 

        private void geriDonButon_Click(object sender, RoutedEventArgs e) {
            // Aşama 1'e geri dön.
            scroller.ScrollToVerticalOffset(0);
        }

        private void asama2Basla() {
            TextBox[] elemanlar = new TextBox[] { b11, b12, b13, b14, b15, b21, b22, b23, b24, b25, b31, b32, b33, b34, b35, b41, b42, b43, b44, b45, b51, b52, b53, b54, b55 };
            var m = Int32.Parse(MBox.Text);
            var n = Int32.Parse(NBox.Text);

            // M = 1 ise 90px aşağıya kaydır (ortala)
            var lm = ((5 - m) * 40) + ((5 - m) * 5);
            // N = 1 ise 90px sağa kaydır (ortala)
            var ln = ((5 - n) * 40) + ((5 - n) * 5);
            // Marginleri yerine koy
            allGrid.Margin = new Thickness(ln/2, lm/2, -ln/2, -lm/2);

            // Kullanılamayan kutuları gizle
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 5; j++) {
                    var el = elemanlar[i * 5 + j];
                    // Karşılık gelen eleman matris sınırları içerisinde ise gözükür.
                    el.Visibility = i + 1 > m || j + 1 > n ? Visibility.Collapsed : Visibility.Visible;
                    // Elemanları temizle.
                    el.Text = "";
                }
            }
        }

        private void otomatikMatrisElemanlari_Click(object sender, RoutedEventArgs e) {
            TextBox[] elemanlar = new TextBox[] { b11, b12, b13, b14, b15, b21, b22, b23, b24, b25, b31, b32, b33, b34, b35, b41, b42, b43, b44, b45, b51, b52, b53, b54, b55 };
            Random r = new Random(); // Aynı random gelmesini engellemek için tek bir random açıp "Next" kullanıyoruz.
            for (int i = 0; i < elemanlar.Length; i++) {
                elemanlar[i].Text = ((double)r.Next(10, 91) / 10).ToString();
            }
        }

        private void CheckForFullMatris(object sender, TextChangedEventArgs e) {
            TextBox[] elemanlar = new TextBox[] { b11, b12, b13, b14, b15, b21, b22, b23, b24, b25, b31, b32, b33, b34, b35, b41, b42, b43, b44, b45, b51, b52, b53, b54, b55 };
            var m = Int32.Parse(MBox.Text);
            var n = Int32.Parse(NBox.Text);
            // Matris elemanlarının kullanılabilir olanları içerisinde gez.
            // 1- Dolu mu diye kontrol et.
            // 2- Double olup olmadığını kontrol et.
            // Kullanılabilir elemanların hepsi bu şartları sağlıyorsa devam edilebilir. 
            int k = 0;
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 5; j++) {
                    if (i + 1 <= m && j + 1 <= n) {
                        var el = elemanlar[i * 5 + j];
                        if (el.Text != "") {
                            double willNotUse;
                            bool isDouble = Double.TryParse(el.Text, out willNotUse);
                            if (isDouble) {
                                k++;
                            }
                        }
                    }
                }
            }
            asama2DevamEt.IsEnabled = k == m * n;
        }

        private void matrisTemizle_Click(object sender, RoutedEventArgs e) {
            TextBox[] elemanlar = new TextBox[] { b11, b12, b13, b14, b15, b21, b22, b23, b24, b25, b31, b32, b33, b34, b35, b41, b42, b43, b44, b45, b51, b52, b53, b54, b55 };
            for (int i = 0; i < 25; i++) {
                elemanlar[i].Text = "";
            }
        }

        private void asama2Bitti() {
            scroller.ScrollToVerticalOffset(2);
            asama3Basla();
        }

        private void asama2DevamEt_Click(object sender, RoutedEventArgs e) {
            asama2Bitti();
        }
        #endregion

        #region Asama 3
        //                                         ____  
        //       /\                               |___ \ 
        //      /  \   ___  __ _ _ __ ___   __ _    __) |
        //     / /\ \ / __|/ _` | '_ ` _ \ / _` |  |__ < 
        //    / ____ \\__ \ (_| | | | | | | (_| |  ___) |
        //   /_/    \_\___/\__,_|_| |_| |_|\__,_| |____/ 

        double[,] a;
        double[,] aT;
        double[,] aTa;
        double[,] aaT;
        double[,] aTaM1;
        double[,] aaTM1;
        double[,] aP;

        private void asama3Basla() {
            toplama_sayisi = 0;
            carpma_sayisi = 0;

            TextBox[] elemanlar = new TextBox[] { b11, b12, b13, b14, b15, b21, b22, b23, b24, b25, b31, b32, b33, b34, b35, b41, b42, b43, b44, b45, b51, b52, b53, b54, b55 };
            var m = Int32.Parse(MBox.Text);
            var n = Int32.Parse(NBox.Text);

            // ELEMANLARI MATRİSE ATA \\
            double[,] matris = new double[m, n];
            for (int i = 0; i < m; i++) {
                for (int j = 0; j < n; j++) {
                    var el = elemanlar[i * 5 + j];
                    matris[i, j] = Double.Parse(el.Text);
                }
            }

            // TERSİNİ AL \\
            matris = pseudoinverse(matris);

            // GÖSTER \\
            gosterMatris(aP);
        }

        private void geriDonButon2_Click(object sender, RoutedEventArgs e) {
            // Aşama 2'ye geri dön.
            scroller.ScrollToVerticalOffset(1);
        }

        private void asama3Bitti() {
            scroller.ScrollToVerticalOffset(3);
            asama4Basla();
        }

        private void asama3DevamEt_Click(object sender, RoutedEventArgs e) {
            asama3Bitti();
        }

        public void gosterMatris(double[,] matris) {
            Label[] yazilar = new Label[] { t11, t12, t13, t14, t15, t21, t22, t23, t24, t25, t31, t32, t33, t34, t35, t41, t42, t43, t44, t45, t51, t52, t53, t54, t55 };
            var m = matris.GetLength(0);
            var n = matris.GetLength(1);

            // TEMİZLE \\
            for (int i = 0; i < 25; i++) {
                yazilar[i].Content = "";
            }

            // YERLEŞTİR \\
            for (int i = 0; i < m; i++) {
                for (int j = 0; j < n; j++) {
                    var el = matris[i, j];
                    yazilar[i * 5 + j].Content = Math.Round(el, 6).ToString();
                }
            }

            // ORTALA \\
            // M = 1 ise 90px aşağıya kaydır (ortala)
            var lm = ((5 - m) * 44);
            // N = 1 ise 90px sağa kaydır (ortala)
            var ln = ((5 - n) * 68);
            // Marginleri yerine koy
            showGrid.Margin = new Thickness(ln / 2, lm / 2, -ln / 2, -lm / 2);
        }
        #endregion

        #region PINV Hesaplama
        //    _____ _____ _   ___      __  _    _                       _                       
        //   |  __ \_   _| \ | \ \    / / | |  | |                     | |                      
        //   | |__) || | |  \| |\ \  / /  | |__| | ___  ___  __ _ _ __ | | __ _ _ __ ___   __ _ 
        //   |  ___/ | | | . ` | \ \/ /   |  __  |/ _ \/ __|/ _` | '_ \| |/ _` | '_ ` _ \ / _` |
        //   | |    _| |_| |\  |  \  /    | |  | |  __/\__ \ (_| | |_) | | (_| | | | | | | (_| |
        //   |_|   |_____|_| \_|   \/     |_|  |_|\___||___/\__,_| .__/|_|\__,_|_| |_| |_|\__,_|
        //                                                       | |                            
        //                                                       |_|                            

        public double[,] pseudoinverse(double[,] matris) {

            /*
             * Moore-Penrose Pseudoinverse
             * 
             * A matrisinin sozde tersini bulmak
             * Gerekli islemler:
             * 1-A matrisinin tanspozu bulunur.(At)
             * 2-At*A islemi bulunur.
             * 3-2.Islem sonucunun -1. kuvveti alinir.
             * 4-3.Islem sonucu At ile carpilir.(At*A)^-1*At
             * 
             */

            int m = matris.GetLength(0); // Matrisin satir sayisi.
            int n = matris.GetLength(1); // Matrisin sutun sayisi.

            a = matris;
            aT = transpose(a);

            if (m > n) {
                aTa = matris_carpimi(aT, a);
                aTaM1 = kuvvet_al(aTa, -1);
                aP = matris_carpimi(aTaM1, aT);
                aTaGoster.Content = "Aᵀ.A";
                aTaM1Goster.Content = "(Aᵀ.A)⁻¹";
                aPGoster.Content = "(Aᵀ.A)⁻¹.Aᵀ";
            } else {
                aaT = matris_carpimi(a, aT);
                aaTM1 = kuvvet_al(aaT, -1);
                aP = matris_carpimi(aT, aaTM1);
                aTaGoster.Content = "A.Aᵀ";
                aTaM1Goster.Content = "(A.Aᵀ)⁻¹";
                aPGoster.Content = "Aᵀ.(A.Aᵀ)⁻¹";
            }

            return aP;
        }

        public double[,] transpose(double[,] matris) {
            int m = matris.GetLength(0); // Matrisin satir sayisi.
            int n = matris.GetLength(1); // Matrisin sutun sayisi.
            double[,] trans = new double[n, m]; // Transpose matrisi.
            // Matris transpose islemi
            for (int i = 0; i < m; i++) {
                for (int j = 0; j < n; j++) {
                    trans[j, i] = matris[i, j];
                }
            }
            return trans;
        }

        public double[,] matris_carpimi(double[,] matris1, double[,] matris2) {
            int m1 = matris1.GetLength(0); // Matris1 satir
            int n1 = matris1.GetLength(1); // Matris1 sutun
            int m2 = matris2.GetLength(0); // Matris2 satir
            int n2 = matris2.GetLength(1); // Matris2 sutun
            if (n1 == m2) {
                double[,] carpim = new double[m1, n2];
                //Matris carpimi
                for (int i = 0; i < m1; i++) { // Carpim matris boyutlari
                    for (int j = 0; j < n2; j++) { // Carpim matris boyutlari
                        for (int h = 0; h < n1; h++) {
                            carpim[i, j] += matris1[i, h] * matris2[h, j]; // Matris carpimi
                            toplama_sayisi++;
                            carpma_sayisi++;
                        }
                    }
                }
                return carpim;
            } else {
                Console.Write("Matris boyutlari hatali. Fonksiyonu tekrar cagiriniz.");
                //Application.Exit();
                return null;
            }
        }

        public double[,] kuvvet_al(double[,] refMatris, int kuvvet) {

            int m = refMatris.GetLength(0); // Matrisin satir sayisi.
            int n = refMatris.GetLength(1); // Matrisin sutun sayisi.

            // Referans matrisimiz degismesin diye klonluyoruz
            double[,] matris = new double[refMatris.GetLength(0), refMatris.GetLength(1)];
            for (int i = 0; i < m; i++) {
                for (int j = 0; j < n; j++) {
                    matris[i, j] = refMatris[i, j];
                }
            }

            // Birim matris oluştur
            double[,] I = new double[m, m];
            for (int i = 0; i < m; i++) {
                for (int j = 0; j < n; j++) {
                    if (i == j) {
                        I[i, j] = 1;
                    } else {
                        I[i, j] = 0;
                    }
                }
            }

            double a, b;
            for (int i = 0; i < m; i++) {
                a = matris[i, i];
                for (int j = 0; j < m; j++) {
                    matris[i, j] = matris[i, j] / a;
                    carpma_sayisi++;
                    I[i, j] = I[i, j] / a;
                    carpma_sayisi++;
                }
                for (int k = 0; k < m; k++) {
                    if (k != i) {
                        b = matris[k, i];
                        for (int j = 0; j < m; j++) {
                            matris[k, j] = matris[k, j] - (matris[i, j] * b);
                            toplama_sayisi++;
                            carpma_sayisi++;
                            I[k, j] = I[k, j] - (I[i, j] * b);
                            toplama_sayisi++;
                            carpma_sayisi++;
                        }
                    }
                }
            }

            return I;
        }
        #endregion

        #region PINV Aşama Butonları 
        private void aGoster_Click(object sender, RoutedEventArgs e) {
            a3aciklama.Text = "Girdiğiniz matris aşağıdadır. (A)";
            gosterMatris(a);
        }

        private void aTGoster_Click(object sender, RoutedEventArgs e) {
            a3aciklama.Text = "Girdiğiniz matrisin transpozu aşağıdadır. (Aᵀ)";
            gosterMatris(aT);
        }

        private void aTaGoster_Click(object sender, RoutedEventArgs e) {

            int m = a.GetLength(0); // Matrisin satir sayisi.
            int n = a.GetLength(1); // Matrisin sutun sayisi.
            if (m > n) {
                gosterMatris(aTa);
                a3aciklama.Text = "Girdiğiniz matrisin transpozu ile kendisinin çarpımı aşağıdadır. (Aᵀ.A)";
            } else {
                gosterMatris(aaT);
                a3aciklama.Text = "Girdiğiniz matris ile transpozunun çarpımı aşağıdadır. (A.Aᵀ)";
            }
        }

        private void aTaM1Goster_Click(object sender, RoutedEventArgs e) {
            int m = a.GetLength(0); // Matrisin satir sayisi.
            int n = a.GetLength(1); // Matrisin sutun sayisi.
            if (m > n) {
                gosterMatris(aTaM1);
                a3aciklama.Text = "Girdiğiniz matrisin transpozu ile kendisinin çarpımının tersi aşağıdadır. (Aᵀ.A)⁻¹";
            } else {
                gosterMatris(aaTM1);
                a3aciklama.Text = "Girdiğiniz matris ile transpozunun çarpımının tersi aşağıdadır. (A.Aᵀ)⁻¹";
            }
        }

        private void aPGoster_Click(object sender, RoutedEventArgs e) {
            a3aciklama.Text = "Girdiğiniz matrisin tersi aşağıdadır. (A⁺)";
            gosterMatris(aP);
        }
        #endregion

        #region Asama 4
        //                                         _  _   
        //       /\                               | || |  
        //      /  \   ___  __ _ _ __ ___   __ _  | || |_ 
        //     / /\ \ / __|/ _` | '_ ` _ \ / _` | |__   _|
        //    / ____ \\__ \ (_| | | | | | | (_| |    | |  
        //   /_/    \_\___/\__,_|_| |_| |_|\__,_|    |_|                                          

        private void asama4Basla() {
            toplama.Content = "Toplama İşlemi: " + toplama_sayisi;
            carpma.Content = "Çarpma İşlemi: " + carpma_sayisi;
        }

        private void geriDonButon3_Click(object sender, RoutedEventArgs e) {
            // Aşama 3'e geri dön.
            scroller.ScrollToVerticalOffset(2);
        }

        private void bastanBasla_Click(object sender, RoutedEventArgs e) {
            // Baştan başla.
            scroller.ScrollToVerticalOffset(0);
            MBox.Text = "";
            NBox.Text = "";
        }
        #endregion
    }
}
