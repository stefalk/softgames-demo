using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Softgames.MainMenu
{
    public class SceneButton : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;

        [SerializeField]
        private Button _button;

        public string Label
        {
            get => _label.text;
            set => _label.text = value;
        }

        public void AddListener(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }
    }
}
