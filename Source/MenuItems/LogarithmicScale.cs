using System;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.TailSizeDynamics.MenuItems;

public class LogarithmicScale : TextMenu.Item
{
    private const int DigitsOfPrecision = Config.TailScaleConfig.DigitsOfPrecision;
    private const int ZeroIndex = 1 + 9 * DigitsOfPrecision * 2;
    private const int MultiplierLength = ZeroIndex + 1 + ZeroIndex;
    private static readonly Multiplier[] Multipliers;

    private static float? _ValueWidth;
    private static float ValueWidth => _ValueWidth ??= CalculateRightWidth();

    // the array is basically
    // - min value
    // - -[1..9] * 10^x, where x goes from 0 -> DigitsOfPrecision-1
    // - -[1..9] * 10^x, where x goes from -DigitsOfPrecision -> -1
    // - zero
    // - +[9..1] * 10^x, where x goes from -1 -> -DigitsOfPrecision
    // - +[9..1] * 10^x, where x goes from DigitsOfPrecision-1 -> 0
    // - max value

    // so MultiplierLength = 1 + 2n + 1 + 2n + 1, and ZeroIndex = 1 + 2n
    // where n = 9 * DigitsOfPrecision

    static LogarithmicScale()
    {
        Multipliers = new Multiplier[MultiplierLength];
        int index = 0;

        // populate the ends...
        SetMultiplier(MathF.Pow(10, DigitsOfPrecision));

        // ...the in-between...
        for (int magnitude = DigitsOfPrecision - 1; magnitude >= -DigitsOfPrecision; magnitude--)
        {
            // use double to avoid precision loss
            double mult = Math.Pow(10, magnitude);
            for (int digit = 9; digit > 0; digit--)
                SetMultiplier((float)(digit * mult));
        }

        // ...and finally the middle
        Multipliers[ZeroIndex] = new Multiplier(0);

        return;

        void SetMultiplier(float value)
        {
            // we need the array to be sorted
            // write the negative first from the beginning, then the positive from the end
            Multipliers[index] = new Multiplier(-value);
            Multipliers[^++index] = new Multiplier(value);
        }
    }

    private static float CalculateRightWidth()
    {
        const float Padding = 60f;
        float width = 0f;
        foreach (string multiplier in Multipliers)
            width = Math.Max(width, ActiveFont.Measure(multiplier).X);

        return width + 2 * Padding;
    }

    public readonly bool AllowNegatives;
    public readonly string Label;
    private readonly float LabelWidth;

    public int Index;

    public Multiplier CurrentValue => Multipliers[Index];

    public event Action<float>? OnValueChange;

    private float Sine;
    private int PreviousDirection;
    private bool IsNegative;

    private bool CanMoveLeft => IsNegative
        ? Index > 0
        : Index < MultiplierLength - 1;

    private bool CanMoveRight => Index != ZeroIndex;

    public LogarithmicScale(string label, float initialMultiplier, bool allowNegatives = true)
    {
        Label = label;
        LabelWidth = ActiveFont.Measure(Label).X + 32f;
        AllowNegatives = allowNegatives;
        Selectable = true;
        Index = Array.BinarySearch(Multipliers, initialMultiplier);
        if (Index < 0)
            Index = ~Index;

        IsNegative = Index < ZeroIndex;
    }

    public override string SearchLabel() => Label;

    public override float Height() => ActiveFont.LineHeight;
    public override float LeftWidth() => LabelWidth;
    public override float RightWidth() => ValueWidth;

    public override void Added()
        => Container.InnerContent = TextMenu.InnerContentMode.TwoColumn;

    public override void LeftPressed()
    {
        if (!CanMoveLeft)
            return;

        Audio.Play(SFX.ui_main_button_toggle_on);
        ValueWiggler.Start();
        MoveIndex(1);
        OnValueChange?.Invoke(CurrentValue);
    }

    public override void RightPressed()
    {
        if (!CanMoveRight)
            return;

        Audio.Play(SFX.ui_main_button_toggle_off);
        ValueWiggler.Start();
        MoveIndex(-1);
        OnValueChange?.Invoke(CurrentValue);
    }

    public override void ConfirmPressed()
    {
        if (Index is < 0 or > MultiplierLength)
            return;

        if (!IsNegative && !AllowNegatives)
        {
            Audio.Play(SFX.ui_main_button_invalid);
            return;
        }

        Audio.Play(SFX.ui_main_button_toggle_on);
        ValueWiggler.Start();
        IsNegative = !IsNegative;

        if (Index is not ZeroIndex)
            Index = 2 * ZeroIndex - Index;

        OnValueChange?.Invoke(CurrentValue);
    }

    private void MoveIndex([ValueRange(-1)] [ValueRange(1)] int direction)
    {
        Index += IsNegative ? -direction : direction;
        PreviousDirection = direction;
    }

    public override void Update() => Sine += Engine.RawDeltaTime;

    public override void Render(Vector2 position, bool highlighted)
    {
        const float ArrowPadding = 40f;

        float alpha = Container.Alpha;
        float valueWidth = RightWidth();
        float wigglerValue = ValueWiggler.Value * 8f;

        Vector2 justifyCenter = TailScaleModule.JustifyCenter;

        Color strokeColor = Color.Black * (alpha * alpha * alpha);
        Color color = Disabled
            ? Color.DarkSlateGray
            : (highlighted ? Container.HighlightColor : Color.White) * alpha;

        // label
        ActiveFont.DrawOutline(
            Label,
            position, TailScaleModule.JustifyCenterLeft, Vector2.One, color,
            2f, strokeColor);

        // value
        Vector2 valuePosition = position
            + new Vector2(
                Container.Width - valueWidth * 0.5f + PreviousDirection * wigglerValue,
                0f);
        ActiveFont.DrawOutline(
            (string)CurrentValue,
            valuePosition, justifyCenter, Vector2.One * 0.8f, color,
            2f, strokeColor);

        Vector2 arrowOffset = highlighted
            ? Vector2.UnitX * (MathF.Sin(Sine * 4f) * 4f)
            : Vector2.Zero;

        // left arrow
        bool canMoveLeft = CanMoveLeft;
        Color leftArrowColor = canMoveLeft ? color : Color.DarkSlateGray * alpha;
        Vector2 leftArrowPosition = position
            + new Vector2(
                Container.Width - valueWidth + ArrowPadding
                + (PreviousDirection > 0 ? -wigglerValue : 0f),
                0f)
            - (canMoveLeft ? arrowOffset : Vector2.Zero);

        ActiveFont.DrawOutline(
            "<",
            leftArrowPosition, justifyCenter, Vector2.One, leftArrowColor,
            2f, strokeColor);

        // right arrow
        bool canMoveRight = CanMoveRight;
        Color rightArrowColor = canMoveRight ? color : Color.DarkSlateGray * alpha;
        Vector2 rightArrowPosition = position
            + new Vector2(
                Container.Width - ArrowPadding
                + (PreviousDirection < 0 ? wigglerValue : 0f),
                0f)
            + (canMoveRight ? arrowOffset : Vector2.Zero);

        ActiveFont.DrawOutline(
            ">",
            rightArrowPosition, justifyCenter, Vector2.One, rightArrowColor,
            2f, strokeColor);
    }

    // it's not necessary to make it implement IEquatable
    public readonly struct Multiplier(float value)
        : IComparable<float>, IComparable<Multiplier>, IComparable
    {
        private readonly float Value = value;
        private readonly string String = TailScaleModule.FormatScale(value);

        public override string ToString() => String;

        public int CompareTo(Multiplier other) => Value.CompareTo(other.Value);
        public int CompareTo(float other) => Value.CompareTo(other);

        public int CompareTo(object? obj) => obj switch {
            null => 1,
            Multiplier otherMult => CompareTo(otherMult),
            float floatMult => CompareTo(floatMult),
            _ => throw new NotSupportedException(),
        };

        public static implicit operator float(Multiplier self) => self.Value;
        public static explicit operator string(Multiplier self) => self.ToString();
    }
}
