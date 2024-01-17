using System;
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
using System.Windows.Shapes;

namespace Käyttöliittymäohjelmoinnin_harjoitustyö {
    /// <summary>
    /// Interaction logic for ImageView.xaml
    /// </summary>
    
    // Tässä luokassa käsitellään kuvia joita voi selata eteen -ja taaksepäin
    public partial class ImageView : Window {

        private List<string> imagePaths; 
        private int currentIndex = 0;   
        public ImageView(List<string> imagePaths) {
            InitializeComponent();

            this.imagePaths = imagePaths;
            LoadImage(currentIndex);
        }
        private void NextButton_Click(object sender, RoutedEventArgs e) {
            // Seuraavaan kuvaan liikkuminen
            currentIndex = (currentIndex + 1) % imagePaths.Count;

            // Ladataan uusi kuva
            LoadImage(currentIndex);
        }
        private void PrevButton_Click(object sender, RoutedEventArgs e) {
            //Liikutaan edelliseen kuvaan
            currentIndex = (currentIndex - 1 + imagePaths.Count) % imagePaths.Count;

            // Ladataan uusi kuva
            LoadImage(currentIndex);
        }
        
        private void LoadImage(int index) {
            
            BitmapImage bitmap = new BitmapImage();

            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePaths[index]);
            bitmap.EndInit();

            imageControl.Source = bitmap;
        }
    }
}
