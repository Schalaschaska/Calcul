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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace C7
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string Answer = null, Memory = null;
        bool isReady = false;

        private void Ans_Click(object sender, RoutedEventArgs e)
        {
            if (!isReady)
            {
                txtBоx.VerticalAlignment = VerticalAlignment.Top;
                txtBоx.Text = Answer;
                isReady = true;
                StаtusLаbel.Visibility = Visibility.Hidden;
            }
            else
            {
                txtBоx.Text += Answer;
            }

        }

        private void MR_Click(object sender, RoutedEventArgs e)
        {
            if (!isReady)
            {
                txtBоx.VerticalAlignment = VerticalAlignment.Top;
                txtBоx.Text = Answer;
                isReady = true;
                StаtusLаbel.Visibility = Visibility.Hidden;
            }
            else
            {
                txtBоx.Text += Answer;
            }

        }

        private void M_Click(object sender, RoutedEventArgs e)
        {
            if (isReady)
            {
                Memory = txtBоx.Text;
            }
            else
            {
                Memory = Answer;
            }
            MR.ToolTip = Memory;

        }

        private void MC_Click(object sender, RoutedEventArgs e)
        {
            Memory = null;
            MR.ToolTip = "No value specified for memory.";

        }

        private void Sqrt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double b = double.Parse(txtBоx.Text);
                b = Math.Sqrt(b);
                txtBоx.Text = b.ToString();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error");

            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isReady)
                {
                    txtBоx.VerticalAlignment = VerticalAlignment.Top;
                    StаtusLаbel.Visibility = Visibility.Hidden;
                    txtBоx.Text = Answer;
                    isReady = true;
                }
                else
                {
                    txtBоx.Text = txtBоx.Text.Substring(0, txtBоx.Text.Length - 1);
                }
            }
            catch (Exception)
            {
                isReady = false;
            }

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            txtBоx.VerticalAlignment = VerticalAlignment.Top;
            StаtusLаbel.Visibility = Visibility.Hidden;
            isReady = false;
            Memory = null;
            txtBоx.Text = null;
            MR.ToolTip = "No value specified for memory.";
            this.Height = 350;
            this.Width = 525;
            Answer = null;

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            EvalLib evaluate = new EvalLib();
            try
            {
                for (int i = 0; i < txtBоx.Text.Length; i++)
                {
                    if (txtBоx.Text[i] == '!' || ((txtBоx.Text[i] >= 40 && txtBоx.Text[i] <= 57) && txtBоx.Text[i] != 44) || txtBоx.Text[i] == 37)//ASCII
                    {
                        continue;
                    }
                    throw new KeyNotFoundException();
                }
                double result = evaluate.TextToValue(txtBоx.Text);
                StаtusLаbel.Content = txtBоx.Text;
                txtBоx.VerticalAlignment = VerticalAlignment.Bottom;
                txtBоx.Text = "=" + result.ToString();
                StаtusLаbel.Visibility = Visibility.Visible;
                isReady = false;
                Answer = result.ToString();

            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show("Your input contains a character\nthat can't be calculated.", "Error");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error");
            }

        }

        private void dgtClick(object sender, RoutedEventArgs e)
        {

            if (!isReady)
            {
                txtBоx.VerticalAlignment = VerticalAlignment.Top;
                txtBоx.Text = ((Button)sender).Content.ToString();
                isReady = true;
                StаtusLаbel.Visibility = Visibility.Hidden;
            }
            else
            {
                txtBоx.Text += ((Button)sender).Content;
            }


        }
        private void About_Click(object sender, RoutedEventArgs e)
        {
            string about = "Write down your own Name, company, and what you want to tell the user about you.";
            MessageBox.Show(about, "About My Calculator", MessageBoxButton.OK, MessageBoxImage.Information);


        }
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            string help =
             "        Just type any kind of complex or simple expression,\n" +
             "the calculator have flexibility to produce accurate result.\n\n" +

             "        You can use memory for store either a result or a \n" +
             "complete expression. If there are any expression prevails \n" +
             "in the screan, this will be stored if you press 'M+'. By \n" +
             "pressing 'MR' you can add the memory content at any\n" +
             "time to screan.'MC' will clear the memory. 'Ans' is an\n" +
             "automatic memory. Every time you have a value for an\n" +
             "expression, the resulted value will be stored in 'Ans'. You\n" +
             "can only use it same as 'MR' but you can't assign anything\n" +
             "to it as it is an automatic memory.\n\n" +


             "         Reset will restore the calculator to its initial value.\n" +
             "Remember, ALL MEMORY WILL BE LOST IF YOU CLICK 'Reset'\n" +
             "You can use Del to clear the memory character by character.\n\n" +

             "         Have fun using this calculator.";
            MessageBox.Show(help, "Help", MessageBoxButton.OK, MessageBoxImage.Information);



        }
    }
}
