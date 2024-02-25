using Downloader;
using Octokit;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace potato_launcher
{
    public partial class MainForm : Form
    {
        readonly IniFile settingsIni = new IniFile(("Version", "", "Info"), ("Path", "", "Info"));
        readonly GitHubClient githubClient = new GitHubClient(new ProductHeaderValue("Potato-Launcher"));
        readonly DownloadService downloader = new DownloadService();
        readonly ZipExtractor extractor = new ZipExtractor();
        readonly FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

        private string? yuzuPath;
        private string? tempDirectory;

        public MainForm()
        {
            InitializeComponent();
            InitializeUI();
            CheckForUpdate();
        }

        private void InitializeUI()
        {
            // Get current version
            label_current.Visible = settingsIni.Read("Version", "Info") != "";
            label_current.Text = settingsIni.Read("Version", "Info");
        }

        private string GetAppData()
        {
            string AppData = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\{Assembly.GetExecutingAssembly().GetName().Name}";

            return AppData;
        }

        private async void CheckForUpdate()
        {

            // Get latest release
            var release = await githubClient.Repository.Release.GetLatest("PineappleEA", "pineapple-src");

            // Find the ZIP file among the assets
            var zipAsset = release.Assets.FirstOrDefault(asset =>
                    asset.ContentType == "application/zip"
                );

            // Display the latest version number
            label_latest.Visible = true;
            label_latest.Text = release.TagName;

            // Check wether yuzu.exe is found next to app
            if (File.Exists($"{Environment.CurrentDirectory}\\yuzu.exe"))
            {
                yuzuPath = Environment.CurrentDirectory;
            }
            // If not, prompt user to select the folder
            else if (settingsIni.Read("Path", "Info") == "")
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    // Use the selected folder to set path to extract
                    yuzuPath = folderBrowserDialog.SelectedPath;

                    // Save this folder to use next time
                    settingsIni.Write("Path", folderBrowserDialog.SelectedPath, "Info");
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            else yuzuPath = settingsIni.Read("Path", "Info");

            // Check whether update is needed, else run yuzu
            if (!settingsIni.Read("Version", "Info").Equals(release.TagName) || settingsIni.Read("Version", "Info") == "")
            {
                DownloadAsset(zipAsset.BrowserDownloadUrl, release.TagName, zipAsset.Name, yuzuPath);
            }
            else
            {
                RunYuzu(yuzuPath);
            }
        }

        private async void DownloadAsset(string downloadUrl, string releaseTag, string filename, string extractPath)
        {
            string downloadPath;

            try
            {
                //Create temp download directory
                tempDirectory = $"{GetAppData()}\\_Temp";
                Directory.CreateDirectory(tempDirectory);
                downloadPath = Path.Combine(tempDirectory, filename);

                label_process.Visible = true;
                label_process.Text = $"Downloading {releaseTag}...";
                progress_bar.Style = ProgressBarStyle.Continuous;

                // Report download progress
                downloader.DownloadProgressChanged += (sender, e) =>
                {
                    this.Invoke(new Action(() =>
                    {
                        // Update progress bar with download progress
                        progress_bar.Value = (int)Math.Min(e.ProgressPercentage, progress_bar.Maximum);

                        // Calculate download speed
                        double speedBps = e.BytesPerSecondSpeed;
                        double speedKBps = speedBps / 1024;
                        double speedMBps = speedKBps / 1024;
                        string speed;

                        // Calculate download size and received size in MB
                        double totalDownloadedMB = e.ReceivedBytesSize / (1024 * 1024.0);
                        double totalMB = e.TotalBytesToReceive / (1024 * 1024.0);

                        // Display speed in different units
                        if (speedMBps > 1)
                        {
                            speed = $"{speedMBps:F2} MB/s";
                        }
                        else if (speedKBps > 1)
                        {
                            speed = $"{speedKBps:F2} KB/s";
                        }
                        else
                        {
                            speed = $"{speedBps:F2} B/s";
                        }

                        // Format all download info for label
                        label_info.Text = string.Format("{0} / {1} MB @ {2}", $"{totalDownloadedMB:F2}", $"{totalMB:F2}", speed);
                        label_info.Visible = true;
                    }));
                };

                // Check if download was interupted
                downloader.DownloadFileCompleted += (sender, e) =>
                {
                    if (e.Cancelled)
                    {
                        if (Directory.Exists(tempDirectory))
                        {
                            Directory.Delete(tempDirectory, true);
                        }
                    }
                };

                // Start download
                await downloader.DownloadFileTaskAsync(downloadUrl, downloadPath);

                label_process.Text = "Extracting...";

                // Report extraction progress
                extractor.UnzipProgressChanged += (sender, e) =>
                {
                    progress_bar.Value = e;
                };

                // Start extraction
                await extractor.ExtractZipAsync(downloadPath, extractPath, ".xz");

                // Save the downloaded version number
                settingsIni.Write("Version", releaseTag, "Info");

                // Delete the temp directory that contains the download
                Directory.Delete(tempDirectory, true);

                // Run Yuzu after download
                RunYuzu(extractPath);
            }
            catch (Exception ex)
            {
                label_process.Text = "Error...";
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RunYuzu(string path)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = $"{path}\\yuzu.exe",
                UseShellExecute = true
            };

            Process process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();

            Environment.Exit(0);
        }

        private void label_github_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer.exe", "https://github.com/DogFoxX/potato-launcher");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (downloader.IsBusy)
            {
                downloader.Pause();

                DialogResult result = MessageBox.Show("Are you sure you want to cancel the download?", "Download in progress", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                // Check the user's choice
                if (result == DialogResult.Yes)
                {
                    // User chose to cancel the download
                    downloader.CancelAsync();
                    // Additional cleanup or handling for cancellation if needed
                }
                else
                {
                    downloader.Resume();
                    e.Cancel = true;
                }
            }
        }
    }
}
