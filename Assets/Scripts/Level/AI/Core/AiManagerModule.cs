using Level.Classes;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Level.AI
{
    public class AiManagerModule : MonoBehaviour, IAIEventReceiver
    {
        public GameEntity Owner { get; private set; }
        public List<BaseAIBehaviour> Behaviours { get; private set; }

        [SerializeField]
        private float _tickTime = 0.1f;
        public float TickTime
        {
            get { return _tickTime; }
            set { _tickTime = value; }
        }

        private BaseAIBehaviour _lastExecutedBehaviour;
        private DateTime _nextExecutionDate = DateTime.Now;
        private IDisposable _tickIntervalTimerSubscription;

        private void Start()
        {
            Owner = GetComponentInParent<GameEntity>();
            Behaviours = new List<BaseAIBehaviour>();
            var componentsInChildren = GetComponentsInChildren<BaseAIBehaviour>();
            Behaviours.AddRange(componentsInChildren);
            _tickIntervalTimerSubscription = Observable.Interval(TimeSpan.FromSeconds(TickTime)).Subscribe(x => OnTick());
        }

        private BaseAIBehaviour GetBehaviourWithHighestPriority()
        {
            try
            {
                return Behaviours.Where(b => b.ActionPriority > 0).MaxBy(b => b.ActionPriority);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        private void CheckNextExecution()
        {
            if (DateTime.Now >= _nextExecutionDate)
            {
                ExecuteNextBehaviour();
            }
        }

        private void ExecuteNextBehaviour()
        {
            var behaviour = GetBehaviourWithHighestPriority();
            if (behaviour != _lastExecutedBehaviour)
            {
                _lastExecutedBehaviour?.Unselect(behaviour);
                _lastExecutedBehaviour = behaviour;
            }
            var delay = _lastExecutedBehaviour?.Execute() ?? TimeSpan.Zero;
            _nextExecutionDate = DateTime.Now + delay;
        }

        public void OnEntityEnteredViewRadius(GameEntity entity)
        {
            Behaviours.ForEach(b => b.OnEntityEnteredViewRadius(entity));
        }

        public void OnEntityLeftViewRadius(GameEntity entity)
        {
            Behaviours.ForEach(b => b.OnEntityLeftViewRadius(entity));
        }

        public void OnEntityEnteredAttackRadius(GameEntity entity)
        {
            Behaviours.ForEach(b => b.OnEntityEnteredAttackRadius(entity));
        }

        public void OnEntityLeftAttackRadius(GameEntity entity)
        {
            Behaviours.ForEach(b => b.OnEntityLeftAttackRadius(entity));
        }

        public void OnOwnerDamaged(DamageSource source)
        {
            Behaviours.ForEach(b => b.OnOwnerDamaged(source));
        }

        public void OnOwnerSpawned()
        {
            Behaviours.ForEach(b => b.OnOwnerSpawned());
        }

        public void OnOwnerHealed(HealSource source)
        {
            Behaviours.ForEach(b => b.OnOwnerHealed(source));
        }

        public void OnDamagedOther(DamageSource source)
        {
            Behaviours.ForEach(b => b.OnDamagedOther(source));
        }

        public void OnCollisionWith(GameEntity entity)
        {
            Behaviours.ForEach(b => b.OnCollisionWith(entity));
        }

        public void OnEntityDied(GameEntity entity)
        {
            Behaviours.ForEach(b => b.OnEntityDied(entity));
        }

        public void OnEntityDestroyed(GameEntity entity)
        {
            Behaviours.ForEach(b => b.OnEntityDestroyed(entity));
            _tickIntervalTimerSubscription.Dispose();
        }

        public void OnOwnerDied()
        {
            _tickIntervalTimerSubscription.Dispose();
        }

        public void OnTick()
        {
            if (!Owner.IsAlive)
                return;
            Behaviours.ForEach(b => b.OnTick());
            CheckNextExecution();
        }
    }
}
