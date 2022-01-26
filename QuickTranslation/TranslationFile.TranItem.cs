using System.Collections.Generic;
using System.Linq;

namespace QuickTranslation
{
    public partial class TranslationFile
    {
        private class TranItem : ITranItem
        {
            public TranslationFile File { get; }
            public List<Member> Members { get; } = new List<Member>();
            public List<ITranItem> Items { get; } = new List<ITranItem>();

            public TranItem(TranslationFile file)
            {
                File = file;
            }

            public string GetSourceText()
            {
                IEnumerable<string> items = Items.Select(item => item.GetSourceText());
                string text = string.Join("\n", items);
                return text;
            }

            public void SetResult(string[] result)
            {
                if (result.Length == Items.Count)
                {
                    for (int i = 0; i < result.Length; i++)
                    {
                        Items[i].SetResult(new[] { result[i] });
                    }
                    lock (File.lockUpdateFile)
                    {
                        for (int i = 0; i < Members.Count; i++)
                        {
                            Members[i].State = TranState.Complete;
                            Members[i].Write();
                            File.untranslatedMembers.Remove(Members[i]);
                            File.ProCompleted++;
                        }
                        File.upTranItem = null;
                        File.ProIncomplete = File.untranslatedMembers.Count;
                        File.ShowItemChange?.Invoke(File, ShowItem.Progress);
                    }
                    lock (File.document)
                    {
                        File.document.Save(File.FilePath);
                    }
                }
                else
                {
                    SetError("结果文本数量与输入数量不一致。");
                }
            }

            public void SetError(string message)
            {
                lock (File.lockUpdateFile)
                {
                    for (int i = 0; i < Members.Count; i++)
                    {
                        Members[i].State = TranState.Error;
                        Members[i].StateMessage = message;
                        File.untranslatedMembers.Remove(Members[i]);
                        File.ProCompleted++;
                    }
                    File.wrongMembers.AddRange(Members);
                    File.upTranItem = null;
                    File.ShowItemChange?.Invoke(File, ShowItem.Progress);
                }
                lock (File.document)
                {
                    File.document.Save(File.FilePath);
                }
            }
        }
    }
}
