using Softgames.Extensions;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Softgames.MagicWords
{
    public class MagicWordsUi : MonoBehaviour
    {
        [SerializeField]
        private AvatarPortrait[] _avatarPortraits;

        [SerializeField]
        private TMP_Text _avatarNameLabel;

        [SerializeField]
        private TMP_Text _dialogueText;

        private Dictionary<string, AvatarPortrait> _avatarPortraitMap;

        private void Awake()
        {
            _avatarPortraitMap = _avatarPortraits.ToDictionary(p => p.PositionId);
        }

        public void SetDialogueText(Avatar avatar, string text)
        {
            _avatarNameLabel.text = avatar.Name;
            _dialogueText.text = text;

            if (_avatarPortraitMap.TryGetValue(avatar.PositionId, out var avatarPortrait))
            {
                avatarPortrait.Show(avatar.Sprite);
            }

            _avatarPortraitMap.Values
                .Where(p => p.PositionId != avatar.PositionId)
                .ForEach(p => p.Hide());
        }
    }
}
