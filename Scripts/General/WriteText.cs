using Godot;
using System;

public partial class WriteText : Label
    {
    /// <summary>
    /// Dynamically adjusts the font size of this RichTextLabel so that the text fits within the label's width.
    /// </summary>
    /// <param name="minFontSize">Minimum font size to try.</param>
    /// <param name="maxFontSize">Maximum font size to try.</param>
    /// <param name="text">Text to fit (optional, defaults to current Text).</param>
    public void AutoSizeFontToFit(int minFontSize, int maxFontSize, string text = null)
    {
        // Get the font resource (assumes normal_font is set in theme overrides)
        var font = this.GetThemeFont("normal_font");
        if (font == null)
            return;

        string fitText = text ?? this.Text;
        int left = minFontSize;
        int right = maxFontSize;
        int bestSize = minFontSize;

        // Use binary search to find the largest font size that fits
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            float width = font.GetStringSize(fitText, HorizontalAlignment.Left, -1, mid).X;
            if (width > this.Size.X)
            {
                right = mid - 1;
            }
            else
            {
                bestSize = mid;
                left = mid + 1;
            }
        }

        // Apply the best font size as a theme override
        this.AddThemeFontSizeOverride("normal_font_size", bestSize);
    }
    private string initialText = "";
    private float charDelay = 0.05f; // Delay in seconds between each character
    private float timer = 0f;
    private int charIndex = 0;
    private bool isWriting = false;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        AutoSizeFontToFit(10, 52);
        if (!isWriting) return;

        timer += (float)delta;
        if (timer >= charDelay && charIndex < initialText.Length)
        {
            this.Text += initialText[charIndex];
            charIndex++;
            timer = 0f;
        }
        else if (charIndex >= initialText.Length)
        {
            isWriting = false;
        }
    }

    public void StartWriting(string text, float delayPerChar = 0.05f)
    {
        initialText = text;
        charDelay = delayPerChar;
        this.Text = "";
        charIndex = 0;
        timer = 0f;
        isWriting = true;
    }

    public void StopWriting()
    {
        isWriting = false;
    }
}
