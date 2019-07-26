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
        _subscription =
            _gameEntity?.Store?.Observable?.OfActionTypes(GameEntityActionTypes.MeleeAttackAction, GameEntityActionTypes.RangedAttackAction).Subscribe(
                a =>
                {
                    switch (a.Type)
                    {
                        case GameEntityActionTypes.MeleeAttackAction:
                            MeleeAttackEntity(a as MeleeAttackTargetAction);
                            break;
                        case GameEntityActionTypes.RangedAttackAction:
                            RangedAttackEntity(a as RangedAttackAction);
                            break;
                    }
                });
    }

    void OnDestroy()
    {
        _subscription?.Dispose();
    }

    public TimeSpan MeleeAttack(GameEntity target)
    {
        //Starte Animation
        var meleeAttackService = MeleeAttackService.GetService();
        if (target != null && target.IsAlive)
        {
            return meleeAttackService.StartAttack(GetComponent<GameEntity>(), target, OnAttackDone);
        }

        _gameEntity.AiManagerModule.OnEntityLeftAttackRadius(target);
        return TimeSpan.FromSeconds(1);
    }

    private void OnAttackDone(DamageSource damageSource)
    {
        damageSource.Target.GameEntity.Store.Dispatch(new DamagedByAction(damageSource));
        damageSource.Source.GameEntity.Store.Dispatch(new DamageDealtToAction(damageSource));
    }

    public void MeleeAttackEntity(MeleeAttackTargetAction action)
    {
        //        _gameEntity.Store.Dispatch(new StopMovementAction());
        //        _gameEntity.Store.Dispatch(new LookAtAction(action.AttackTarget.Position));
        MeleeAttack(action.AttackTarget);
    }

    private void RangedAttackEntity(RangedAttackAction action)
    {
        var projectileVelocityVector = CalculateVelocity(transform.position, action.Target.transform.position, action.AirborneTime);
        var obj = Instantiate(action.ProjectilePrefab, transform.position, Quaternion.identity);
        obj.velocity = projectileVelocityVector;
    }

    private Vector2 CalculateVelocity(Vector2 origin, Vector2 target, float airborneTime)
    {
        var distance = target - origin;
        var distanceX = distance;
        distanceX.y = 0f;

        var Sy = distance.y;
        var Sx = distanceX.magnitude;

        var velocityX = Sx / airborneTime;
        var velocityY = Sy / airborneTime + 0.5f * Mathf.Abs(Physics.gravity.y) * airborneTime;

        var result = distanceX.normalized;
        result *= velocityX;
        result.y = velocityY;

        return result;
    }
}
