using Microsoft.Xna.Framework;

namespace Celeste.Mod.TailSizeDynamics.MenuItems;

public class RecolorableButton(string label) : TextMenu.Button(label)
{
    public Color DisabledColor = Color.DarkSlateGray;
    public Color EnabledColor = Color.White;
    public Color HighlightColorA = TextMenu.HighlightColorA;
    public Color HighlightColorB = TextMenu.HighlightColorB;
    public Color StrokeColor = Color.Black;

    public Color HighlightColor
    {
        get
        {
            if (Container == null)
                return default;

            Color highlightColor = Container.HighlightColor;

            if (highlightColor == TextMenu.HighlightColorA)
                return HighlightColorA;

            if (highlightColor == TextMenu.HighlightColorB)
                return HighlightColorB;

            return highlightColor;
        }
    }

    public override void Render(Vector2 position, bool highlighted)
    {
        float alpha = Container.Alpha;
        Color color = GetItemColor(Disabled, highlighted, alpha);
        Color strokeColor = StrokeColor * (alpha * alpha * alpha);

        bool uncentered = Container.InnerContent == TextMenu.InnerContentMode.TwoColumn && !AlwaysCenter;

        Vector2 position2 = position
            + (uncentered ? Vector2.Zero : new Vector2(Container.Width * 0.5f, 0f));
        Vector2 justify = uncentered && !AlwaysCenter
            ? new Vector2(0f, 0.5f)
            : new Vector2(0.5f, 0.5f);

        ActiveFont.DrawOutline(Label, position2, justify, Vector2.One, color, 2f, strokeColor);
    }

    private Color GetItemColor(bool disabled, bool focused, float alpha)
    {
        if (disabled)
            return DisabledColor;

        if (focused)
            return HighlightColor * alpha;

        return EnabledColor * alpha;
    }
}
