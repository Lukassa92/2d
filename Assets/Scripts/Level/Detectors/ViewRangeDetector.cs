using JetBrains.Annotations;
using UnityEngine;

namespace Level.Detectors
{
    public class ViewRangeDetector : MonoBehaviour
    {

        private GameEntity _gameEntity;

        [UsedImplicitly]
        void Start()
        {
            _gameEntity = GetComponentInParent<GameEntity>();
            Debug.Log($"Own gameEntity found: { _gameEntity != null }");
        }

        [UsedImplicitly]
        void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.transform.CompareTag("Detector"))
            {
                _gameEntity.AiManagerModule.OnEntityEnteredViewRadius(GetTargetEntityFromCollider(coll));
            }
        }

        public void SetVisibility(float visibility)
        {
            GetComponent<CircleCollider2D>().radius = visibility;
        }

        private GameEntity GetTargetEntityFromCollider(Collider2D coll)
        {
            var targetEntityFromCollider = coll.GetComponentInParent<GameEntity>();
            Debug.Log($"Colliding gameEntity found: { targetEntityFromCollider != null }");
            return targetEntityFromCollider;
        }

        [UsedImplicitly]
        void OnTriggerExit2D(Collider2D coll)
        {
            if (coll.transform.tag == "Detector")
            {
                _gameEntity.AiManagerModule.OnEntityLeftViewRadius(GetTargetEntityFromCollider(coll));
            }

        }
    }
}
