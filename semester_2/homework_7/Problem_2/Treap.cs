using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomSet
{
    /// <summary>
    /// Implements simple treap
    /// </summary>
    public class Treap<T> : ISet<T>
    {
        /// <summary>
        /// The root of the treap
        /// </summary>
        private TreapTreeElement mainTree;

        /// <summary>
        /// Random priority generator
        /// </summary>
        private Random randomGenerator = new Random();

        /// <summary>
        /// Sets seed for the random generator.
        /// If == 0 then default seed to be used.
        /// </summary>
        /// <returns>Random seed (0 means default)</returns>
        public int RandomSeed
        { 
            set => this.randomGenerator = value == 0 ?
                    new Random() : new Random(value);
        }

        /// <summary>
        /// Count of the elements in the treap
        /// </summary>
        /// <returns>Count of the elements</returns>
        public int Count { get; private set; }

        /// <summary>
        /// Returns true if the treap is read only
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Inserts new element into the treap
        /// </summary>
        /// <param name="newElementValue">Value to insert</param>
        /// <returns>True if new element was 
        /// inserted successfully</returns>
        public bool Add(T newElementValue)
        {
            if (newElementValue == null || this.Contains(newElementValue))
            {
                return false;
            }

            var newElementTree = 
                new TreapTreeElement(newElementValue, this.randomGenerator.Next());

            var oldCount = this.Count;
            var (leftSubtree, rightSubtree) = this.SpliceTree(newElementValue);

            leftSubtree = this.MergeTree(leftSubtree, newElementTree);
            this.RepairTreap(leftSubtree, rightSubtree, oldCount + 1);

            return true;
        }

        /// <summary>
        /// Inserts new element into the treap
        /// </summary>
        /// <param name="newElementValue">Value to insert</param>
        /// <returns>True if new element was 
        /// inserted successfully</returns>
        void ICollection<T>.Add(T newElementValue)
        {
            this.Add(newElementValue);
        }

        /// <summary>
        /// Removes all the elements from the treap
        /// </summary>
        public void Clear()
        {
            this.mainTree = null;
            this.Count = 0;
        }

        /// <summary>
        /// Checks if value is in treap
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>True if element with given value is in treap</returns>
        public bool Contains(T value)
        {
            var currentElement = this.mainTree;
            while (currentElement != null && 
                currentElement.Value.GetHashCode() != value.GetHashCode())
            {
                if (currentElement.Value.GetHashCode() < value.GetHashCode())
                {
                    currentElement = currentElement.RightSon;
                }
                else
                {
                    currentElement = currentElement.LeftSon;
                }
            }

            return !(currentElement == null);
        }

        /// <summary>
        /// Copies the content of the treap into array
        /// </summary>
        /// <param name="destination">Destination array</param>
        /// <param name="count">Count of elements to copy</param>
        public void CopyTo(T[] destination, int count)
        {
            if (count < 0)
            {
                throw new ArgumentException("Count should be non-negative!");
            }

            var currentCount = 0;
            foreach (var element in this)
            {
                if (currentCount >= count)
                {
                    break;
                }

                destination[currentCount] = element;
                currentCount++;
            }
        }

        /// <summary>
        /// Removes all elements in the specified
        /// collection from the current set.
        /// </summary>
        /// <param name="inputCollection">Input collection</param>
        public void ExceptWith(IEnumerable<T> inputCollection)
        {
            foreach (var element in inputCollection)
            {
                this.Remove(element);
            }
        }

        /// <summary>
        /// Provides enumerator of the treap
        /// </summary>
        /// <returns>Valid enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            if (this.Count == 0)
            {
                yield break;
            }

            var traversalRadius = new Stack<TreapTreeElement>();

            var currentVertex = this.mainTree;
            traversalRadius.Push(currentVertex);
            while (traversalRadius.Count != 0)
            {
                while (currentVertex.LeftSon != null)
                {
                    currentVertex = currentVertex.LeftSon;
                    traversalRadius.Push(currentVertex);
                }

                currentVertex = traversalRadius.Pop();
                yield return currentVertex.Value;

                while (traversalRadius.Count != 0 && currentVertex.RightSon == null)
                {
                    currentVertex = traversalRadius.Pop();
                    yield return currentVertex.Value;                    
                }

                if (currentVertex.RightSon != null)
                {
                    currentVertex = currentVertex.RightSon;
                    traversalRadius.Push(currentVertex);
                }
            }
        }

        /// <summary>
        /// Provides enumerator of the treap
        /// </summary>
        /// <returns>Valid enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Modifies the current set so that it contains
        /// only elements that are also in a specified collection.
        /// </summary>
        /// <param name="inputCollection">Input collection</param>
        public void IntersectWith(IEnumerable<T> inputCollection)
        {
            var commonElements = new List<T>();
            foreach (var element in inputCollection)
            {
                if (this.Remove(element))
                {
                    commonElements.Insert(0, element);
                }
            }

            this.Clear();

            foreach(var element in commonElements)
            {
                this.Add(element);
            }
        }   

        /// <summary>
        /// Determines whether the current set is a 
        /// proper (strict) subset of a specified collection.
        /// </summary>
        /// <param name="inputCollection">Input collection</param>
        /// <returns>
        /// True if the treap is proper 
        /// subset of given collection
        /// </returns>
        public bool IsProperSubsetOf(IEnumerable<T> inputCollection)
        {
            var secondTreap = this.BuildTreap(inputCollection);

            return this.IsSubsetOf(secondTreap) 
                    && this.Count != secondTreap.Count;
        }

        
        /// <summary>
        /// Determines whether the current set is a 
        /// proper (strict) superset of a specified collection.
        /// </summary>
        /// <param name="inputSet">Input collection</param>
        /// <returns>
        /// True if the treap is proper 
        /// superset of given collection
        /// </returns>
        public bool IsProperSupersetOf(IEnumerable<T> inputSet)
        {
            var secondTreap = this.BuildTreap(inputSet);

            foreach (var element in inputSet)
            {
                if (!this.Contains(element))
                {
                    return false;
                }
            }

            return this.Count > secondTreap.Count;
        }


        /// <summary>
        /// Determines whether the current set is a 
        /// subset of a specified collection.
        /// </summary>
        /// <param name="inputSet">Input collection</param>
        /// <returns>
        /// True if the treap is a 
        /// subset of given collection
        /// </returns>
        public bool IsSubsetOf(IEnumerable<T> inputSet)
        {
            var secondTreap = this.BuildTreap(inputSet);

            return this.IsSubsetOf(secondTreap);
        }


        /// <summary>
        /// Determines whether the current set is a 
        /// superset of a specified collection.
        /// </summary>
        /// <param name="inputCollection">Input collection</param>
        /// <returns>
        /// True if the treap is
        /// superset of given collection
        /// </returns>
        public bool IsSupersetOf(IEnumerable<T> inputCollection)
        {
            foreach (var element in inputCollection)
            {
                if (!this.Contains(element))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether the current set
        /// overlaps with the specified collection.
        /// </summary>
        /// <param name="inputCollection">Input collection</param>
        /// <returns>
        /// True if current set overlaps 
        /// with the collection
        /// </returns>
        public bool Overlaps(IEnumerable<T> inputCollection)
        {
            foreach (var element in inputCollection)
            {
                if (this.Contains(element))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes a specific object from the treap
        /// </summary>
        /// <param name="value">Value to remove</param>
        /// <returns>True if the value was removes successfully</returns>
        public bool Remove(T value)
        {
            if (value == null)
            {
                return false;
            }

            var oldCount = this.Count;
            var (leftSubtree, rightSubtree) = this.SpliceTree(value);

            if (leftSubtree == null)
            {
                this.RepairTreap(leftSubtree, rightSubtree, oldCount);
                return false;
            }

            var currentElement = leftSubtree;
            var currentParent = leftSubtree;
            while (currentElement.RightSon != null)
            {
                currentParent = currentElement;
                currentElement = currentElement.RightSon;
            }

            if (!currentElement.Value.Equals(value))
            {
                this.RepairTreap(leftSubtree, rightSubtree, oldCount);
                return false;
            }

            if (currentElement == leftSubtree)
            {
                leftSubtree = currentElement.LeftSon;
            }
            else
            {
                currentParent.RightSon = currentElement.LeftSon;
            }

            this.RepairTreap(leftSubtree, rightSubtree, oldCount - 1);

            return true;
        }

        /// <summary>
        /// Determines whether the current set and 
        /// the specified collection contain the same elements.
        /// </summary>
        /// <param name="inputCollection">Input collection</param>
        /// <returns>True if sets are equal</returns>
        public bool SetEquals(IEnumerable<T> inputCollection)
        {
            var secondTreap = this.BuildTreap(inputCollection);

            return this.IsSubsetOf(secondTreap) && 
                    this.Count == secondTreap.Count;
        }

        /// <summary>
        /// Modifies the current set so that it contains only elements 
        /// that are present either in the current set
        /// or in the specified collection, but not both.
        /// </summary>
        /// <param name="inputCollection">Input collection</param>
        public void SymmetricExceptWith(IEnumerable<T> inputCollection)
        {
            var secondTreap = new Treap<T>();

            foreach (var element in inputCollection)
            {
                if (!this.Contains(element))
                {
                    secondTreap.Add(element);
                }
                else
                {
                    this.Remove(element);
                }
            }

            foreach (var element in this)
            {
                secondTreap.Remove(element);
            }

            this.UnionWith(secondTreap);
        }

        /// <summary>
        /// Modifies the current set so that it contains all elements
        /// that are present in the current set, 
        /// in the specified collection, or in both.
        /// </summary>
        /// <param name="inputCollection">Input collection</param>
        public void UnionWith(IEnumerable<T> inputCollection)
        {
            if (inputCollection == null)
            {
                return;
            }

            foreach (var element in inputCollection)
            {
                this.Add(element);
            }
        }

        /// <summary>
        /// Merges treap which has keys larger than
        /// keys in left treap and the right treap
        /// </summary>
        /// <param name="leftSubtee">
        /// First subtree to merge (keys >= keys in current treap)
        /// </param>
        /// <param name="rightSubtree">
        /// Second subtree to merge (keys < keys in current treap)
        /// </param>
        TreapTreeElement MergeTree(
            TreapTreeElement leftSubtree,
            TreapTreeElement rightSubtree)
        {
            if (leftSubtree == null || rightSubtree == null)
            {
                return leftSubtree ?? rightSubtree;
            }

            if (leftSubtree.Priority <= rightSubtree.Priority)
            {
                rightSubtree.LeftSon = MergeTree(leftSubtree, rightSubtree.LeftSon);        
                return rightSubtree;
            }
            else
            {
                leftSubtree.RightSon = MergeTree(leftSubtree.RightSon, rightSubtree);
                return leftSubtree;
            }
        }

        /// <summary>
        /// Constructs treap from two subtrees
        /// </summary>
        /// <param name="leftSubtee">Left subtree (keys < r.subtee)</param>
        /// <param name="rightSubtree">Right subtree (keys > l.subtree)</param>
        /// <param name="elementCount">Summary element count</param>
        protected void RepairTreap(
            TreapTreeElement leftSubtee,
            TreapTreeElement rightSubtree,
            int elementCount)
        {
            this.mainTree = this.MergeTree(leftSubtee, rightSubtree);
            this.Count = elementCount;
        }

        /// <summary>
        /// Divides a trea into two treap relative to a given value.
        /// Current treap will become empty
        /// </summary>
        /// <param name="middleValue">Middle value</param>
        /// <returns>Left and right trees from result</returns>
        protected (TreapTreeElement, TreapTreeElement) SpliceTree(T middleValue)
        {
            (TreapTreeElement, TreapTreeElement) SpliceIntoTwoTrees(
                TreapTreeElement inputTree)
            {
                if (inputTree == null)
                {
                    return (null, null);
                }

                if (inputTree.Value.GetHashCode() <= middleValue.GetHashCode())
                {
                    var (leftBranch, rightBranch) = 
                            SpliceIntoTwoTrees(inputTree.RightSon);
                    inputTree.RightSon = leftBranch;

                    return (inputTree, rightBranch);
                }
                else
                {
                    var (leftBranch, rightBranch) = 
                            SpliceIntoTwoTrees(inputTree.LeftSon);
                    inputTree.LeftSon = rightBranch;

                    return (leftBranch, inputTree);
                }
            }

            var (leftSubtree, rightSubtree) = SpliceIntoTwoTrees(this.mainTree);

            this.Clear();

            return (leftSubtree, rightSubtree);
        }

        /// <summary>
        /// Check if current treap is the subset of given treap
        /// </summary>
        /// <param name="inputTreap">Input treap</param>
        /// <returns>True if current treap is 
        /// the subset of given treap</returns>
        private bool IsSubsetOf(Treap<T> inputTreap)
        {
            foreach (var element in this)
            {
                if (!inputTreap.Contains(element))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Builds treap from given collection
        /// </summary>
        /// <param name="inputCollection">Input collection</param>
        /// <returns>Treap built from given collection</returns>
        private Treap<T> BuildTreap(IEnumerable<T> inputCollection)
        {
            var newTreap = new Treap<T>();
            foreach(var element in inputCollection)
            {
                newTreap.Add(element);
            }

            return newTreap;
        }

        /// <summary>
        /// Implements single element of the treap
        /// </summary>
        protected class TreapTreeElement
        {
            /// <summary>
            /// Initialises new instance of TreapTreeElement
            /// </summary>
            /// <param name="value">Value of the new element</param>
            /// <param name="priority">Priority of the new element</param>
            public TreapTreeElement(T value, int priority)
            {
                this.Value = value;
                this.Priority = priority;
            }

            /// <summary>
            /// Value of the element
            /// </summary>
            public T Value { get; private set; }

            /// <summary>
            /// Priority of the element
            /// </summary>
            public int Priority { get; private set; }

            /// <summary>
            /// Right son of the element
            /// </summary>
            public TreapTreeElement RightSon { get; set; }

            /// <summary>
            /// Left son of the element
            /// </summary>
            public TreapTreeElement LeftSon { get; set; }
        }
    }
}