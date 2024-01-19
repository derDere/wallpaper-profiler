using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IO = System.IO;
using System.Windows;
using Drawing = System.Drawing;
using System.Diagnostics;
using System.Drawing;

namespace WallpaperProfiler.Klassen {
  public class Config {

    #region Initialisation
    private static Config myself;

    private static string FilePath() {
      string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      path += "\\WallpaperProfiler\\";
      if (!System.IO.Directory.Exists(path)) {
        System.IO.Directory.CreateDirectory(path);
      }
      path += "config.json";
      return path;
    }

    public static void Save() {
      if (myself == null) {
        myself = new Config();
      }
      string jj = Newtonsoft.Json.JsonConvert.SerializeObject(myself, Newtonsoft.Json.Formatting.Indented);
      try {
        IO.File.WriteAllText(FilePath(), jj);
      }
      catch (Exception ex) {
        MessageBox.Show($"Error while saving configuration:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    public static void Reload() {
      string path = FilePath();
      if (!IO.File.Exists(path)) {
        Save();
      }
      else {
        try {
          string jj = IO.File.ReadAllText(path);
          myself = Newtonsoft.Json.JsonConvert.DeserializeObject<Config>(jj);
        }
        catch (Exception ex) {
          MessageBox.Show($"Error while loading configuration:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
      }
    }

    private Config() {
      Reload();
    }
    #endregion

    #region Definitions
    public enum WallpaperStyle {
      none = 0,

      center = 1,
      tile = 2,
      stretch = 4,
      fit = 8,
      fill = 16
    }

    public enum ScreenType {
      none = 0,

      p_4x3 = 1,
      p_3x4 = 2,
      p_16x9 = 4,
      p_9x16 = 8,
      p_16x10 = 16,
      p_10x16 = 32,
      p_21x9 = 64,
      p_9x21 = 128,
      p_21x10 = 256,
      p_10x21 = 512,
      p_32x9 = 1024,
      p_9x32 = 2048,
      p_32x10 = 4096,
      p_10x32 = 8192,

      p_old_h = p_4x3,
      p_old_v = p_3x4,
      p_wide_h = p_16x9 | p_16x10,
      p_wide_v = p_9x16 | p_10x16,
      p_extra_wide_h = p_21x9 | p_21x10,
      p_extra_wide_v = p_9x21 | p_10x21,
      p_ultra_wide_h = p_32x9 | p_32x10,
      p_ultra_wide_v = p_9x32 | p_10x32,
      p_vertical = p_3x4 | p_9x16 | p_10x16 | p_9x21 | p_10x21 | p_9x32 | p_10x32,
      p_horizontal = p_4x3 | p_16x9 | p_16x10 | p_21x9 | p_21x10 | p_32x9 | p_32x10,
      p_all = p_4x3 | p_3x4 | p_16x9 | p_9x16 | p_16x10 | p_10x16 | p_21x9 | p_9x21 | p_21x10 | p_10x21 | p_32x9 | p_9x32 | p_32x10 | p_10x32
    }

    public class Info<T> {
      public T Value { get; set; }
      public string DisplayName { get; set; }
      public string Tooltip { get; set; }
      public Info(string displayName, string tooltip, T value) {
        DisplayName = displayName;
        Tooltip = tooltip;
        Value = value;
      }
    }

    public static readonly IReadOnlyDictionary<WallpaperStyle, Info<WallpaperStyle>> WallpaperStyles = new Dictionary<WallpaperStyle, Info<WallpaperStyle>>() {
      { WallpaperStyle.none, new Info<WallpaperStyle>("None", "There will be no Wallpaper shown.", WallpaperStyle.none) },
      { WallpaperStyle.center, new Info<WallpaperStyle>("Center", "The Wallpaper will be centered in the screen with no change in size. (Other than the scale.)", WallpaperStyle.center) },
      { WallpaperStyle.tile, new Info<WallpaperStyle>("Tile", "The Wallpaper will be tiled to fill the screen. (After being scaled.)", WallpaperStyle.tile) },
      { WallpaperStyle.stretch, new Info<WallpaperStyle>("Stretch", "The Wallpaper will be stretched to fill the screen, changing the aspect ratio. (Ignoring the scaled.)", WallpaperStyle.stretch) },
      { WallpaperStyle.fit, new Info<WallpaperStyle>("Fit", "The Wallpaper will be scaled to fit the screen, with gaps. (Without changing the aspect ratio.)", WallpaperStyle.fit) },
      { WallpaperStyle.fill, new Info<WallpaperStyle>("Fill", "The Wallpaper will be scaled to fill the screen, without gaps. (Without changing the aspect ratio.)", WallpaperStyle.fill) }
    };

    public static readonly IReadOnlyDictionary<ScreenType, Info<ScreenType>> ScreenTypes = new Dictionary<ScreenType, Info<ScreenType>>() {
      { ScreenType.none, new Info<ScreenType>("None", "Won't match any screen!", ScreenType.none) },
      { ScreenType.p_4x3, new Info<ScreenType>("4:3", "Will match any 4:3 screen.", ScreenType.p_4x3) },
      { ScreenType.p_3x4, new Info<ScreenType>("3:4", "Will match any 3:4 screen.", ScreenType.p_3x4) },
      { ScreenType.p_16x9, new Info<ScreenType>("16:9", "Will match any 16:9 screen.", ScreenType.p_16x9) },
      { ScreenType.p_9x16, new Info<ScreenType>("9:16", "Will match any 9:16 screen.", ScreenType.p_9x16) },
      { ScreenType.p_16x10, new Info<ScreenType>("16:10", "Will match any 16:10 screen.", ScreenType.p_16x10) },
      { ScreenType.p_10x16, new Info<ScreenType>("10:16", "Will match any 10:16 screen.", ScreenType.p_10x16) },
      { ScreenType.p_21x9, new Info<ScreenType>("21:9", "Will match any 21:9 screen.", ScreenType.p_21x9) },
      { ScreenType.p_9x21, new Info<ScreenType>("9:21", "Will match any 9:21 screen.", ScreenType.p_9x21) },
      { ScreenType.p_21x10, new Info<ScreenType>("21:10", "Will match any 21:10 screen.", ScreenType.p_21x10) },
      { ScreenType.p_10x21, new Info<ScreenType>("10:21", "Will match any 10:21 screen.", ScreenType.p_10x21) },
      { ScreenType.p_32x9, new Info<ScreenType>("32:9", "Will match any 32:9 screen.", ScreenType.p_32x9) },
      { ScreenType.p_9x32, new Info<ScreenType>("9:32", "Will match any 9:32 screen.", ScreenType.p_9x32) },
      { ScreenType.p_32x10, new Info<ScreenType>("32:10", "Will match any 32:10 screen.", ScreenType.p_32x10) },
      { ScreenType.p_10x32, new Info<ScreenType>("10:32", "Will match any 10:32 screen.", ScreenType.p_10x32) },
      { ScreenType.p_old_h, new Info<ScreenType>("Old Horizontal", "Will match any old horizontal screen. (4:3)", ScreenType.p_old_h) },
      { ScreenType.p_old_v, new Info<ScreenType>("Old Vertical", "Will match any old vertical screen. (3:4)", ScreenType.p_old_v) },
      { ScreenType.p_wide_h, new Info<ScreenType>("Wide Horizontal", "Will match any wide horizontal screen. (16:9, 16:10)", ScreenType.p_wide_h) },
      { ScreenType.p_wide_v, new Info<ScreenType>("Wide Vertical", "Will match any wide vertical screen. (9:16, 10:16)", ScreenType.p_wide_v) },
      { ScreenType.p_extra_wide_h, new Info<ScreenType>("Extra Wide Horizontal", "Will match any extra wide horizontal screen. (21:9, 21:10)", ScreenType.p_extra_wide_h) },
      { ScreenType.p_extra_wide_v, new Info<ScreenType>("Extra Wide Vertical", "Will match any extra wide vertical screen. (9:21, 10:21)", ScreenType.p_extra_wide_v) },
      { ScreenType.p_ultra_wide_h, new Info<ScreenType>("Ultra Wide Horizontal", "Will match any ultra wide horizontal screen. (32:9, 32:10)", ScreenType.p_ultra_wide_h) },
      { ScreenType.p_ultra_wide_v, new Info<ScreenType>("Ultra Wide Vertical", "Will match any ultra wide vertical screen. (9:32, 10:32)", ScreenType.p_ultra_wide_v) },
      { ScreenType.p_vertical, new Info<ScreenType>("Vertical", "Will match any vertical screen. (3:4, 9:16, 10:16, 9:21, 10:21, 9:32, 10:32)", ScreenType.p_vertical) },
      { ScreenType.p_horizontal, new Info<ScreenType>("Horizontal", "Will match any horizontal screen. (4:3, 16:9, 16:10, 21:9, 21:10, 32:9, 32:10", ScreenType.p_horizontal)},
      { ScreenType.p_all, new Info<ScreenType>("All", "Will match any screen.", ScreenType.p_all) }
    };

    public class Position {
      public VerticalAlignment VAlign { get; set; } = VerticalAlignment.Center;
      public HorizontalAlignment HAlign { get; set; } = HorizontalAlignment.Center;
      public Drawing.PointF Origin = new Drawing.PointF(0.5f, 0.5f);
      public Drawing.Point Offset = new Drawing.Point(0, 0);
      public Position() { }
    }

    public class PlacedText {
      public string Text { get; set; } = "Wallpaper Profiler";
      public Drawing.Color Color { get; set; } = Drawing.Color.White;
      public Drawing.Font Font { get; set; } = new Drawing.Font("Arial", 22);
      public Position Position { get; set; } = new Position();
      public PlacedText() { }
    }

    public class ScreenProfile {
      public ScreenType Type { get; set; } = ScreenType.p_all;
      public WallpaperStyle Style { get; set; } = WallpaperStyle.center;
      public string ImagePath { get; set; } = "::::<_DEFAULT_>::::";
      public decimal Scale { get; set; } = 1;
      public List<PlacedText> Texts { get; set; } = new List<PlacedText>();
      public bool Enabled { get; set; } = true;
      public ScreenProfile() { }
    }

    public class Profile {
      public Guid id { get; set; } = Guid.NewGuid();
      public string Name { get; set; } = "Unnamed Profile";
      public List<ScreenProfile> Screens { get; set; } = new List<ScreenProfile>();
      public Drawing.Color BackgroundColor { get; set; } = Drawing.Color.Black;
      public Profile() { }
    }
    #endregion

    #region Properties
    public const string DEFAULT_PROFILE_NAME = "Default Profile";
    public const string DEFAULT_ID = "01234567-89AB-CDEF-FEDC-BA9876543210";
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    [Newtonsoft.Json.JsonProperty("Profiles")]
    private Dictionary<Guid, Profile> _profiles = new Dictionary<Guid, Profile>() {
      {
        new Guid(DEFAULT_ID),
        new Profile() {
          Name = DEFAULT_PROFILE_NAME,
          Screens = new List<ScreenProfile>() {
            new ScreenProfile() {
              Texts = new List<PlacedText>() {
                new PlacedText() {
                  Position = new Position() {
                    VAlign = VerticalAlignment.Top,
                    HAlign = HorizontalAlignment.Left,
                    Origin = new Drawing.PointF(0.5f, 0.5f),
                    Offset = new Drawing.Point(80, 150)
                  }
                }
              },
            }
          },
        }
      }
    };
    public static Dictionary<Guid, Profile> Profiles {
      get {
        return myself._profiles;
      }
      set {
        myself._profiles = value;
      }
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    [Newtonsoft.Json.JsonProperty("CurrentProfile")]
    private Guid _currentProfile = Guid.Empty;
    public static Guid CurrentProfile {
      get {
        return myself._currentProfile;
      }
      set {
        myself._currentProfile = value;
      }
    }
    #endregion
  }
}
