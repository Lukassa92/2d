using System;
using UniRx;

namespace Core
{
    public static class ObservableExtensions
    {
        public static IObservable<T> OfActionType<T>(this IObservable<GameAction> source) where T : GameAction
        {
            return source.Where(a => a is T).Select(a => a as T).Where(a => a != null);
        }

    }
}
