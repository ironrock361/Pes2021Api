﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pes2021Api
{
    [Serializable]
    public class BitList : IList<bool>, ICloneable
    {
        private BitArray _backingArray;

        [NonSerialized]
        private int _version;

        public int Count { get; private set; }

        #region Constructors
        public BitList(int capacity = 32, bool defaultValue = false)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            _backingArray = new BitArray(GetNearestOptimalCapacity(capacity), defaultValue);
            Count = capacity;
        }

        public BitList(int[] data, int count)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            _backingArray = new BitArray(data);
            if (_backingArray.Count < count)
            {
                throw new ArgumentException("The data set has less values than count.");
            }
            Count = count;
        }

        public BitList(bool[] data, int count)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            _backingArray = new BitArray(data);
            if (_backingArray.Count < count)
            {
                throw new ArgumentException("The data set has less values than count.");
            }
            Count = count;
        }

        public BitList(BitList bitList) : this(bitList.GetData(), bitList.Count)
        { }
        #endregion

        public bool this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException($"{index} is out of bounds [0;{Count - 1}]");
                }

                return _backingArray[index];
            }
            set
            {
                if (index < 0 || index > Count)
                {
                    throw new IndexOutOfRangeException($"{index} is out of bounds [0;{Count - 1}]");
                }

                if (index == Count)
                {
                    Add(value);
                    return;
                }

                _backingArray[index] = value;
                IncrementVersion();
            }
        }

        public void Add(bool item)
        {
            EnsureCapacity(Count + 1);
            _backingArray[Count] = item;
            Count++;
            IncrementVersion();
        }

        public void Insert(int index, bool item)
        {
            if (index == Count)
            {
                Add(item);
                return;
            }

            bool[] data = GetBoolArray();
            bool[] newData = new bool[data.Length + 1];
            Array.Copy(data, 0, newData, 0, index);
            Array.Copy(data, index, newData, index + 1, data.Length - index);
            newData[index] = item;
            _backingArray = new BitArray(newData);
            Count++;
            IncrementVersion();
        }

        public void Clear()
        {
            Count = 0;
            Trim();
            IncrementVersion();
        }

        public void RemoveAt(int index)
        {
            if (index == Count - 1)
            {
                Count--;
                return;
            }

            bool[] data = GetBoolArray();
            bool[] newData = new bool[data.Length - 1];
            Array.Copy(data, 0, newData, 0, index - 1);
            Array.Copy(data, index + 1, newData, index, data.Length - index - 1);
            _backingArray = new BitArray(newData);
            Count--;
            IncrementVersion();
        }

        public bool Remove(bool item)
        {
            int index = IndexOf(item);
            if (index == -1)
            {
                return false;
            }

            RemoveAt(index);
            return true;
        }

        public void CopyTo(bool[] array, int arrayIndex)
        {
            if (array.Length - arrayIndex < Count)
            {
                throw new ArgumentException("array is too small.");
            }

            bool[] data = new bool[_backingArray.Length];
            _backingArray.CopyTo(data, 0);
            Array.Copy(data, 0, array, arrayIndex, Count);
        }

        public bool IsReadOnly => false;

        public bool Contains(bool item)
        {
            // Can be optimized if needed.
            foreach (bool b in this)
            {
                if (b == item)
                {
                    return true;
                }
            }

            return false;
        }

        public int IndexOf(bool item)
        {
            // Can be optimized if needed.
            for (var i = 0; i < this.Count; i++)
            {
                if (this[i] == item)
                {
                    return i;
                }
            }

            return -1;
        }

        public IEnumerator<bool> GetEnumerator()
        {
            int version = _version;
            for (int i = 0; i < Count; i++)
            {
                if (_version != version)
                {
                    throw new InvalidOperationException("Collection modified");
                }
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public object Clone()
        {
            return new BitList(this);
        }

        public int[] GetData()
        {
            int[] currentContents = new int[_backingArray.Count / 4];
            _backingArray.CopyTo(currentContents, 0);
            int[] newArray = new int[GetNearestOptimalCapacity(Count) / 4];
            Array.Copy(currentContents, newArray, newArray.Length);
            return newArray;
        }

        public bool[] GetBoolArray()
        {
            bool[] data = new bool[Count];
            CopyTo(data, 0);
            return data;
        }

        private void IncrementVersion()
        {
            unchecked
            {
                _version++;
            }
        }

        #region Capacity
        public void EnsureCapacity(int requestedCapacity)
        {
            if (requestedCapacity < _backingArray.Count)
            {
                // Array is already large enough.
                return;
            }

            int currentCapacityDoubled = _backingArray.Count * 2;
            if (requestedCapacity < currentCapacityDoubled)
            {
                // To prevent frequent re-allocations, the capacity is at least doubled.
                SetCapacity(currentCapacityDoubled);
            }

            // Otherwise, resize to fit requestedCapacity
            SetCapacity(GetNearestOptimalCapacity(requestedCapacity));
        }

        public void Trim()
        {
            SetCapacity(GetNearestOptimalCapacity(Count));
        }

        private void SetCapacity(int capacity)
        {
            if (capacity > _backingArray.Count)
            {
                // Increase capacity.
                int[] newArray = new int[capacity / 4];
                _backingArray.CopyTo(newArray, 0);
                _backingArray = new BitArray(newArray);
            }
            else if (capacity < _backingArray.Count)
            {
                // Decrease capacity
                int[] currentContents = new int[_backingArray.Count / 4];
                _backingArray.CopyTo(currentContents, 0);
                int[] newArray = new int[capacity / 4];
                Array.Copy(currentContents, newArray, newArray.Length);
                _backingArray = new BitArray(newArray);
            }
        }

        private int GetNearestOptimalCapacity(int requestedCapacity)
        {
            // The optimal capacity is the smallest multiple of 4 larger than requestedCapacity.
            // 4 because BitArray uses ints for storage.
            return (int)Math.Ceiling(requestedCapacity / 4.0d) * 4;
        }
        #endregion

        [OnSerializing]
        public void OnSerializing(StreamingContext context)
        {
            Trim();
        }
    }
}
