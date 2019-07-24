using Classes;
using Level.Classes;
using UnityEngine;

namespace Level.Behaviours
{
    public class EntitySettings : MonoBehaviour, ILevelEntityStats
    {
        [SerializeField] private int _health = 100;
        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        [SerializeField] private int _maxHealth = 100;
        public int MaxHealth
        {
            get { return _maxHealth; }
            set { _maxHealth = value; }
        }

        [SerializeField] private bool _isAlive = true;
        public bool IsAlive
        {
            get { return _isAlive; }
            set { _isAlive = value; }
        }

        [SerializeField] private EntityType _entityType = EntityType.Enemy;
        public EntityType EntityType
        {
            get { return _entityType; }
            set { _entityType = value; }
        }

        [SerializeField] private float _baseMovementSpeed = 150.0f;
        public float BaseMovementSpeed
        {
            get { return _baseMovementSpeed; }
            set { _baseMovementSpeed = value; }
        }

        [SerializeField] private float _attackSpeed = 1.0f;
        public float AttackSpeed
        {
            get { return _attackSpeed; }
            set { _attackSpeed = value; }
        }

        [SerializeField] private int _baseDamage = 5;
        public int BaseDamage
        {
            get { return _baseDamage; }
            set { _baseDamage = value; }
        }

        [Range(0.0f, 500.0f)]
        [SerializeField]
        private float _viewRange = 0.04f;
        public float ViewRange
        {
            get { return _viewRange; }
            set { _viewRange = value; }
        }

        [Range(0.0f, 500.0f)]
        [SerializeField] private float _attackRange = 0.02f;
        public float AttackRange
        {
            get { return _attackRange; }
            set { _attackRange = value; }
        }
    }
}
