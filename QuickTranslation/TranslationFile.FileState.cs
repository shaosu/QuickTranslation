using System;
using System.Drawing;

using Himesyo;

namespace QuickTranslation
{
    public enum FileState
    {
        [StateInfo("准备初始化")]
        New,
        [StateInfo("正在初始化")]
        Init,
        [StateInfo("正在分析文件")]
        Analyzing,
        [StateInfo("暂停")]
        Pause,
        [StateInfo("等待中")]
        Wait,
        [StateInfo("翻译中")]
        Translating,
        [StateInfo("翻译完成")]
        Translated,
        [StateInfo("完成")]
        Complete,
        [StateInfo("错误", "Red", "Red")]
        Error
    }

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class StateInfoAttribute : Attribute
    {
        public string Name { get; set; }
        public int? Value { get; set; }
        public string ShowName { get; set; }
        public string ShowColor { get; set; }
        public string MessageColor { get; set; }
        public StateInfoAttribute(string showName)
        {
            ShowName = showName;
        }
        public StateInfoAttribute(string showName, string showColor, string messageColor)
        {
            ShowName = showName;
            ShowColor = showColor;
            MessageColor = messageColor;
        }

        /// <summary>
        /// 将特性值转化为易使用的 <see cref="StateInfo"/> 对象。
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidCastException">颜色字符串格式无效。</exception>
        public StateInfo ToStateInfo()
        {
            StateInfo stateInfo = new StateInfo()
            {
                Name = Name.FormatNull(),
                Value = Value,
                ShowName = ShowName.FormatNull()
            };
            try
            {
                if (!string.IsNullOrWhiteSpace(ShowColor))
                {
                    stateInfo.ShowColor = (Color)new ColorConverter().ConvertFromString(ShowColor);
                }
                if (!string.IsNullOrWhiteSpace(MessageColor))
                {
                    stateInfo.MessageColor = (Color)new ColorConverter().ConvertFromString(MessageColor);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidCastException("字符串格式无效，无法转换至 System.Color 类型。", ex);
            }
            return stateInfo;
        }
    }

    public class StateInfo
    {
        public string Name { get; set; }
        public int? Value { get; set; }
        public string ShowName { get; set; }
        public Color? ShowColor { get; set; }
        public Color? MessageColor { get; set; }
    }
}
