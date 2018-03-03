using UnityEngine;
using Wikitude;
namespace ARTag
{

    public struct Location
    {
        public ImageTarget target;
        public Vector3 position;

        public Location(ImageTarget target, Vector3 position)
        {
            this.target = target;
            this.position = position;
        }
    }

}