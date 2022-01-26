using System;

namespace QuickTranslation
{
    public partial class TranslationFile
    {
        [Flags]
        public enum HandleType
        {
            Insert = 0x0001,
            Source = 0x0002,
            /// <summary>
            /// 单独处理
            /// </summary>
            Separate = 0x0004,
            /// <summary>
            /// 首尾保持一个空格
            /// </summary>
            Space = 0x0100,
            LeftSpace = 0x0200,
            RightSpace = 0x0400,
            /// <summary>
            /// 使用原本的空格
            /// </summary>
            SourceSpace = 0x800,
            /// <summary>
            /// 开始于新行。如果含有 <see cref="Paragraph"/> ，则在含有文本的情况下视为段落。
            /// </summary>
            StartLine = 0x1000,
            /// <summary>
            /// 后跟新行。如果含有 <see cref="Paragraph"/> ，则在含有文本的情况下视为段落。
            /// </summary>
            EndLine = 0x2000,
            /// <summary>
            /// 段落。如果含有 <see cref="StartLine"/> 或者 <see cref="EndLine"/>，则在含有文本的情况下视为段落。
            /// </summary>
            Paragraph = 0x4000
        }
    }
}
