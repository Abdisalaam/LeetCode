// See https://aka.ms/new-console-template for more information

using System.ComponentModel;
using System.Text;

int[] s = [1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0];
int result = Solution.LongestOnes(s , 2);
//Console.WriteLine(result);
Console.WriteLine(string.Join(", ", result));
public class Solution
{
    public static int LongestOnes(int[] nums, int k)
    {
        int result = 0;
        return result;
    }
    public static int MaxVowels(string s, int k)
    {
        HashSet<char> vowels = new HashSet<char>()
    {
        'a','e','i','o','u',
        'A','E','I','O','U'
    };
        int current = 0; int max = 0;       
        for (int i = 0; i < k; i++)
        {
            if (vowels.Contains(s[i]))
                current++;
        }
        max = current;       
        for (int i = k; i < s.Length; i++)
        {           
            if (vowels.Contains(s[i]))
                current++;
           
            if (vowels.Contains(s[i - k]))
                current--;

            max = Math.Max(max, current);
        }

        return max;
    }
    public static double FindMaxAverage(int[] nums, int k)
    {
        int sum = 0;
        for(int i =0; i < k; i++)
        {
            sum += nums[i];
        }
        int maxSum = sum;
        for(int i = k; i < nums.Length; i++)
        {            
            sum += nums[i] - nums[i - k];
            maxSum = Math.Max(maxSum, sum);
        }
        double result = (double)maxSum / k;
        return result;

    }
    public static int MaxOperations(int[] nums, int k)
    {
        int operations = 0;
        Dictionary<int, int> numbersCounts = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++)
        {            
            int number = k - nums[i];
            if (numbersCounts.ContainsKey(number) && numbersCounts[number] > 0)
            {
                operations++;                
                numbersCounts[number]--;
            }
            else
            {
                if (!numbersCounts.ContainsKey(nums[i]))
                {
                    numbersCounts[nums[i]] = 0;
                }
                numbersCounts[nums[i]]++;
            }
        }
        return operations;
    }
    public static int MaxArea(int[] height)
    {
        int left = 0;
        int right = height.Length - 1;
        int maxArea = 0;
        while (left < right)
        {
           
            int currentArea = Math.Min(height[left], height[right]) * (right - left);
            if (currentArea > maxArea)
            {
                maxArea = currentArea;
            }
           if (height[left] < height[right])
            {
                left++;
            }
            else
            {
                right--;
            }
        }
        return maxArea;
    }
    public static bool IsSubsequence(string s, string t)
    {
        if (s.Length == 0) return true;
        if (t.Length == 0) return false;
        int j = 0;
        for (int i = 0; i < t.Length; i++)
        {
           
            if (j < s.Length && s[j] == t[i])
            {
                j++;
            }
        }
        return j == s.Length;
    }
    public static int[] MoveZeroes(int[] nums)
    {        
        int pivot = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != 0)
            {
                int temp = nums[pivot];
                nums[pivot] = nums[i];
                nums[i] = temp;
                pivot++;
            }
        }
        return nums;
    }
    public static int Compress2(char[] chars)
    {
        int write = 0;
        int count = 1;
        for (int i = 0; i < chars.Length; i++)
        {
            if (i < chars.Length - 1 && chars[i] == chars[i + 1])
            {
                count++;
            }
            else
            {
                chars[write] = chars[i];
                write++;
                if (count > 1)
                {
                    string countStr = count.ToString();
                    foreach (char c in countStr)
                    {
                        chars[write] = c;
                        write++;
                    }
                }
                count = 1;
            }
        }

        return write;
    }
    public static int Compress(char[] chars)
    {
        int write = 0;
        Dictionary<char, int> charCount = new Dictionary<char, int>();
        for (int i = 0; i < chars.Length; i++)
        {
            if (charCount.ContainsKey(chars[i]))
            {
                charCount[chars[i]]++;
            }
            else
            {
                charCount[chars[i]] = 1;
            }
            bool isLastChar = (i == chars.Length - 1) || chars[i] != chars[i + 1];
            if (isLastChar)
            {                
                chars[write] = chars[i];
                write++;
                if (charCount[chars[i]] > 1)
                {
                    string countStr = charCount[chars[i]].ToString();
                    foreach (char c in countStr)
                    {                       
                        chars[write] = c;
                        write++;
                    }
                }
                charCount.Clear();
            }
        }        
        return write;
    }
    public static bool IncreasingTriplet(int[] nums)
    {
       int first = int.MaxValue;
        int second = int.MaxValue;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] <= first)
            {
                first = nums[i];
            }
            else if(nums[i] <= second)
            {
                second = nums[i];
            }
            else
            {                              
                return true;
            }

        }

        return false;
    }
    public static int[] ProductExceptSelf(int[] nums)
    {
        //int[] nums = { 1, 2, 3, 4 };

        //int[] Left = { 1, 1, 2, 6 };

        //int[] Right = { 24, 12, 4, 1 };
        //int[] Result = { 24, 12, 8, 6 };

        int[] result = new int[nums.Length]; 
        
        int leftProduct = 1;
        for (int i = 0; i < nums.Length; i++)
        {
            result[i] = leftProduct;
            leftProduct *= nums[i];
        }

        int rightProduct = 1;
        for (int i = nums.Length - 1; i >= 0; i--)
        {
            result[i] *= rightProduct;
            rightProduct *= nums[i];
        }
        return result;
    }
    public static string ReverseWords(string s)
    {
        //string[] words = s.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        //Array.Reverse(words);
        //return string.Join(" ", words);

        string[] splitidWord = s.Split(' ');
        List<string> words = new List<string>();
        for (int i = splitidWord.Length - 1; i >= 0; i--)
        {
            if (!string.IsNullOrEmpty(splitidWord[i]))
            {
                words.Add(splitidWord[i]);
            }
        }
        return string.Join(" ", words);
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
