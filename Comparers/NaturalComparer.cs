﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FinalstreamCommons.Comparers
{
    /// <summary>
    ///     エクスプローラと同じソートを行う。
    /// </summary>
    /// <remarks>http://www.codeproject.com/Articles/22517/Natural-Sort-Comparer</remarks>
    public class NaturalComparer : IComparer, IDisposable
    {
        private Dictionary<string, string[]> table;

        public NaturalComparer()
        {
            table = new Dictionary<string, string[]>();
        }

        public virtual int Compare(object s1, object s2)
        {
            var x = s1.ToString();
            var y = s2.ToString();
            if (x == y)
            {
                return 0;
            }
            string[] x1, y1;
            if (!table.TryGetValue(x, out x1))
            {
                x1 = Regex.Split(x.Replace(" ", ""), "([0-9]+)");
                table.Add(x, x1);
            }
            if (!table.TryGetValue(y, out y1))
            {
                y1 = Regex.Split(y.Replace(" ", ""), "([0-9]+)");
                table.Add(y, y1);
            }

            for (var i = 0; i < x1.Length && i < y1.Length; i++)
            {
                if (x1[i] != y1[i])
                {
                    return PartCompare(x1[i], y1[i]);
                }
            }
            if (y1.Length > x1.Length)
            {
                return 1;
            }
            if (x1.Length > y1.Length)
            {
                return -1;
            }
            return 0;
        }

        public void Dispose()
        {
            table.Clear();
            table = null;
        }

        private static int PartCompare(string left, string right)
        {
            int x, y;
            if (!int.TryParse(left, out x))
            {
                return left.CompareTo(right);
            }

            if (!int.TryParse(right, out y))
            {
                return left.CompareTo(right);
            }

            return x.CompareTo(y);
        }
    }
}