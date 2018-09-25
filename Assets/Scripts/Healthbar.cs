using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Image _healthBarImage;
    private float _maxHealth;
    private float _actualHealth;
    private LevelEntity _levelEntity;
	void Start ()
    { 
        _levelEntity = GetComponentInParent<GameEntity>().LevelEntity;
	    _maxHealth = _levelEntity.BaseMaxHealth;
	    _healthBarImage = GetComponent<Image>();
        InvokeRepeating("CheckHealthAndReact", 1.0f, 1.0f);
	}

    [UsedImplicitly]
    private void CheckHealthAndReact()
    {
        //Das irgendwie noch anders lösen
        var newHealth = _levelEntity.Health;
        if (newHealth < _actualHealth && newHealth > _maxHealth)
        {
            ReduceHealthByPercent(GetHealthDifferenceInPercent(newHealth));
            _actualHealth = newHealth;
        } else if (newHealth > _actualHealth && newHealth > _maxHealth)
        {
            IncreaseHealthByPercent(GetHealthDifferenceInPercent(newHealth));
            _actualHealth = newHealth;
        }
        else
        {
            //Do nothing!
        }
    }

    private float GetHealthDifferenceInPercent(int newHealth)
    {
        return _maxHealth / (_actualHealth - newHealth);
    }

    private void ReduceHealthByPercent(float percent)
    {
        _healthBarImage.transform.localScale = new Vector3
        {
            x = _healthBarImage.transform.localScale.x - (percent / 10),
            y = _healthBarImage.transform.localScale.y,
            z = _healthBarImage.transform.localScale.z
        };
    }

    private void IncreaseHealthByPercent(float percent)
    {
        _healthBarImage.transform.localScale = new Vector3
        {
            x = _healthBarImage.transform.localScale.x + (percent / 10),
            y = _healthBarImage.transform.localScale.y,
            z = _healthBarImage.transform.localScale.z
        };
    }


}
