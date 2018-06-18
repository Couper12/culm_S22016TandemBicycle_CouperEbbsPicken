/*
 * CouperEbbsPicken
 * 6/18/2018
 * Do a problem
 */ 
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
using System.IO;

namespace culm_S2TandemBicycle_CouperEbbsPicken
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // global variables
        StreamReader streamReader = new StreamReader("Input.txt");
        string input1;
        string input2;
        string input3;
        string input4;
        int N;
        int[] speeds1;
        int[] speeds2;
        int[] spacesPeg;
        int[] spacesDmo;
        string pegSpeeds;
        string dmoSpeeds;
        int totalSpeed;
        int counter;
        bool good;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        // when the button gets clicked
        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // gets the first 4 lines of input
                while (!streamReader.EndOfStream)
                {
                    input1 = streamReader.ReadLine();
                    input2 = streamReader.ReadLine();
                    input3 = streamReader.ReadLine();
                    input4 = streamReader.ReadLine();
                }
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // sets variables
            int j;
            int k;
            int counter2 = 0;
            counter = 0;
            totalSpeed = 0;
            pegSpeeds = input4;
            dmoSpeeds = input3;
            int.TryParse(input2, out N);
            speeds1 = new int[N];
            speeds2 = new int[N];
            spacesPeg = new int[N - 1];
            spacesDmo = new int[N - 1];

            // creates an array of all the space indexes
            foreach (char c in pegSpeeds)
            {
                if (c == ' ')
                {
                    spacesPeg[counter] = pegSpeeds.IndexOf(c);
                    pegSpeeds = pegSpeeds.Substring(0, pegSpeeds.IndexOf(c)) + "_" + pegSpeeds.Substring(pegSpeeds.IndexOf(c) + 1);
                    //MessageBox.Show(pegSpeeds);
                    counter++;
                }
            }

            // same but for other list
            counter = 0;
            foreach (char c in dmoSpeeds)
            {
                if (c == ' ')
                {
                    spacesDmo[counter] = dmoSpeeds.IndexOf(c);
                    dmoSpeeds = dmoSpeeds.Substring(0, dmoSpeeds.IndexOf(c)) + "_" + dmoSpeeds.Substring(dmoSpeeds.IndexOf(c) + 1);
                    counter++;
                }
            }

            // adds the first speeds
            string temp1 = pegSpeeds.Substring(0, spacesPeg[0]);
            string temp2 = dmoSpeeds.Substring(0, spacesDmo[0]);
            int.TryParse(temp1, out j);
            int.TryParse(temp2, out k);
            speeds1[0] = j;
            speeds2[0] = k;

            // adds all the middle speeds
            for (int i = 0; i < N - 2; i++)
            {
                temp1 = pegSpeeds.Substring(spacesPeg[i] + 1, spacesPeg[i + 1] - (spacesPeg[i] + 1));
                temp2 = dmoSpeeds.Substring(spacesDmo[i] + 1, spacesDmo[i + 1] - (spacesDmo[i] + 1));

                int.TryParse(temp1, out j);
                speeds1[i + 1] = j;

                if (speeds1[i + 1] < speeds1[i])
                {
                   speeds1[i + 1] = speeds1[i];
                   speeds1[i] = j;
                }

                int.TryParse(temp2, out k);
                speeds2[i + 1] = k;

                if (speeds2[i + 1] < speeds2[i])
                {
                   speeds2[i + 1] = speeds2[i];
                   speeds2[i] = k;
                }
            }

            // adds the last speeds
            temp1 = pegSpeeds.Substring(spacesPeg[N - 2] + 1);
            temp2 = dmoSpeeds.Substring(spacesDmo[N - 2] + 1);
            int.TryParse(temp1, out j);
            speeds1[speeds1.Length - 1] = j;

            if (speeds1[speeds1.Length - 1] < speeds1[speeds1.Length - 2])
            {
                speeds1[speeds1.Length - 1] = speeds1[speeds1.Length - 2];
                speeds1[speeds1.Length - 2] = j;
            }



            int.TryParse(temp2, out k);
            speeds2[speeds2.Length - 1] = k;

            if (speeds2[speeds2.Length - 1] < speeds2[speeds2.Length - 2])
            {
                speeds2[speeds2.Length - 1] = speeds2[speeds2.Length - 2];
                speeds2[speeds2.Length - 2] = k;
            }

            // sorts the speeds into ascending order
           while (good == false)
            {
                counter2 = 0;
                for (int i = 0; i < N - 1; i++)
                {
                    if (speeds1[i + 1] < speeds1[i])
                    {
                        j = speeds1[i + 1];
                        speeds1[i + 1] = speeds1[i];
                        speeds1[i] = j;
                        counter2++;
                    }

                    if (speeds2[i + 1] < speeds2[i])
                    {
                        k = speeds2[i + 1];
                        speeds2[i + 1] = speeds2[i];
                        speeds2[i] = k;
                        counter2++;
                    }

                }

                if (counter2 == 0)
                {
                    good = true;
                }
            }

           // answer if question 1
            if (input1 == "1")
            {
                for (int i = N - 1; i > -1; i--)
                {
                    totalSpeed += Math.Max(speeds1[i], speeds2[i]);
                }
            }

            // does answer if question 2
            else if (input1 == "2")
            {
                int i = N - 1;
                for (int y = 0; y < N; y++)
                {
                    totalSpeed += Math.Max(speeds1[i], speeds2[y]);
                    i--;
                }
            }

            // outputs answer
            lblOutput.Content = totalSpeed.ToString();
        }
    }
}
