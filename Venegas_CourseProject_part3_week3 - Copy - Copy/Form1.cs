using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Venegas_CourseProject_Part2
{
    public partial class Form1 : Form
    {
        //class level references
        private string[] titleArray = new string[5];    //song titles
        private string[] artistArray = new string[5];   //associated artists
        private string[] genreArray = new string[5];    //associated genres
        private int[] yearArray = new int[5];   //associated years
        private string[] urlArray = new string[5];  //associated urls
        private int songCount = 0;  //number of songs in the arrays

        public Form1()
        {
            InitializeComponent();
        }

        private bool ValidInput()
        {

            bool isValid = true;

            if (string.IsNullOrEmpty(titleText.Text))
            {
                //Title is blank
                MessageBox.Show("The Song Title Can Not Be Blank!");
                isValid = false;
            }
            else if (string.IsNullOrEmpty(artistText.Text))
            {
                //Artist is blank
                MessageBox.Show("The Artist Can Not Be Blank!");
                isValid = false;
            }
            else if (string.IsNullOrEmpty(genreText.Text))
            {
                //Genre is blank
                MessageBox.Show("The Genre Can Not Be Blank!");
                isValid = false;
            }
            else if (string.IsNullOrEmpty(yearText.Text))
            {
                //year cannot be  blank
                MessageBox.Show("The Year Can Not Be Blank!");
                isValid = false;
            }

            else if (string.IsNullOrEmpty(urlText.Text))
            {
                //URL is blank
                MessageBox.Show("The URL Can Not Be Blank!");
                isValid = false;
            }
            return isValid;
        }


            private void AddButton_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder(outputText.Text);
            string nl = "\r\n";

            if ( songCount == titleArray.Length )
                MessageBox.Show("Song list is full!");
            else if ( ValidInput() )

            {
                //Add Song Title to the list box
                songListBox.Items.Add(titleText.Text);
                titleArray[songCount] = titleText.Text;
                artistArray[songCount] = artistText.Text;
                genreArray[songCount] = genreText.Text;
                yearArray[songCount] = int.Parse(yearText.Text);
                urlArray[songCount] = urlText.Text;


                //Build the output
                sb.Append(titleText.Text);
                sb.Append(nl);
                sb.Append(artistText.Text);
                sb.Append(nl);
                sb.Append(genreText.Text);
                sb.Append(nl);
                sb.Append(yearText.Text);
                sb.Append(nl);
                sb.Append(urlText.Text);
                sb.Append(nl);
                sb.Append(yearText.Text);
                sb.Append(nl);
                sb.Append(nl);

                outputText.Text = sb.ToString();

                //increment the song count
                songCount++;


                //clear the input fields
                titleText.Clear();
                artistText.Clear();
                genreText.Clear();
                yearText.Clear();
                urlText.Clear();  //move cursor to titleText
            }
        }

        private void allSongsButton_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder(string.Empty);
            string nl = "\r\n";

            //build the output text
            foreach(var item in songListBox.Items)
            {
                sb.Append(item.ToString());
                sb.Append(nl);
            }

            //put the output text in the output text box
            outputText.Text = sb.ToString();
        }

        private int GetSong( string songTitle)
        {
            int songIndex = songListBox.Items.IndexOf(songTitle);
            return songIndex;

        }

        private void songListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int songIndex = songListBox.SelectedIndex;

            if (songIndex > -1)
            {
                StringBuilder sb = new StringBuilder(string.Empty);
                string nl = "\r\n";

                //Build the output
                sb.Append(titleArray[songIndex]);
                sb.Append(nl);
                sb.Append(artistArray[songIndex]);
                sb.Append(nl);
                sb.Append(genreArray[songIndex]);
                sb.Append(nl);
                sb.Append(yearArray[songIndex].ToString());
                sb.Append(nl);
                sb.Append(urlArray[songIndex]);
                sb.Append(nl);
                sb.Append(yearArray[songIndex]);
                sb.Append(nl);
                sb.Append(nl);

                outputText.Text = sb.ToString();
            }

        }

        private bool SongInList(string songTitle)
        {
            bool songFound = false;
            
            foreach(var item in songListBox.Items)
            {
                string currentSong = item as string;
                if (songTitle.ToLower()== currentSong.ToLower())
                {
                    songFound = true;
                    break;
                 }
            }
        
            return songFound;
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            if (SongInList(titleText.Text))
            {
            

                StringBuilder sb = new StringBuilder(string.Empty);
                string nl = "\r\n";

                int songIndex = GetSong(titleText.Text);

                if(songIndex < 0)
                { 
                    MessageBox.Show("Song not found!");
                    return;
                }
                //Build the output
                sb.Append(titleArray[songIndex]);
                sb.Append(nl);
                sb.Append(artistArray[songIndex]);
                sb.Append(nl);
                sb.Append(genreArray[songIndex]);
                sb.Append(nl);
                sb.Append(yearArray[songIndex].ToString());
                sb.Append(nl);
                sb.Append(urlArray[songIndex]);
                sb.Append(nl);
                sb.Append(yearArray[songIndex]);
                sb.Append(nl);
                sb.Append(nl);

                outputText.Text = sb.ToString();
            }
            else
            {
                MessageBox.Show("Song not found!");
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            titleText.Clear();
            artistText.Clear();
            genreText.Clear();
            yearText.Clear();
            urlText.Clear();
            titleText.Focus(); //move insertion point to titleText
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            int songIndex = songListBox.SelectedIndex;
            string url = urlArray[songIndex];
            webViewDisplay.CoreWebView2.Navigate(url);
        }
    }
}
