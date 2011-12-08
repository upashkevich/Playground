    /// <summary>
    /// This class represents a simple implementation of trie in C#
    /// </summary>
    public class Trie
    {
        public class TrieNode
        {
            // base char represents the smallest char used in this node. It helps to optimize memory usage, while still having O(1) random element access.
            private char baseChar;

            private TrieNode[] nodes;

            // true if this node correspondes to some entry in trie
            // false otherwise (if this node is just an intermediate one)
            public bool IsEntry { get; set; }

            public TrieNode AddChild(char c)
            {
                if (nodes == null)
                {
                    nodes = new TrieNode[1];
                    baseChar = c;
                }
                else if (c < baseChar)
                {
                    var tempArray = new TrieNode[(baseChar - c) + nodes.Length];
                    nodes.CopyTo(tempArray, baseChar - c);
                    nodes = tempArray;
                    baseChar = c;
                }
                else if (c >= baseChar + nodes.Length)
                {
                    Array.Resize(ref nodes, c - baseChar + 1); 
                }

                if (nodes[c - baseChar] == null)
                {
                    nodes[c - baseChar] = new TrieNode();
                }

                return nodes[c - baseChar];
            }

            public TrieNode GetChild(char c)
            {
                var index = c - baseChar;

                if (index < 0 || index >= nodes.Length)
                {
                    return null;
                }

                return nodes[index];
            }
        }

        private TrieNode root = new TrieNode();

        public bool Contains(string key)
        {
            var node = root;

            foreach (var ch in key)
            {
                node = node.GetChild(ch);

                if (node == null)
                {
                    return false;
                }
            }

            return node.IsEntry;
        }

        public void Insert(string key)
        {
            var node = root;

            foreach (var ch in key)
            {
                node = node.AddChild(ch);
            }

            node.IsEntry = true;
        }
    }