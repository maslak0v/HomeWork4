using System;

// Класс, представляющий стек строк.
public class Stack
{
    private StackItem _top;

    // Конструктор, который инициализирует стек с переданными строками.
    public Stack(params string[] items)
    {
        foreach (var item in items)
        {
            Add(item);
        }
    }

    // Добавляем строку в стек.
    public void Add(string item)
    {
        _top = new StackItem(item, _top);
    }

    // Извлекает и удаляет верхний элемент из стека.
    //Выбрасывает InvalidOperationException, если стек пустой.
    public string Pop()
    {
        if (_top == null)
        {
            throw new InvalidOperationException("Стек пустой");
        }
        string item = _top.Value;
        _top = _top.Previous;
        return item;
    }

    // Возвращает количество элементов в стеке.
    public int Size
    {
        get
        {
            int count = 0;
            var current = _top;
            while (current != null)
            {
                count++;
                current = current.Previous;
            }
            return count;
        }
    }

    // Возвращает значение верхнего элемента стека без его удаления.
    public string Top => _top?.Value;

    public StackItem GetTop() => _top;

    // Статический метод, который объединяет несколько стеков в один.
    public static Stack Concat(params Stack[] stacks)
    {
        var result = new Stack();
        foreach (var stack in stacks)
        {
            var current = stack.GetTop();
            while (current != null)
            {
                result.Add(current.Value);
                current = current.Previous;
            }
        }
        return result;
    }

    // Внутренний класс, представляющий элемент стека.
    public class StackItem
    {
        public string Value { get; } // Значение элемента стека.
        public StackItem Previous { get; } // Ссылка на предыдущий элемент стека.

        public StackItem(string value, StackItem previous) // Конструктор, инициализирующий элемент стека.
        {
            Value = value;
            Previous = previous;
        }
    }
}

public static class StackExtensions // Класс расширения для стека.
{
    public static void Merge(this Stack s1, Stack s2) // Метод расширения, который объединяет два стека.
    {
        var current = s2.GetTop();
        while (current != null)
        {
            s1.Add(current.Value);
            current = current.Previous;
        }
    }
}

class Program // Основной класс программы.
{
    static void Main()
    {
        var s = new Stack("a", "b", "c");
        Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'");

        var deleted = s.Pop();
        Console.WriteLine($"Извлек верхний элемент '{deleted}' Size = {s.Size}");

        s.Add("d");
        Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'");

        s.Pop();
        s.Pop();
        s.Pop();
        Console.WriteLine($"size = {s.Size}, Top = {(s.Top == null ? "null" : s.Top)}");

        try
        {
            s.Pop();
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }

        var s1 = new Stack("a", "b", "c");
        s1.Merge(new Stack("1", "2", "3"));
        Console.WriteLine($"size = {s1.Size}, Top = '{s1.Top}'");

        var s2 = Stack.Concat(new Stack("a", "b", "c"), new Stack("1", "2", "3"), new Stack("А", "Б", "В"));
        Console.WriteLine($"size = {s2.Size}, Top = '{s2.Top}'");
    }
}