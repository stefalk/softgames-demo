using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvatarPortrait : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvasGroup;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private TMP_Text _errorLabel;

    [SerializeField]
    private string _positionId = "left";

    public string PositionId => _positionId;

    private void Awake()
    {
        _canvasGroup.alpha = 0;
    }

    private void OnDestroy()
    {
        _canvasGroup.DOKill();
    }

    public void Show(Sprite sprite)
    {
        _image.sprite = sprite;
        _errorLabel.text = sprite == null ? "Missing sprite" : string.Empty;

        _canvasGroup.DOKill();
        _canvasGroup.DOFade(1, 1);
    }

    public void Hide()
    {
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0, 1);
    }
}
