using System.Collections;
namespace WordSetProject;


public class Node<T>
{
    public readonly T Value;
    public Node<T> Next;

    public Node(T value)
    {
        Value = value;
    }
}


public class MyLinkedList<T> : IEnumerable<Node<T>>
{
    public Node<T> Head;
    public Node<T> Tail;

    public MyLinkedList()
    {
        Head = null;
        Tail = Head;
    }

    public void AddFirst(T word)
    {
        if (Head == null)
        {
            Head = new Node<T>(word);
            Tail = Head;
        }
        else
        {
            var temp = new Node<T>(word);
            temp.Next = Head;
            Head = temp;
        }
    }

    public void AddLast(T word)
    {
        if (Head == null)
        {
            AddFirst(word);
        }
        else
        {
            Tail.Next = new Node<T>(word);
            Tail = Tail.Next;
        }
    }

    public void AddAfter(Node<T> node, T word)
    {
        var temp = new Node<T>(word);
        temp.Next = node.Next;
        node.Next = temp;
    }

    public T RemoveFirst()
    {
        if (Head == null) throw new ArgumentException();
        var temp = Head.Value;
        Head = null; 
        Tail = Head; 
        return temp;
    }

    public T RemoveLast()
    {
        T result;
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

    public T RemoveAfter(Node<T> node)
    {
        if (node.Next == null) throw new ArgumentException();
        if (node == null) RemoveFirst();

        var result = node.Value;
        node.Next = node.Next.Next;
        return result;
    }

    
    public bool IsEmpty()
    {
        return (Head == null);
    }
    
    public IEnumerator<Node<T>> GetEnumerator()
    {
        if (IsEmpty()) yield break;
        
        var current = Head;
        while (current.Next != null)
        {
            yield return current;
            current = current.Next;
        }

        yield return current;
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
