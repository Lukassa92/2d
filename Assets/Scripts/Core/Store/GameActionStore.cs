using UniRx;

namespace Core
{
    public class GameActionStore
    {
        private readonly Subject<GameAction> _subject;
        public IObservable<GameAction> Observable
        {
            get { return _subject.AsObservable(); }
        }

        public GameActionStore()
        {
            _subject = new Subject<GameAction>();
        }

        public void Dispose()
        {
            _subject.OnCompleted();
            _subject.Dispose();
        }

        public void Dispatch(GameAction action)
        {
            _subject.OnNext(action);
        }
    }
}
