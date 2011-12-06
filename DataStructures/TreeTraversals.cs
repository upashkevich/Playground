        public class TreeNode
        {
            public int Key { get; set; }

            public TreeNode Left { get; set; }

            public TreeNode Right { get; set; }
        }

        public static class TreeTraversals
        {
            private static void PreorderRecursive(TreeNode node)
            {
                Console.Write(node.Key + " ");

                if (node.Left != null)
                {
                    PreorderRecursive(node.Left);
                }

                if (node.Right != null)
                {
                    PreorderRecursive(node.Right);
                }
            }

            private static void PreorderIterative(TreeNode root)
            {
                var stack = new Stack<TreeNode>();
                stack.Push(root);

                while (stack.Count > 0)
                {
                    var currentElement = stack.Pop();
                    Console.Write(currentElement.Key + " ");

                    if (currentElement.Right != null)
                    {
                        stack.Push(currentElement.Right);
                    }

                    if (currentElement.Left != null)
                    {
                        stack.Push(currentElement.Left);
                    }
                }
            }

            private static void InorderRecursive(TreeNode node)
            {
                if (node.Left != null)
                {
                    InorderRecursive(node.Left);
                }

                Console.Write(node.Key + " ");

                if (node.Right != null)
                {
                    InorderRecursive(node.Right);
                }
            }

            private static void InorderIterative(TreeNode root)
            {
                var stack = new Stack<TreeNode>();
                stack.Push(root);
                var leftAdded = false;

                while (stack.Count > 0)
                {
                    var currentElement = stack.Peek();

                    if (!leftAdded && currentElement.Left != null)
                    {
                        stack.Push(currentElement.Left);
                        continue;
                    }

                    currentElement = stack.Pop();
                    leftAdded = true;
                    Console.Write(currentElement.Key + " ");

                    if (currentElement.Right != null)
                    {
                        stack.Push(currentElement.Right);
                        leftAdded = false;
                    }
                }
            }

            private static void PostorderRecursive(TreeNode node)
            {
                if (node.Left != null)
                {
                    PostorderRecursive(node.Left);
                }

                if (node.Right != null)
                {
                    PostorderRecursive(node.Right);
                }

                Console.Write(node.Key + " ");
            }

            private static void PostorderIterative(TreeNode root)
            {
                var stack = new Stack<TreeNode>();
                stack.Push(root);
                TreeNode currentNode = null;
                TreeNode prevNode;

                while (stack.Count > 0)
                {
                    prevNode = currentNode;
                    currentNode = stack.Peek();

                    if (prevNode == null || prevNode.Left == currentNode || prevNode.Right == currentNode)
                    {
                        // in this case we are going down the tree
                        if (currentNode.Left != null)
                        {
                            stack.Push(currentNode.Left);
                        }
                        else if (currentNode.Right != null)
                        {
                            stack.Push(currentNode.Right);
                        }
                        else
                        {
                            currentNode = stack.Pop();
                            Console.Write(currentNode.Key + " ");
                        }
                    }
                    else if (currentNode.Left == prevNode)
                    {
                        // in this case we are going up the tree from the left
                        if (currentNode.Right != null)
                        {
                            stack.Push(currentNode.Right);
                        }
                        else
                        {
                            currentNode = stack.Pop();
                            Console.Write(currentNode.Key + " ");
                        }
                    }
                    else
                    {
                        // in this case we are going up the tree from the right
                        currentNode = stack.Pop();
                        Console.Write(currentNode.Key + " ");
                    }
                }
            }

            public static void TestTraversals(TreeNode root)
            {
                PreorderRecursive(root);
                Console.WriteLine();
                PreorderIterative(root);
                Console.WriteLine();
                InorderRecursive(root);
                Console.WriteLine();
                InorderIterative(root);
                Console.WriteLine();
                PostorderRecursive(root);
                Console.WriteLine();
                PostorderIterative(root);
                Console.WriteLine();
            }
        }