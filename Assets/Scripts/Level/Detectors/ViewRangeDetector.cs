using JetBrains.Annotations;
using UnityEngine;

namespace Level.Detectors
{
    public class ViewRangeDetector : MonoBehaviour
    {

        private GameEntity _gameEntity;

        void Start()
        {
            _gameEntity = GetComponentInParent<GameEntity>();
        }

        void OnTriggerEnter2D(Collider2D coll)
        {
            var entity = coll.GetComponent<GameEntity>();
            if (coll is BoxCollider2D && entity != null)
            {
                _gameEntity.AiManagerModule.OnEntityEnteredViewRadius(entity);
            }
        }

        public void SetVisibility(float visibility)
        {
            GetComponent<CircleCollider2D>().radius = visibility;
        }

        void OnTriggerExit2D(Collider2D coll)
        {
            var entity = coll.GetComponent<GameEntity>();
            if (coll is BoxCollider2D && entity != null)
            {
                _gameEntity.AiManagerModule.OnEntityLeftViewRadius(entity);
            }

        }
    }
}
