using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

using Himesyo;
using Himesyo.Linq;

namespace QuickTranslation
{
    public partial class TranslationFile
    {
        private partial class Member
        {
            [DebuggerDisplay("ReplaceText [ Type = {Type}, Text = {Text} ]")]
            private class ReplaceText : IItem, ITranItem
            {
                private static readonly Regex regexPlaceholder = new Regex(@"(?<prefix>\s*)<\s*(?<name>p\d+)\s*/\s*>(?<suffix>\s*)", RegexOptions.IgnoreCase);

                private string currPlaceholderName = "p0";

                public List<XNode> Source { get; } = new List<XNode>();

                private readonly Dictionary<string, SourceValue> placeholder = new Dictionary<string, SourceValue>(StringComparer.OrdinalIgnoreCase);

                public string Text { get; set; }

                public string Result { get; set; }

                public bool Original { get; set; }

                public TranType Type { get; set; }

                public TranState State { get; set; }

                public string AddPlaceholder(XElement element, HandleType handleType)
                {
                    string placeholderName = NextPlaceholderName();
                    placeholder.Add(placeholderName, new SourceValue(element, handleType));
                    return new XElement(placeholderName).ToString();
                }

                public long GetTextLength()
                {
                    return Text.FormatEmpty().Length;
                }

                public void Write()
                {
                    string text = Result;
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        object[] cont;
                        if (placeholder.Count > 0)
                        {
                            cont = ReplacePlaceholder(text);
                        }
                        else
                        {
                            cont = new object[] { text };
                        }
                        if (Type == TranType.ElementContent
                            && Source.Count == 1
                            && Source[0] is XElement element)
                        {
                            XElement original = null;
                            if (Original)
                            {
                                original = new XElement("para");
                                original.Add("原文：");
                                original.Add(element.Nodes().ToArray());
                            }
                            element.ReplaceNodes(cont);
                            if (original != null)
                            {
                                element.Add(original);
                            }
                        }
                        else
                        {
                            if (Original)
                            {
                                XNode node = Source.FirstOrDefault();
                                if (node != null)
                                {
                                    node.AddBeforeSelf(cont);
                                    XElement original = new XElement("para");
                                    node.AddBeforeSelf(original);
                                    Source.ForEach(item => item.Remove());
                                    original.Add("原文：");
                                    original.Add(Source.ToArray());
                                }
                            }
                            else
                            {
                                XNode nodeLast = null;
                                foreach (var item in Source)
                                {
                                    nodeLast?.Remove();
                                    nodeLast = item;
                                }
                                nodeLast?.ReplaceWith(cont);
                            }
                        }
                    }
                    Result = null;
                }

                private string NextPlaceholderName()
                {
                    return currPlaceholderName = currPlaceholderName.NextValue();
                }

                private object[] ReplacePlaceholder(string text)
                {
                    List<object> result = new List<object>(placeholder.Count * 4 + 1);
                    int startat = 0;
                    Match match = regexPlaceholder.Match(text);
                    while (match.Success)
                    {
                        string name = match.Groups["name"].Value;
                        if (placeholder.TryGetValue(name, out SourceValue value))
                        {
                            HandleType handle = value.HandleType;
                            string prefix = " ";
                            string suffix = " ";
                            if (handle.HasFlag(HandleType.SourceSpace))
                            {
                                prefix = match.Groups["prefix"].Value;
                                suffix = match.Groups["suffix"].Value;
                            }
                            else if (handle.HasFlag(HandleType.Space))
                            {
                                prefix = " ";
                                suffix = " ";
                            }
                            else if (handle.HasFlag(HandleType.LeftSpace))
                            {
                                prefix = " ";
                                suffix = string.Empty;
                            }
                            else if (handle.HasFlag(HandleType.RightSpace))
                            {
                                prefix = string.Empty;
                                suffix = " ";
                            }
                            if (handle.HasFlag(HandleType.Paragraph))
                            {
                                if (string.IsNullOrWhiteSpace(value.Element.Value))
                                {
                                    if (handle.HasFlag(HandleType.StartLine))
                                    {
                                        prefix = "\r\n";
                                    }
                                    else if (handle.HasFlag(HandleType.EndLine))
                                    {
                                        suffix = "\r\n";
                                    }
                                    else
                                    {
                                        prefix = "\r\n";
                                        suffix = "\r\n";
                                    }
                                }
                                else
                                {
                                    prefix = "\r\n";
                                    suffix = "\r\n";
                                }
                            }
                            else if (handle.HasFlag(HandleType.StartLine))
                            {
                                prefix = "\r\n";
                            }
                            else if (handle.HasFlag(HandleType.EndLine))
                            {
                                suffix = "\r\n";
                            }
                            result.Add(text.Substring(startat, match.Index - startat));
                            if (result.Count == 0 || !prefix.Equals(result[result.Count - 1]))
                            {
                                result.Add(prefix);
                            }
                            result.Add(value.Element);
                            result.Add(suffix);
                            startat = match.Index + match.Value.Length;
                        }
                        match = match.NextMatch();
                    }
                    if (startat < text.Length)
                    {
                        result.Add(text.Substring(startat));
                    }
                    return result.ToArray();
                }

                public string GetSourceText()
                {
                    return Text;
                }

                public void SetResult(string[] result)
                {
                    Result = string.Join(Environment.NewLine, result.NullToEmpty());
                }

                public void SetError(string message)
                {
                    State = TranState.Error;
                }
            }

        }
    }
}
