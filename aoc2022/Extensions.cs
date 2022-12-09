using System.Drawing;

namespace aoc2022
{
    internal static class Extensions
    {
        internal static void Print(this object input) => Console.WriteLine(input);

        internal static T[][] Transpose<T>(this T[][] jaggedArray)
        {
            var elemMin = jaggedArray.Min(x => x.Length);
            return jaggedArray
                .SelectMany(x => x.Take(elemMin).Select((y, i) => new { val = y, idx = i }))
                .GroupBy(x => x.idx, x => x.val, (_, y) => y.ToArray()).ToArray();
        }

        internal static void Deconstruct<T>(this IList<T> list, out T first, out IList<T> rest)
        {
            first = list.Count > 0 ? list[0] : default!; // or throw
            rest = list.Skip(1).ToList();
        }

        internal static void Deconstruct<T>(this IList<T> list, out T first, out T second, out IList<T> rest)
        {
            first = list.Count > 0 ? list[0] : default!; // or throw
            second = list.Count > 1 ? list[1] : default!; // or throw
            rest = list.Skip(2).ToList();
        }

        internal static T[,] To2DArray<T>(this IEnumerable<IEnumerable<T>> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            int max = source.Select(l => l).Max(l => l.Count());

            var result = new T[source.Count(), max];

            for (int i = 0; i < source.Count(); i++)
            {
                var inner = source.ElementAt(i);
                for (int j = 0; j < inner.Count(); j++)
                {
                    result[i, j] = inner.ElementAt(j);
                }
            }

            return result;
        }

        internal static int Product(this IEnumerable<int> source)
        {
            int i = 1;
            foreach (var x in source)
                i *= x;

            return i;
        }

        internal static long Product<T>(this IEnumerable<T> source, Func<T, long> selector)
        {
            long i = 1;
            foreach (var x in source.Select(selector))
                i *= x;

            return i;
        }

        public static IEnumerable<TResult> ZipWithDefault<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> selector)
        {
            bool firstMoveNext, secondMoveNext = false;

            using var enum1 = first.GetEnumerator();
            using var enum2 = second.GetEnumerator();
            while ((firstMoveNext = enum1.MoveNext()) && (secondMoveNext = enum2.MoveNext()))
                yield return selector(enum1.Current, enum2.Current);

            if (firstMoveNext && !secondMoveNext)
            {
                yield return selector(enum1.Current, default!);
                while (enum1.MoveNext())
                {
                    yield return selector(enum1.Current, default!);
                }
            }
            else if (!firstMoveNext && secondMoveNext)
            {
                yield return selector(default!, enum2.Current);
                while (enum2.MoveNext())
                {
                    yield return selector(default!, enum2.Current);
                }
            }
        }

        public static void Print(this HashSet<Point> hash, Point[] rope)
        {
            int top = hash.Max(p => p.Y), bot = hash.Min(p => p.Y),
                lef = hash.Min(p => p.X), rig = hash.Max(p => p.X);
            for (var y = top + 3; y >= bot - 3; y--)
            {
                for (var x = lef - 3; x <= rig + 3; x++)
                {
                    var p = new Point(x, y);
                    var idx = Array.IndexOf(rope, p);
                    if (rope.Contains(p) && idx == 0)
                        Console.Write("H");
                    else if (rope.Contains(p) && idx == rope.Length - 1)
                        Console.Write("T");
                    else if (rope.Contains(p))
                        Console.Write(idx);
                    else if (x == 0 && y == 0)
                        Console.Write('s');
                    else if (hash.Contains(p))
                        Console.Write('#');
                    else
                        Console.Write('.');
                }
                Console.WriteLine();
            }
            Console.WriteLine("-----------------------------------");
        }
    }
}
