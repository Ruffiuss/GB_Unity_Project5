using System;
using System.Collections.Generic;
using UnityEngine;


namespace Tournament
{
    internal sealed class Animation
    {
        #region Fields

        internal List<Sprite> Sprites;
        internal AnimState Track;

        internal float Speed = 10.0f;
        internal float Counter = 0.0f;
        internal bool IsLooped = true;
        internal bool IsSleeps = false;


        #endregion


        #region Methods

        internal void Animate(float deltaTime)
        {
            if (IsSleeps) return;
            Counter += deltaTime * Speed;

            if (IsLooped)
            {
                while (Counter > Sprites.Count)
                {
                    Counter -= Sprites.Count;
                }
            }
            else if (Counter > Sprites.Count)
            {
                Counter = Sprites.Count;
                IsSleeps = true;
            }
        }

        #endregion
    }
}