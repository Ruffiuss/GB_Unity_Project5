using System.Collections.Generic;
using UnityEngine;


namespace Tournament
{
    internal sealed class PlayerInit
    {
        #region Properties

        internal LevelObjectView View { get; private set; }
        internal SpriteAnimatorController Animator { get; private set; }
        internal PlayerController Controller { get; private set; }

        #endregion


        #region ClassLifeCycles

        internal PlayerInit(SpriteAnimatorConfig spriteAnimatorConfig, GameObject view, Vector3 spawnPosition)
        {
            var player = Object.Instantiate(view, spawnPosition, Quaternion.identity);

            if (player.TryGetComponent(out LevelObjectView playerView))
            {
                View = playerView;
            }
            else throw new System.Exception($"{player.name} does not contain view component");

            Animator = new SpriteAnimatorController(spriteAnimatorConfig);
            Animator.StartAnimation(View._spriteRenderer, AnimState.Run, true, 20);

            Controller = new PlayerController(View, Animator);
        }

        #endregion


        #region Methods

        internal void Respawn(Vector3 spawnPoint)
        {
            View._transform.position = spawnPoint;
        }

        #endregion
    }
}