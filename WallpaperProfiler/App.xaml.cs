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

    private Forms.NotifyIcon notifyIcon;
    private MainWindow mainWindow = null;
    private Forms.ToolStripMenuItem profilesItem = null;

    public App() {
      profilesItem = new Forms.ToolStripMenuItem("Profiles...", WallpaperProfiler.Properties.Resources.users);

      notifyIcon = new Forms.NotifyIcon();
      notifyIcon.MouseDoubleClick += NotifyIcon_DoubleClick;
      notifyIcon.Icon = WallpaperProfiler.Properties.Resources.icon_small;
      notifyIcon.Visible = true;
      notifyIcon.Text = "Wallpaper Profiler";
      notifyIcon.ContextMenuStrip = new Forms.ContextMenuStrip();
      notifyIcon.ContextMenuStrip.Items.Add("Show", WallpaperProfiler.Properties.Resources.application_blue, ShowItem_Click);
      notifyIcon.ContextMenuStrip.Items.Add(profilesItem);
      notifyIcon.ContextMenuStrip.Items.Add(new Forms.ToolStripSeparator());
      notifyIcon.ContextMenuStrip.Items.Add("Exit", WallpaperProfiler.Properties.Resources.cross, ExitItem_Click);
    }

    private void NotifyIcon_DoubleClick(object sender, Forms.MouseEventArgs e) {
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
      if (mainWindow.WindowState == WindowState.Minimized) {
        mainWindow.WindowState = WindowState.Normal;
      }
      mainWindow.Activate();
    }

    private void MainWindow_Closed(object sender, EventArgs e) {
      mainWindow = null;
    }

    protected override void OnStartup(StartupEventArgs e) {
      notifyIcon.Visible = true;
      base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e) {
      notifyIcon.Visible = false;
      notifyIcon.Dispose();
      notifyIcon = null;
      base.OnExit(e);
    }

  }
}
