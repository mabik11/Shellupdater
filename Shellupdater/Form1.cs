using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Shellupdater
{
    public partial class Form1 : Form
    {
        private bool isWingetInstalled = false;
        private string wingetVersion = "";

        public Form1()
        {
            InitializeComponent();
            CheckWingetInstallation();
        }

        private void AppendConsoleText(string text)
        {
            if (txtConsoleOutput.InvokeRequired)
            {
                txtConsoleOutput.Invoke(new Action<string>(AppendConsoleText), text);
            }
            else
            {
                txtConsoleOutput.AppendText(text + "\n");
                txtConsoleOutput.ScrollToCaret();
            }
        }

        private void CheckWingetInstallation()
        {
            AppendConsoleText("Winget kurulum durumu kontrol ediliyor...");

            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/C where winget",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(psi))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (string.IsNullOrEmpty(error) && output.Contains("winget.exe"))
                    {
                        isWingetInstalled = true;
                        GetWingetVersion();
                        return;
                    }
                }

                isWingetInstalled = false;
                AppendConsoleText("Winget yüklü değil.");
                AskToInstallWinget();
            }
            catch (Exception ex)
            {
                AppendConsoleText($"Kontrol hatası: {ex.Message}");
            }
        }

        private void GetWingetVersion()
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/C winget --version",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(psi))
                {
                    wingetVersion = process.StandardOutput.ReadToEnd().Trim();
                    AppendConsoleText($"Winget yüklü (Sürüm: {wingetVersion})");
                }
            }
            catch
            {
                wingetVersion = "Bilinmeyen";
            }
        }

        private void AskToInstallWinget()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(AskToInstallWinget));
                return;
            }

            var result = MessageBox.Show("Winget yüklü değil. Şimdi yüklemek ister misiniz?",
                                      "Winget Gerekli",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                InstallWinget();
            }
            else
            {
                AppendConsoleText("Winget yükleme iptal edildi. Uygulama sınırlı işlevsellikle çalışacak.");
            }
        }

        private async void InstallWinget()
        {
            try
            {
                SetUIState(false);
                AppendConsoleText("Winget yükleniyor...");

                string downloadUrl = "https://aka.ms/getwinget";
                string tempPath = Path.GetTempPath();
                string installerPath = Path.Combine(tempPath, "Microsoft.DesktopAppInstaller_8wekyb3d8bbwe.msixbundle");

                using (WebClient client = new WebClient())
                {
                    client.DownloadProgressChanged += (s, e) =>
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            progressBar1.Value = e.ProgressPercentage;
                        });
                    };

                    await client.DownloadFileTaskAsync(new Uri(downloadUrl), installerPath);

                    AppendConsoleText("Yükleyici başarıyla indirildi. Kurulum başlıyor...");
                    await InstallWingetPackage(installerPath);
                }
            }
            catch (Exception ex)
            {
                AppendConsoleText($"Yükleme hatası: {ex.Message}");
            }
            finally
            {
                SetUIState(true);
            }
        }

        private async Task InstallWingetPackage(string installerPath)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"Add-AppxPackage -Path '{installerPath}'\"",
                    Verb = "runas",
                    UseShellExecute = true
                };

                var process = Process.Start(psi);
                AppendConsoleText("Kurulum başlatıldı. Lütfen bekleyin...");

                await Task.Delay(5000);
                CheckWingetInstallation();
            }
            catch (Exception ex)
            {
                AppendConsoleText($"Kurulum hatası: {ex.Message}");
            }
        }

        private async Task ExecuteWingetCommand(string command)
        {
            if (!isWingetInstalled)
            {
                AppendConsoleText("Hata: Winget yüklü değil.");
                return;
            }

            try
            {
                SetUIState(false);
                AppendConsoleText($"> winget {command}");

                var psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/C winget {command}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    Verb = "runas"
                };

                using (var process = new Process { StartInfo = psi })
                {
                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            AppendConsoleText(e.Data);
                        }
                    };

                    process.ErrorDataReceived += (sender, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            AppendConsoleText("HATA: " + e.Data);
                        }
                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    await Task.Run(() => process.WaitForExit());
                }
            }
            catch (Exception ex)
            {
                AppendConsoleText($"Komut hatası: {ex.Message}");
            }
            finally
            {
                SetUIState(true);
            }
        }

        private void SetUIState(bool enabled)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<bool>(SetUIState), enabled);
                return;
            }

            btnGuncellemeleriKontrolEt.Enabled = enabled;
            btnTumunuGuncelle.Enabled = enabled;
            btnWingetYukle.Enabled = enabled;
            Cursor.Current = enabled ? Cursors.Default : Cursors.WaitCursor;
        }

        private async void btnCheckUpdates_Click(object sender, EventArgs e)
        {
            if (!isWingetInstalled)
            {
                CheckWingetInstallation();
                return;
            }

            AppendConsoleText("Güncellemeler kontrol ediliyor...");
            await ExecuteWingetCommand("upgrade");
        }

        private async void btnInstallUpdates_Click(object sender, EventArgs e)
        {
            if (!isWingetInstalled)
            {
                CheckWingetInstallation();
                return;
            }

            var result = MessageBox.Show("Tüm uygulamalar güncellenecek. Devam etmek istiyor musunuz?",
                                      "Onay",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                AppendConsoleText("Güncellemeler yükleniyor...");
                await ExecuteWingetCommand("upgrade --all");
            }
        }

        private void btnInstallWinget_Click(object sender, EventArgs e)
        {
            InstallWinget();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AppendConsoleText("=== Winget Güncelleme Aracı ===");
            AppendConsoleText("1. 'Winget Kontrol Et' - Winget kurulumunu doğrular");
            AppendConsoleText("2. 'Güncellemeleri Kontrol Et' - Kullanılabilir güncellemeleri listeler");
            AppendConsoleText("3. 'Tümünü Güncelle' - Tüm uygulamaları günceller");
        }
    }
}