using Core;
using Level.Classes;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Image _healthBarImage;
    private float _maxHealthBarLength;
    private GameEntity _gameEntity;
    private IDisposable _subscription;

    void Start()
    {
        _healthBarImage = GetComponent<Image>();
        _gameEntity = GetComponentInParent<GameEntity>();
        _subscription = _gameEntity.Store.Observable.OfActionType<HealthChangedAction>().Subscribe(HealthChanged);
        _maxHealthBarLength = _healthBarImage.transform.localScale.x;
    }

    private void OnDestroy()
    {
        _subscription.Dispose();
    }

    private void HealthChanged(HealthChangedAction action)
    {
        var percent = (action.NewHealth * 1.0f) / action.MaxHealth;
        _healthBarImage.transform.localScale = new Vector3
        {
            y = _healthBarImage.transform.localScale.y,
            z = _healthBarImage.transform.localScale.z,
            x = _maxHealthBarLength * percent
        };
    }
}
