using MudBlazor;

namespace CodeStash.UI.Components;

public static class CodeStashThemes
{
    public static MudTheme DarkTheme = new MudTheme()
    {
        PaletteDark = new PaletteDark
        {
            Primary = CodeStashColors.Primary,
            Secondary = CodeStashColors.Secondary,
            Tertiary = CodeStashColors.Tertiary,
            Background = CodeStashColors.Background,
            AppbarBackground = CodeStashColors.AppbarBackground,
            DrawerBackground = CodeStashColors.DrawerBackground,
            LinesDefault = CodeStashColors.Primary40
        },
        Typography = new Typography()
        {
            Default = new DefaultTypography()
            {
                FontFamily = new[] { "Quicksand", "Helvetica", "Arial", "sans-serif" },
                FontSize = ".875rem",
                FontWeight = "400",
                LineHeight = "1.43",
                LetterSpacing = ".01071em"
            },
            H1 = new H1Typography()
            {
                FontFamily = new[] { "Quicksand", "Helvetica", "Arial", "sans-serif" },
                FontSize = "3rem",
                FontWeight = "700",
                LineHeight = "1.5",
                LetterSpacing = ".01071em"
            },
            H2 = new H2Typography()
            {
                FontFamily = new[] { "Quicksand", "Helvetica", "Arial", "sans-serif" },
                FontSize = "1.75rem",
                FontWeight = "500",
                LineHeight = "1.43",
                LetterSpacing = ".01071em"
            },
            H3 = new H3Typography()
            {
                FontFamily = new[] { "Quicksand", "Helvetica", "Arial", "sans-serif" },
                FontSize = "1.5rem",
                FontWeight = "400",
                LineHeight = "1.43",
                LetterSpacing = ".01071em"
            },
        }
    };
}
