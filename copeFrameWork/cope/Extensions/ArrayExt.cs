#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace cope.Extensions
{
    /// <summary>
    /// Static class offering various extension methods for every array type.
    /// </summary>
    public static class ArrayExt
    {
        #region Array Queries

        /// <summary>
        /// Counts how often o2 appears in this array using the Equals method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o1"></param>
        /// <param name="o2"></param>
        /// <returns></returns>
        public static int CountEqual<T>(this T[] o1, T o2)
        {
            return o1.Count(p => o2.Equals(p));
        }

        /// <summary>
        /// Returns whether the contents of the two arrays are equal by using the Equals method.
        /// </summary>
        /// <param name="o1"></param>
        /// <param name="o2"></param>
        /// <returns></returns>
        public static bool IsEqual<T>(this T[] o1, T[] o2)
        {
            if (o1.Length != o2.Length)
                return false;
            for (int i = 0; i < o1.Length; i++)
            {
                if (!o1.Equals(o2))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Returns 'length' Ts from a T array starting at the specified index.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index">Index of the first value to get.</param>
        /// <param name="length">Number of values to get.</param>
        /// <returns></returns>
        public static T[] GetValues<T>(this T[] array, int index, int length)
        {
            var tmp = new T[length];
            Array.Copy(array, index, tmp, 0, length);
            return tmp;
        }

        /// <summary>
        /// Returns the entries from [from] to [to].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="array" /> is <c>null</c>.</exception>
        /// <exception cref="Exception">The 'from' parameter must be smaller than the 'to' parameter!</exception>
        public static IEnumerable<T> For<T>(this T[] array, int from, int to)
        {
            if (array == null) throw new ArgumentNullException("array");
            if (from > to) throw new Exception("The 'from' parameter must be smaller than the 'to' parameter!");
            int idx0 = Math.Max(from, 0);
            int idx1 = Math.Min(to, array.Length - 1);
            for (; idx0 <= idx1; idx0++)
                yield return array[idx0];
        }

        #endregion

        #region Array Modification

        /// <summary>
        /// Appends the specified T[] to this T[].
        /// </summary>
        /// <param name="t1">T[] 1</param>
        /// <param name="t2">T[] 2</param>
        /// <returns></returns>
        public static T[] Append<T>(this T[] t1, T[] t2)
        {
            var tmp = new T[t1.Length + t2.Length];
            t1.CopyTo(tmp, 0);
            t2.CopyTo(tmp, t1.Length);
            return tmp;
        }

        /// <summary>
        /// Appends the specified T to this T[].
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="appendix">T to append</param>
        /// <returns></returns>
        public static T[] Append<T>(this T[] t1, T appendix)
        {
            var tmp = new T[t1.Length + 1];
            t1.CopyTo(tmp, 0);
            tmp[t1.Length] = appendix;
            return tmp;
        }

        /// <summary>
        /// Prepends the specified T[] to this T[].
        /// </summary>
        /// <param name="t1">T[] 1</param>
        /// <param name="t2">T[] 2</param>
        /// <returns></returns>
        public static T[] Prepend<T>(this T[] t1, T[] t2)
        {
            var tmp = new T[t1.Length + t2.Length];
            t2.CopyTo(tmp, 0);
            t1.CopyTo(tmp, t2.Length);
            return tmp;
        }

        /// <summary>
        /// Prepends the specified T to this T[].
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="prependix">T to prepend.</param>
        /// <returns></returns>
        public static T[] Prepend<T>(this T[] t1, T prependix)
        {
            var tmp = new T[t1.Length + 1];
            t1.CopyTo(tmp, 1);
            tmp[0] = prependix;
            return tmp;
        }

        /// <summary>
        /// Removes all occurences of o2 in this array using the Equals method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o1"></param>
        /// <param name="o2"></param>
        /// <returns></returns>
        public static T[] RemoveEqual<T>(this T[] o1, T o2)
        {
            var tmp = new T[o1.Length - o1.CountEqual(o2)];
            int index = 0;
            foreach (T t in o1)
            {
                if (!t.Equals(o2))
                    tmp[index++] = t;
            }
            return tmp;
        }

        /// <summary>
        /// Clears the array entries starting at 'index';
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="index">Index to start clearing.</param>
        /// <returns></returns>
        public static void Clear<T>(this T[] b1, int index)
        {
            Array.Clear(b1, index, b1.Length - index);
        }

        /// <summary>
        /// Clears 'numToClear' array entries starting at 'index';
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="index">Index to start clearing.</param>
        /// <param name="numToClear">Number of entries to clear.</param>
        /// <returns></returns>
        public static void Clear<T>(this T[] b1, int index, int numToClear)
        {
            Array.Clear(b1, index, numToClear);
        }

        /// <summary>
        /// Sets 'numToCopy' Ts from this instance to Ts from 'source'. Works like memset.
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="source">Ts to write.</param>
        /// <param name="numToCopy">Number of Ts to copy.</param>
        /// <returns></returns>
        public static void SetValues<T>(this T[] b1, T[] source, int numToCopy)
        {
            Array.Copy(source, 0, b1, 0, numToCopy);
        }

        /// <summary>
        /// Sets the Ts starting at 'destIndex' to the Ts from 'source'.
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="destIndex">Zero-based index to start writing in the destination array.</param>
        /// <param name="source">Ts to write.</param>
        /// <returns></returns>
        public static void SetValues<T>(this T[] b1, int destIndex, T[] source)
        {
            Array.Copy(source, 0, b1, destIndex, source.Length);
        }

        /// <summary>
        /// Sets the Ts starting at 'destIndex' to the Ts from 'source' starting at 'sourceIndex'.
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="destIndex">Zero-based index to start writing in the destination array.</param>
        /// <param name="source">Ts to write.</param>
        /// <param name="sourceIndex">Index of first value to copy from 'values'.</param>
        /// <returns></returns>
        public static void SetValues<T>(this T[] b1, int destIndex, T[] source, int sourceIndex)
        {
            Array.Copy(source, sourceIndex, b1, destIndex, source.Length - sourceIndex);
        }

        /// <summary>
        /// Sets the 'numToCopy' Ts starting at 'destIndex' to the Ts from 'source' starting at 'sourceIndex'.
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="destIndex">Zero-based index to start writing in the destination array.</param>
        /// <param name="source">Ts to write.</param>
        /// <param name="sourceIndex">Index of first value to copy from 'values'.</param>
        /// <param name="numToCopy">Number of values to copy.</param>
        /// <returns></returns>
        public static void SetValues<T>(this T[] b1, int destIndex, T[] source, int sourceIndex, int numToCopy)
        {
            Array.Copy(source, sourceIndex, b1, destIndex, numToCopy);
        }

        /// <summary>
        /// Performs an inplace-transformation, that is it calls the specified delegate for every entry of the array and assigns
        /// the return value to the array-entry. Returns the array (for chaining).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="transformator"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="transformator" /> is <c>null</c>.</exception>
        public static T[] MapInplace<T>(this T[] array, Func<T, T> transformator)
        {
            if (array == null) throw new ArgumentNullException("array");
            array.Map(transformator, array, 0, 0, array.Length);
            return array;
        }

        /// <summary>
        /// Calls the specified delegate for each array entry and returns an array holding the results in the same order.
        /// </summary>
        /// <typeparam name="TSource">Type of the input array.</typeparam>
        /// <typeparam name="TDest">Type of the output array.</typeparam>
        /// <param name="source"></param>
        /// <param name="transformator"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="transformator" /> is <c>null</c>.</exception>
        public static TDest[] Map<TSource, TDest>(this TSource[] source, Func<TSource, TDest> transformator)
        {
            if (source == null) throw new ArgumentNullException("source");
            TDest[] dest = new TDest[source.Length];
            source.Map(transformator, dest, 0, 0, source.Length);
            return dest;
        }

        /// <summary>
        /// Calls the specified delegate for each array entry and copies the entries to the specified output array beginning at index.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDest"></typeparam>
        /// <param name="source"></param>
        /// <param name="transformator"></param>
        /// <param name="dest"></param>
        /// <param name="destIndex"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c>.</exception>
        /// <exception cref="CopeException">Destination array is too small to hold all source entries or the selected starting index is too big.</exception>
        /// <exception cref="IndexOutOfRangeException">The specified index is out of range in the destination array.</exception>
        public static void Map<TSource, TDest>(this TSource[] source, Func<TSource, TDest> transformator, TDest[] dest, int destIndex = 0)
        {
            if (source == null) throw new ArgumentNullException("source");
            source.Map(transformator, dest, destIndex, 0, source.Length);
        }

        /// <summary>
        /// Transforms a number of elements starting at a specified index in an array and stores the result of the transformation in a provided
        /// destination array (where they are also placed starting at a given index).
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDest"></typeparam>
        /// <param name="source">The array containing the items to be transformed.</param>
        /// <param name="transformator">The transformation function to be applied.</param>
        /// <param name="dest">The destination array for the operation.</param>
        /// <param name="destIndex">The index in the destination array to start writing the transformed items to.</param>
        /// <param name="sourceIndex">The index in the source array to start transforming elements.</param>
        /// <param name="numToTransform">The number of elements that are to be converted.</param>
        /// <exception cref="ArgumentNullException"><paramref name="source" /> is <c>null</c>.</exception>
        /// <exception cref="IndexOutOfRangeException">The specified index is out of range in the destination array.</exception>
        /// <exception cref="CopeException">Destination array is too small to hold all source entries or the selected starting index is too big.</exception>
        public static void Map<TSource, TDest>(this TSource[] source, Func<TSource, TDest> transformator, TDest[] dest, int destIndex, int sourceIndex, int numToTransform)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (transformator == null) throw new ArgumentNullException("transformator");
            if (dest == null) throw new ArgumentNullException("dest");
            if (destIndex < 0 || dest.Length <= destIndex) throw new IndexOutOfRangeException("The specified index is out of range in the destination array.");
            if (sourceIndex < 0 || source.Length <= sourceIndex) throw new IndexOutOfRangeException("The specified index is out of range in the source array.");
            if (source.Length < sourceIndex + numToTransform) throw new IndexOutOfRangeException("The specified number of elements to transform outranges the source array.");
            if (dest.Length + destIndex < source.Length) throw new CopeException("Destination array is too small to hold all source entries or the selected starting index is too big.");
            for (int i = 0; i < numToTransform; i++)
                dest[destIndex + i] = transformator(source[sourceIndex + i]);
        }

        /// <summary>
        /// Flattens an IEnumerable of an array-type to a single array.
        /// This operation will evaluate the IEnumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arrays"></param>
        /// <returns></returns>
        public static T[] Flatten<T>(this IEnumerable<T>[] arrays)
        {
            return arrays.SelectMany(x => x).ToArray();
        }

        /// <summary>
        /// Flattens an IEnumerable of an array-type into a specified array, writing the entries starting at a specified index.
        /// This operation will evaluate the IEnumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arrays">The IEnumerable of arrays to flatten.</param>
        /// <param name="dest">The destination array to write the flattened version to.</param>
        /// <param name="destIndex">The index to start writing values in the destination array.</param>
        /// <exception cref="ArgumentNullException"><paramref name="arrays" /> is <c>null</c>.</exception>
        /// <exception cref="IndexOutOfRangeException">The destination index must be inside the range of the destination array.</exception>
        public static void Flatten<T>(this IEnumerable<T[]> arrays, T[] dest, int destIndex)
        {
            if (arrays == null) throw new ArgumentNullException("arrays");
            if (dest == null) throw new ArgumentNullException("dest");
            if (destIndex < 0 || destIndex >= dest.Length) throw new IndexOutOfRangeException("The destination index must be inside the range of the destination array.");
            int idx = destIndex;
            foreach (var a in arrays)
            {
                if (idx >= dest.Length)
                    throw new IndexOutOfRangeException("The destination array is not big enough to hold all values.");
                a.CopyTo(dest, idx);
                idx += a.Length;
            }
        }

        #endregion

        #region Multi dimensional array manipulation

        /// <summary>
        /// Calls the specified function for all entries in this array from [fromX,fromY] to [toX - 1,toY - 1] and assigns
        /// the returned value of the function to the corresponding array entry.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="fromX"></param>
        /// <param name="fromY"></param>
        /// <param name="toX"></param>
        /// <param name="toY"></param>
        /// <param name="func"></param>
        /// <exception cref="ArgumentNullException"><paramref name="array" /> is <c>null</c>.</exception>
        /// <exception cref="Exception">Parameter fromX must be within the array's limits!</exception>
        public static void TransformRectangular<T>(this T[,] array, int fromX, int fromY, int toX, int toY,
                                                   Func<T, T> func)
        {
            if (array == null) throw new ArgumentNullException("array");
            if (func == null) throw new ArgumentNullException("func");
            if (fromX < 0 || fromX > array.GetLength(0))
                throw new Exception("Parameter fromX must be within the array's limits!");
            if (toX < 0 || toX > array.GetLength(0))
                throw new Exception("Parameter toX must be within the array's limits!");
            if (fromY < 0 || fromY > array.GetLength(1))
                throw new Exception("Parameter fromY must be within the array's limits!");
            if (toY < 0 || toY > array.GetLength(1))
                throw new Exception("Parameter toY must be within the array's limits!");
            int x0 = Math.Min(fromX, toX);
            int x1 = Math.Max(fromX, toX);
            int y0 = Math.Min(fromY, toY);
            int y1 = Math.Max(fromY, toY);
            for (int x = x0; x < x1; x++)
            {
                for (int y = y0; y < y1; y++)
                {
                    array[x, y] = func(array[x, y]);
                }
            }
        }

        /// <summary>
        /// Calls the specified function for the specified cell and all its neighbors (also for the diagonal neighbors).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="func"></param>
        /// <exception cref="ArgumentNullException"><paramref name="func" /> is <c>null</c>.</exception>
        /// <exception cref="Exception">Parameter x must be within the array's limits!</exception>
        public static void ApplyToCellAndNeighbors<T>(this T[,] array, int x, int y, Action<int, int> func)
        {
            if (array == null) throw new ArgumentNullException("array");
            if (func == null) throw new ArgumentNullException("func");
            if (x < 0 || x >= array.GetLength(0)) throw new Exception("Parameter x must be within the array's limits!");
            if (y < 0 || y >= array.GetLength(1)) throw new Exception("Parameter y must be within the array's limits!");
            func(x, y);
            if (x > 0)
            {
                func(x - 1, y);
                if (y > 0)
                    func(x - 1, y - 1);
                if (y < array.GetLength(1) - 1)
                    func(x - 1, y + 1);
            }
            if (x < array.GetLength(0) - 1)
            {
                func(x + 1, y);
                if (y > 0)
                    func(x + 1, y - 1);
                if (y < array.GetLength(1) - 1)
                    func(x + 1, y + 1);
            }
            if (y > 0)
                func(x, y - 1);
            if (y < array.GetLength(1) - 1)
                func(x, y + 1);
        }

        /// <summary>
        /// Calls the specified function for the specified cell and all its neighbors (also for the diagonal neighbors).
        /// It will stop as soon as the function returns true. Returns false if it terminated early, otherwise false.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="func"></param>
        /// <exception cref="ArgumentNullException"><paramref name="func" /> is <c>null</c>.</exception>
        /// <exception cref="Exception">Parameter x must be within the array's limits!</exception>
        public static bool ApplyToCellAndNeighbors<T>(this T[,] array, int x, int y, Func<int, int, bool> func)
        {
            if (array == null) throw new ArgumentNullException("array");
            if (func == null) throw new ArgumentNullException("func");
            if (x < 0 || x >= array.GetLength(0)) throw new Exception("Parameter x must be within the array's limits!");
            if (y < 0 || y >= array.GetLength(1)) throw new Exception("Parameter y must be within the array's limits!");
            if (func(x, y))
                return false;
            if (x > 0)
            {
                if (func(x - 1, y))
                    return false;
                if (y > 0)
                    if (func(x - 1, y - 1))
                        return false;
                if (y < array.GetLength(1) - 1)
                    if (func(x - 1, y + 1))
                        return false;
            }
            if (x < array.GetLength(0) - 1)
            {
                if (func(x + 1, y))
                    return false;
                if (y > 0)
                    if (func(x + 1, y - 1))
                        return false;
                if (y < array.GetLength(1) - 1)
                    if (func(x + 1, y + 1))
                        return false;
            }
            if (y > 0)
                if (func(x, y - 1))
                    return false;
            if (y < array.GetLength(1) - 1)
                if (func(x, y + 1))
                    return false;
            return true;
        }

        /// <summary>
        /// Calls the specified function for the specified cell and all its neighbors (also for the diagonal neighbors).
        /// It will stop as soon as the function returns true. Returns false if it terminated early, otherwise false.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="func"></param>
        /// <exception cref="ArgumentNullException"><paramref name="func" /> is <c>null</c>.</exception>
        /// <exception cref="Exception">Parameter x must be within the array's limits!</exception>
        public static bool ApplyToCellAndNeighbors<T>(this T[,] array, int x, int y, Func<T, bool> func)
        {
            if (array == null) throw new ArgumentNullException("array");
            if (func == null) throw new ArgumentNullException("func");
            if (x < 0 || x >= array.GetLength(0)) throw new Exception("Parameter x must be within the array's limits!");
            if (y < 0 || y >= array.GetLength(1)) throw new Exception("Parameter y must be within the array's limits!");
            if (func(array[x, y]))
                return false;
            if (x > 0)
            {
                if (func(array[x - 1, y]))
                    return false;
                if (y > 0)
                    if (func(array[x - 1, y - 1]))
                        return false;
                if (y < array.GetLength(1) - 1)
                    if (func(array[x - 1, y + 1]))
                        return false;
            }
            if (x < array.GetLength(0) - 1)
            {
                if (func(array[x + 1, y]))
                    return false;
                if (y > 0)
                    if (func(array[x + 1, y - 1]))
                        return false;
                if (y < array.GetLength(1) - 1)
                    if (func(array[x + 1, y + 1]))
                        return false;
            }
            if (y > 0)
                if (func(array[x, y - 1]))
                    return false;
            if (y < array.GetLength(1) - 1)
                if (func(array[x, y + 1]))
                    return false;
            return true;
        }

        /// <summary>
        /// Collapses this array of two dimensional arrays into a single two dimensional array using the specified
        /// merger function. This function assumes that all the two dimensional arrays are of the same size.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="merger"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="array" /> is <c>null</c>.</exception>
        public static T[,] Collapse<T>(this T[][,] array, Func<T, T, T> merger)
        {
            if (array == null) throw new ArgumentNullException("array");
            if (merger == null) throw new ArgumentNullException("merger");
            if (array.Length == 0) return new T[0,0];
            if (array.Length == 1) return array[0];
            int limx = array[0].GetLength(0);
            int limy = array[0].GetLength(1);
            var collapsed = new T[limx,limy];
            for (int x = 0; x < limx; x++)
            {
                for (int y = 0; y < limy; y++)
                {
                    collapsed[x, y] = array[0][x, y];
                    for (int i = 1; i < array.Length; i++)
                        collapsed[x, y] = merger(collapsed[x, y], array[i][x, y]);
                }
            }
            return collapsed;
        }

        /// <summary>
        /// Returns the entries of the array which are inside the rectangle from [fromX,fromY] to [toX - 1,toY - 1];
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="fromX"></param>
        /// <param name="fromY"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <exception cref="Exception">Parameter fromX must be within the array's limits!</exception>
        /// <exception cref="ArgumentNullException"><paramref name="array" /> is <c>null</c>.</exception>
        public static IEnumerable<T> Rectangle<T>(this T[,] array, int fromX, int fromY, int width, int height)
        {
            if (array == null) throw new ArgumentNullException("array");
            int toX = Math.Min(fromX + width, array.GetLength(0));
            int toY = Math.Min(fromY + height, array.GetLength(1));
            if (fromX < 0 || fromX >= array.GetLength(0))
                throw new Exception("Parameter fromX must be within the array's limits!");
            if (fromY < 0 || fromY >= array.GetLength(1))
                throw new Exception("Parameter fromY must be within the array's limits!");
            int xStep = width > 0 ? 1 : -1;
            int yStep = height > 0 ? 1 : -1;
            for (int x = fromX; x != toX; x += xStep)
            {
                for (int y = fromY; y != toY; y += yStep)
                {
                    yield return array[x, y];
                }
            }
        }

        /// <summary>
        /// Applies the specified function to all entries of an array whose distance is at max 'radius' from the
        /// specified point in the array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="centerX">This is the first index (coordinate) of the center of the circle.</param>
        /// <param name="centerY">This is the second index (coordinate) of the center of the circle.</param>
        /// <param name="radius">This is the maximum euclidean distance of points to process.</param>
        /// <exception cref="ArgumentNullException"><paramref name="array" /> is <c>null</c>.</exception>
        /// <exception cref="Exception">Dim1 must be within the array.</exception>
        public static IEnumerable<T> Circle<T>(this T[,] array, int centerX, int centerY, double radius)
        {
            if (array == null) throw new ArgumentNullException("array");
            if (centerX < 0 || centerX >= array.GetLength(0))
                throw new Exception("centerX must be within the array's limits.");
            if (centerY < 0 || centerY >= array.GetLength(1))
                throw new Exception("centerY must be within the array's limits.");

            int radiusSquared = (int) (radius * radius);
            int limX = array.GetLength(0);
            int limY = array.GetLength(1);
            for (int xOffset = 0; xOffset <= radius; xOffset++)
            {
                int xSquared = xOffset * xOffset;
                for (int yOffset = 0; yOffset <= radius; yOffset++)
                {
                    if (xSquared + yOffset * yOffset > radiusSquared)
                        continue;
                    int x1 = centerX + xOffset;
                    int x2 = centerX - xOffset;
                    int y1 = centerY + yOffset;
                    int y2 = centerY - yOffset;
                    if (x1 >= 0 && x1 < limX)
                    {
                        if (y1 >= 0 && y1 < limY)
                            yield return array[x1, y1];
                        if (y2 >= 0 && y2 < limY)
                            yield return array[x1, y2];
                    }
                    if (x2 >= 0 && x2 < limX)
                    {
                        if (y1 >= 0 && y1 < limY)
                            yield return array[x2, y1];
                        if (y2 >= 0 && y2 < limY)
                            yield return array[x2, y2];
                    }
                }
            }
        }

        /// <summary>
        /// Applies the specified function to all entries of an array whose distance is at max 'radius' from the
        /// specified point in the array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="centerY">This is the first index (coordinate) of the center of the circle.</param>
        /// <param name="centerX">This is the second index (coordinate) of the center of the circle.</param>
        /// <param name="radius">This is the maximum euclidean distance of points to process.</param>
        /// <param name="minRadius">This is the minimum euclidean distance of points to process.</param>
        /// <exception cref="ArgumentNullException"><paramref name="array" /> is <c>null</c>.</exception>
        /// <exception cref="Exception">Dim1 must be within the array.</exception>
        public static IEnumerable<T> Ring<T>(this T[,] array, int centerY, int centerX, double radius, double minRadius)
        {
            if (array == null) throw new ArgumentNullException("array");
            if (centerY < 0 || centerY >= array.GetLength(0))
                throw new Exception("CenterX must be within the array's limits.");
            if (centerX < 0 || centerX >= array.GetLength(1))
                throw new Exception("CenterY must be within the array's limits.");

            int radiusSquared = (int) (radius * radius);
            int minSquared = (int) (minRadius * minRadius);
            int limX = array.GetLength(0);
            int limY = array.GetLength(1);
            for (int xOffset = (int) minRadius; xOffset <= radius; xOffset++)
            {
                int xSquared = xOffset * xOffset;
                for (int yOffset = (int) minRadius; yOffset <= radius; yOffset++)
                {
                    if (xOffset == 0 && yOffset == 0)
                        continue;
                    int dsquared = xSquared + yOffset * yOffset;
                    if (dsquared > radiusSquared)
                        continue;
                    if (dsquared < minSquared)
                        continue;
                    int x1 = centerY + xOffset;
                    int x2 = centerY - xOffset;
                    int y1 = centerX + yOffset;
                    int y2 = centerX - yOffset;
                    if (x1 >= 0 && x1 < limX)
                    {
                        if (y1 >= 0 && y1 < limY)
                            yield return array[x1, y1];
                        if (y2 >= 0 && y2 < limY)
                            yield return array[x1, y2];
                    }
                    if (x2 >= 0 && x2 < limX)
                    {
                        if (y1 >= 0 && y1 < limY)
                            yield return array[x2, y1];
                        if (y2 >= 0 && y2 < limY)
                            yield return array[x2, y2];
                    }
                }
            }
        }

        #endregion
    }
}