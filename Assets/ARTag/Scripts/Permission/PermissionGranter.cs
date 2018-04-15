
namespace ARTag
{
    using UnityEngine;
    using System.Collections;

    public class PermissionGranter : MonoBehaviour
    {
        public AndroidPermission permission;

        IEnumerator Start()
        {
            UniAndroidPermission.RequestPermission(permission);
            yield return UniAndroidPermission.IsPermitted(permission);
        }

    }

}
