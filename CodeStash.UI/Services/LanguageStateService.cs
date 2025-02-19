namespace CodeStash.UI.Services;

public class LanguageStateService
{
    public string? SelectedLanguage { get; private set; }

    public void SetSelectedLanguage(string language)
    {
        SelectedLanguage = language;
    }

    public void ClearSelectedLanguage()
    {
        SelectedLanguage = null;
    }
}
