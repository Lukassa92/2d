using System;
using Assets.Scripts.Level.Services;
using UnityEngine;
using UnityEngine.UI;

public class AttackBehaviour : MonoBehaviour
{
    private GameEntity _gameEntity;

    private bool _canAttack = false;

    public GameEntity Target { get; set; }

    // Use this for initialization
    void Start()
    {
        _gameEntity = GetComponent<GameEntity>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
//        if (_canAttack && coll.GetComponentInParent<GameEntity>().transform.name == Target.transform.name)
//        {
//            var damageSource = new DamageSource()
//            {
//                Source = _gameEntity.LevelEntity,
//                Target = GetTargetEntityFromCollider(coll).LevelEntity,
//                Damage = 5
//            };
//            _gameEntity.AI.OnDamagedOther(damageSource);
//        }
    }

    private GameEntity GetTargetEntityFromCollider(Collider2D coll)
    {
        return coll.GetComponentInParent<GameEntity>();
    }

    private void OnAttackDone(DamageSource damageSource)
    {
        _canAttack = false;
        DisplayDamage(damageSource.Damage);
    }

    public void DisplayDamage(float damage)
    {
        var text = GetComponentInChildren<Text>();
        text.text = damage.ToString();
    }

    public TimeSpan Attack(GameEntity target)
    {
        Debug.Log("Attacke!");
        //Starte Animation
        _canAttack = true;
        Target = target;
        var meleeAttackService = MeleeAttackService.GetService();
        return meleeAttackService.StartAttack(GetComponent<GameEntity>(), target, OnAttackDone);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
