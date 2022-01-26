using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

using Himesyo;
using Himesyo.Check;
using Himesyo.IO;
using Himesyo.Linq;
using Himesyo.Logger;

namespace QuickTranslation
{
    public partial class TranslationFile
    {
        private static Dictionary<FileState, StateInfo> fileStates;
        private static Dictionary<string, HandleType> elementHandleBase;
        private static Dictionary<string, HandleType> elementHandle;

        private XDocument document;
        private TranInfo tranInfo;
        private ITranItem upTranItem;
        private readonly object lockUpdateFile = new object();

        private readonly List<Member> untranslatedMembers = new List<Member>();
        private readonly List<Member> wrongMembers = new List<Member>();

        public static string TranNamespace { get; } = "http://docs.himesyo.com/ref/TranslationFile";
        public static XName FileStateName { get; } = XNamespace.Get(TranNamespace).GetName("filestate");
        public static string TranslatingPath { get; } = Path.GetFullPath("translating");
        public static string TranslatedPath { get; } = Path.GetFullPath("translated");

        public static int TranItemNumber { get; set; } = 10;

        public string Name { get; set; }

        public string InitPath { get; }
        public string FilePath { get; private set; }
        public string SourcePath { get; private set; }

        private FileState FileState
        {
            get
            {
                string fileState = document?.Root?.Attribute(FileStateName)?.Value;
                Enum.TryParse(fileState, true, out FileState state);
                return state;
            }
            set
            {
                document?.Root?.SetAttributeValue(FileStateName, value);
            }
        }
        public FileState State { get; private set; }
        public string StateMessage { get; private set; }

        public long FileLength { get; private set; }
        public long TextLength { get; private set; }

        public int ProCompleted { get; private set; }
        public int ProIncomplete { get; private set; }

        public event Action<TranslationFile, ShowItem> ShowItemChange;
        public event Action<TranslationFile> Analyzed;

        static TranslationFile()
        {
            //elementHandle = new Dictionary<string, HandleType>(StringComparer.OrdinalIgnoreCase);
            //elementHandle.Add("c", HandleType.Insert | HandleType.Source);
            //elementHandle.Add("para", HandleType.Separate | HandleType.NewLine | HandleType.Paragraph);
            //elementHandle.Add("", HandleType.Insert | HandleType.Space);
            elementHandleBase = new Dictionary<string, HandleType>(StringComparer.OrdinalIgnoreCase)
            {
                { "c"           , HandleType.Insert   | HandleType.Source },
                { "para"        , HandleType.Separate | HandleType.StartLine | HandleType.Paragraph },
                { "see"         , HandleType.Insert   | HandleType.Space },
                { "code"        , HandleType.Insert   | HandleType.Source },
                { "param"       , HandleType.Insert   | HandleType.Space },
                { "seealso"     , HandleType.Insert   | HandleType.Space },
                { "example"     , HandleType.Insert   | HandleType.Space },
                { "paramref"    , HandleType.Insert   | HandleType.Space },
                { "typeparam"   , HandleType.Insert   | HandleType.Space },
                { "typeparamref", HandleType.Insert   | HandleType.Space },
                { "list"        , HandleType.Separate | HandleType.Space },
                { "description" , HandleType.Separate | HandleType.Source },
                { "listheader"  , HandleType.Separate | HandleType.Source },
                { "term"        , HandleType.Separate | HandleType.Source },
                { "br"          , HandleType.Separate | HandleType.StartLine | HandleType.Paragraph },
            };
            elementHandle = elementHandleBase;
            fileStates = new Dictionary<FileState, StateInfo>();
            FieldInfo[] fields = typeof(FileState).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                StateInfoAttribute stateInfoAttribute = field.GetCustomAttribute<StateInfoAttribute>();
                if (stateInfoAttribute != null)
                {
                    try
                    {
                        StateInfo stateInfo = stateInfoAttribute.ToStateInfo();
                        FileState state = (FileState)field.GetRawConstantValue();
                        fileStates[state] = stateInfo;
                    }
                    catch (Exception ex)
                    {
                        LoggerSimple.WriteError($"类型初始化异常。", ex);
                    }
                }
            }
        }

        public TranslationFile(string path)
        {
            try
            {
                InitPath = Path.GetFullPath(path);
                Name = Path.GetFileName(path);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"无效的路径。{path.FormatNull("null")}", nameof(path), ex);
            }
        }

        public static StateInfo GetStateInfo(FileState state)
        {
            fileStates.TryGetValue(state, out var result);
            return result;
        }

        public static Dictionary<string, HandleType> GetElementHandleBase()
        {
            return elementHandleBase.ToDictionary(
                e => e.Key,
                e => e.Value,
                StringComparer.OrdinalIgnoreCase);
        }

        public static void SetElementHandle(Dictionary<string, HandleType> userHandle)
        {
            if (userHandle == null || userHandle.Count == 0)
            {
                elementHandle = elementHandleBase;
            }
            else
            {
                Dictionary<string, HandleType> handle = GetElementHandleBase();
                userHandle.ForEach(pair =>
                {
                    handle[pair.Key.Trim()] = pair.Value;
                });
                elementHandle = handle;
            }
        }

        public StateInfo GetStateInfo()
        {
            return GetStateInfo(State);
        }

        public void Init()
        {
            if (State == FileState.New)
            {
                SetState(FileState.Init, "正在初始化...");
                if (string.IsNullOrWhiteSpace(InitPath))
                {
                    SetState(FileState.Error, "未设置路径。");
                    return;
                }
                if (File.Exists(InitPath))
                {
                    try
                    {
                        string dir = Path.GetDirectoryName(InitPath);
                        string stateFile;
                        if (string.Equals(dir, TranslatingPath, StringComparison.OrdinalIgnoreCase))
                        {
                            FilePath = InitPath;
                            stateFile = Path.ChangeExtension(FilePath, "state");
                            if (File.Exists(stateFile))
                            {
                                try
                                {
                                    tranInfo = AppConfig.Load<TranInfo>(stateFile) ?? new TranInfo();
                                }
                                catch (Exception ex)
                                {
                                    LoggerSimple.WriteWarning($"[{ex.GetType().FullName} : {ex.Message}] 读取文件状态信息失败:{stateFile}");
                                    tranInfo = new TranInfo();
                                }
                                if (!string.IsNullOrWhiteSpace(tranInfo.SourcePath))
                                {
                                    SourcePath = tranInfo.SourcePath;
                                }
                            }
                            else
                            {
                                tranInfo = new TranInfo();
                            }
                            if (string.IsNullOrWhiteSpace(SourcePath))
                            {
                                string name = Path.GetFileName(InitPath);
                                Name = Regex.Replace(name, @"_[0-9a-f]{32}\.file$", "", RegexOptions.IgnoreCase);
                            }
                        }
                        else
                        {
                            SourcePath = InitPath;
                            Directory.CreateDirectory(TranslatingPath);
                            string hash = InitPath.ToLowerInvariant().ComputeMD5().ToShow();
                            string name = Path.GetFileName(InitPath);
                            FilePath = Path.Combine(TranslatingPath, $"{name}_{hash}.file");
                            stateFile = Path.ChangeExtension(FilePath, "state");
                            if (!File.Exists(FilePath))
                            {
                                File.Copy(InitPath, FilePath);
                            }
                            tranInfo = new TranInfo();
                            tranInfo.SourcePath = SourcePath;
                        }
                        if (string.IsNullOrWhiteSpace(SourcePath))
                        {
                            SourcePath = InitPath;
                        }
                        tranInfo.Save(stateFile);
                        ShowItemChange?.Invoke(this, ShowItem.Name | ShowItem.FilePath);
                        FileInfo fileInfo = new FileInfo(FilePath);
                        if (fileInfo.Exists)
                        {
                            FileLength = fileInfo.Length;
                            ShowItemChange?.Invoke(this, ShowItem.FileLength);
                        }
                        else
                        {
                            document = new XDocument();
                            LoggerSimple.WriteError($"无法加载指定的文件，文件不存在：{FilePath}");
                            SetState(FileState.Error, $"无法加载指定的文件，文件不存在。");
                            return;
                        }
                        document = XDocument.Load(FilePath);
                    }
                    catch (Exception ex)
                    {
                        document = new XDocument();
                        LoggerSimple.WriteError($"未能成功加载指定文件：{FilePath}", ex);
                        SetState(FileState.Error, $"无法加载指定文件。{ex.Message}");
                        return;
                    }
                    try
                    {
                        Analyze();
                    }
                    catch (Exception ex)
                    {
                        LoggerSimple.WriteError($"未能成功分析指定文件：{FilePath}", ex);
                        SetState(FileState.Error, $"无法分析指定文件。{ex.Message}");
                        return;
                    }
                    //SetState(FileState.Wait, "等待中...");
                }
                else
                {
                    SetState(FileState.Error, "指定的文件不存在。");
                    return;
                }
            }
        }

        public void Analyze()
        {
            SetState(FileState.Analyzing, "分析中...");
            ProCompleted = 0;
            upTranItem = null;
            untranslatedMembers.Clear();
            wrongMembers.Clear();
            FileState fileState = FileState;
            if (document != null && document.Root != null)
            {
                long textLength = 0;
                int completedNumber = 0;
                string assemblyName = document.XPathSelectElement("/doc/assembly/name")?.Value;
                if (!string.IsNullOrWhiteSpace(assemblyName))
                {
                    Name = assemblyName;
                    ShowItemChange?.Invoke(this, ShowItem.Name);
                }
                XName tran = XNamespace.Xmlns.GetName("tran");
                if (document.Root != null && document.Root.Attribute(tran) == null)
                {
                    document.Root.SetAttributeValue(tran, TranNamespace);
                }
                if (fileState != FileState.Complete)
                {
                    IEnumerable<XElement> memberElements = document.XPathSelectElements("/doc/members/member");
                    foreach (XElement memberElement in memberElements)
                    {
                        string state = memberElement.Attribute(Member.TranStateName)?.Value;
                        TranState tranState = Enum.TryParse(state, true, out TranState result) ? result : TranState.None;
                        if (tranState == TranState.None)
                        {
                            Member member = new Member(memberElement, elementHandle);
                            member.Analyze();
                            textLength += member.GetTextLength();
                            untranslatedMembers.Add(member);
                        }
                        else if (tranState == TranState.Error)
                        {
                            Member member = new Member(memberElement, elementHandle);
                            member.Analyze();
                            textLength += member.GetTextLength();
                            wrongMembers.Add(member);
                        }
                        else if (tranState == TranState.Complete)
                        {
                            completedNumber++;
                        }
                    }
                    TextLength = textLength;
                    ProCompleted = completedNumber;
                    ProIncomplete = untranslatedMembers.Count;
                    ShowItemChange?.Invoke(this, ShowItem.TextLength | ShowItem.Progress);
                }
            }
            SetState(FileState.Pause, "分析完成");
            Analyzed?.Invoke(this);
            switch (fileState)
            {
                case FileState.Wait:
                case FileState.Pause:
                case FileState.Translating:
                    SetState(fileState, string.Empty);
                    break;
                case FileState.Translated:
                case FileState.Complete:
                    if (fileState == FileState.Translated)
                    {
                        if (wrongMembers.Count > 0)
                        {
                            SetState(FileState.Translated, "翻译完成。但有错误项。");
                            break;
                        }
                    }
                    try
                    {
                        FileState = FileState.Complete;
                        Directory.CreateDirectory(TranslatedPath);
                        string output = Path.Combine(TranslatedPath, Path.GetFileName(FilePath));
                        lock (document)
                        {
                            document.Save(output);
                        }
                        File.Delete(FilePath);
                        File.Delete(Path.ChangeExtension(FilePath, "state"));
                        SetState(FileState.Complete, "已完成。");
                    }
                    catch (Exception ex)
                    {
                        SetState(FileState.Translated, $"已完成。但保存文件时失败。{ex.Message}");
                        LoggerSimple.WriteError($"已完成。但保存文件时失败。{FilePath}", ex);
                    }
                    break;
            }
        }

        public void StartTran()
        {
            FileState state = State;
            if (state == FileState.Pause || state == FileState.Wait)
            {
                SetState(FileState.Translating, "开始翻译");
            }
        }

        public void StartWait()
        {
            FileState state = State;
            if (state == FileState.Pause || state == FileState.Translating)
            {
                SetState(FileState.Wait, string.Empty);
                lock (document)
                {
                    document.Save(FilePath);
                }
            }
        }

        public void Pause()
        {
            FileState state = State;
            if (state == FileState.Wait || state == FileState.Translating)
            {
                SetState(FileState.Pause, "已暂停");
                lock (document)
                {
                    document.Save(FilePath);
                }
            }
        }

        public void TranError()
        {
            lock (lockUpdateFile)
            {
                if (untranslatedMembers.Count == 0)
                {
                    untranslatedMembers.AddRange(wrongMembers);
                    wrongMembers.Clear();
                    ProIncomplete = untranslatedMembers.Count;
                    SetState(FileState.Wait, string.Empty);
                    ShowItemChange?.Invoke(this, ShowItem.Progress);
                }
            }
        }

        public void MarkComplete()
        {
            SetState(FileState.Complete, "已完成");
        }

        public ITranItem NextTranItems()
        {
            FileState state = State;
            if (state <= FileState.Analyzing || state == FileState.Error)
            {
                throw new InvalidOperationException("当前状态下无法获取翻译项。");
            }
            try
            {
                if (upTranItem == null)
                {
                    if (untranslatedMembers.Count > 0)
                    {
                        int max = Math.Max(TranItemNumber, 5);
                        TranItem tranItem = new TranItem(this);
                        foreach (var member in untranslatedMembers)
                        {
                            List<ITranItem> items = member.EnumerableTranItems().ToList();
                            if (tranItem.Items.Count > 0 && tranItem.Items.Count + items.Count > max)
                            {
                                return upTranItem = tranItem;
                            }
                            tranItem.Members.Add(member);
                            tranItem.Items.AddRange(items);
                            if (tranItem.Items.Count >= max)
                            {
                                return upTranItem = tranItem;
                            }
                        }
                        if (tranItem.Items.Count > 0)
                        {
                            return upTranItem = tranItem;
                        }
                    }
                    else if (State != FileState.Translated && State != FileState.Complete)
                    {
                        if (wrongMembers.Count > 0)
                        {
                            FileState = FileState.Translated;
                            lock (document)
                            {
                                document.Save(FilePath);
                            }
                            SetState(FileState.Translated, "翻译完成。但有错误项。");
                        }
                        else
                        {
                            FileState = FileState.Complete;
                            try
                            {
                                Directory.CreateDirectory(TranslatedPath);
                                string fileNameHash = Path.GetFileName(FilePath);
                                string fileName = Regex.Replace(fileNameHash, @"_[0-9a-f]{32}\.file$", "", RegexOptions.IgnoreCase);
                                string output = Path.Combine(TranslatedPath, fileName);
                                if (File.Exists(output))
                                {
                                    output = Path.Combine(TranslatedPath, fileNameHash);
                                    if (File.Exists(output))
                                    {
                                        string newName = $"{fileNameHash}_01.xml";
                                        output = Path.Combine(TranslatedPath, newName);
                                        while (File.Exists(output))
                                        {
                                            newName = newName.NextValue(DigitType.ArabicNumerals | DigitType.Continuity | DigitType.First);
                                            output = Path.Combine(TranslatedPath, newName);
                                        }
                                    }
                                }
                                document.Save(output);
                                File.Delete(FilePath);
                                File.Delete(Path.ChangeExtension(FilePath, "state"));
                                SetState(FileState.Complete, "已完成。");
                            }
                            catch (Exception ex)
                            {
                                SetState(FileState.Translated, $"已完成。但保存文件时失败。{ex.Message}");
                                LoggerSimple.WriteError($"已完成。但保存文件时失败。{FilePath}", ex);
                            }
                        }
                    }
                    return upTranItem;
                }
                else
                {
                    return upTranItem;
                }
            }
            finally
            {
                Member member = untranslatedMembers.FirstOrDefault();
                SetState(State, member?.Name);
            }
        }

        private void SetState(FileState fileState, string stateMessage)
        {
            ShowItem items = 0;
            if (State != fileState)
            {
                State = fileState;
                items |= ShowItem.State;
                switch (fileState)
                {
                    case FileState.Pause:
                    case FileState.Wait:
                    case FileState.Translating:
                    case FileState.Translated:
                    case FileState.Complete:
                        FileState = fileState;
                        break;
                }
            }
            if (stateMessage != null)
            {
                StateMessage = stateMessage.FormatEmpty();
                items |= ShowItem.StateMessage;
            }
            if (items != 0)
            {
                ShowItemChange?.Invoke(this, items);
            }
        }
    }
}
