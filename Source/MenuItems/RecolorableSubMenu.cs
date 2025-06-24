using Microsoft.Xna.Framework;
using Monocle;
using MonoMod.Utils;

namespace Celeste.Mod.TailSizeDynamics.MenuItems;

public class RecolorableSubmenu : TextMenuExt.SubMenu
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

    private readonly DynamicData BaseData;

    private float Ease => BaseData.Get<float>("ease");

    private MTexture ArrowIcon => BaseData.Get<MTexture>("Icon")!;

    public RecolorableSubmenu(string label, bool enterOnSelect) : base(label, enterOnSelect)
    {
        BaseData = DynamicData.For(this);
    }

    // stole- i mean, copied and adjusted from the everest repo
    public override void Render(Vector2 position, bool highlighted)
    {
        Vector2 top = new(position.X, position.Y - Height() / 2);

        float alpha = Container.Alpha;
        Color color = GetItemColor(Disabled, highlighted, alpha);
        Color strokeColor = StrokeColor * (alpha * alpha * alpha);

        bool uncentered = Container.InnerContent == TextMenu.InnerContentMode.TwoColumn && !AlwaysCenter;

        Vector2 titlePosition = top
            + Vector2.UnitY * TitleHeight / 2
            + (uncentered ? Vector2.Zero : new Vector2(Container.Width * 0.5f, 0f));
        Vector2 justify = uncentered
            ? new Vector2(0f, 0.5f)
            : new Vector2(0.5f, 0.5f);
        Vector2 iconJustify = uncentered
            ? new Vector2(ActiveFont.Measure(Label).X + ArrowIcon.Width, 5f)
            : new Vector2(ActiveFont.Measure(Label).X / 2 + ArrowIcon.Width, 5f);

        ArrowIcon.DrawOutlineCentered(
            titlePosition + iconJustify,
            GetItemColor(Disabled || Items.Count < 1, Focused) * alpha);
        ActiveFont.DrawOutline(Label, titlePosition, justify, Vector2.One, color, 2f, strokeColor);

        if (!(Focused && Ease > 0.9f))
            return;

        Vector2 menuPosition = new(top.X + ItemIndent, top.Y + TitleHeight + ItemSpacing);
        RecalculateSize();
        foreach (TextMenu.Item item in Items)
        {
            if (!item.Visible)
                continue;

            float height = item.Height();
            Vector2 itemPosition = menuPosition + new Vector2(0f, height * 0.5f + item.SelectWiggler.Value * 8f);

            if (itemPosition.Y + height * 0.5f > 0f && itemPosition.Y - height * 0.5f < Engine.Height)
                item.Render(itemPosition, Focused && Current == item);

            menuPosition.Y += height + ItemSpacing;
        }
    }

    private Color GetItemColor(bool disabled, bool focused, float alpha)
    {
        if (disabled)
            return DisabledColor;

        if (focused)
            return HighlightColor * alpha;

        return EnabledColor * alpha;
    }

    private Color GetItemColor(bool disabled, bool focused)
    {
        if (disabled)
            return DisabledColor;

        if (focused)
            return HighlightColor;

        return EnabledColor;
    }
}
