using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using Himesyo;
using Himesyo.IO;
using Himesyo.Linq;
using Himesyo.Logger;

namespace QuickTranslation
{
    /// <summary>
    /// 主窗体
    /// </summary>
    public partial class FormMain : Form
    {
        private readonly object lockCreateTask = new object();
        private Task taskTran = null;
        private TranslationFile currTranFile = null;

        /// <summary>
        /// 全局工具提示组件。
        /// </summary>
        public static ToolTip ToolTip { get; } = new ToolTip()
        {
            AutoPopDelay = 360000,
            ReshowDelay = 1
        };

        public static FileState DefaultState { get; private set; } = FileState.Wait;
        public static FileState GlobalState { get; private set; } = FileState.Translating;

        public static BaiduTranslator Translator { get; set; }

        public static string GetPaintString()
        {
            /*
              Random random = new Random(183147241);
              string output = $"\r\n  int[] result = new int[] {{ {string.Join(", ","作者：姬子夜\r\nQQ：183147241\r\nGithub ：https://github.com/Himesyo/QuickTranslation".Select(c=>((int)c) - random.Next()))} }};\r\n";
              Console.WriteLine(output);
            */
            Random random = new Random(183147241);
            int[] result = new int[] { -1392711124, -186752957, -1537803965, -871511786, -1707052567, -1650855270, -1891764010, -1439808275, -695804128, -907529536, -1814260607, -237974133, -824943589, -1396016534, -2076449589, -286393343, -1693006149, -359035737, -536002535, -490734173, -649541759, -258964132, -759631272, -2117312407, -1557169623, -753483245, -2062448972, -16612432, -1487465887, -283758387, -13731547, -395674013, -624741263, -1103555650, -356148748, -2015670147, -84533319, -1360485232, -2113918673, -501478776, -1857327539, -122535336, -903877717, -1692106094, -1202502903, -932118334, -135675883, -1881777504, -980396838, -1883096010, -1875127737, -891700451, -1843988633, -731114156, -1170617734, -1133767187, -1574637987, -1568040325, -1461848597, -953592489, -1735912541, -1875151386, -2099825929, -411980402, -893797869, -1418651674, -1760716335, -1868871422, -1039867625, -60779317, -201859924, -332520814, -392600548 };

            return new string(result.Select(c => (char)(c + random.Next())).ToArray());
        }

        /// <summary>
        /// 默认构造函数。
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
        }

        private void Filter()
        {
            string filter = txtFilter.Value.FormatNull();
            lstMain.SuspendLayout();
            if (filter == null)
            {
                foreach (Control item in lstMain.Controls)
                {
                    item.Visible = true;
                }
            }
            else
            {
                filter = filter.Trim();
                Regex regex = new Regex(Regex.Escape(filter), RegexOptions.IgnoreCase);
                foreach (Control item in lstMain.Controls)
                {
                    item.Visible = regex.IsMatch(item.Name.FormatEmpty());
                }
            }
            lstMain.ResumeLayout();
            lstMain.PerformLayout();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dialogSelectXml.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in dialogSelectXml.FileNames)
                {
                    AddBox(file);
                }
                Filter();
            }
        }

        private void AddBox(string path)
        {
            try
            {
                string fullPath = Path.GetFullPath(path);
                TranBox tranBox = lstMain.Controls
                    .OfType<TranBox>()
                    .FirstOrDefault(box => string.Equals(box.TranFile.SourcePath, fullPath, StringComparison.OrdinalIgnoreCase));
                if (tranBox == null)
                {
                    TranslationFile file = new TranslationFile(fullPath);
                    file.Analyzed += (sender) =>
                    {
                        if (DefaultState == FileState.Wait)
                        {
                            sender.StartWait();
                        }
                    };
                    file.ShowItemChange += (sender, e) =>
                    {
                        if (e.HasFlag(ShowItem.State))
                        {
                            if (sender.State == FileState.Translating
                                || sender.State == FileState.Wait)
                            {
                                RunTranslate();
                            }
                        }
                    };
                    tranBox = new TranBox(file);
                    tranBox.RightClick += tranBox_RightClick;
                    lstMain.Controls.Add(tranBox);
                }
                else
                {
                    lstMain.ScrollControlIntoView(tranBox);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加失败。{ex.Message}", "添加");
                LoggerSimple.WriteError($"添加文件失败。", ex);
            }
        }

        public static bool VerifyTranslator(bool showConfig)
        {
            var translator = Translator;
            if (translator == null
                || string.IsNullOrWhiteSpace(translator.AppID)
                || string.IsNullOrWhiteSpace(translator.SecretKey))
            {
                if (showConfig)
                {
                    FormConifg conifg = new FormConifg();
                    conifg.Translator = Translator;
                    conifg.ShowDialog();
                    Translator = conifg.Translator;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
            translator = Translator;
            if (translator == null
                || string.IsNullOrWhiteSpace(translator.AppID)
                || string.IsNullOrWhiteSpace(translator.SecretKey))
            {
                return false;
            }
            return true;
        }

        private void RunTranslate()
        {
            if (taskTran == null && GlobalState == FileState.Translating)
            {
                lock (lockCreateTask)
                {
                    if (taskTran == null && GlobalState == FileState.Translating)
                    {
                        if (VerifyTranslator(false))
                        {
                            currTranFile = GetTranslationFile();
                            if (taskTran == null && currTranFile != null)
                            {
                                taskTran = Task.Run(() =>
                                {
                                    try
                                    {
                                        while (currTranFile != null)
                                        {
                                            TranslationFile file = currTranFile;
                                            while (file != null && file.State == FileState.Translating)
                                            {
                                                Translate(file);
                                            }
                                            currTranFile = GetTranslationFile();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        LoggerSimple.WriteError("翻译任务异常退出。", ex);
                                    }
                                    finally
                                    {
                                        taskTran = null;
                                    }
                                });
                            }
                        }
                        else
                        {
                            currTranFile = null;
                        }
                    }
                }
            }
        }

        private TranslationFile GetTranslationFile()
        {
            TranslationFile curr = lstMain.Controls
                .OfType<TranBox>()
                .Select(box => box.TranFile)
                .FirstOrDefault(file => file.State == FileState.Translating);
            if (curr == null)
            {
                curr = lstMain.Controls
                    .OfType<TranBox>()
                    .Select(box => box.TranFile)
                    .FirstOrDefault(file => file.State == FileState.Wait);
                curr?.StartTran();
            }

            return curr;
        }

        private void Translate(TranslationFile file)
        {
            if (file != null)
            {
                BaiduTranslator translator = Translator;
                ITranItem item = file.NextTranItems();
                if (item != null)
                {
                    string text = item.GetSourceText();
                    translator.Wait();
                    if (translator.TryTran(text, out string[] result, out string code, out string message))
                    {
                        item.SetResult(result);
                    }
                    else
                    {
                        LoggerSimple.WriteWarning($"翻译文本失败：{code}:{message}\r\n原文：{text}");
                        // 两次机会
                        translator.Wait();
                        if (translator.TryTran(text, out result, out code, out message))
                        {
                            item.SetResult(result);
                        }
                        else
                        {
                            LoggerSimple.WriteWarning($"第二次翻译文本失败：{code}:{message}\r\n原文：{text}");
                            item.SetError($"{code}:{message}");
                        }
                    }
                }
            }
        }

        private void tranBox_RightClick(TranBox tranBox, Control sender, MouseEventArgs e)
        {
            TranslationFile file = tranBox.TranFile;
            menuItemAdd.Enabled = file.State == FileState.Pause;
            menuItemStart.Enabled = file.State == FileState.Pause || file.State == FileState.Wait;
            menuItemPause.Enabled = file.State == FileState.Wait || file.State == FileState.Translating;
            menuItemRetryError.Enabled = file.State == FileState.Translated;
            menuItemMarkComplete.Enabled = file.State == FileState.Translated;
            menuManager.Tag = tranBox;
            menuManager.Show(sender, e.Location);
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            DefaultState = AppMain.Config.DefaultState;
            GlobalState = AppMain.Config.GlobalState;

            if (Directory.Exists(TranslationFile.TranslatingPath))
            {
                lstMain.SuspendLayout();
                foreach (var item in Directory.EnumerateFiles(TranslationFile.TranslatingPath, "*.file"))
                {
                    AddBox(item);
                }
                lstMain.ResumeLayout();
                Filter();
            }
            BaiduTranslator translator;
            try
            {
                translator = BaiduTranslator.Load("baidu.translator", string.Empty);
                while (string.IsNullOrWhiteSpace(translator.Password))
                {
                    // 设有密码
                    FormPassword password = new FormPassword();
                    password.Caption = $"请输入翻译器的密码：\r\n\r\nAppid : {translator.AppID}";
                    password.Password = string.Empty;
                    if (password.ShowDialog() == DialogResult.OK)
                    {
                        translator = BaiduTranslator.Load("baidu.translator", password.Password);
                    }
                    else
                    {
                        translator = new BaiduTranslator();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                // 文件损坏或被篡改
                translator = new BaiduTranslator();
                LoggerSimple.WriteError($"未能读取帐号密钥。", ex);
            }
            Translator = translator;
            if (VerifyTranslator(true))
            {
                RunTranslate();
            }
            else
            {
                // 未设置翻译器
                GlobalState = FileState.Pause;
            }
        }

        private void txtFilter_ValueChange(object sender, EventArgs e)
        {
            Filter();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            RunTranslate();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            FormConifg conifg = new FormConifg();
            conifg.Translator = Translator;
            conifg.ShowDialog();
            Translator = conifg.Translator;
        }

        private void menuItemAdd_Click(object sender, EventArgs e)
        {
            if (menuManager.Tag is TranBox tranBox)
            {
                TranslationFile file = tranBox.TranFile;
                file.StartWait();
                if (VerifyTranslator(true))
                {
                    GlobalState = FileState.Translating;
                    RunTranslate();
                }
            }
        }

        private void menuItemStart_Click(object sender, EventArgs e)
        {
            if (menuManager.Tag is TranBox tranBox)
            {
                lstMain.Controls
                    .OfType<TranBox>()
                    .ForEach(box =>
                    {
                        if (box.TranFile.State == FileState.Translating)
                        {
                            box.TranFile.StartWait();
                        }
                    });
                TranslationFile file = tranBox.TranFile;
                file.StartTran();
                currTranFile = file;
                if (VerifyTranslator(true))
                {
                    GlobalState = FileState.Translating;
                    RunTranslate();
                }
            }
        }

        private void menuItemPause_Click(object sender, EventArgs e)
        {
            if (menuManager.Tag is TranBox tranBox)
            {
                TranslationFile file = tranBox.TranFile;
                file.Pause();
            }
        }

        private void menuItemDelete_Click(object sender, EventArgs e)
        {
            if (menuManager.Tag is TranBox tranBox)
            {
                tranBox.AskDelete();
            }
        }

        private void menuItemAllAdd_Click(object sender, EventArgs e)
        {
            lstMain.Controls
                .OfType<TranBox>()
                .ForEach(box => box.TranFile.StartWait());
            if (VerifyTranslator(true))
            {
                GlobalState = FileState.Translating;
                RunTranslate();
            }
        }

        private void menuItemAllPause_Click(object sender, EventArgs e)
        {
            GlobalState = FileState.Pause;
            lstMain.Controls
                .OfType<TranBox>()
                .ForEach(box => box.TranFile.Pause());
        }

        private void menuItemRetryError_Click(object sender, EventArgs e)
        {
            if (menuManager.Tag is TranBox tranBox)
            {
                TranslationFile file = tranBox.TranFile;
                file.TranError();
            }
        }

        private void menuItemMarkComplete_Click(object sender, EventArgs e)
        {
            if (menuManager.Tag is TranBox tranBox)
            {
                TranslationFile file = tranBox.TranFile;
                file.MarkComplete();
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void btnOpenOut_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(TranslationFile.TranslatedPath);
            FileHelper.Open(TranslationFile.TranslatedPath);
        }

        private void btnStartTran_Click(object sender, EventArgs e)
        {
            if (VerifyTranslator(true))
            {
                GlobalState = FileState.Translating;
                RunTranslate();
            }
        }
    }
}
