using System.Net;
using System;
using System.IO;
using System.Windows.Forms;
using MediaToolkit; // used nuget
using MediaToolkit.Model; // used nuget
using VideoLibrary;
using YoutubeExplode;
using System.Diagnostics.Tracing;
using System.Diagnostics; // used nuget

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

                    await DownloadAndConvertVideo(link, downloadprogress);
                    
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

        private async Task DownloadAndConvertVideo(string link, frmDownloadProgress downloadprogress)
        {
            try
            {
                var youtube = new YoutubeClient();
                var video = await youtube.Videos.GetAsync(link);
                var username = Environment.UserName;
                var downloadsfolder = $"C:\\Users\\{username}\\Downloads";
                var videofilepath = Path.Combine(downloadsfolder, video.Title + ".mp4");
                var progress = new Progress<int>(percentage =>
                {
                    // update progress bar using the provided downloadprogress form
                    downloadprogress.updateProgressBar(percentage);
                });
                await DownloadVideo(video.Url, videofilepath, progress, downloadprogress);
                await ConvertVideo(videofilepath, downloadsfolder);


                if (!chkbxKeepVideo.Checked)
                {
                    File.Delete(videofilepath);
                }

                ProcessStartInfo startinfo = new ProcessStartInfo
                {
                    Arguments = downloadsfolder
                };
            }
            catch
            {
                // AHAHAHAH HANDLE EXCEPTIONS MORE EXCEPTIONS LDJKFLSKDJFKLSDFJKLSFJED
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

        private static async Task DownloadVideo(string link, string filepath, IProgress<int> progress, frmDownloadProgress downloadprogress)
        {
            try
            {
                MessageBox.Show("Starting download...");
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync(link, HttpCompletionOption.ResponseHeadersRead))
                    {
                        response.EnsureSuccessStatusCode();
                        var contentLength = response.Content.Headers.ContentLength;

                        using (var stream = await response.Content.ReadAsStreamAsync())
                        {
                            var totalbytesread = 0L;
                            var buffer = new byte[8192];
                            var ismoretoread = true;
                            int percentage = 0;

                            using (var filestream = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.None, buffer.Length, true))
                            {
                                do
                                {
                                    var bytesread = await stream.ReadAsync(buffer, 0, buffer.Length);
                                    if (bytesread == 0)
                                    {
                                        ismoretoread = false;
                                        progress.Report(100);
                                        continue;
                                    }
                                    await filestream.WriteAsync(buffer, 0, bytesread);
                                    totalbytesread += bytesread;

                                    if (contentLength.HasValue)
                                    {
                                        percentage = (int)Math.Round((double)totalbytesread / (double)contentLength.Value * 100);
                                        progress.Report(percentage);
                                        downloadprogress.updateProgressBar(percentage); // Use downloadprogress to update the progress bar
                                    }
                                } while (ismoretoread);
                            }
                        }
                    }
                }
                downloadprogress.Close(); // Close the progress form when download is complete
            }
            catch (Exception ex)
            {
                System.Media.SystemSounds.Hand.Play();
                MessageBox.Show($"An error occurred while downloading the video: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private static async Task ConvertVideo(string videofilepath, string outputfolder)
        {
            var inputfile = new MediaFile { Filename = videofilepath };
            var outputfilepath =  Path.Combine(outputfolder, Path.GetFileNameWithoutExtension(videofilepath) + ".mp3");
            var outputfile = new MediaFile { Filename = outputfilepath };
            using (var engine =  new Engine())
            {
                engine.GetMetadata(inputfile);
                engine.Convert (inputfile, outputfile);
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // not needed!!!! dont delete though it will break form designer!!!!!
        }
    }
}
