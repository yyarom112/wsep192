using System;
using System.Collections.Generic;

namespace src.Domain
{
    class TreeNodeImpl<T>
    {
        public delegate bool TraversalDataDelegate(T data);
        public delegate bool TraversalNodeDelegate(TreeNodeImpl<T> node);

        private readonly T _data;
        private readonly TreeNodeImpl<T> _parent;
        private readonly int _level;
        private readonly List<TreeNodeImpl<T>> _children;

        public TreeNodeImpl(T data)
        {
            _data = data;
            _children = new List<TreeNodeImpl<T>>();
            _level = 0;
        }

        public TreeNodeImpl()
        {
            _children = new List<TreeNodeImpl<T>>();
            _level = 0;
        }

        public TreeNodeImpl(T data, TreeNodeImpl<T> parent) : this(data)
        {
            _parent = parent;
            _level = _parent != null ? _parent.Level + 1 : 0;
        }

        public int Level { get { return _level; } }
        public int Count { get { return _children.Count; } }
        public bool IsRoot { get { return _parent == null; } }
        public bool IsLeaf { get { return _children.Count == 0; } }
        public T Data { get { return _data; } }
        public TreeNodeImpl<T> Parent { get { return _parent; } }

        public TreeNodeImpl<T> this[int key]
        {
            get { return _children[key]; }
        }

        public void Clear()
        {
            _children.Clear();
        }

        public TreeNodeImpl<T> AddChild(T value)
        {
            TreeNodeImpl<T> node = new TreeNodeImpl<T>(value, this);
            _children.Add(node);

            return node;
        }

        public bool HasChild(T data)
        {
            return FindInChildren(data) != null;
        }

        public TreeNodeImpl<T> FindInChildren(T data)
        {
            int i = 0, l = Count;
            for (; i < l; ++i)
            {
                TreeNodeImpl<T> child = _children[i];
                if (child.Data.Equals(data)) return child;
            }

            return null;
        }

        public bool RemoveChild(TreeNodeImpl<T> node)
        {
            return _children.Remove(node);
        }

        public void Traverse(TraversalDataDelegate handler)
        {
            if (handler(_data))
            {
                int i = 0, l = Count;
                for (; i < l; ++i) _children[i].Traverse(handler);
            }
        }

        public void Traverse(TraversalNodeDelegate handler)
        {
            if (handler(this))
            {
                int i = 0, l = Count;
                for (; i < l; ++i) _children[i].Traverse(handler);
            }
        }
    }
}
