// See https://aka.ms/new-console-template for more information

using System.Text;

int[] nums = { 2, 7, 11, 15 };
string result = Solution.ReverseWords("a good   example");
Console.WriteLine(result);
public class Solution
{
    public static string ReverseWords(string s)
    {
        string[] words = s.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        Array.Reverse(words);
        return string.Join(" ", words);

        //string[] splitidWord = s.Split(' ');
        //List<string> words = new List<string>();
        //for (int i = splitidWord.Length - 1; i >= 0; i--)
        //{
        //    if (!string.IsNullOrEmpty(splitidWord[i]))
        //    {
        //        words.Add(splitidWord[i]);
        //    }
        //}
        //return string.Join(" ", words);
    }
    public static string ReverseVowels(string s)
    {
        HashSet<char> vowels = new HashSet<char>()
            {
                'a','e','i','o','u',
                'A','E','I','O','U'
            };
        char[] word = s.ToCharArray();
        int leftStartPoint = 0;
        int rightStartPoint = word.Length - 1;

        while (leftStartPoint < rightStartPoint)
        {

            while (leftStartPoint < rightStartPoint && !vowels.Contains(word[leftStartPoint]))
            {
                leftStartPoint++;
            }
            while (rightStartPoint > leftStartPoint && !vowels.Contains(word[rightStartPoint]))
            {
                rightStartPoint--;
            }

            (word[rightStartPoint], word[leftStartPoint]) = (word[leftStartPoint], word[rightStartPoint]);
            leftStartPoint++;
            rightStartPoint--;
        }

        return new string(word);
    }

    public static bool CanPlaceFlowers(int[] flowerbed, int n)
    {
        for (int i = 0; i < flowerbed.Length; i++)
        {
            if (flowerbed[i] == 0)
            {
                bool emptyCellLeft = (i == 0) || flowerbed[i - 1] == 0;
                bool emptyCellRight = (i == flowerbed.Length - 1) || flowerbed[i + 1] == 0;

                if (emptyCellLeft && emptyCellRight)
                {
                    flowerbed[i] = 1; // plant a flower
                    n--;
                }
            }
        }

        return n <= 0;
    }
    public static IList<bool> KidsWithCandies(int[] candies, int extraCandies)
    {
        IList<bool> bools = new List<bool>();
        int maxCountOfCandies = candies.Max();

        for (int i = 0; i < candies.Length; i++)
        {
            if (candies[i] + extraCandies >= maxCountOfCandies)
            {
                bools.Add(true);
            }
            else
            {
                bools.Add(false);
            }
        }
        return bools;
    }
    public static string GcdOfStrings(string str1, string str2)
    {
        if (str1 + str2 != str2 + str1)
        {
            return "";
        }
        int firstStringLength = str1.Length;
        int secondStringLength = str2.Length;
        while (secondStringLength != 0)
        {
            int temp = secondStringLength;
            secondStringLength = firstStringLength % secondStringLength;
            firstStringLength = temp;
        }
        return str1.Substring(0, firstStringLength);
    }

    public static string MergeAlternately(string word1, string word2)
    {
        var result = new StringBuilder(word1.Length + word2.Length);
        int i = 0;

        while (i < word1.Length || i < word2.Length)
        {
            if (i < word1.Length) result.Append(word1[i]);
            if (i < word2.Length) result.Append(word2[i]);
            i++;
        }

        return result.ToString();
    }
    public static void PrintList(ListNode head)
    {
        ListNode current = head;
        while (current != null)
        {
            Console.Write(current.val);

            if (current.next != null)
                Console.Write(" -> ");

            current = current.next;
        }

        Console.WriteLine();
    }
    public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        ListNode root = new ListNode(0);
        ListNode result = root;

        int leftover = 0;

        while (l1 != null || l2 != null || leftover > 0)
        {
            int x = (l1 != null) ? l1.val : 0;
            int y = (l2 != null) ? l2.val : 0;

            int sum = x + y + leftover;

            leftover = sum / 10;

            result.next = new ListNode(sum % 10);
            result = result.next;

            if (l1 != null) l1 = l1.next;
            if (l2 != null) l2 = l2.next;
        }

        return root.next;
    }
    public static int[] TwoSum(int[] nums, int target)
    {
        var solution = new Dictionary<int, int>();

        for (int i = 0; i < nums.Length; i++)
        {
            int outCome = target - nums[i];

            if (solution.ContainsKey(outCome))
            {
                return new int[] { solution[outCome], i };
            }

            solution[nums[i]] = i;
        }

        return Array.Empty<int>();

    }
}
public class ListNode
{
    public int val;
    public ListNode next;

    public ListNode(int val = 0, ListNode next = null)
    {
        this.val = val;
        this.next = next;
    }
    public static ListNode BuildList(int[] digits)
    {
        ListNode dummy = new ListNode(0);
        ListNode current = dummy;

        foreach (int d in digits)
        {
            current.next = new ListNode(d);
            current = current.next;
        }

        return dummy.next;
    }
}
