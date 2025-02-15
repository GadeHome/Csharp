using Gtk;
using System;

class TextEditorApp
{
    private static TextView textView;

    static void Main(string[] args)
    {
        Application.Init();

        Window window = new Window("Текстовый Редактор");
        window.SetDefaultSize(600, 400);
        window.DeleteEvent += (o, args) => Application.Quit();

        VBox vbox = new VBox(false, 5);
        textView = new TextView();
        textView.WrapMode = WrapMode.Word;
        vbox.PackStart(textView, true, true, 0);

        HBox toolbar = new HBox(false, 5);

        Button insertButton = CreateButton("Вставить", InsertText);
        Button joinButton = CreateButton("Объединить", JoinText);
        Button lastIndexOfButton = CreateButton("Найти Последнее", LastIndexOfChar);
        Button removeButton = CreateButton("Удалить", RemoveText);
        Button replaceButton = CreateButton("Заменить", ReplaceText);
        Button splitButton = CreateButton("Разделить", SplitText);
        Button substringButton = CreateButton("Подстрока", GetSubstring);
        Button toLowerButton = CreateButton("В Нижний Регистр", ConvertToLower);
        Button trimButton = CreateButton("Удалить Пробелы", TrimText);

        toolbar.PackStart(insertButton, true, true, 5);
        toolbar.PackStart(joinButton, true, true, 5);
        toolbar.PackStart(lastIndexOfButton, true, true, 5);
        toolbar.PackStart(removeButton, true, true, 5);
        toolbar.PackStart(replaceButton, true, true, 5);
        toolbar.PackStart(splitButton, true, true, 5);
        toolbar.PackStart(substringButton, true, true, 5);
        toolbar.PackStart(toLowerButton, true, true, 5);
        toolbar.PackStart(trimButton, true, true, 5);

        vbox.PackStart(toolbar, false, false, 0);

        window.Add(vbox);
        window.ShowAll();

        Application.Run();
    }

    static Button CreateButton(string label, EventHandler handler)
    {
        Button button = new Button(label);
        button.Clicked += handler;
        return button;
    }

    static string GetText()
    {
        return textView.Buffer.Text;
    }

    static void SetText(string text)
    {
        textView.Buffer.Text = text;
    }

    static void InsertText(object sender, EventArgs e)
    {
        InputDialog dialog = new InputDialog("Вставка", "Введите позицию и текст:", new[] { "Позиция", "Текст" });
        if (dialog.Run() == (int)ResponseType.Ok)
        {
            try
            {
                int position = int.Parse(dialog.Inputs[0]);
                string newText = dialog.Inputs[1];
                string currentText = GetText();
                string updatedText = currentText.Insert(position, newText);
                SetText(updatedText);
            }
            catch (ArgumentOutOfRangeException)
            {
                ShowError("Ошибка: некорректная позиция для вставки");
            }
            catch (FormatException)
            {
                ShowError("Ошибка: неверный формат числа");
            }
        }
        dialog.Destroy();
    }

    static void JoinText(object sender, EventArgs e)
    {
        InputDialog dialog = new InputDialog("Объединение", "Введите элементы через пробел и разделитель:", new[] { "Элементы", "Разделитель" });
        if (dialog.Run() == (int)ResponseType.Ok)
        {
            string[] elements = dialog.Inputs[0].Split(' ');
            char separator = dialog.Inputs[1][0];
            string result = string.Join(separator, elements);
            ShowMessage("Результат объединения:\n" + result);
        }
        dialog.Destroy();
    }

    static void LastIndexOfChar(object sender, EventArgs e)
    {
        InputDialog dialog = new InputDialog("Поиск Последнего", "Введите символ для поиска:", new[] { "Символ" });
        if (dialog.Run() == (int)ResponseType.Ok)
        {
            char character = dialog.Inputs[0][0];
            string currentText = GetText();
            int index = currentText.LastIndexOf(character);
            if (index != -1)
            {
                ShowMessage($"Последнее вхождение символа '{character}' находится на позиции {index}");
            }
            else
            {
                ShowMessage($"Символ '{character}' не найден в тексте");
            }
        }
        dialog.Destroy();
    }

    static void RemoveText(object sender, EventArgs e)
    {
        InputDialog dialog = new InputDialog("Удаление", "Введите начальную позицию и количество символов:", new[] { "Начало", "Количество" });
        if (dialog.Run() == (int)ResponseType.Ok)
        {
            try
            {
                int start = int.Parse(dialog.Inputs[0]);
                int count = int.Parse(dialog.Inputs[1]);
                string currentText = GetText();
                string updatedText = currentText.Remove(start, count);
                SetText(updatedText);
            }
            catch (ArgumentOutOfRangeException)
            {
                ShowError("Ошибка: некорректные параметры удаления");
            }
            catch (FormatException)
            {
                ShowError("Ошибка: неверный формат числа");
            }
        }
        dialog.Destroy();
    }

    static void ReplaceText(object sender, EventArgs e)
    {
        InputDialog dialog = new InputDialog("Замена", "Введите старую и новую подстроки:", new[] { "Старая", "Новая" });
        if (dialog.Run() == (int)ResponseType.Ok)
        {
            string oldText = dialog.Inputs[0];
            string newText = dialog.Inputs[1];
            string currentText = GetText();
            string updatedText = currentText.Replace(oldText, newText);
            SetText(updatedText);
        }
        dialog.Destroy();
    }

    static void SplitText(object sender, EventArgs e)
    {
        InputDialog dialog = new InputDialog("Разделение", "Введите разделитель:", new[] { "Разделитель" });
        if (dialog.Run() == (int)ResponseType.Ok)
        {
            char separator = dialog.Inputs[0][0];
            string currentText = GetText();
            string[] parts = currentText.Split(separator);
            string result = string.Join("\n", parts);
            ShowMessage("Результат разделения:\n" + result);
        }
        dialog.Destroy();
    }

    static void GetSubstring(object sender, EventArgs e)
    {
        InputDialog dialog = new InputDialog("Подстрока", "Введите начальную позицию и длину:", new[] { "Начало", "Длина" });
        if (dialog.Run() == (int)ResponseType.Ok)
        {
            try
            {
                int start = int.Parse(dialog.Inputs[0]);
                int length = int.Parse(dialog.Inputs[1]);
                string currentText = GetText();
                string substring = currentText.Substring(start, length);
                ShowMessage("Подстрока:\n" + substring);
            }
            catch (ArgumentOutOfRangeException)
            {
                ShowError("Ошибка: некорректные параметры подстроки");
            }
            catch (FormatException)
            {
                ShowError("Ошибка: неверный формат числа");
            }
        }
        dialog.Destroy();
    }

    static void ConvertToLower(object sender, EventArgs e)
    {
        string currentText = GetText();
        SetText(currentText.ToLower());
    }

    static void TrimText(object sender, EventArgs e)
    {
        string currentText = GetText();
        SetText(currentText.Trim());
    }

    static void ShowMessage(string message)
    {
        MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, message);
        dialog.Run();
        dialog.Destroy();
    }

    static void ShowError(string message)
    {
        MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, message);
        dialog.Run();
        dialog.Destroy();
    }
}

class InputDialog : Dialog
{
    public string[] Inputs { get; private set; }

    public InputDialog(string title, string description, string[] labels) : base(title, null, DialogFlags.Modal)
    {
        SetDefaultSize(300, 200);
        VBox vbox = new VBox(false, 10);

        Label label = new Label(description);
        vbox.PackStart(label, false, false, 5);

        Inputs = new string[labels.Length];
        Entry[] entries = new Entry[labels.Length];

        for (int i = 0; i < labels.Length; i++)
        {
            HBox hbox = new HBox(false, 5);
            Label entryLabel = new Label(labels[i]);
            Entry entry = new Entry();
            hbox.PackStart(entryLabel, false, false, 5);
            hbox.PackStart(entry, true, true, 5);
            vbox.PackStart(hbox, false, false, 5);

            entries[i] = entry;
        }

        AddButton("OK", ResponseType.Ok);
        AddButton("Отмена", ResponseType.Cancel);

        vbox.ShowAll();
        ContentArea.Add(vbox);

        Response += (o, args) =>
        {
            if (args.ResponseId == ResponseType.Ok)
            {
                for (int i = 0; i < entries.Length; i++)
                {
                    Inputs[i] = entries[i].Text;
                }
            }
        };
    }
}
