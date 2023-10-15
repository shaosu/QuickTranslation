using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

using Himesyo;
using Himesyo.Logger;

using static System.Net.Mime.MediaTypeNames;

namespace QuickTranslation
{
    public partial class TranslationFile
    {
        [DebuggerDisplay("Member [ State = {analyzeResult == null ? \"NotAnalyzed\" : \"Analyzed\"}, Name = {Name} ]")]
        private partial class Member
        {
            private static readonly Regex regex = new Regex(@"\s*\n\s*");

            private readonly Dictionary<string, HandleType> elementHandle;

            private ReplaceElement analyzeResult;

            public static XName TranStateName { get; } = XNamespace.Get(TranNamespace).GetName("state");

            public XElement Element { get; }

            public string Name
            {
                get
                {
                    return Element.Attribute("name")?.Value;
                }
            }

            public TranState State
            {
                get
                {
                    string state = Element.Attribute(TranStateName)?.Value;
                    Enum.TryParse(state, true, out TranState result);
                    return result;
                }
                set
                {
                    Element.SetAttributeValue(TranStateName, value);
                }
            }

            public string StateMessage { get; set; }

            public Member(XElement element)
            {
                Element = element ?? throw new ArgumentNullException(nameof(element));
                elementHandle = elementHandleBase;
            }

            public Member(XElement element, Dictionary<string, HandleType> handle)
            {
                Element = element ?? throw new ArgumentNullException(nameof(element));
                elementHandle = handle ?? elementHandleBase;
            }

            public long GetTextLength() => (analyzeResult?.GetTextLength()).GetValueOrDefault();

            public void Analyze()
            {
                ReplaceElement result = new ReplaceElement();
                result.Target = Element;
                foreach (XElement item in Element.Elements())
                {
                    if (item.HasElements)
                    {
                        ReplaceElement replaceElement = new ReplaceElement()
                        {
                            Source = item,
                            Target = new XElement(item),
                            Original = AppMain.Config.ShowOriginal
                        };
                        Analyze(replaceElement);
                        result.Items.Add(replaceElement);
                    }
                    else if (!string.IsNullOrWhiteSpace(item.Value))
                    {
                        ReplaceText replaceText = new ReplaceText();
                        replaceText.Source.Add(item);
                        replaceText.Type = TranType.ElementContent;
                        replaceText.Text = ToText(item.Value);
                        replaceText.Original = AppMain.Config.ShowOriginal;
                        result.Items.Add(replaceText);
                    }
                }
                analyzeResult = result;

            }

            public IEnumerable<ITranItem> EnumerableTranItems()
            {
                if (analyzeResult != null)
                {
                    foreach (var item in analyzeResult)
                    {
                        yield return item;
                    }
                }
            }

            public void Write()
            {
                analyzeResult?.Write();
            }

            private bool Analyze(ReplaceElement root)
            {
                XElement element = root.Target;
                ReplaceText replaceText = new ReplaceText();
                StringBuilder text = new StringBuilder();
                bool hasText = false;
                foreach (XNode item in element.Nodes())
                {
                    if (item is XText txt && !string.IsNullOrWhiteSpace(txt.Value))
                    {
                        replaceText.Source.Add(item);
                        text.Append(txt.Value);
                        text.SureEndString(" ");
                        hasText = true;
                    }
                    else if (item is XCData cdata && !string.IsNullOrWhiteSpace(cdata.Value))
                    {
                        replaceText.Source.Add(item);
                        text.Append(cdata.Value);
                        text.SureEndString(" ");
                        hasText = true;
                    }
                    else if (item is XElement ele)
                    {
                        string name = ele.Name.LocalName;
                        if (elementHandle.TryGetValue(name, out HandleType handle))
                        {
                            if (handle.HasFlag(HandleType.Separate))
                            {
                                ClearText();
                                ReplaceElement replaceElement = new ReplaceElement();
                                replaceElement.Target = ele;
                                Analyze(replaceElement);
                                if (replaceElement.Items.Count > 0)
                                {
                                    root.Items.Add(replaceElement);
                                }
                            }
                            else if (handle.HasFlag(HandleType.Source))
                            {
                                ClearText();
                            }
                            else if (handle.HasFlag(HandleType.Insert))
                            {
                                string placeholder = replaceText.AddPlaceholder(ele, handle);
                                replaceText.Source.Add(item);
                                text.Append(placeholder);
                                text.SureEndString(" ");
                            }
                        }
                        else
                        {
                            ClearText();
                            StateMessage = $"未知的元素名称 '{name}'。";
                            LoggerSimple.WriteWarning(StateMessage);
                        }
                    }
                }
                ClearText();
                return true;

                void ClearText()
                {
                    if (hasText)
                    {
                        replaceText.Text = ToText(text.ToString());
                        root.Items.Add(replaceText);
                        hasText = false;
                    }
                    replaceText = new ReplaceText();
                    text.Clear();
                }
            }

            private string ToText(string sourceText)
            {
                if (string.IsNullOrWhiteSpace(sourceText))
                {
                    return string.Empty;
                }
                return regex.Replace(sourceText.Trim(), " ");
            }

            public string Show()
            {
                if (analyzeResult == null)
                {
                    return $"NotAnalyzed [ Name = \"{Name}\" ]";
                }
                else
                {
                    return $"Analyzed [ Name = \"{Name}\" ]";
                }
            }

        }
    }
}
