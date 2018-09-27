using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Image _healthBarImage;
    private float _maxHealthBarLength;
	void Start ()
    { 
	    _healthBarImage = GetComponent<Image>();
	    _maxHealthBarLength = _healthBarImage.transform.localScale.x;
	}

    public void DecreaseHealthByPercent(float percent)
    {
        Debug.Log("bekomme von der LevelEntity: "+percent);
        var healthBarFactor = GetHealthBarFactorInPercent(percent);
        Debug.Log("healtbar factor: "+healthBarFactor);
        _healthBarImage.transform.localScale = new Vector3
        {
            x = _healthBarImage.transform.localScale.x - healthBarFactor,
            y = _healthBarImage.transform.localScale.y,
            z = _healthBarImage.transform.localScale.z
        };
    }

    private float GetHealthBarFactorInPercent(float percent)
    {
        Debug.Log("bekomme hier: "+percent+" und maxBar is: "+_maxHealthBarLength);
        return _maxHealthBarLength / (percent * 100);
    }

    public void IncreaseHealthByPercent(float percent)
    {
        _healthBarImage.transform.localScale = new Vector3
        {
            x = _healthBarImage.transform.localScale.x + (percent *  10),
            y = _healthBarImage.transform.localScale.y,
            z = _healthBarImage.transform.localScale.z
        };
    }


}
