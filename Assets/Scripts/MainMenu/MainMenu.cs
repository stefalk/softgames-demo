using Softgames.Extensions;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Softgames.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private string[] _sceneNames;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private SceneButton _buttonPrefab;

        [SerializeField]
        private RectTransform _buttonParent;

        [SerializeField]
        private TMP_Text _fpsLabel;

        private bool _isBusy;

        private void Awake()
        {
            _sceneNames.ForEach(name => CreateSceneButton(name, name));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleVisibility();
            }

            var fps = Time.deltaTime == 0f ? 0f : 1f / Time.deltaTime;
            _fpsLabel.text = $"fps: {Mathf.Floor(fps)}";
        }

        private SceneButton CreateSceneButton(string label, string sceneName)
        {
            var button = Instantiate(_buttonPrefab, _buttonParent);

            button.Label = label;
            button.AddListener(() => SwitchToScene(sceneName));

            return button;
        }

        private async void SwitchToScene(string sceneName)
        {
            if (_isBusy)
            {
                return;
            }

            _isBusy = true;

            Hide();

            foreach (var name in _sceneNames)
            {
                var scene = SceneManager.GetSceneByName(name);
                if (scene.isLoaded)
                {
                    await SceneManager.UnloadSceneAsync(name);
                }
            }

            if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }

            _isBusy = false;
        }

        private void ToggleVisibility()
        {
            if (_isBusy)
            {
                return;
            }

            var isVisible = _canvasGroup.alpha > 0;

            if (isVisible)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        private void Show()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        private void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}
