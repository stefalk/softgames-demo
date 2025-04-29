using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Softgames.AceOfShadows
{
    public class CardGameUi : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _messageCanvasGroup;

        public void ShowFinalMessage()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_messageCanvasGroup.DOFade(1f, 1f))
               .AppendInterval(1f)
               .Append(_messageCanvasGroup.DOFade(0f, 2f));
        }
    }
}
