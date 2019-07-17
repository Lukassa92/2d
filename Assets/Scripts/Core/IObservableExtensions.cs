using System;
using System.Linq;
using UniRx;

namespace Core
{
    public static class ObservableExtensions
    {
        public static IObservable<T> OfActionType<T>(this IObservable<GameAction> source) where T : GameAction
        {
            return source.Where(a => a is T).Select(a => a as T).Where(a => a != null);
        }

        public static IObservable<GameAction<TEnum>> OfActionTypes<TEnum>(this IObservable<GameAction> source, params TEnum[] types) where TEnum : struct, IConvertible
        {
            return source.Select(a => a as GameAction<TEnum>).Where(a => a != null).Where(a => types.Contains(a.Type));
        }

    }
}
