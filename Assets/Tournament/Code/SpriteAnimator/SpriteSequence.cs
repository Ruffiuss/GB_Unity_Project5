using System;
using System.Collections.Generic;
using UnityEngine;


namespace Tournament
{
    [Serializable]
    public sealed class SpriteSequence
    {
        #region Fields

        public AnimState Track;
        public List<Sprite> Sprites = new List<Sprite>();

        #endregion
    }
}