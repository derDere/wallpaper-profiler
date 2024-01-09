using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Forms = System.Windows.Forms;

namespace WallpaperProfiler {
  /// <summary>
  /// Interaktionslogik für "App.xaml"
  /// </summary>
  public partial class App : Application {

    private Forms.NotifyIcon _notifyIcon;
    private MainWindow mainWindow = null;

    public App() {
      _notifyIcon = new Forms.NotifyIcon();
      _notifyIcon.MouseClick += NotifyIcon_Click;
      //_notifyIcon.Icon = WallpaperProfiler.Properties.Resources.AppIcon;
      _notifyIcon.Visible = true;
      _notifyIcon.Text = "Wallpaper Profiler";
      _notifyIcon.ContextMenuStrip = new Forms.ContextMenuStrip();
      _notifyIcon.ContextMenuStrip.Items.Add("Show", null, ShowItem_Click);
      _notifyIcon.ContextMenuStrip.Items.Add("Exit", null, ExitItem_Click);
    }

    private void NotifyIcon_Click(object sender, Forms.MouseEventArgs e) {
      if (e.Button == Forms.MouseButtons.Left) {
        ShowWindow();
      }
    }

    private void ShowItem_Click(object sender, EventArgs e) {
      ShowWindow();
    }

    private void ExitItem_Click(object sender, EventArgs e) {
      Current.Shutdown();
    }

    private void ShowWindow() {
      if (mainWindow == null) {
        mainWindow = new MainWindow();
        mainWindow.Closed += MainWindow_Closed;
      }
      mainWindow.Show();
      mainWindow.Activate();
    }

    private void MainWindow_Closed(object sender, EventArgs e) {
      mainWindow = null;
    }

    protected override void OnStartup(StartupEventArgs e) {
      _notifyIcon.Visible = true;
      base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e) {
      _notifyIcon.Visible = false;
      _notifyIcon.Dispose();
      _notifyIcon = null;
      base.OnExit(e);
    }

  }
}
