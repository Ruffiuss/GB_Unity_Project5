using System;
using System.Collections.Generic;
using UnityEngine;


namespace Tournament
{ 
    [CreateAssetMenu(fileName = "SpriteAnimatorConfig", menuName = "Configs/Animator", order = 1)]
    public sealed class SpriteAnimatorConfig : ScriptableObject
    {
        #region Fields

        public List<SpriteSequence> Sequence = new List<SpriteSequence>();

        #endregion
    }
}