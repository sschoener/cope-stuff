/*
Copyright (c) 2011 Sebastian Schoener

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */
using System.Collections.Generic;
using System.Windows.Forms;

namespace ModTool.Core
{
    public delegate void TreeNodeAddInvoke(TreeNode node);

    static public class TreeNodeExt
    {
        static public void InsertNodeSorted(this TreeNodeCollection parent, TreeNode child)
        {
            var comparer = new TreeNodeFileSorter();
            for (int i = 0; i < parent.Count; i++)
            {
                if (comparer.Compare(parent[i], child) > 0)
                {
                    parent.Insert(i, child);
                    return;
                }
            }
            parent.Add(child);
        }

        static public TreeNode GetFileTreeNodeByPath(this TreeView trv, string path, bool lastIsFile = true)
        {
            string[] elements = path.Split('\\');
            TreeNodeCollection current = trv.Nodes;
            TreeNode currentNode = null;
            int k = elements.Length;
            if (lastIsFile)
                k--;
            string key;
            for (int i = 0; i < k; i++)
            {
                key = 'D' + elements[i];
                if (current.ContainsKey(key))
                {
                    currentNode = current[key];
                    current = currentNode.Nodes;
                }
                else
                    return null;
            }
            if (!lastIsFile)
                return currentNode;
            key = 'F' + elements[elements.Length - 1];
            if (current.ContainsKey(key))
                return current[key];
            return null;
        }

        static public TreeNode GetNodeByPath(this TreeNodeCollection coll, string path)
        {
            string[] elements = path.Split('\\');
            TreeNodeCollection current = coll;
            TreeNode currentNode = null;
            int k = elements.Length;
            for (int i = 0; i < k; i++)
            {
                string key = elements[i];
                if (current.ContainsKey(key))
                {
                    currentNode = current[key];
                    current = currentNode.Nodes;
                }
                else
                    return null;
            }
            return currentNode;
        }
    }

    public class TreeNodeFileSorter : IComparer<TreeNode>
    {
        public int Compare(TreeNode x, TreeNode y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}