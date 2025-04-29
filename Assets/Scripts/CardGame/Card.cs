using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

namespace Softgames.AceOfShadows
{
    public class Card : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _renderer;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void OnDestroy()
        {
            _transform.DOKill();
        }

        public void SetPosition(Vector3 position)
            => _transform.position = position;

        public async Task MoveTo(Vector3 targetPosition, int targetSortingOrder)
        {
            SetSortingOrder(10000);
            await _transform.DOMove(targetPosition, 1).AsyncWaitForCompletion();

            // Checks if this object has been destroyed while animation was running
            if (this == null)
                return;

            SetSortingOrder(targetSortingOrder);
        }

        public void SetSortingOrder(int order)
            => _renderer.sortingOrder = order;
    }
}