using System.Collections.Generic;
using UnityEngine;


namespace Tournament
{
    internal sealed class ResourceLoader
    {
        #region Properties

        private Dictionary<string, SpriteAnimatorConfig> _configs;

        #endregion


        #region ClassLifeCycles

        internal ResourceLoader()
        {
            _configs = new Dictionary<string, SpriteAnimatorConfig>();
        }

        #endregion


        #region Methods

        internal SpriteAnimatorConfig LoadConfig(string path)
        {
            if (_configs.ContainsKey(path))
            {
                return _configs[path];
            }
            else
            {
                var config = Resources.Load<SpriteAnimatorConfig>(path);
                if (config)
                {
                    _configs.Add(path, config);
                    return _configs[path];
                }
                else throw new System.Exception("Config not founded");
            }
        }

        internal GameObject LoadPrefab(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            if (prefab)
            {
                return prefab;
            }
            else throw new System.Exception("Prefab not founded");
        }

        #endregion
    }
}