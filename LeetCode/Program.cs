using System.Text;

string s = "3[a12[cb]]";
var result = Solution.DecodeString(s);
//Console.WriteLine(result);
Console.WriteLine(string.Join(", ", result));
public class Solution
{
    public static string DecodeString(string s)
    {
       Stack<string> stackSubString = new Stack<string>();
        Stack<int> stackNumber = new Stack<int>();
        int number = 0;
        StringBuilder currentSting = new StringBuilder();

        foreach (char c in s)
        {
            if (char.IsDigit(c))
            {
                number = number * 10 + (c - '0');
            }
            else if (c == '[')
            {
                stackNumber.Push(number);
                stackSubString.Push(currentSting.ToString());

                number = 0;
                currentSting.Clear();
            }
            else if (c == ']')
            {
                int storedNumber = stackNumber.Pop();
                StringBuilder storedLetter = new StringBuilder(stackSubString.Pop());
                string temp = storedLetter.ToString();
                for (int i = 0; i < storedNumber; i++)
                {
                    storedLetter.Append(currentSting);
                }
                currentSting = storedLetter;
            }
            else
            {
                currentSting.Append(c);
            }
        }
        return currentSting.ToString();
    }

    public static int[] AsteroidCollision(int[] asteroids)
    {       
        Stack<int> stack = new Stack<int>();       
        for (int i = 0; i < asteroids.Length; i++)
        {
            if (asteroids[i] > 0)
            {
                stack.Push(asteroids[i]);
            }
            else
            {
                while (stack.Count > 0 && stack.Peek() > 0 && stack.Peek() < Math.Abs(asteroids[i]))
                {
                    stack.Pop();
                }
                if (stack.Count == 0 || stack.Peek() < 0)
                {
                    stack.Push(asteroids[i]);
                }
                else if (stack.Peek() == Math.Abs(asteroids[i]))
                {
                    stack.Pop();
                }
            }
        }
        int[] result = stack.ToArray();
        Array.Reverse(result);
        return result;
    }
    public static string RemoveStars(string s)
    {
        if (!s.Contains('*'))
        {
            return s;
        }
        string result = "";
        Stack<char> stack = new Stack<char>();
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '*')
            {
                if (stack.Count > 0)
                {
                    stack.Pop();
                }
            }
            else
            {
                stack.Push(s[i]);
            }
        }
        var chars = stack.ToArray();

        Array.Reverse(chars);
        result = new string(chars);
        return result;
    }
    public static int EqualPairs(int[][] grid)
    {        
        int count = 0;
        for (int row = 0; row < grid.Length; row++)
        {
            for (int col = 0; col < grid.Length; col++)
            {
                bool isEqual = true;
                for (int j = 0; j < grid.Length; j++)
                {
                    
                    if (grid[row][j] != grid[j][col])
                    {
                        isEqual = false;
                        break;
                    }
                }
                if (isEqual)
                {
                    count++;
                }
            }
        }
        return count;
    }
    public static bool CloseStrings(string word1, string word2)
    {        
        if (word1.Length != word2.Length)
        {
            return false;
        }
        HashSet<char> word1Set = [.. word1];
        HashSet<char> word2Set = [.. word2];
        if (!word1Set.SetEquals(word2Set))
        {
            return false;
        }
        Dictionary<char, int> word1frequency = [];
        Dictionary<char, int> word2frequency = [];

       for (int i = 0; i < word1.Length; i++)
        {
            if (!word1frequency.TryAdd(word1[i], 1))
            {
                word1frequency[word1[i]]++;
            } 
            if (!word2frequency.TryAdd(word2[i], 1))
            {
                word2frequency[word2[i]]++;
            }
        }       
        var f1 = word1frequency.Values.OrderBy(x => x).ToList();
        var f2 = word2frequency.Values.OrderBy(x => x).ToList();

        return f1.SequenceEqual(f2);
       
    }
    public static bool UniqueOccurrences(int[] arr)
    {        
        HashSet<int> nums1Set = [];
        Dictionary<int, int> numberOccurrences = [];
        foreach (var number in arr)
        {
            if (!numberOccurrences.TryAdd(number,1))
            {
                numberOccurrences[number]++;
            }
        }
        foreach (var item in numberOccurrences.Values)
        {
            if (!nums1Set.Add(item))
            {
                return false;
            }           
        }
        return true;
    }
    public static int[] RunningSum(int[] nums)
    {
        int[] result = new int[nums.Length];
        result[0] = nums[0];
        for (int i = 1; i < nums.Length; i++)
        {
            result[i] = result[i - 1] + nums[i];
        }
        return result;
    }
    public static IList<IList<int>> FindDifferenceV2(int[] nums1, int[] nums2)
    {
        HashSet<int> nums1Set = [.. nums1];
        HashSet<int> nums2Set = [.. nums2];
        HashSet<int> only1 = [.. nums1Set];
        HashSet<int> only2 = [.. nums2Set];
        only1.ExceptWith(nums2Set);
        only2.ExceptWith(nums1Set);
        return [[.. only1], [.. only2]];
    }

    public static IList<IList<int>> FindDifferenceV1(int[] nums1, int[] nums2)
    {
        IList<IList<int>> ints = new List<IList<int>>();       
        HashSet<int> nums1Set = new HashSet<int>(nums1);
        HashSet<int> nums2Set = new HashSet<int>(nums2);
         List<int> ints1 = new List<int>();
         List<int> ints2 = new List<int>();
        int count = nums1Set.Count;
        for (int i = 0; i < nums1Set.Count; i++)
        {
            if (!nums2Set.Contains(nums1Set.ElementAt(i)))
            {
                ints1.Add(nums1Set.ElementAt(i));
            }
        }
        for (int i = 0; i < nums2Set.Count; i++)
        {
            if (!nums1Set.Contains(nums2Set.ElementAt(i)))
            {
                ints2.Add(nums1Set.ElementAt(i));
            }
        }
        ints.Add(ints1); ;
        ints.Add(ints2);
        return ints;
    }
    public static int PivotIndex(int[] nums)
    {
        int result = -1;
        int totalSum = nums.Sum();
        int leftSum = 0;
        int rightSum = 0;
        for(int i = 0; i < nums.Length; i++)
        {
            rightSum = totalSum - leftSum - nums[i];
            if (leftSum == rightSum)
            {
                return i;                
            }
            leftSum += nums[i];
        }
        return result;
    }
    public static int LargestAltitude(int[] gain)
    {
        int largestAltitude = 0;
        int currentAltitude = 0;
        Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();

        for (int i = 0; i < gain.Length; i++)
        {            
            currentAltitude +=  gain[i];
            largestAltitude = Math.Max(largestAltitude, currentAltitude);
        }
        return largestAltitude;

    }
    public static int LongestSubarray(int[] nums)
    {
        int j = 0;
        int zeroes = 0;
        int maxlength = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            zeroes += nums[i] ^ 1;
            while (zeroes > 1)
            {
                zeroes -= nums[j] ^ 1;
                j++;
            }
            maxlength = Math.Max(maxlength,i - j);
        }
        return maxlength;

    }
    public static int LongestOnes(int[] nums, int k)
    {
        int j = 0;
        int countOfzeros = 0;
        int longestOnesLength = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == 0)
            {
                countOfzeros++;
            }
            while(k < countOfzeros)
            {
                if(nums[j] == 0)
                {
                    countOfzeros--;
                }
                j++;
            }
            longestOnesLength = Math.Max(longestOnesLength, i - j + 1);
        }
        return longestOnesLength;
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
