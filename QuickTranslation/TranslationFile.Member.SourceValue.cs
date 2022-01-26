using System.Diagnostics;
using System.Xml.Linq;

namespace QuickTranslation
{
    public partial class TranslationFile
    {
        private partial class Member
        {
            [DebuggerDisplay("SourceValue [ HandleType = {HandleType}, ElementName = {Element?.Name} ]")]
            private class SourceValue
            {
                public XElement Element { get; set; }
                public HandleType HandleType { get; set; }

                public SourceValue() { }
                public SourceValue(XElement element, HandleType handleType)
                {
                    Element = element;
                    HandleType = handleType;
                }
            }

        }
    }
}
