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

        // Создаем главное окно
        Window window = new Window("Калькулятор");
        window.SetDefaultSize(300, 400);
        window.DeleteEvent += (o, args) => Application.Quit();

        // Создаем вертикальный контейнер для элементов интерфейса
        VBox vbox = new VBox();

        // Поле для вывода результата
        display = new Entry();
        display.IsEditable = false; // Запрещаем редактирование вручную
        vbox.PackStart(display, false, false, 0);

        // Создаем кнопки для цифр и операций
        string[] buttons = {
            "C", "7", "8", "9", "/",  // Добавляем кнопку "C" в первую строку
            "4", "5", "6", "*",
            "1", "2", "3", "-",
            "0", ".", "=", "+"
        };

        // Создаем таблицу для кнопок
        Table table = new Table(5, 4, true); // Изменяем размер таблицы на 5x4
        int row = 0, col = 0;

        foreach (string buttonText in buttons)
        {
            Button button = new Button(buttonText);
            button.Clicked += OnButtonClicked;

            // Прикрепляем кнопку к таблице
            table.Attach(button, (uint)col, (uint)(col + 1), (uint)row, (uint)(row + 1));

            col++;
            if (col > 3)
            {
                col = 0;
                row++;
            }
        }

        vbox.PackStart(table, true, true, 0);

        // Добавляем контейнер в окно
        window.Add(vbox);

        // Показываем окно
        window.ShowAll();

        // Запускаем главный цикл приложения
        Application.Run();
    }

    // Обработчик нажатия на кнопку
    static void OnButtonClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string buttonText = button.Label;

        if (char.IsDigit(buttonText[0]) || buttonText == ".")
        {
            // Если нажата цифра или точка, добавляем к текущему числу
            display.Text += buttonText;
        }
        // ДОБАВИЛ КНОПКУ ОТЧИСТКИ
        else if (buttonText == "C")
        {
            display.Text = "";
            firstNumber = 0;
            operation = "";
        }
        else if (buttonText == "=")
        {
            // Вычисление результата
            double secondNumber = double.Parse(display.Text);
            double result = Calculate(firstNumber, secondNumber, operation);
            display.Text = result.ToString();
            operation = "";
        }
        else
        {
            // Сохранение первого числа и операции
            if (!string.IsNullOrEmpty(display.Text))
            {
                firstNumber = double.Parse(display.Text);
            }
            operation = buttonText;
            display.Text = "";
        }
    }

    // Метод для выполнения арифметических операций
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
                    MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Ошибка: деление на ноль!");
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