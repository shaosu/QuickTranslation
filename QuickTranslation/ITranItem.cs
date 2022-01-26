namespace QuickTranslation
{
    public interface ITranItem
    {
        string GetSourceText();
        void SetResult(string[] result);
        void SetError(string message);
    }
}
