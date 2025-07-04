using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        private void CheckWingetInstallation()
        {
            txtConsoleOutput.AppendText("Winget kurulum durumu kontrol ediliyor...\n");

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

                // Winget bulunamadı
                isWingetInstalled = false;
                txtConsoleOutput.AppendText("Winget yüklü değil.\n");
                AskToInstallWinget();
            }
            catch (Exception ex)
            {
                txtConsoleOutput.AppendText($"Kontrol hatası: {ex.Message}\n");
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
                    txtConsoleOutput.AppendText($"Winget yüklü (Sürüm: {wingetVersion})\n\n");
                }
            }
            catch
            {
                wingetVersion = "Bilinmeyen";
            }
        }

        private void AskToInstallWinget()
        {
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
                txtConsoleOutput.AppendText("Winget yükleme iptal edildi. Uygulama sınırlı işlevsellikle çalışacak.\n");
            }
        }

        private void InstallWinget()
        {
            try
            {
                txtConsoleOutput.AppendText("Winget yükleniyor...\n");

                // GitHub'dan en son Winget release'ini indir
                string downloadUrl = "https://aka.ms/getwinget";
                string tempPath = Path.GetTempPath();
                string installerPath = Path.Combine(tempPath, "Microsoft.DesktopAppInstaller_8wekyb3d8bbwe.msixbundle");

                // Dosyayı indir
                using (WebClient client = new WebClient())
                {
                    client.DownloadProgressChanged += (s, e) =>
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            progressBar1.Value = e.ProgressPercentage;
                        });
                    };

                    client.DownloadFileCompleted += (s, e) =>
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            if (e.Error != null)
                            {
                                txtConsoleOutput.AppendText($"İndirme hatası: {e.Error.Message}\n");
                                return;
                            }

                            txtConsoleOutput.AppendText("Yükleyici başarıyla indirildi. Kurulum başlıyor...\n");
                            InstallWingetPackage(installerPath);
                        });
                    };

                    txtConsoleOutput.AppendText("Winget yükleyici indiriliyor...\n");
                    client.DownloadFileAsync(new Uri(downloadUrl), installerPath);
                }
            }
            catch (Exception ex)
            {
                txtConsoleOutput.AppendText($"Yükleme hatası: {ex.Message}\n");
            }
        }

        private void InstallWingetPackage(string installerPath)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"Add-AppxPackage -Path '{installerPath}'\"",
                    Verb = "runas", // Yönetici olarak çalıştır
                    UseShellExecute = true
                };

                Process.Start(psi);
                txtConsoleOutput.AppendText("Kurulum başlatıldı. Lütfen bekleyin...\n");

                // Kurulumun bitmesini bekle ve kontrol et
                System.Timers.Timer timer = new System.Timers.Timer(5000);
                timer.Elapsed += (s, e) =>
                {
                    timer.Stop();
                    this.Invoke((MethodInvoker)delegate
                    {
                        CheckWingetInstallation();
                    });
                };
                timer.Start();
            }
            catch (Exception ex)
            {
                txtConsoleOutput.AppendText($"Kurulum hatası: {ex.Message}\n");
            }
        }

        private void ExecuteWingetCommand(string command)
        {
            if (!isWingetInstalled)
            {
                txtConsoleOutput.AppendText("Hata: Winget yüklü değil.\n");
                return;
            }

            txtConsoleOutput.AppendText($"> {command}\n");

            var psi = new ProcessStartInfo
            {
                FileName = "winget.exe",
                Arguments = command,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = false,
                Verb = "runas"
            };

            try
            {
                using (var process = new Process { StartInfo = psi })
                {
                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                txtConsoleOutput.AppendText(e.Data + "\n");
                                txtConsoleOutput.ScrollToCaret();
                            });
                        }
                    };

                    process.ErrorDataReceived += (sender, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                txtConsoleOutput.AppendText("HATA: " + e.Data + "\n");
                                txtConsoleOutput.ScrollToCaret();
                            });
                        }
                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                txtConsoleOutput.AppendText($"Komut hatası: {ex.Message}\n");
            }
        }

        private void btnCheckUpdates_Click(object sender, EventArgs e)
        {
            if (!isWingetInstalled)
            {
                CheckWingetInstallation();
                return;
            }

            txtConsoleOutput.AppendText("Güncellemeler kontrol ediliyor...\n");
            ExecuteWingetCommand("upgrade");
        }

        private void btnInstallUpdates_Click(object sender, EventArgs e)
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
                txtConsoleOutput.AppendText("Güncellemeler yükleniyor...\n");
                ExecuteWingetCommand("upgrade --all");
            }
        }

        private void btnInstallWinget_Click(object sender, EventArgs e)
        {
            InstallWinget();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtConsoleOutput.AppendText("=== Winget Güncelleme Aracı ===\n");
            txtConsoleOutput.AppendText("1. 'Winget Kontrol Et' - Winget kurulumunu doğrular\n");
            txtConsoleOutput.AppendText("2. 'Güncellemeleri Kontrol Et' - Kullanılabilir güncellemeleri listeler\n");
            txtConsoleOutput.AppendText("3. 'Tümünü Güncelle' - Tüm uygulamaları günceller\n\n");
        }
    }
}