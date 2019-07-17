using Core;
using Level.Classes;
using Level.Services;
using System;
using UniRx;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    private GameEntity _gameEntity;

    private IDisposable _subscription;

    void Start()
    {
        _gameEntity = GetComponent<GameEntity>();
        try
        {
            _subscription =
                _gameEntity.Store.Observable.OfActionType<MeleeAttackTargetAction>().Subscribe(AttackEntity);
        }
        catch (NullReferenceException ex)
        {
            Debug.Log(ex.Message);
        }
    }

    void OnDestroy()
    {
        if (_subscription != null)
        {
            _subscription.Dispose();
        }
    }

    public TimeSpan Attack(GameEntity target)
    {
        //Starte Animation
        var meleeAttackService = MeleeAttackService.GetService();
        if (target != null && target.IsAlive)
        {
            Debug.Log(transform.name + " attacks " + target.transform.name);
            return meleeAttackService.StartAttack(GetComponent<GameEntity>(), target, OnAttackDone);
        }

        _gameEntity.AI.OnEntityLeftAttackRadius(target);
        return TimeSpan.FromSeconds(1);
    }

    private void OnAttackDone(DamageSource damageSource)
    {
        damageSource.Target.GameEntity.Store.Dispatch(new DamagedByAction(damageSource));
        damageSource.Source.GameEntity.Store.Dispatch(new DamageDealtToAction(damageSource));
        Debug.Log("Damage dealt: " + damageSource.Damage);
    }

    public void AttackEntity(MeleeAttackTargetAction action)
    {
        //        _gameEntity.Store.Dispatch(new StopMovementAction());
        //        _gameEntity.Store.Dispatch(new LookAtAction(action.AttackTarget.Position));
        Attack(action.AttackTarget);
    }
}
