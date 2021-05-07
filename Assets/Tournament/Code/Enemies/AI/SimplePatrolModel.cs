﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tournament
{
    public class SimplePatrolModel
    {
        #region Fields

        private readonly AIConfig _config;
        private Transform _target;
        private int _currentPointIndex;

        #endregion


        #region Class life cycles

        public SimplePatrolModel(AIConfig config)
        {
            _config = config;
            _target = GetNextWaypoint();
        }

        #endregion


        #region Methods

        public Vector2 CalculateVelocity(Vector2 fromPosition)
        {
            var sqrDistance = Vector2.SqrMagnitude((Vector2)_target.position - fromPosition);
            Debug.Log($"Current distance to target: {sqrDistance}");
            if (sqrDistance <= _config.minSqrDistanceToTarget)
            {
                _target = GetNextWaypoint();
            }

            var direction = ((Vector2)_target.position - fromPosition).normalized;
            return _config.speed * direction;
        }

        private Transform GetNextWaypoint()
        {
            _currentPointIndex = (_currentPointIndex + 1) % _config.waypoints.Length;
            Debug.Log($"Current taget is{_config.waypoints[_currentPointIndex]}");
            return _config.waypoints[_currentPointIndex];
        }

        #endregion
    }
}