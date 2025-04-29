using System;

namespace Softgames.MagicWords.Json
{
    [Serializable]
    public class DialogueData
    {
        public TextData[] dialogue;
        public AvatarData[] avatars;
    }
}
