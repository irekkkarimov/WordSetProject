namespace WordSetProject;


public class WordSet
{
    public MyLinkedList<string> Set;
    public readonly int? WordLength;

    // Constructor
    public WordSet(string[] arr = null, int? length = null)
    {
        WordLength = length;
        arr = arr != null ? SortArray(arr) : arr;
        Set = new MyLinkedList<string>();
        if (arr == null) {}
        else
        {
            foreach (var word in arr)
            {
                ArgumentThrower(word);
                if (Set.Head == null)
                {
                    Set.Head = new Node<string>(word);
                    Set.Tail = Set.Head;
                }
                else if (Set.Tail.Value.CompareTo(word) != 0)
                {
                    Set.AddLast(word);
                }
            }
        }
    }

    public WordSet NewWordSetByWordLength(int length)
    {
        var result = new WordSet(null, length);
        foreach (var item in Set)
        {
            var value = item.Value;
            if (value.Length <= length)
            {
                result.Set.AddLast(value);
            }
        }

        return result;
    }

    // Sorry for garbage code(
    public WordSet(WordSet w1, WordSet w2)
    {
        var current1 = w1.Set.Head;
        var current2 = w2.Set.Head;
        Set = new MyLinkedList<string>();
        
        while (!(current1 == null && current2 == null))
        {
            if (current1 == null)
            {
                Set.AddLast(current2.Value);
                current2 = current2.Next;
                continue;
            }

            if (current2 == null)
            {
                Set.AddLast(current1.Value);
                current1 = current1.Next;
                continue;
            }

            switch (current1.Value.CompareTo(current2.Value))
            {
                case 1:
                {
                    Set.AddLast(current2.Value);
                    current2 = current2.Next;
                    break;
                }
                case 0:
                {
                    Set.AddLast(current2.Value);
                    current1 = current1.Next;
                    current2 = current2.Next;
                    break;
                }

                case -1:
                {
                    Set.AddLast(current1.Value);
                    current1 = current1.Next;
                    break;
                }
            }
        }
    }

    // BubbleSort
    private static string[] SortArray(string[] arr)
    {
        int length = arr.Length;
        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = i; j < arr.Length; j++)
            {
                if (arr[i].CompareTo(arr[j]) == 1)
                {
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                }
            }
        }

        return arr;
    }

    public void Out(string filename)
    {
        using (StreamWriter writer = new StreamWriter($"C:\\Users\\Booba\\RiderProjects\\WordSetProject\\WordSetProject\\bin\\Debug\\net6.0\\{filename}"))
        {
            foreach (var node in Set)
            {
                writer.WriteLineAsync(node.Value);
            }
        }
    }

    public void Insert(string word)
    {
        ArgumentThrower(word);
        if (Set.IsEmpty())
        {
            Set.AddFirst(word);
        }
        if (Set.Tail.Value.CompareTo(word) < 0) Set.AddLast(word);
        else
        {
            var current = Set.Head;
            Node<string> previous = null;

            while (current != null)
            {
                if (current == null) Set.AddLast(word);
                if (word.CompareTo(current.Value) == 0) break;

                if (word.CompareTo(current.Value) < 0)
                {
                    if (current == Set.Head)
                    {
                        Set.AddFirst(word);
                        break;
                    }
                    
                    Set.AddAfter(previous, word);
                    break;
                }

                previous = current;
                current = current.Next;
            }
        }
    }

    public void Delete(string word)
    {
        Node<string> current = null;
        foreach (var item in Set)
        {
            if (item.Value.CompareTo(word) == 0)
            {
                if (current == null)
                {
                    Set.RemoveFirst();
                }
                else
                {
                    Set.RemoveAfter(current);
                }
            }

            current = item;
        }
    }
    
    public WordSet[] VowelDivide()
    {
        // Russian and english vowels
        const string vowels = "аоиыуэ_АОИЫУЭ_aeiouy_AEIOUY";
        
        var vowelWordSet = new WordSet();
        var consonantWordSet = new WordSet();

        foreach (var node in Set)
        {
            if (vowels.Contains(node.Value[0]))
            {
                vowelWordSet.Set.AddLast(node.Value);
            }
            else
            {
                consonantWordSet.Set.AddLast(node.Value);
            }
        }

        return new WordSet[2] { vowelWordSet, consonantWordSet };
    }

    public void RemovePalindrome()
    {
        Node<string> previous = null;

        foreach (var node in Set)
        {
            if (IsPalindrome(node.Value))
            {
                Set.RemoveAfter(previous);
            }

            previous = node;
        }
    }

    private bool IsPalindrome(string word)
    {
        for (int i = 0; i < word.Length / 2; i++)
        {
            if (word[i] != word[^(1 + i)]) return false;
        }

        return true;
    }
    
    public void ArgumentThrower(string word)
    {
        if (!IfContainsOnlyLetters(word)) throw new ArgumentException("The string argument was not a word");
        if (WordLength != null && word.Length > WordLength)
            throw new ArgumentException("Word is not of the appropriate length");
    }
    
    public static bool IfContainsOnlyLetters(string word)
    {
        var result = false;
        if (word.Length == 0) throw new ArgumentException();
        foreach (var symbol in word)
        {
            if (Char.IsLetter(symbol))
            {
                result = true;
            }
        }

        return result;
    }
}