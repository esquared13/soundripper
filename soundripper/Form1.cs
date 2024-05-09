using System.Net;
using System;
using System.IO;
using System.Windows.Forms;
using MediaToolkit; // used nuget
using MediaToolkit.Model; // used nuget
using VideoLibrary; // used nuget

namespace soundripper
{
    public partial class frmsoundripper : Form
    {
        public frmsoundripper()
        {
            InitializeComponent();
        }

        private frmDownloadProgress downloadprogress;
        // add conversion progress here 

        private async void btnConvert_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtVideoLink.Text)) // check that the texbox is not empty
            {
                string link = txtVideoLink.Text; // sets link equal to text in textbox

                if (await IsYoutube(link)) // call IsYoutube method and checks if is true, then converts youtube video to MP3
                {
                    downloadprogress = new frmDownloadProgress(); // initialize downloadprogress
                    downloadprogress.Show();
                    await downloadVideo(link);
                }
                else
                {
                    System.Media.SystemSounds.Hand.Play();
                    MessageBox.Show("Please specify a valid link.", "soundripper", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                System.Media.SystemSounds.Hand.Play(); // plays error sound!!!!
                MessageBox.Show("Please specify a YouTube link.", "soundripper", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<bool> IsYoutube(string link) // checks if remote file exists
        {
            HttpResponseMessage response;
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(10); // sets request timeout
                    response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, link));
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return true;
                    }
                }
            }
            catch (WebException ex)
            {
                System.Media.SystemSounds.Hand.Play();
                MessageBox.Show(ex.Message + "\nUnable to process request.  There may have been an issue with your network connectivity or the site requested.", "soundripper", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (UriFormatException ex1)
            {
                System.Media.SystemSounds.Hand.Play();
                MessageBox.Show(ex1.Message + "\nText entered was not a valid URL.", "soundripper", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                response = null;
                response?.Dispose(); // closes response
            }
            return false;
        }
        private async Task downloadVideo (string link)
        {
            var downloadsfolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // grabs user's path to downloads folder
            var youtube = YouTube.Default;
            var video = youtube.GetVideo(link); // gets video
            var filepath = Path.Combine(downloadsfolder, video.FullName); // creates filepath

            using (var client = new WebClient())
            {
                client.DownloadProgressChanged += (s, e) => // changed event handler; updates progress bar
                {
                    downloadprogress.updateProgressBar(e.ProgressPercentage); // calls updateProgressBar method
                };

                client.DownloadFileCompleted += (s, e) => // completed event handler; saves file
                {
                    var inputFile = new MediaFile { Filename = filepath };
                    var outputFilePath = Path.Combine(downloadsfolder, $"{video.Title}.mp3");
                    var outputFile = new MediaFile { Filename = outputFilePath };

                    MessageBox.Show($"Input file path: {inputFile.Filename}");
                    

                    using (var engine = new Engine())
                    {
                        engine.GetMetadata(inputFile);
                        engine.Convert(inputFile, outputFile);
                    }

                    if (!chkbxKeepVideo.Checked) // checks that checkbox is not checked
                    {
                        File.Delete(filepath);
                    }

                    downloadprogress.Close();

                };

                await client.DownloadFileTaskAsync(new Uri(video.Uri), filepath);

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // not needed!!!! dont delete though it will break form designer!!!!!
        }
    }
}
