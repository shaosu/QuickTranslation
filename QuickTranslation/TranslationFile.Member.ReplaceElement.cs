using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace QuickTranslation
{
    public partial class TranslationFile
    {
        private partial class Member
        {
            [DebuggerDisplay("ReplaceElement [ ElementName = {Target?.Name}, Count = {Items.Count} ]")]
            private class ReplaceElement : IItem, IEnumerable<ReplaceText>
            {
                public XElement Source { get; set; }
                public XElement Target { get; set; }

                public bool Original { get; set; }

                public List<IItem> Items { get; } = new List<IItem>();

                public long GetTextLength()
                {
                    return Items.Sum(item => item.GetTextLength());
                }

                public void Write()
                {
                    foreach (var item in Items)
                    {
                        item.Write();
                    }
                    if (Source != null && Source != Target)
                    {
                        if (Original)
                        {
                            XElement original = new XElement("para");
                            original.Add("原文：");
                            original.Add(Source.Nodes().ToArray());
                            if (Target == null)
                            {
                                Target = original;
                            }
                            else
                            {
                                Target.Add(original);
                            }
                        }
                        Source.ReplaceWith(Target);
                    }
                }

                public IEnumerator<ReplaceText> GetEnumerator()
                {
                    foreach (var item in Items)
                    {
                        if (item is ReplaceElement element)
                        {
                            foreach (var text in element)
                            {
                                yield return text;
                            }
                        }
                        else if (item is ReplaceText text)
                        {
                            yield return text;
                        }
                    }
                }

                IEnumerator IEnumerable.GetEnumerator()
                {
                    return GetEnumerator();
                }
            }

        }
    }
}
