using System;

namespace QuickTranslation
{
    [Flags]
    public enum ShowItem
    {
        State = 0x01,
        StateMessage = 0x02,
        Name = 0x04,
        FilePath = 0x08,
        FileLength = 0x10,
        TextLength = 0x20,
        Progress = 0x40
    }
}
