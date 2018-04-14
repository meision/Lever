using System;
using System.Collections.Generic;

namespace Meision.Automatization
{

    public static class DelegateResultCache
    {
        private static DelegateResultCache<string> __stringCache;
        public static DelegateResultCache<string> StringCache
        {
            get
            {
                if (DelegateResultCache.__stringCache == null)
                {
                    lock (typeof(DelegateResultCache))
                    {
                        DelegateResultCache.__stringCache = new DelegateResultCache<string>();
                    }
                }

                return DelegateResultCache.__stringCache;
            }
        }

        private static DelegateResultCache<string[]> __stringsCache;
        public static DelegateResultCache<string[]> StringsCache
        {
            get
            {
                if (DelegateResultCache.__stringsCache == null)
                {
                    lock (typeof(DelegateResultCache))
                    {
                        DelegateResultCache.__stringsCache = new DelegateResultCache<string[]>();
                    }
                }

                return DelegateResultCache.__stringsCache;
            }
        }
    }

    public sealed class DelegateResultCache<TResult>
    {
        Dictionary<Func<TResult>, TResult> _dictionary = new Dictionary<Func<TResult>, TResult>();
        public TResult GetCacheResult(Func<TResult> expression)
        {
            if (!this._dictionary.ContainsKey(expression))
            {
                this._dictionary[expression] = expression.Invoke();
            }
            return this._dictionary[expression];
        }

        public void Clear()
        {
            this._dictionary.Clear();
        }
    }
}
