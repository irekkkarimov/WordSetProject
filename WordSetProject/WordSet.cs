using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace WordSetProject;


public class Node
{
    public readonly string Value;
    public Node Next;

    public Node(string value)
    {
        Value = value;
    }
}


public class MyLinkedList : IEnumerable<Node>
{
    public Node Head;
    public Node Tail;
    public readonly int? WordLength;

    public MyLinkedList(string[] arr, int? wordLength)
    {
        WordLength = wordLength;
        foreach (var word in arr)
        {
            ArgumentThrower(word);
            if (Head == null)
            {
                Head = new Node(word);
                Tail = Head;
            }
            
            AddLast(word);
        }
    }

    public void AddFirst(string word)
    {   
        ArgumentThrower(word);
        var temp = new Node(word);
        temp.Next = Head;
        Head = temp;
    }
    
    public void AddLast(string word)
    {
        ArgumentThrower(word);
        Tail.Next = new Node(word);
        Tail = Tail.Next;
    }

    public void AddAfter(Node node, string word)
    {
        ArgumentThrower(word);
        var temp = new Node(word);
        temp.Next = node.Next.Next;
        node.Next = temp;
    }

    public string RemoveFirst()
    {
        if (Head == null) throw new ArgumentException();
        var temp = Head.Value;
        Head = null; 
        Tail = Head; 
        return temp;
    }

    public string RemoveLast()
    {
        string result;
        if (Head == null) throw new ArgumentException();
        if (Head == Tail) return RemoveFirst();
        
        var current = Head;
        while (true)
        {
            if (current.Next == Tail)
            {
                result = Tail.Value;
                Tail = current;
                break;
            }

            current = current.Next;
        }

        return result;
    }

    public string RemoveAfter(Node node)
    {
        if (node.Next == null) throw new ArgumentException();
        if (node == null) RemoveFirst();

        var result = node.Value;
        node.Next = node.Next.Next;
        return result;
    }

    // In case if there will be need to check for digits
    public static bool IfContainsOnlyLetters(string word)
    {
        if (word.Length == 0) throw new ArgumentException();
        foreach (var symbol in word)
        {
            if (Char.IsLetter(symbol))
            {
                return true;
            }
        }

        return false;
    }

    public void ArgumentThrower(string word)
    {
        if (!IfContainsOnlyLetters(word)) throw new ArgumentException("The string argument was not a word");
        if (WordLength != null && word.Length <= WordLength)
            throw new ArgumentException();
    }
    
    public IEnumerator<Node> GetEnumerator()
    {
        var current = Head;

        while (current != null)
        {
            yield return current;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}


// WordSet should contain strings with ONLY letters
public class WordSet
{
    public MyLinkedList Set;

    // Constructors
    public WordSet()
    {
        Set = new MyLinkedList(Array.Empty<string>(), null);
    }
    public WordSet(string[] arr)
    {
        Set = new MyLinkedList(arr, null);
    }
    public WordSet(string[] arr, int length)
    {
        Set = new MyLinkedList(arr, length);
    }

    public static WordSet NewWordSetByWordLength(int length)
    {
        return new WordSet(Array.Empty<string>(), length);
    }

    // Sorry for garbage code(
    public WordSet(WordSet w1, WordSet w2)
    {
        var current1 = w1.Set.Head;
        var current2 = w2.Set.Head;
        Set = new MyLinkedList(Array.Empty<string>(), null);


        while (current1 == null && current2 == null)
        {
            if (current1 == null)
            {
                Set.AddLast(current2.Value);
                current2 = current2.Next;
            }

            if (current2 == null)
            {
                Set.AddLast(current1.Value);
                current1 = current1.Next;
            }

            switch (current1.Value.CompareTo(current2))
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
            for (int j = 0; j < arr.Length; j++)
            {
                if (arr[i].CompareTo(arr[j]) > 0)
                {
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                }
            }
        }

        return arr;
    }

    public void Out(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            var current = Set.Head;
            while (current != null)
            {
                writer.WriteLine(current.Value);
                current = current.Next;
            }
        }
    }

    public void Insert(string word)
    {
        var current = Set.Head;
        Node previous = null;
        
        while (true)
        {
            if (current == null)
            {
                Set.AddLast(word);
                break;
            }
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

    public WordSet[] vowelDivide()
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
        Node previous = null;

        foreach (var node in Set)
        {
            if (IsPalindrome(node.Value))
            {
                Set.RemoveAfter(previous);
            }

            previous = node;
        }
    }

    public static bool IsPalindrome(string word)
    {
        for (int i = 0; i < word.Length / 2; i++)
        {
            if (word[i] != word[^(1 + i)]) return false;
        }

        return true;
    }
}