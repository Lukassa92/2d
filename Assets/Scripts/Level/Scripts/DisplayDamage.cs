using System;
using Core;
using Level.Classes;
using UniRx;
using UnityEngine;

public class DisplayDamage : MonoBehaviour
{
    public GameObject FloatingTextPrefab;
    public GameEntity GameEntity;
    private IDisposable _subscription;

    // Start is called before the first frame update
    void Start()
    {
        GameEntity = GetComponentInParent<GameEntity>();
        _subscription = GameEntity.Store.Observable.OfActionType<DamagedByAction>().Subscribe(ShowDamage);
    }

    private void ShowDamage(DamagedByAction a)
    {
        var damage = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        damage.GetComponent<TextMesh>().text = a.DamageSource.Damage.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
