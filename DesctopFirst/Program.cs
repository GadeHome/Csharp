using Gtk;
using System;

class Program
{
    private static Entry display;
    private static double firstNumber = 0;
    private static string operation = "";

    static void Main(string[] args)
    {
        Application.Init();

        Window window = new Window("Калькулятор");
        window.SetDefaultSize(300, 400);
        window.DeleteEvent += (o, args) => Application.Quit();

        VBox vbox = new VBox();

        display = new Entry();
        display.IsEditable = false;
        vbox.PackStart(display, false, false, 0);

        string[] buttons = {
            "C", "7", "8", "9", "/",
            "4", "5", "6", "*",
            "1", "2", "3", "-",
            "0", ".", "=", "+"
        };

        Table table = new Table(5, 4, true);
        int row = 0, col = 0;

        foreach (string buttonText in buttons)
        {
            Button button = new Button(buttonText);
            button.Clicked += OnButtonClicked;

            table.Attach(button, (uint)col, (uint)(col + 1), (uint)row, (uint)(row + 1));

            col++;
            if (col > 3)
            {
                col = 0;
                row++;
            }
        }

        vbox.PackStart(table, true, true, 0);

        window.Add(vbox);
        window.ShowAll();
        Application.Run();
    }

    static void OnButtonClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string buttonText = button.Label;

        if (char.IsDigit(buttonText[0]) || buttonText == ".")
        {
            display.Text += buttonText;
        }
        else if (buttonText == "C")
        {
            display.Text = "";
            firstNumber = 0;
            operation = "";
        }
        else if (buttonText == "=")
        {
            double secondNumber = double.Parse(display.Text);
            double result = Calculate(firstNumber, secondNumber, operation);
            display.Text = result.ToString();
            operation = "";
        }
        else
        {
            if (!string.IsNullOrEmpty(display.Text))
            {
                firstNumber = double.Parse(display.Text);
            }
            operation = buttonText;
            display.Text = "";
        }
    }

    static double Calculate(double first, double second, string op)
    {
        switch (op)
        {
            case "+":
                return first + second;
            case "-":
                return first - second;
            case "*":
                return first * second;
            case "/":
                if (second == 0)
                {
                    MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Ошибка: деление на ноль");
                    dialog.Run();
                    dialog.Destroy();
                    return 0;
                }
                return first / second;
            default:
                return 0;
        }
    }
}
