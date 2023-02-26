using System.Collections;
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
