namespace QuickTranslation
{
    public partial class TranslationFile
    {
        private partial class Member
        {
            private interface IItem
            {
                /// <summary>
                /// 获取文本长度
                /// </summary>
                /// <returns></returns>
                long GetTextLength();
                /// <summary>
                /// 写入结果
                /// </summary>
                void Write();
            }

        }
    }
}
