using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Himesyo;
using Himesyo.ComponentModel.Design;
using Himesyo.IO;
using Himesyo.Linq;
using Himesyo.WinForm;

namespace QuickTranslation
{
    /// <summary>
    /// 用于显示翻译文件的进度、状态等。
    /// </summary>
    public partial class TranBox : UserControl
    {
        /// <summary>
        /// 关联的翻译文件。
        /// </summary>
        [Browsable(false)]
        public TranslationFile TranFile { get; }

        /// <summary>
        /// 右键单击时触发。
        /// </summary>
        public event Action<TranBox, Control, MouseEventArgs> RightClick;

        /// <summary>
        /// 使用一个翻译文件初始化新实例。
        /// </summary>
        /// <param name="file"></param>
        public TranBox(TranslationFile file)
        {
            TranFile = file;
            InitializeComponent();
            if (components == null)
            {
                components = new Container();
            }
            components.Add(thread);
            Name = TranFile.Name;
            Controls.OfType<Control>().ForEach(control =>
            {
                control.MouseClick += TranBox_MouseClick;
            });

            TranFile.ShowItemChange += (sender, e) =>
            {
                thread.SetResult(e);
            };
            lblLength.Text = string.Empty;
        }

        public void AskDelete()
        {
            MsgBox box = MsgBox.Show(
                $"您确定要删除此条记录吗？\r\n\r\n记录名称：{TranFile.Name}\r\n记录状态：{TranFile.State}\r\n文件路径：{TranFile.SourcePath}\r\n缓存文件：{TranFile.FilePath}",
                $"删除记录 {TranFile.Name}",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);
            if (box.Result == DialogResult.OK)
            {
                File.Delete(TranFile.FilePath);
                File.Delete(Path.ChangeExtension(TranFile.FilePath, "state"));
                Dispose();
            }
        }

        private async void TranBox_Load(object sender, EventArgs e)
        {
            UpdateName();
            _ = Task.Run(TranFile.Init);
            await WaitUpdate();
        }

        private async Task WaitUpdate()
        {
            while (true)
            {
                if (Disposing || IsDisposed || thread.IsDispose)
                {
                    return;
                }
                ShowItem showItem = await thread.WaitMessage();
                if (thread.IsDispose)
                {
                    return;
                }
                if (showItem.HasFlag(ShowItem.State))
                {
                    FileState state = TranFile.State;
                    var info = TranFile.GetStateInfo();
                    if (info == null)
                    {
                        lblState.Text = state.ToString();
                        lblState.ForeColor = Color.Black;
                        lblMessage.ForeColor = Color.Black;
                        lblName.ForeColor = Color.Black;
                    }
                    else
                    {
                        lblState.Text = info.ShowName ?? state.ToString();
                        lblState.ForeColor = info.ShowColor ?? Color.Black;
                        lblMessage.ForeColor = info.MessageColor ?? Color.Black;
                        lblName.ForeColor = info.ShowColor ?? Color.FromArgb(220, 0, 255);
                    }
                    if (state == FileState.Complete)
                    {
                        Dispose();
                        return;
                    }
                }
                if (showItem.HasFlag(ShowItem.StateMessage))
                {
                    lblMessage.Text = TranFile.StateMessage;
                }
                if (showItem.HasFlag(ShowItem.Name))
                {
                    lblName.Text = TranFile.Name;
                    Name = TranFile.Name.FormatEmpty();
                }
                if (showItem.HasFlag(ShowItem.FilePath))
                {
                    lblName.Tag = TranFile.SourcePath;
                    FormMain.ToolTip.SetToolTip(lblName, TranFile.SourcePath);
                }
                if (showItem.HasFlag(ShowItem.FileLength) || showItem.HasFlag(ShowItem.TextLength))
                {
                    string fileLength = new FileLengthFormatter().Format("L", TranFile.FileLength, null);
                    lblLength.Text = $"文件大小：{fileLength,10} | 字符数量：{TranFile.TextLength:N0}";
                }
                if (showItem.HasFlag(ShowItem.Progress))
                {
                    int value = TranFile.ProCompleted;
                    int max = TranFile.ProCompleted + TranFile.ProIncomplete;
                    if (max == 0)
                    {
                        probarMain.Maximum = 1;
                        probarMain.Value = 0;
                        lblPro.Text = "0/0";
                    }
                    else
                    {
                        probarMain.Maximum = max;
                        probarMain.Value = value;
                        lblPro.Text = $"{value}/{max}";
                    }
                }
            }
        }

        private void UpdateName()
        {
            lblName.Text = TranFile.Name;
            lblName.Tag = TranFile.SourcePath;
            FormMain.ToolTip.SetToolTip(lblName, TranFile.SourcePath);
        }

        private void lblName_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                FileHelper.ShowInExplorer(TranFile.SourcePath);
            }
        }

        private void btnCancel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                AskDelete();
            }
        }

        private void TranBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                RightClick?.Invoke(this, sender as Control, e);
            }
        }

    }
}
