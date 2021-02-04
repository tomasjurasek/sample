using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ConsoleApp17
{
    class Program
    {
        static void Main(string[] args)
        {
            Option<int> option2 = new Option<int>(0);
            Option<int> option3 = new Option<int>();

            Console.WriteLine(option2.GetHashCode() == option3.GetHashCode());

            var results = GetNumbers().Take(10);
            var enumerator = results.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
        }

        public static IEnumerable<int> GetNumbers()
        {
            var i = 0;
            while (true)
            {
                yield return i;
                i += 2;
            }
        }
    }

    public struct Option<T> : IEquatable<Option<T>>
    {
        private readonly T _value;
        private readonly bool _hasValue;
        public static string A { get; set; }

        public Option(T value)
        {
            _value = value;
            _hasValue = true;
        }

        public bool HasValue => _hasValue;

        public bool TryGetValue(out T value)
        {
            value = _hasValue ? _value : default(T);
            return _hasValue ? true : false;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;

            var comparer = EqualityComparer<T>.Default;

            return (obj is Option<T> o && Equals(o)) || (obj is T v && comparer.Equals(v, _value));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_value, _hasValue);
        }

        public bool Equals([AllowNull] Option<T> other)
        {
            if (!_hasValue && !other._hasValue) return true;
            if (_hasValue && !other._hasValue) return false;
            if (!_hasValue && other._hasValue) return false;

            var comparer = EqualityComparer<T>.Default;
            return comparer.Equals(other._value, _value);
        }

        public static bool operator ==(Option<T> option, Option<T> other)
        {
            return option.Equals(other);
        }
        public static bool operator !=(Option<T> option, Option<T> other)
        {
            return !option.Equals(other);
        }

        public static bool operator ==(Option<T> option, T other)
        {
            var comparer = EqualityComparer<T>.Default;
            return comparer.Equals(option._value, other);
        }

        public static bool operator !=(Option<T> option, T other)
        {
            var comparer = EqualityComparer<T>.Default;
            return !comparer.Equals(option._value, other);
        }
    }

    public struct Option2<T>
    {
        public Option2(bool hasValue)
        {
            HasValue = hasValue;
        }

        public bool HasValue { get; }

        public bool TryGetValue(out T value)
        {
            value = default(T);
            return true;
        }
    }
}
