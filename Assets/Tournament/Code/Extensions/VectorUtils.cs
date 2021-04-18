using UnityEngine;


namespace Tournament
{
    static public class VectorUtils
    {
        #region Methods

        public static Vector3 Change(this Vector3 source, object x = null, object y = null, object z = null)
        {
            return new Vector3(x == null ? source.x : (float)x, y == null ? source.y : (float)y, z == null ? source.z : (float)z);
        }

        public static Vector2 Change(this Vector2 source, object x = null, object y = null)
        {
            return new Vector3(x == null ? source.x : (float)x, y == null ? source.y : (float)y);
        }

        #endregion
    }
}