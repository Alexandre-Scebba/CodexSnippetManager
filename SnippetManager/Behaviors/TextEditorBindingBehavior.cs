using System.Windows;
using ICSharpCode.AvalonEdit;
using Microsoft.Xaml.Behaviors;

namespace SnippetManager.Behaviors;

public class TextEditorBindingBehavior : Behavior<TextEditor>
{
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(TextEditorBindingBehavior),
            new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnTextChanged));

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.TextChanged += OnTextEditorTextChanged;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.TextChanged -= OnTextEditorTextChanged;
    }

    private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TextEditorBindingBehavior behavior && behavior.AssociatedObject != null)
            behavior.AssociatedObject.Text = (string)e.NewValue;
    }

    private void OnTextEditorTextChanged(object sender, EventArgs e)
    {
        Text = AssociatedObject.Text;
    }
}