using CodeStash.Core.Entities;

namespace CodeStash.UI.Services;

public class TagStateService
{
    public Tag? SelectedTag { get; private set; }

    public void SetSelectedTag(Tag tag)
    {
        SelectedTag = tag;
    }

    public void ClearSelectedTag()
    {
        SelectedTag = null;
    }
}
