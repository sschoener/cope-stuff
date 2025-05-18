using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace cope
{
    /// <summary>
    /// Implements a patricia tree for strings.
    /// </summary>
    public class PatriciaTree<T> : IDictionary<string, T>
    {
        private readonly Node _root = new Node("", null, default(T));
        private int _numberOfElements;
        private bool _recount;

        class Node
        {
            private bool _isEndOfWord;
            private T _value;

            public Node(string substring, Node parent)
            {
                Substring = substring;
                Parent = parent;
            }

            public Node(string substring, Node parent, T value)
            {
                Substring = substring;
                Value = value;
                Parent = parent;
            }

            public Dictionary<string, Node> Successors = new Dictionary<string, Node>();

            /// <summary>
            /// The value stored at this node.
            /// </summary>
            public T Value
            {
                get { return _value; }
                set
                {
                    IsEndOfWord = true;
                    _value = value;
                }
            }

            /// <summary>
            /// Whether this node marks the end of a word.
            /// </summary>
            public bool IsEndOfWord
            {
                get { return _isEndOfWord; }
                set
                {
                    if (!value)
                        Value = default(T);
                    _isEndOfWord = value;
                }
            }

            /// <summary>
            /// The substring at the edge leaving to this node.
            /// </summary>
            public string Substring;

            /// <summary>
            /// The parent node of this node.
            /// </summary>
            public Node Parent;
        }

        /// <summary>
        /// Navigates to the first node at which splitting is necessary, starting from
        /// the specified node.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="item"></param>
        /// <param name="rest"></param>
        /// <returns></returns>
        static Node NavigateToEnd(Node from, string item, out string rest)
        {
            Node currentNode = from;
            string toProcess = item;
            while (toProcess.Length > 0)
            {
                var next = currentNode.Successors.Keys.FirstOrDefault(toProcess.StartsWith);
                if (next == null)
                    break;
                toProcess = toProcess.Substring(next.Length);
                currentNode = currentNode.Successors[next];
            }
            rest = toProcess;
            return currentNode;
        }

        /// <summary>
        /// Collects all strings starting at a given node. Adds the given
        /// prefix to every string thus encountered.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        static IEnumerable<KeyValuePair<string, T>> CollectFrom(Node from, string prefix)
        {
            // We are using a more elaborate version of a dfs in which we are
            // storing enumerators instead of nodes in order to save stack space.
            var enums = new Stack<IEnumerator<KeyValuePair<string, Node>>>();
            var prefixes = new Stack<string>();

            if (from.IsEndOfWord)
                yield return new KeyValuePair<string, T>(prefix, from.Value);

            enums.Push(from.Successors.GetEnumerator());
            prefixes.Push(prefix);
            while (enums.Count > 0)
            {
                var currentEnum = enums.Peek();
                if (!currentEnum.MoveNext())
                {
                    enums.Pop();
                    prefixes.Pop();
                    continue;
                }
                var currentNode = currentEnum.Current.Value;
                var oldPrefix = prefixes.Peek();
                var currentPrefix = oldPrefix + currentNode.Substring;
                if (currentNode.IsEndOfWord)
                    yield return new KeyValuePair<string, T>(currentPrefix, currentNode.Value);
                if (currentNode.Successors.Count > 0)
                {
                    enums.Push(currentNode.Successors.GetEnumerator());
                    prefixes.Push(currentPrefix);
                }
            }
        }

        /// <summary>
        /// Returns all words in the tree with the given prefix.
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<string, T>> GetWordsForPrefix(string prefix)
        {
            return GetSuffixesForPrefix(prefix).Select(s => new KeyValuePair<string, T>(prefix + s.Key, s.Value));
        }

        /// <summary>
        /// Returns all suffixes for a given prefix.
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<string, T>> GetSuffixesForPrefix(string prefix)
        {
            string rest;
            var node = NavigateToEnd(_root, prefix, out rest);
            // if the prefix has been fully consumed, start the search right here.
            if (rest.Length == 0)
                return CollectFrom(node, "");

            // otherwise see if there is any edge which has the rest as a prefix
            var edge = node.Successors.Keys.FirstOrDefault(k => k[0] == rest[0]);
            if (edge == null) return Enumerable.Empty<KeyValuePair<string, T>>();
            return CollectFrom(node.Successors[edge], edge.Substring(rest.Length));
        }

        /// <summary>
        /// Drops all strings with a given prefix.
        /// </summary>
        /// <param name="prefix"></param>
        public void DropPrefix(string prefix)
        {
            string rest;
            var node = NavigateToEnd(_root, prefix, out rest);
            if (rest.Length == 0)
            {
                RemoveNode(node);
                _recount = true;
            }
            else
            {
                var edge = node.Successors.Keys.FirstOrDefault(k => k[0] == rest[0]);
                if (edge != null && edge.StartsWith(rest))
                {
                    RemoveNode(node.Successors[edge]);
                    _recount = true;
                }
            }
        }

        /// <summary>
        /// Replaces all entries that start with a given prefix with entries with
        /// the given other prefix. Overrides existing entries.
        /// </summary>
        /// <param name="oldPrefix"></param>
        /// <param name="newPrefix"></param>
        public void ChangePrefix(string oldPrefix, string newPrefix)
        {
            var suffixes = GetSuffixesForPrefix(oldPrefix).ToList();
            DropPrefix(oldPrefix);

            string newRest;
            var newBase = NavigateToEnd(_root, newPrefix, out newRest);
            foreach (var s in suffixes)
                AddOrUpdate(newBase, newRest + s.Key, s.Value, false);
            _recount = true;
        }

        /// <summary>
        /// Adds or updates the given string at node. Returns true iff a new word was added.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="exceptionOnUpdate"></param>
        private bool AddOrUpdate(Node to, string key, T value, bool exceptionOnUpdate)
        {
            string rest;
            var node = NavigateToEnd(to, key, out rest);
            if (rest.Length == 0)
            {
                if (node.IsEndOfWord && exceptionOnUpdate)
                    throw new ArgumentException("There already is a value for the key " + key, key);
                bool hadValue = node.IsEndOfWord;
                node.Value = value;
                return !hadValue;
            }

            // find the outgoing edge; we only need to compare the first letter
            // because if two edges had the same starting letter, they could be merged.
            var edge = node.Successors.Keys.FirstOrDefault(k => k[0] == rest[0]);
            if (edge == null)
                node.Successors.Add(rest, new Node(rest, node, value));
            else
            {
                // get the longest common prefix
                var commonPrefixLength = LengthOfCommonPrefix(rest, edge);
                var commonPrefix = rest.Substring(0, commonPrefixLength);
                var suffix1 = rest.Substring(commonPrefixLength);
                var suffix2 = edge.Substring(commonPrefixLength);

                // remove the old edge and insert a new one with the common prefix.
                var commonNode = new Node(commonPrefix, node);
                node.Successors.Add(commonPrefix, commonNode);

                var newEntry = new Node(suffix1, commonNode, value);
                commonNode.Successors.Add(suffix1, newEntry);
                var oldChild = node.Successors[edge];
                oldChild.Parent = commonNode;
                oldChild.Substring = suffix2;
                commonNode.Successors.Add(suffix2, oldChild);
                node.Successors.Remove(edge);
            }
            return true;
        }

        private static int LengthOfCommonPrefix(string str1, string str2)
        {
            int length = Math.Min(str1.Length, str2.Length);
            for (int i = 0; i < length; i++)
            {
                if (str1[i] != str2[i])
                    return i;
            }
            return length;
        }

        private static void RemoveNode(Node node)
        {
            node.IsEndOfWord = false;
            node.Successors.Clear();
            var currentNode = node;
            while (currentNode.Successors.Count == 0 && !currentNode.IsEndOfWord)
            {
                var key = currentNode.Substring;
                currentNode = currentNode.Parent;
                if (currentNode == null)
                    break;
                currentNode.Successors.Remove(key);
                // if the current node has only one child and does not mark
                // the end of a word, merge it with its child.
                if (currentNode.Successors.Count == 1 && !currentNode.IsEndOfWord && currentNode.Parent != null)
                {
                    var childNode = currentNode.Successors.FirstOrDefault().Value;
                    currentNode.Parent.Successors.Remove(currentNode.Substring);
                    currentNode.Substring += childNode.Substring;
                    currentNode.Parent.Successors.Add(currentNode.Substring, currentNode);
                    currentNode.Successors = childNode.Successors;
                    currentNode.Value = childNode.Value;
                    currentNode.IsEndOfWord = childNode.IsEndOfWord;
                    break;
                }
            }
        }

        public IEnumerator<KeyValuePair<string, T>> GetEnumerator()
        {
            return CollectFrom(_root, "").GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Adds a key-value pair to the collection. Throws an exception if
        /// there already is an element with this key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, T value)
        {
            if (AddOrUpdate(_root, key, value, true))
                _numberOfElements++;
        }

        /// <summary>
        /// Adds a key-value pair to the collection. Throws an exception if
        /// there already is an element with this key.
        /// </summary>
        public void Add(KeyValuePair<string, T> item)
        {
            Add(item.Key, item.Value);
        }

        public void AddOrUpdate(string key, T value)
        {
            if (AddOrUpdate(_root, key, value, false))
                _numberOfElements++;
        }

        public void Clear()
        {
            _root.Successors.Clear();
        }

        public bool Contains(KeyValuePair<string, T> item)
        {
            return ContainsKey(item.Key);
        }

        public void CopyTo(KeyValuePair<string, T>[] array, int arrayIndex)
        {
            int idx = arrayIndex;
            foreach (var s in this)
            {
                if (idx >= array.Length)
                    break;
                array[idx] = s;
                idx++;
            }
        }

        public bool Remove(KeyValuePair<string, T> item)
        {
            return Remove(item.Key);
        }

        public bool ContainsKey(string key)
        {
            string rest;
            var end = NavigateToEnd(_root, key, out rest);
            return rest.Length == 0 && end.IsEndOfWord;
        }

        public bool Remove(string key)
        {
            string rest;
            var node = NavigateToEnd(_root, key, out rest);
            if (rest.Length > 0 || !node.IsEndOfWord)
                return false;

            _numberOfElements--;

            RemoveNode(node);
            return true;
        }

        public bool TryGetValue(string key, out T value)
        {
            string rest;
            var end = NavigateToEnd(_root, key, out rest);
            value = end.IsEndOfWord ? end.Value : default(T);
            return rest.Length == 0 && end.IsEndOfWord;
        }

        public T this[string key]
        {
            get
            {
                T val;
                if (!TryGetValue(key, out val))
                    throw new KeyNotFoundException("No element for key " + key);
                return val;
            }
            set
            {
                AddOrUpdate(key, value);
            }
        }

        public ICollection<string> Keys
        {
            get { return this.Select(kvp => kvp.Key).ToList(); }
        }

        public ICollection<T> Values
        {
            get { return this.Select(kvp => kvp.Value).ToList(); }
        }

        public int Count
        {
            get
            {
                if (_recount)
                {
                    _numberOfElements = 0;
                    foreach (var elem in this)
                        _numberOfElements++;
                }
                return _numberOfElements;
            }
        }

        public bool IsReadOnly { get { return false; } }
    }
}
