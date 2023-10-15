using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Himesyo.IO;
using Himesyo.Logger;

namespace QuickTranslation
{
    internal static class AppMain
    {
        public static QuickTranslationConfig Config { get; private set; }

        public static void LoadConfig()
        {
            try
            {
                Config = AppConfig.Load<QuickTranslationConfig>("QuickTranslation.config");
            }
            catch (Exception ex)
            {
                LoggerSimple.WriteError("读取配置失败。", ex);
                File.Copy("QuickTranslation.config", "QuickTranslation_error.config", true);
            }
            Config = Config ?? new QuickTranslationConfig();
            SaveConfig();
            LoggerSimple.WriteWarning("已初始化配置。");
        }
        public static void SaveConfig()
        {
            Config.Save("QuickTranslation.config");
        }

        public static void StartLoop()
        {
            Environment.CurrentDirectory = Application.StartupPath;
            LoggerSimple.Init("Logs", "QuickTranslation");
            LoadConfig();
            Application.Run(new FormMain());
        }
    }

    /// <summary>
    /// 配置文件
    /// </summary>
    public class QuickTranslationConfig : AppConfig
    {
        public FileState DefaultState { get; set; } = FileState.Wait;
        public FileState GlobalState { get; set; } = FileState.Translating;
        public bool ShowOriginal { get; set; } = false;
    }
}
