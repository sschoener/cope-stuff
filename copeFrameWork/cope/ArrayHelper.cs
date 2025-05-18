namespace cope
{
    public static class ArrayHelper
    {
        /// <summary>
        /// Creates an array by repeating the given value a specified number of times.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public static T[] Repeat<T>(T t, int times)
        {
            T[] ts = new T[times];
            for (int i = 0; i < times; i++)
                ts[i] = t;
            return ts;
        }
    }
}
