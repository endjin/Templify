namespace Endjin.Templify.Client.Framework
{
    #region Using Directives

    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    #endregion

    /// <summary>
    /// Provides Hint text for a control
    /// </summary>
    public class HintTextDecorator : ContentControl
    {
        /// <summary>
        /// Using a DependencyProperty as the backing store for HintTextVisible.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty HintTextVisibleProperty = DependencyProperty.Register("HintTextVisible", typeof(bool), typeof(HintTextDecorator), new UIPropertyMetadata(false));

        /// <summary>
        /// Using a DependencyProperty as the backing store for HintText.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty HintTextProperty = DependencyProperty.Register("HintText", typeof(string), typeof(HintTextDecorator), new UIPropertyMetadata(string.Empty));

        static HintTextDecorator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HintTextDecorator), new FrameworkPropertyMetadata(typeof(HintTextDecorator)));
        }

        /// <summary>
        /// Get/set the hint text to display
        /// </summary>
        public string HintText
        {
            get { return (string)this.GetValue(HintTextProperty); }
            set { this.SetValue(HintTextProperty, value); }
        }

        /// <summary>
        /// Get/set whether the hint text is visible
        /// </summary>
        public bool HintTextVisible
        {
            get { return (bool)this.GetValue(HintTextVisibleProperty); }
            set { this.SetValue(HintTextVisibleProperty, value); }
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            this.Detach(oldContent);
            this.Attach(newContent);
        }

        private void Attach(object newContent)
        {
            // Save the user the bother of binding to visibility
            // if we have a textbox or a combobox-derived child
            // (the most common cases)
            var tb = newContent as TextBox;
            
            if (tb != null)
            {
                tb.TextChanged += this.TextBoxTextChanged;
                tb.GotKeyboardFocus += this.TextBoxGotKeyboardFocus;
                tb.LostKeyboardFocus += this.TextBoxLostKeyboardFocus;
                
                this.UpdateVisibility(tb);
                
                return;
            }

            var cb = newContent as ComboBox;
            
            if (cb != null)
            {
                cb.SelectionChanged += this.ComboBoxSelectionChanged;
                cb.GotKeyboardFocus += this.ComboBoxGotKeyboardFocus;
                cb.LostKeyboardFocus += this.ComboBoxLostKeyboardFocus;
                
                this.UpdateVisibility(cb);
            }
        }

        private void Detach(object oldContent)
        {
            // Save the user the bother of binding to visibility
            // if we have a textbox or a combobox-derived child
            // (the most common cases)
            var tb = oldContent as TextBox;
            
            if (tb != null)
            {
                tb.TextChanged -= this.TextBoxTextChanged;
                tb.GotKeyboardFocus -= this.TextBoxGotKeyboardFocus;
                tb.LostKeyboardFocus -= this.TextBoxLostKeyboardFocus;
                
                return;
            }
            
            var cb = oldContent as ComboBox;
            
            if (cb != null)
            {
                cb.SelectionChanged -= this.ComboBoxSelectionChanged;
                cb.GotKeyboardFocus -= this.ComboBoxGotKeyboardFocus;
                cb.LostKeyboardFocus -= this.ComboBoxLostKeyboardFocus;
            }
        }

        private void TextBoxLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            this.UpdateVisibility(sender as TextBox);
        }

        private void TextBoxGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            this.UpdateVisibility(sender as TextBox);
        }

        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            this.UpdateVisibility(sender as TextBox);
        }

        private void UpdateVisibility(TextBox textBox)
        {
            if (textBox == null)
            {
                return;
            }

            this.HintTextVisible = string.IsNullOrEmpty(textBox.Text) && !textBox.IsKeyboardFocusWithin;
        }

        private void ComboBoxTextInput(object sender, TextCompositionEventArgs e)
        {
            this.UpdateVisibility(sender as ComboBox);
        }

        private void ComboBoxLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            this.UpdateVisibility(sender as ComboBox);
        }

        private void ComboBoxGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            this.UpdateVisibility(sender as ComboBox);
        }

        private void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.UpdateVisibility(sender as ComboBox);
        }

        private void UpdateVisibility(ComboBox comboBox)
        {
            if (comboBox == null)
            {
                return;
            }

            this.HintTextVisible = (comboBox.SelectedItem == null) && (!comboBox.IsEditable || (!comboBox.IsKeyboardFocusWithin && string.IsNullOrEmpty(comboBox.Text)));
        }
    }
}
