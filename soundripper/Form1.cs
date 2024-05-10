using System.Net;
using System;
using System.IO;
using System.Windows.Forms;
using MediaToolkit; // used nuget
using MediaToolkit.Model; // used nuget
using VideoLibrary;
using System.Diagnostics.Tracing;
using System.Diagnostics;
using YoutubeExplode.Channels; // used nuget

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
        string username = Environment.UserName; // gets username
        string downloadspath;  // finds path to user downloads folder



        private async void btnConvert_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtVideoLink.Text)) // check that the texbox is not empty
            {
                string link = txtVideoLink.Text; // sets link equal to text in textbox

                if (await IsYoutube(link)) // call IsYoutube method and checks if is true, then downloads youtube video
                {
                    downloadspath = $"C:\\Users\\{username}\\Downloads\\";
                    var youtube = YouTube.Default;
                    var video = youtube.GetVideo(link);
                    var videotitle = video.Title;
                    var path = $"{downloadspath}{videotitle}.mp4"; // mp4 path
                    var path2 = $"{downloadspath}{videotitle}.mp3"; // mp3 path
                    downloadprogress = new frmDownloadProgress();
                    downloadprogress.Show();
                    await DownloadVideo(video, path, downloadspath, downloadprogress); // download video
                    downloadprogress.Close();
                    downloadprogress.Dispose();
                    await ConvertVideo(path, path2); // convert video to mp3
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

        private static async Task DownloadVideo(YouTubeVideo video, string path, string downloadspath, frmDownloadProgress downloadProgressForm) // downloads youtube video
        {
            var bytes = video.GetBytes();
            // THIS WORKS TO DOWNLOAD FILE await File.WriteAllBytesAsync(path, bytes); // write file to path

            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write)) // uses progress bar
            {
                long totalBytes = bytes.Length;
                long bytesRead = 0;
                int bufferSize = 1024;
                byte[] buffer = new byte[bufferSize];
                int bytesToRead;

                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    while ((bytesToRead = await ms.ReadAsync(buffer, 0, bufferSize)) > 0)
                    {
                        await fs.WriteAsync(buffer, 0, bytesToRead);
                        bytesRead += bytesToRead;
                        int progress = (int)(((double)bytesRead / totalBytes) * 100);
                        downloadProgressForm.UpdateProgressBar(progress); // update progress bar in frmDownloadProgress
                    }
                }
            }
        }

        private static async Task ConvertVideo(string inputfilepath, string outputfilepath)
        {
            using (var engine = new Engine())
            {
                var outputfile = new MediaFile(outputfilepath);
                var inputfile = new MediaFile(inputfilepath);
                engine.GetMetadata(inputfile);
                engine.Convert(inputfile, outputfile);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // not needed!!!! dont delete though it will break form designer!!!!!
        }
    }
}
