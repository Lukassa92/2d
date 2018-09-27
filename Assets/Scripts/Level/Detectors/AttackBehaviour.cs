using System;
using Assets.Scripts.Level.Services;
using UnityEngine;
using UnityEngine.UI;

public class AttackBehaviour : MonoBehaviour
{
    private GameEntity _gameEntity;
    private GameObject _this;
    private bool _canAttack = false;

    public GameEntity Target { get; set; }

    // Use this for initialization
    void Start()
    {
        _gameEntity = GetComponent<GameEntity>();
    }

    public void AttackBehaviourConstruct(float radius, Transform parent)
    {
        _this = GameObject.Find(transform.name);
        var collider = _this.AddComponent<CircleCollider2D>();
        collider.radius = radius;
        _this.transform.SetParent(parent);
        _this.transform.localPosition = new Vector3(0,0,0);
        _this.tag = "Detector";
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
//        if (_canAttack && coll.GetComponentInParent<GameEntity>().transform.name == Target.transform.name)
//        {
//            var damageSource = new DamageSource()
//            {
//                Source = _gameEntity.BaseLevelEntity,
//                Target = GetTargetEntityFromCollider(coll).BaseLevelEntity,
//                Damage = 5
//            };
//            _gameEntity.AI.OnDamagedOther(damageSource);
//        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {

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
        if (target != null && target.IsAlive)
        {
            Debug.Log(transform.name + " attacks " + target.transform.name);
            return meleeAttackService.StartAttack(GetComponent<GameEntity>(), target, OnAttackDone);
        }
        else
        {
            _gameEntity.AI.OnEntityLeftAttackRadius(target);
            return TimeSpan.FromSeconds(1);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
