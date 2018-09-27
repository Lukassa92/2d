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
    private float _maxHealthbarLength = 0.25f;
    private BaseLevelEntity _baseLevelEntity;
	void Start ()
    { 
        _baseLevelEntity = GetComponentInParent<GameEntity>().BaseLevelEntity;
	    _maxHealth = _baseLevelEntity.BaseMaxHealth;
        _actualHealth = _maxHealth;
	    _healthBarImage = GetComponent<Image>();
        InvokeRepeating("CheckHealthAndReact", 1.0f, 1.0f);
	}

    [UsedImplicitly]
    private void CheckHealthAndReact()
    {
        //Das irgendwie noch anders lösen
        var newHealth = _baseLevelEntity.Health;
        if (newHealth < _actualHealth)
        {
            ReduceHealthByPercent(GetHealthDifferenceInPercent(newHealth));
            _actualHealth = newHealth;
        } else if (newHealth > _actualHealth)
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
        //Bullshit Berechnung definitv refactoren aber ging so jetzt recht fix
        var diff = _actualHealth - newHealth;
        var percent = 100 / (_maxHealth / diff);
        var onePercentFromHealthbarLength = _maxHealthbarLength / 100;
        return onePercentFromHealthbarLength * percent;
    }

    private void ReduceHealthByPercent(float percent)
    {
        Debug.Log(percent + "von 0.25");
        _healthBarImage.transform.localScale = new Vector3
        {
            x = _healthBarImage.transform.localScale.x - (percent * 10),
            y = _healthBarImage.transform.localScale.y,
            z = _healthBarImage.transform.localScale.z
        };
    }

    private void IncreaseHealthByPercent(float percent)
    {
        _healthBarImage.transform.localScale = new Vector3
        {
            x = _healthBarImage.transform.localScale.x + (percent *  10),
            y = _healthBarImage.transform.localScale.y,
            z = _healthBarImage.transform.localScale.z
        };
    }


}
