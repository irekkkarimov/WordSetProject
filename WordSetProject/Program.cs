

using System.Xml;
using WordSetProject;

public class Program
{
    public static void Main()
    {
        var wordsArray = new string[]
        {
            "lemon", "grape", "orange", "pineapple", "banana",
            "tangerine", "kiwi", "apple", "citrus",
            "mango", "peach", "palilap"
        };
        
        var testingWordSet = new WordSet(wordsArray);
        
        testingWordSet.Insert("cherry");
        testingWordSet.Insert("cherry");

        testingWordSet.Out("outputfile.txt");
        
        testingWordSet.RemovePalindrome();
        
        var temp = testingWordSet.VowelDivide();
        temp[0].Out("outputfile2.txt");
        temp[1].Out("outputfile3.txt");
        
        var emergedSet = new WordSet(temp[0], temp[1]);
        emergedSet.Delete("pineapple");
        emergedSet.Delete("pineapple");
        emergedSet.Out("outputfile4.txt");

        var wordSetByLength = emergedSet.NewWordSetByWordLength(5);
        wordSetByLength.Insert("te");
        wordSetByLength.Insert("st");
        wordSetByLength.Insert("wrong");
        wordSetByLength.Insert("wrongword");
        wordSetByLength.Out("outputfile5.txt");

        Console.WriteLine("Success!");
    }
}