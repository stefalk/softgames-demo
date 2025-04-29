using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Softgames.AceOfShadows
{
    public class CardGame : MonoBehaviour
    {
        [SerializeField]
        private CardGameUi _cardGameUi;

        [SerializeField]
        private CardStack _cardStack1;

        [SerializeField]
        private CardStack _cardStack2;

        [SerializeField]
        private Card _cardPrefab;

        [SerializeField]
        private int _numbersOfCardsToInstantiate = 144;

        void Start()
        {
            for (var i = 0; i < _numbersOfCardsToInstantiate; i++)
            {
                var card = Instantiate(_cardPrefab, transform);
                card.SetPosition(_cardStack1.GetNextTopPosition());
                card.SetSortingOrder(_cardStack1.GetNextTopSortingOrder());
                _cardStack1.Push(card);
            }

            StartCoroutine(SendCardTriggerCoroutine());
        }

        IEnumerator SendCardTriggerCoroutine()
        {
            while (_cardStack1.Count > 0)
            {
                MoveNextCard();
                yield return new WaitForSeconds(1);
            }
        }

        private async void MoveNextCard()
        {
            var targetPosition = _cardStack2.GetNextTopPosition();
            var targetSortingOrder = _cardStack2.GetNextTopSortingOrder();

            var card = _cardStack1.Pop();
            await card.MoveTo(targetPosition, targetSortingOrder);
            _cardStack2.Push(card);

            if (_cardStack1.Count == 0)
            {
                _cardGameUi.ShowFinalMessage();
            }
        }
    }
}