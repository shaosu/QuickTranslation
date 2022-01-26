namespace QuickTranslation
{

    public partial class TranslationFile
    {
        private enum TranState
        {
            None,
            Complete,
            Error,
            Ignore
        }
    }
}
