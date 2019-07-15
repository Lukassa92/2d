using UnityEngine;

public class Test : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        var testObject = new GameObject("TEstiBLABLA");
        var attackBehav = testObject.AddComponent<AttackBehaviour>();
        attackBehav.AttackBehaviourConstruct(40.0f, transform);
    }
}
