using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Reflection;
using System.Text;
using System.Xml.Linq;



int result = Solution.OrangesRotting();
Console.WriteLine(result);

//Console.WriteLine(string.Join(", ", result));
public class TreeNode
{
    private static int max = 0;
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
    public static TreeNode InsertIntoBST(TreeNode root, int val)
    {
        if (root == null)
            return new TreeNode(val);

        if (val < root.val)
            root.left = InsertIntoBST(root.left, val);
        else
            root.right = InsertIntoBST(root.right, val);

        return root;
    }

    public static TreeNode InsertIntoBSTIterative(TreeNode root, int val)
    {
        if (root == null)
            return new TreeNode(val);
        TreeNode current = root;
        while (true)
        {
            if (val < current.val)
            {
                if (current.left == null)
                {
                    current.left = new TreeNode(val);
                    break;
                }
                else
                {
                    current = current.left;
                }
            }
            else
            {
                if (current.right == null)
                {
                    current.right = new TreeNode(val);
                    break;
                }
                else
                {
                    current = current.right;
                }
            }
        }
        return root;
    }

    public static TreeNode DeleteNodeV2(TreeNode root, int val)
    {
        if (root == null)
        {
            return null;
        }
        if (val < root.val)
        {
            root.left = DeleteNodeV2(root.left, val);
        }
        else if (val > root.val)
        {
            root.right = DeleteNodeV2(root.right, val);
        }
        else
        {
            
            if (root.right == null)
            {
                return root.left;
            }
            if(root.left == null)
            { 
                return root.right;
            }
            TreeNode treeNode = root.right;
            while (treeNode.left != null)
            {
                treeNode = treeNode.left;               
            }
            root.val = treeNode.val;

            root.right = DeleteNodeV2(root.right, treeNode.val);
        }
        return root;
    }
    public static TreeNode DeleteNode(TreeNode root, int key)
    {
        if (root == null)
            return null;

        if (key < root.val)
        {
            root.left = DeleteNode(root.left, key);
        }
        else if (key > root.val)
        {
            root.right = DeleteNode(root.right, key);
        }
        else
        {           
            if (root.left == null)
            {
                return root.right;
            } 
            if (root.right == null)
            {
                return root.left;
            }  
            TreeNode minNode = root.right;
            while (minNode.left != null)
            {
                minNode = minNode.left;
            }
                
            root.val = minNode.val;
            root.right = DeleteNode(root.right, minNode.val);
        }
        return root;

    }
    private static TreeNode FindMin(TreeNode node)
    {
        while (node.left != null)
            node = node.left;
        return node;
    }

    public static TreeNode SearchBST(TreeNode root, int val)
    {
        if (root == null)
        {
            return null;
        }        
        if (val < root.val)            
        {
            return SearchBST(root.left, val);
        }
        else if (val > root.val)
        {
            return SearchBST(root.right, val);
        }
        else
        {
            return root;
        }
        
    }

    public static int MaxLevelSum(TreeNode root)
    {
        if (root == null)
        {
            return 0;
        }
        int maximalLevelSum = int.MinValue;
        int levelWithMaxSum = 0;
        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        int currentLevel = 1;
        while (queue.Count > 0)
        {
            int levelSize = queue.Count;
            int currentLevelSum = 0;
            for (int i = 0; i < levelSize; i++)
            {
                TreeNode currentNode = queue.Dequeue();
                currentLevelSum += currentNode.val;
                if (currentNode.left != null)
                {
                    queue.Enqueue(currentNode.left);
                }
                if (currentNode.right != null)
                {
                    queue.Enqueue(currentNode.right);
                }
            }
            if (currentLevelSum > maximalLevelSum)
            {
                maximalLevelSum = currentLevelSum;
                levelWithMaxSum = currentLevel;
            }
            currentLevel++;
        }
        return levelWithMaxSum;
    }
    public static IList<int> RightSideView(TreeNode root)
    {
        List<int> result = new List<int>();
        DFS(root, 0, result);
        return result;
    }

    private static void DFS(TreeNode root, int depth, IList<int> result)
    {
        if (root == null)
            return;
        int RootValue = root.val;
        int DepthValue = depth;
        int ListCount = result.Count;
        if (depth == result.Count)
            result.Add(root.val);

        DFS(root.right, depth + 1, result);
        DFS(root.left, depth + 1, result);
    }

    public static TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        if (root == null)
        {
            return null;
        }
        int rootVaue = root.val;
        int P_Value = p.val;
        int Q_Value = q.val;
        //[3, 5, 1, 6, 2, 0, 8, null, null, 7, 4];
        // P = 5
        // q = 1
        if (root == p || root == q)
        {
            return root;
        }
        TreeNode left = LowestCommonAncestor(root.left, p, q);
        TreeNode right = LowestCommonAncestor(root.right, p, q);
        if (left != null && right != null)
        {
            return root;
        }
        if (left != null && right == null)
        {
            return left;
        }
        else
        {
            return right;
        }
    }
    // Depth using recursion
    public static int GetDepth(TreeNode root, TreeNode target, int depth)
    {
        if (root == null) return -1;
        if (root == target) return depth;

        int left = GetDepth(root.left, target, depth + 1);
        if (left != -1) return left;

        return GetDepth(root.right, target, depth + 1);
    }

    // Count nodes between p and q
    public static int CountNodesBetween(TreeNode root, TreeNode p, TreeNode q)
    {
        TreeNode lca = LowestCommonAncestor(root, p, q);

        int depthP = GetDepth(root, p, 0);
        int depthQ = GetDepth(root, q, 0);
        int depthLCA = GetDepth(root, lca, 0);

        return depthP + depthQ - 2 * depthLCA + 1;
    }
    public static TreeNode Find(TreeNode root, int val)
    {
        if (root == null) return null;
        if (root.val == val) return root;

        TreeNode left = Find(root.left, val);
        if (left != null) return left;

        return Find(root.right, val);
    }

   

    public static int LongestZigZag(TreeNode root)
    {
        if (root == null)
        {
            return max;
        }
        return 0;


        DFS(root.right, false, true, 1);
        DFS(root.left, true, false, 1);

        return max;
    }

    private static void DFS(TreeNode node, bool cameFromLeft, bool cameFromRight, int length)
    {
        if (node == null)
            return;

        max = Math.Max(max, length);

        if (cameFromLeft)
        {
            DFS(node.right, false, true, length + 1);

            DFS(node.left, true, false, 1);
        }
        if (cameFromRight)
        {
            DFS(node.left, true, false, length + 1);

            DFS(node.right, false, true, 1);
        }
    }
    public static int MyPathSum(TreeNode root, int targetSum)
    {
        Dictionary<long, int> containsCountOfRunningSum = new Dictionary<long, int>();
        containsCountOfRunningSum[0L] = 0;
        return DFSRecursiveForThePathSum(root, 0L, targetSum, containsCountOfRunningSum);
    }
    public static int DFSRecursiveForThePathSum(TreeNode root, long runningSum, int targetSum, Dictionary<long, int> containsCountOfRunningSum)
    {
        if (root == null)
        {
            return 0;
        }
        runningSum += root.val;
        long neededSum = runningSum - targetSum;

        int count = 0;
        if (containsCountOfRunningSum.ContainsKey(neededSum))
        {
            count = containsCountOfRunningSum[neededSum];
        }
        if (!containsCountOfRunningSum.ContainsKey(runningSum))
        {
            containsCountOfRunningSum[runningSum] = 0;
        }
        containsCountOfRunningSum[runningSum]++;

        int totalCount = count + DFSRecursiveForThePathSum(root.left, runningSum, targetSum, containsCountOfRunningSum);
        totalCount += DFSRecursiveForThePathSum(root.right, runningSum, targetSum, containsCountOfRunningSum);
        containsCountOfRunningSum[runningSum]--;
        return totalCount;
    }
    public static int PathSumRunningSum(TreeNode root, int targetSum)
    {
        // Use long for prefix sums to avoid overflow
        Dictionary<long, int> prefixSumCount = new Dictionary<long, int>();

        // Empty path prefix sum
        prefixSumCount[0L] = 1;

        return DFS(root, 0L, targetSum, prefixSumCount);
    }

    private static int DFS(TreeNode node, long runningSum, int targetSum, Dictionary<long, int> prefixSumCount)
    {
        if (node == null)
        {
            return 0;
        }


        // Update running sum using long
        runningSum += (long)node.val;

        long needed = runningSum - (long)targetSum;

        int pathsEndingHere = prefixSumCount.ContainsKey(needed) ? prefixSumCount[needed] : 0;

        if (!prefixSumCount.ContainsKey(runningSum))
        {
            prefixSumCount[runningSum] = 0;
        }

        prefixSumCount[runningSum]++;

        int totalPaths = pathsEndingHere;
        totalPaths += DFS(node.left, runningSum, targetSum, prefixSumCount);
        totalPaths += DFS(node.right, runningSum, targetSum, prefixSumCount);

        // Backtrack
        prefixSumCount[runningSum]--;

        return totalPaths;
    }
    public static int PathSum(TreeNode root, int targetSum)
    {
        if (root == null)
        {
            return 0;
        }

        int startFromRoot = GetValueOfPathCount(root, targetSum);
        int startFromLeft = GetValueOfPathCount(root.left, targetSum);
        int startFromRight = GetValueOfPathCount(root.right, targetSum);

        return startFromRoot + startFromLeft + startFromRight;
    }
    public static int GetValueOfPathCount(TreeNode treeNode, int target)
    {
        if (treeNode == null)
        { return 0; }
        int count = 0;
        int total = target - treeNode.val;
        if (treeNode.val == target)
        {
            count++;
        }
        count += GetValueOfPathCount(treeNode.left, target - treeNode.val);
        count += GetValueOfPathCount(treeNode.right, target - treeNode.val);
        return count;
    }

    public static void PrintTree(TreeNode root)
    {
        if (root == null)
        {
            Console.WriteLine("Empty tree");
            return;
        }
        Queue<TreeNode> q = new Queue<TreeNode>();
        q.Enqueue(root);

        while (q.Count > 0)
        {
            TreeNode node = q.Dequeue();

            if (node == null)
            {
                Console.Write("null ");
                continue;
            }
            Console.Write(node.val + " ");
            q.Enqueue(node.left);
            q.Enqueue(node.right);
        }
        Console.WriteLine();
    }
    public static TreeNode BuildTreeBFS(object[] arr)
    {
        if (arr == null || arr.Length == 0 || arr[0] == null)
        {
            return null;
        }


        TreeNode root = new TreeNode((int)arr[0]);
        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        int i = 1;

        while (queue.Count > 0 && i < arr.Length)
        {
            TreeNode current = queue.Dequeue();

            // Left child
            if (i < arr.Length && arr[i] != null)
            {
                current.left = new TreeNode((int)arr[i]);
                queue.Enqueue(current.left);
            }
            i++;

            // Right child
            if (i < arr.Length && arr[i] != null)
            {
                current.right = new TreeNode((int)arr[i]);
                queue.Enqueue(current.right);
            }
            i++;
        }

        return root;
    }

    public static TreeNode BuildTreeDFS(object[] arr)
    {
        if (arr == null || arr.Length == 0)
            return null;

        TreeNode[] nodes = new TreeNode[arr.Length];

        // Create nodes only for non-null values
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] != null)
                nodes[i] = new TreeNode((int)arr[i]);
        }

        // Link children
        for (int i = 0; i < arr.Length; i++)
        {
            if (nodes[i] == null)
                continue;

            int left = 2 * i + 1;
            int right = 2 * i + 2;
            if (left < arr.Length)
            {
                nodes[i].left = nodes[left];
            }
            if (right < arr.Length)
            {
                nodes[i].right = nodes[right];
            }
        }
        return nodes[0];
    }

    public static int MaxDepth(TreeNode root)
    {
        if (root == null)
        {
            return 0;
        }
        // preorder
        int RootValue = root.val;
        int leftDepth = MaxDepth(root.left);
        int rightDepth = MaxDepth(root.right);
        int maximial = Math.Max(leftDepth, rightDepth) + 1;
        return maximial;
    }
    public static int MaxDepthIterativeVersion(TreeNode root)
    {
        if (root == null)
        {
            return 0;
        }
        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        int depth = 0;

        while (queue.Count > 0)
        {
            int levelSize = queue.Count;
            depth++;

            for (int i = 0; i < levelSize; i++)
            {
                TreeNode node = queue.Dequeue();
                if (node.left != null) queue.Enqueue(node.left);
                if (node.right != null) queue.Enqueue(node.right);
            }
        }

        return depth;
    }
    public bool LeafSimilar(TreeNode root1, TreeNode root2)
    {
        if (root1 == null || root2 == null)
        {
            return false;
        }
        List<int> nodes1 = new List<int>();
        List<int> nodes2 = new List<int>();
        GetLeaves(root1, nodes1);
        GetLeaves(root2, nodes2);
        if (nodes1.Count != nodes2.Count)
        {
            return false;
        }
        for (int i = 0; i < nodes1.Count; i++)
        {
            if (nodes1[i] != nodes2[i])
            {
                return false;
            }

        }
        return true;
    }

    public static void GetLeaves(TreeNode root, List<int> listLeaves)
    {
        if (root == null)
        {
            return;
        }

        if (root.left == null && root.right == null)
        {
            listLeaves.Add(root.val);
            return;
        }
        GetLeaves(root.left, listLeaves);
        GetLeaves(root.right, listLeaves);

    }
    public static int GoodNodes(TreeNode root)
    {
        int rootNodeValue = root.val;
        int goodNodeCount = GetGoodNodeCount(root, rootNodeValue);

        return goodNodeCount;
    }

    public static int GetGoodNodeCount(TreeNode root, int rootNodeCount)
    {
        int goodNodeCount = 0;
        if (root == null)
        {
            return 0;
        }
        if (root.val >= rootNodeCount)
        {
            goodNodeCount++;

        }
        rootNodeCount = Math.Max(rootNodeCount, root.val);
        goodNodeCount += GetGoodNodeCount(root.left, rootNodeCount);
        goodNodeCount += GetGoodNodeCount(root.right, rootNodeCount);

        return goodNodeCount;
    }


}

public class RecentCounter
{
    private Queue<int> q;
    public RecentCounter()
    {
        q = new Queue<int>();
    }
    public int Ping(int t)
    {
        q.Enqueue(t);
        while (q.Peek() < t - 3000)
        {
            q.Dequeue();
        }
        return q.Count;
    }
}
public class ListNode(int val = 0, ListNode? next = null)
{
    public int val = val;
    public ListNode next = next;
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
    public void PrintList(ListNode head)
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
    public ListNode DeleteMiddle(ListNode head)
    {      
        int count = 0;
        ListNode cureentNode = head;       
        while(cureentNode != null)
        {
            count++;
            cureentNode = cureentNode.next;
        }
        int toBeDeleted = count / 2;
        ListNode updatedNodeList = new ListNode(0, head);
        cureentNode = updatedNodeList;
        for (int i = 0; i < toBeDeleted; i++)            
        {
            cureentNode = cureentNode.next;
        }        
        cureentNode.next = cureentNode.next.next;
        return updatedNodeList.next;
    }
    public ListNode OddEvenList(ListNode head)
    {
        if (head == null || head.next == null)
        {  return head; }
            
        ListNode oddNodes = head;
        ListNode evenNodes = head.next;
        ListNode evenHead = evenNodes;
        while (evenNodes != null && evenNodes.next != null)
        {
            oddNodes.next = evenNodes.next;
            oddNodes = oddNodes.next;

            evenNodes.next = oddNodes.next;
            evenNodes = evenNodes.next;

        }
        oddNodes.next = evenHead;
       
        return head;
    }

    public ListNode ReverseList(ListNode head)
    {
        if (head == null || head.next == null)
        {
            return head;
        }
        int n = 8;
        for (int i = 0; i < n; i++)
        {
            int aa = i + (n / 2) - 1;
        }
        ListNode CurrentNode = head;
        ListNode Backward = null;
        while (CurrentNode != null)
        {
            ListNode Next = CurrentNode.next;
            CurrentNode.next = Backward;
            Backward = CurrentNode;
            CurrentNode = Next;
        }
        return Backward;
    }
    public int PairSum(ListNode head)
    {
        if (head == null || head.next == null)
        {
            return 0;
        }           
        int maxSum = int.MinValue; 
        ListNode slow = head;
        ListNode fast = head;
        ListNode prev = null;
        while (fast != null && fast.next != null)
        {
            prev = slow;
            slow = slow.next;
            fast = fast.next.next;            
        }
        
        prev.next = null;
        ListNode secondHalf = slow;        
        ListNode firstHalf = head;
        prev = null;
        while (secondHalf != null)
        {
            ListNode next = secondHalf.next;
            secondHalf.next = prev;
            prev = secondHalf;
            secondHalf = next;
        }
        while (prev != null)
        {
            maxSum = Math.Max(maxSum, firstHalf.val + prev.val);
            prev = prev.next;
            firstHalf = firstHalf.next;
        }
        return maxSum;
    }
}

public class Solution
{
    public static int OrangesRotting()
    {
        int[][] grid = [[2, 1, 1], [1, 1, 0], [0, 1, 1]];
        int rows = grid.Length;
        int cols = grid[0].Length;

        Queue<(int row, int col)> q = new Queue<(int, int)>();
        int freshOranges = 0;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (grid[r][c] == 2)
                    q.Enqueue((r, c));
                else if (grid[r][c] == 1)
                    freshOranges++;
            }
        }
        if (freshOranges == 0) return 0;
        int minutes = 0;
        int[][] dirs = new int[][] {        
            [1,0],
            [-1,0],        
            [0,1], 
            [0,-1]    
        };        
        while (q.Count > 0)
        {
            int size = q.Count;
            bool isItRottend = false;

            for (int i = 0; i < size; i++)
            {
                var (row, col) = q.Dequeue();

                foreach (var d in dirs)
                {
                    int rowNumber = row + d[0];
                    int colNumber = col + d[1];

                    if (rowNumber >= 0 && rowNumber < rows && colNumber >= 0 && colNumber < cols && grid[rowNumber][colNumber] == 1)
                    {
                        grid[rowNumber][colNumber] = 2;   // rot it
                        freshOranges--;
                        q.Enqueue((rowNumber, colNumber));
                        isItRottend = true;
                    }
                }
            }

            if (isItRottend)
                minutes++;
        }

        return freshOranges == 0 ? minutes : -1;
    }

    public static int NearestExit()
    {
        char[][] maze = [['+', '+', '+'], ['.', '.', '.'], ['+', '+', '+']];
        int[] entrance = [1, 0];
        int mazeRows = maze.Length;
        int mazeCols = maze[0].Length;

        int entranceRow = entrance[0];
        int entranceCol = entrance[1];
        
        int[][] directions = new int[][]
        {
        [1, 0],   
        [-1, 0],  
        [0, 1],   
        [0, -1]  
        };

        
        Queue<(int row, int col, int dist)> queue = new Queue<(int, int, int)>();
        queue.Enqueue((entranceRow, entranceCol, 0));

       
        bool[,] visited = new bool[mazeRows, mazeCols];
        visited[entranceRow, entranceCol] = true;

        while (queue.Count > 0)
        {
            var (row, col, dist) = queue.Dequeue();

            foreach (var direction in directions)
            {
                int rowNumber = row + direction[0];
                int colNumber = col + direction[1];

                // Byond Border
                if (rowNumber < 0 || rowNumber >= mazeRows || colNumber < 0 || colNumber >= mazeCols)
                    continue;

                
                if (maze[rowNumber][colNumber] == '+' || visited[rowNumber, colNumber])
                    continue;

                
                if ((rowNumber == 0 || rowNumber == mazeRows - 1 || colNumber == 0 || colNumber == mazeCols - 1)
                    && !(rowNumber == entranceRow && colNumber == entranceCol))
                {
                    return dist + 1;
                }

                visited[rowNumber, colNumber] = true;
                queue.Enqueue((rowNumber, colNumber, dist + 1));
            }
        }

        return -1;
    }

    public static double[] CalcEquation()
    {
         
    IList<IList<string>> equations = [["a", "b"], ["b", "c"]];
    double[] values = [2.0, 3.0];
    IList<IList<string>> queries = [["a", "c"], ["b", "a"], ["a", "e"], ["a", "a"], ["x", "x"]];
        // 1) Build graph: each variable → list of (neighbor, weight)
        var lettersAndItsValue = new Dictionary<string, List<(string neighbor, double weight)>>();
        for (int i = 0; i < equations.Count; i++)
        {
            string firstLetter = equations[i][0];
            string secondLetter = equations[i][1];
            double value = values[i];

            if (!lettersAndItsValue.ContainsKey(firstLetter))
                lettersAndItsValue[firstLetter] = new List<(string, double)>();

            if (!lettersAndItsValue.ContainsKey(secondLetter))
                lettersAndItsValue[secondLetter] = new List<(string, double)>();

            // A / B = value
            lettersAndItsValue[firstLetter].Add((secondLetter, value));

            // B / A = 1/value
            lettersAndItsValue[secondLetter].Add((firstLetter, 1.0 / value));
        }

        // 2) Process queries
        double[] result = new double[queries.Count];

        for (int i = 0; i < queries.Count; i++)
        {
            string start = queries[i][0];
            string end = queries[i][1];

            result[i] = Evaluate(start, end, lettersAndItsValue);
        }

        return result;
    }
    private static double Evaluate(string start, string end,
    Dictionary<string, List<(string neighbor, double weight)>> graph)
    {
        // If either variable does not exist → impossible
        if (!graph.ContainsKey(start) || !graph.ContainsKey(end))
            return -1.0;

        // a / a = 1
        if (start == end)
            return 1.0;

        var queue = new Queue<(string node, double value)>();
        var visited = new HashSet<string>();

        queue.Enqueue((start, 1.0));
        visited.Add(start);

        while (queue.Count > 0)
        {
            var (current, currentValue) = queue.Dequeue();

            foreach (var edge in graph[current])
            {
                if (visited.Contains(edge.neighbor))
                    continue;

                double nextValue = currentValue * edge.weight;

                if (edge.neighbor == end)
                    return nextValue;

                visited.Add(edge.neighbor);
                queue.Enqueue((edge.neighbor, nextValue));
            }
        }

        return -1.0;
    }

    public static int MinReorder(int n, int[][] connections)
        {
            List<List<(int, int)>> graph = new List<List<(int, int)>>();
            for (int i = 0; i < n; i++)
            {
                graph.Add(new List<(int, int)>());
            }

            foreach (var conn in connections)
            {
                int u = conn[0];
                int v = conn[1];

                graph[u].Add((v, 1));
                graph[v].Add((u, 0));
            }

            int changes = 0;
            bool[] visited = new bool[n];

            void DFS(int city)
            {
                visited[city] = true;
                foreach (var (neighbor, cost) in graph[city])
                {
                    if (!visited[neighbor])
                    {
                        changes += cost;
                        DFS(neighbor);
                    }
                }
            }

            DFS(0);
            return changes;
        }
   
    public static int MinReorderBFS(int n, int[][] cityConnections)
    {
        List<List<(int neighbor, int fixCost)>> connections = new();

        for (int i = 0; i < n; i++)
        {
            connections.Add(new List<(int, int)>());
        }

       
        foreach (var city in cityConnections)
        {
            int from = city[0];
            int to = city[1];
            connections[from].Add((to, 1));
            connections[to].Add((from, 0)); 
        }
       
        bool[] visited = new bool[n];
        Queue<int> q = new();
        q.Enqueue(0);
        visited[0] = true;

        int changes = 0;

        while (q.Count > 0)
        {
            int city = q.Dequeue();

            foreach (var currentcity in connections[city])
            {
                if (!visited[currentcity.neighbor])
                {
                    visited[currentcity.neighbor] = true;
                    changes += currentcity.fixCost;
                    q.Enqueue(currentcity.neighbor);
                }
            }
        }
        return changes;
    }

    public static int CountAlleNewProv(int[][] verbondenSteden)
    {
        
        int count = 0;
        bool[] isHetVerbonden = new bool[verbondenSteden.Length];
        for (int i = 0; i < verbondenSteden.Length; i++)
        {
            if (!isHetVerbonden[i])
            {
                LoopDoorAllSteden(i, verbondenSteden, isHetVerbonden);
                count++;
            }
        }
        return count;
    }

    private static void LoopDoorAllSteden(int i, int[][] verbondenSteden, bool[] isHetVerbonden)
    {
       
        isHetVerbonden[i] = true;
        for (int j = 0; j < verbondenSteden.Length; j++)
        {
            if (verbondenSteden[i][j] == 1 && !isHetVerbonden[j])
            {
                LoopDoorAllSteden(j, verbondenSteden, isHetVerbonden);
            }
        }
    }

    public static int FindCircleNum(int[][] isConnected)
    {
        int count = 0;
        bool[] conected = new bool[isConnected.Length];
        for (int i = 0; i < isConnected.Length; i++)
        {
            if (!conected[i])
            {
                CityDFS(i, isConnected, conected);
                count++;
            }
        }
        return count;
    }

    private static void CityDFS(int i, int[][] isConnected, bool[] conected)
    {
        conected[i] = true;
        for (int j = 0; j < isConnected.Length; j++)
        {
            if (isConnected[i][j] == 1 && !conected[j])
            {
                CityDFS(j, isConnected, conected);
            }
        }
    }

    public static bool VisitidAllRooms(IList<IList<int>> rooms)
    {
        bool[] visited = new bool[rooms.Count];
        int count = 0;
        MyDFS(0, rooms, visited, ref count);
        return count == visited.Length;
    }

    private static void MyDFS(int key, IList<IList<int>> rooms, bool[] visited , ref int count)
    {
       if (!visited[key])
        {
            visited[key] = true;
            count++;
            foreach (int nextKey in rooms[key])
            {
                MyDFS(nextKey, rooms, visited, ref count);
            }
        }
    }

    public static bool CanVisitAllRoomsSuperFast(IList<IList<int>> rooms)
        {

            bool[] visited = new bool[rooms.Count()];
            int counter = 0;

            VisitRoomAndCollectKeys(0, rooms, visited, ref counter);

            if (counter == visited.Length)
                return true;

            return false;
        }

    static void VisitRoomAndCollectKeys(int room, IList<IList<int>> rooms, bool[] visited, ref int counter)
        {
            if (!visited[room])
            {
                visited[room] = true;
                counter++;

                foreach (int key in rooms[room])
                {
                    VisitRoomAndCollectKeys(key, rooms, visited, ref counter);
                }
            }
        }
    

    public static bool CanVisitAllKamers(IList<IList<int>> rooms)
    {
        HashSet<int> visitedRooms = new HashSet<int>();
        Stack<int> TobevisitedRooms = new Stack<int>();
        visitedRooms.Add(0);
        TobevisitedRooms.Push(0);
        while(TobevisitedRooms.Count > 0)
        {
            int currentRoom = TobevisitedRooms.Pop();
            foreach(int key in rooms[currentRoom])
            {
                if(!visitedRooms.Contains(key))
                {
                    visitedRooms.Add(key);
                    TobevisitedRooms.Push(key);
                }
            }
        }
        if(visitedRooms.Count == rooms.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool CanVisitAllRooms(IList<IList<int>> rooms)
    {
        HashSet<int> visitedRoom = new HashSet<int>();
        Stack<int> roomsTobeVisited = new Stack<int>();

        // Start with room 0
        visitedRoom.Add(0);
        roomsTobeVisited.Push(0);

        while (roomsTobeVisited.Count > 0)
        {
            int currentRoom = roomsTobeVisited.Pop();

            foreach (int key in rooms[currentRoom])
            {
                if (!visitedRoom.Contains(key))
                {
                    visitedRoom.Add(key);
                    roomsTobeVisited.Push(key);
                }
            }
        }

        return visitedRoom.Count == rooms.Count;
    }

    public static bool CanVisitAllRoomsSlow(IList<IList<int>> rooms)
    {
        HashSet<int> visitedRooms = new HashSet<int>();
        HashSet<int> keys = new HashSet<int>();
        visitedRooms.Add(0);
        keys.UnionWith(rooms[0]);
        bool foundNewKey = true;
        while(foundNewKey)
        {
            foundNewKey = false;
            for (int i = 1; i < rooms.Count; i++)
            {                
                if (!visitedRooms.Contains(i) && keys.Contains(i))
                {
                    visitedRooms.Add(i);
                    keys.UnionWith(rooms[i]);
                    foundNewKey = true;
                }
            }
        }
        
        return rooms.Count == visitedRooms.Count;
    }

    public static string PredictPartyVictory()
    {
        string senate = "DRRDDRRD";
        Queue<int> Radiant = new Queue<int>();
        Queue<int> Dire = new Queue<int>();
        int n = senate.Length;
        for (int i = 0; i < senate.Length; i++)
        {
            char c = senate[i];
            if (c == 'R')
            {
                Radiant.Enqueue(i);
            }
            else
            {
                Dire.Enqueue(i);
            }
        }
        while (Radiant.Count > 0 && Dire.Count > 0)
        {
            int r = Radiant.Dequeue();
            int d = Dire.Dequeue();
            if (r < d)
            {
                Radiant.Enqueue(r + n);
            }
            else
            {
                Dire.Enqueue(d + n);
            }
        }
        return Radiant.Count > 0 ? "Radiant" : "Dire";
       
    }
    public static string DecodeString()
    {
        string s = "3[a2[dh]]";
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
                //string temp = storedLetter.ToString();
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

    public static int[] AsteroidCollision()
    {
        int[] asteroids = [2, 3, -5];

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
                if (stack.Count > 0 && stack.Peek() == Math.Abs(asteroids[i]))
                {
                    stack.Pop();
                }
                if (stack.Count == 0 || stack.Peek() < 0)
                {
                    stack.Push(asteroids[i]);
                }
            }
        }
        int[] result = stack.ToArray();
        Array.Reverse(result);
        return result;
    }

    public static int[] AsteroidCollision1()
    {
        int[] asteroids = [5, 10, -5];

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
            int aa = nums[i];
            sum += nums[i];
        }
        int maxSum = sum;
        //[1, 12, -5, -6, 50, 3];
        for (int i = k; i < nums.Length; i++)
        {    
            int kk = nums[i];
            int hh = nums[i - k];
            int tty = nums[i] - nums[i - k];
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

