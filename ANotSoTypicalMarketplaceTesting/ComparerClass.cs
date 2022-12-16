using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANotSoTypicalMarketplaceTesting
{
    public class ComparerClass
    {
        public static Comparer<U> GetComparer<U>(Func<U, U, bool> func)
        {
            return new Comparer<U>(func);
        }
    }


    public class Comparer<T> : ComparerClass, IEqualityComparer<T>
    {
        private Func<T, T, bool> funcComparison;

        public Comparer(Func<T, T, bool> funcInstance)
        {
            this.funcComparison = funcInstance;
        }

        public bool Equals(T x, T y)
        {
            return funcComparison(x, y);
        }

        public int GetHashCode([DisallowNull] T obj)
        {
            return obj.GetHashCode();
        }
    }
}
