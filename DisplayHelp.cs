namespace AT_SCC;

public class DisplayHelp {

    // The DisplayHelp class contains functions utilized to setup the UI

    public void SetTextBox(TextBox textBox, Point location, int width, string text, Color backColor, bool readOnly, Control parentControl)
    {
        textBox.Location = location;
        textBox.Width = width;
        textBox.TextAlign = HorizontalAlignment.Center;
        textBox.Text = text;
        textBox.BackColor = backColor;
        textBox.ReadOnly = readOnly;
        textBox.Cursor = Cursors.Arrow;
        parentControl.Controls.Add(textBox);
    }

    public void SetCheckBoxes(CheckBox checkBox, Point location, String text, Control parentControl)   // sets up all checkboxes with settings
    {      // sets the settings for the checkboxes

        checkBox.Width = 125;
        checkBox.Height = 20;
        checkBox.Font = new Font("Arial", 8);
        checkBox.Location = location;
        checkBox.Text = text;
        parentControl.Controls.Add(checkBox);
    }

    public void AddLabel(string labelText, Point location, Font font, Control parentControl)      // creates the labels on the MainDisplay form
    {
        Label label = new()
        {
            Text = labelText,
            Location = location,
            AutoSize = true,
            Font = font,
            ForeColor = Color.Black
        };
        parentControl.Controls.Add(label);
    }

    public void SetPanels(Panel panel, Point location, Control parentControl)  // sets up all panels with settings
    {
        panel.Location = location;
        panel.BackColor = Color.LightBlue;
        panel.Size = new Size(155, 190);
        panel.AutoScroll = true;
        parentControl.Controls.Add(panel);
    }

    public void SetButtons(Button button, Point location, String text, Color backColor, EventHandler eventHandler, Control parentControl)       // sets the settings for the buttons
    {
        button.Width = 155;
        button.Height = 40;
        button.AutoSize = true;
        button.DialogResult = DialogResult.OK;
        button.FlatStyle = FlatStyle.Flat;
        button.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#00457c");
        button.FlatAppearance.BorderSize = 2;
        button.Font = new Font("Arial", 8);
        button.Location = location;
        button.Text = text;
        button.BackColor = backColor;
        button.Click += eventHandler;
        parentControl.Controls.Add(button);
    }

}