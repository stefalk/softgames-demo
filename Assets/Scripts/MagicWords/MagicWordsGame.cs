using Softgames.MagicWords.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

namespace Softgames.MagicWords
{
    public class MagicWordsGame : MonoBehaviour
    {
        private static Regex textVariableRegex = new Regex(@"\{[a-zA-Z0-9]+\}");

        [SerializeField]
        private string _url;

        [SerializeField]
        private MagicWordsUi _ui;

        private Dictionary<string, Avatar> _avatars;
        private DialogueData _dialogueData;

        [SerializeField]
        private Dictionary<string, string> _emojiMap;

        [SerializeField]
        private TextVariable[] _textVariables;

        [SerializeField]
        private float _dialogIntervalSeconds = 2;

        private Dictionary<string, string> _textVariableMap;

        private void Awake()
        {
            _avatars = new();
            _textVariableMap = _textVariables.ToDictionary(v => $"{{{v.Key}}}", v => v.Value);
        }

        private void Start()
        {
            StartCoroutine(LoadDialogue(_url));
        }

        private void OnDialogueLoaded()
        {
            StartCoroutine(PlayDialogue());
        }

        private IEnumerator LoadDialogue(string url)
        {
            _avatars.Clear();
            _dialogueData = null;

            string dialogueJson;

            using (var request = UnityWebRequest.Get(url))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Failed to load url {url}. Error was {request.error}");
                    yield break;
                }

                dialogueJson = request.downloadHandler.text;
            }

            _dialogueData = JsonUtility.FromJson<DialogueData>(dialogueJson);

            foreach (var avatar in _dialogueData.avatars)
            {
                using (var request = UnityWebRequestTexture.GetTexture(avatar.url))
                {
                    yield return request.SendWebRequest();

                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        Debug.LogWarning($"Failed to load avatar {avatar.url}");
                    }
                    else
                    {
                        var texture = DownloadHandlerTexture.GetContent(request);
                        var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                        _avatars[avatar.name] = new Avatar {
                            Name = avatar.name,
                            Sprite = sprite,
                            PositionId = avatar.position,
                        };
                    }
                }
            }

            var namesOfMissingAvatars = _dialogueData.dialogue
                .Where(d => d.name != null && !_avatars.ContainsKey(d.name))
                .Select(d => d.name)
                .Distinct();

            foreach (var name in namesOfMissingAvatars)
            {
                Debug.LogWarning($"Missing avatar: {name}");
                _avatars[name] = new Avatar { Name = name, PositionId = "left" };
            }

            OnDialogueLoaded();
        }

        private IEnumerator PlayDialogue()
        {
            foreach (var dialogueText in _dialogueData.dialogue)
            {
                _avatars.TryGetValue(dialogueText.name, out var avatar);
                _ui.SetDialogueText(avatar, SubstituteTextVariables(dialogueText.text));
                yield return new WaitForSeconds(_dialogIntervalSeconds);
            }
        }

        private string SubstituteTextVariables(string text)
        {
            var variables = textVariableRegex.Matches(text)
                .Select(m => m.Groups[0].Value)
                .Distinct();

            foreach (var variable in variables)
            {
                if (_textVariableMap.TryGetValue(variable, out var value))
                {
                    text = text.Replace(variable, value);
                }
            }

            return text;
        }
    }
}
