using System;
using System.Collections.Generic;
using UnityEngine;


namespace Tournament
{
    internal sealed class SpriteAnimatorController : IDisposable, IExecutable
    {
        #region Fields

        private SpriteAnimatorConfig _config;
        private Dictionary<SpriteRenderer, Animation> _activeAnimation;

        #endregion


        #region ClassLifeCycles

        internal SpriteAnimatorController(SpriteAnimatorConfig config)
        {
            _config = config;
            _activeAnimation = new Dictionary<SpriteRenderer, Animation>();
        }


        #endregion


        #region Methods

        internal void StartAnimation(SpriteRenderer spriteRenderer, AnimState track, bool isLoop, float speed)
        {
            if (_activeAnimation.TryGetValue(spriteRenderer, out var animation))
            {
                animation.IsLooped = isLoop;
                animation.Speed = speed;
                animation.IsSleeps = false;

                if (animation.Track!=track)
                {
                    animation.Track = track;
                    animation.Sprites = _config.Sequence.Find(sequence => sequence.Track == track).Sprites;
                    animation.Counter = 0.0f;
                }
            }
            else
            {
                _activeAnimation.Add(spriteRenderer, new Animation()
                {
                    Track = track,
                    Sprites = _config.Sequence.Find(sequence => sequence.Track == track).Sprites,
                    IsLooped = isLoop,
                    Speed = speed,
                });
            }
        }

        internal void StopAnimation(SpriteRenderer sprite)
        {
            if (_activeAnimation.ContainsKey(sprite))
            {
                _activeAnimation.Remove(sprite);
            }
        }

        public void Execute(float deltaTime)
        {
            foreach (var animation in _activeAnimation)
            {
                animation.Value.Animate(deltaTime);

                if (animation.Value.Counter<animation.Value.Sprites.Count)
                {
                    animation.Key.sprite = animation.Value.Sprites[(int)animation.Value.Counter];
                }
            }
        }

        public void Dispose()
        {
            _activeAnimation.Clear();
        }

        #endregion
    }
}