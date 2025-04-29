using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Softgames.AceOfShadows
{
    public class CardStack : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _positionOffsetPerCard;

        [SerializeField]
        private TMP_Text _label;

        private Stack<Card> _cards;
        private Transform _transform;

        public int Count => _cards.Count;

        private void Awake()
        {
            _cards = new();
            _transform = transform;
            _label.text = string.Empty;
        }

        public void Push(Card card)
        {
            if (card == null)
                return;

            if (_cards.Contains(card))
                return;

            _cards.Push(card);
            _label.text = $"{_cards.Count}";
        }

        public Card Pop()
        {
            var card = _cards.Pop();
            _label.text = $"{_cards.Count}";
            return card;
        }

        public Vector3 GetNextTopPosition()
            => _transform.position + _positionOffsetPerCard * (_cards.Count + 1f);

        public int GetNextTopSortingOrder()
            => _cards.Count + 1;
    }
}
