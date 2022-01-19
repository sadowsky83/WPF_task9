using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WPF_task9
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<string> styles = new List<string>() {"Светлая тема", "Темная тема"};
            styleBox.ItemsSource = styles;
            styleBox.SelectionChanged += ThemeCange;
            styleBox.SelectedIndex = 0;            
        }

        private void ThemeCange(object sender, SelectionChangedEventArgs e)
        {
            int styleIndex = styleBox.SelectedIndex;
            Uri uri = new Uri("brightThemeDict.xaml", UriKind.Relative);
            if(styleIndex == 1)
            {
                uri = new Uri("darkThemeDict.xaml", UriKind.Relative);
            }
            ResourceDictionary resource = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resource);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) // Шррифт
        {
            string fontName = ((sender as ComboBox).SelectedItem as string);
            if (textBox != null)
                textBox.FontFamily = new FontFamily(fontName);
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e) // Размер шрифта
        {
            double fontSize = Convert.ToDouble(((sender as ComboBox).SelectedItem as string));
            if (textBox != null)
                textBox.FontSize = fontSize;
        }

        private void btnBold_Click(object sender, RoutedEventArgs e) // Жирный
        {
            if (textBox.FontWeight == FontWeights.Normal)
                textBox.FontWeight = FontWeights.Bold;
            else
                textBox.FontWeight = FontWeights.Normal;
        }

        private void btnItalic_Click(object sender, RoutedEventArgs e) // Курсив
        {
            if (textBox.FontStyle == FontStyles.Normal)
                textBox.FontStyle = FontStyles.Italic;
            else
                textBox.FontStyle = FontStyles.Normal;
        }

        private void btnUnderLine_Click(object sender, RoutedEventArgs e) // Подчеркивание
        {
            if (textBox.TextDecorations == null)
            {
                textBox.TextDecorations = TextDecorations.Underline;
            }
            else
                textBox.TextDecorations = null;
        }

        private void rbtnBlack_Click(object sender, RoutedEventArgs e) // Черный
        {
            textBox.Foreground = Brushes.Black;
        }

        private void rbtnRed_Click(object sender, RoutedEventArgs e) // Красный
        {
            textBox.Foreground = Brushes.Red;
        }       

        private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            if (openDialog.ShowDialog() == true)
                textBox.Text = File.ReadAllText(openDialog.FileName);
        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            if (saveDialog.ShowDialog() == true)
                File.WriteAllText(saveDialog.FileName, textBox.Text);
        }

        private void CloseExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
