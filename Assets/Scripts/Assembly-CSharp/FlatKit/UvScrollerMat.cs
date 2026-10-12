using UnityEngine;

namespace FlatKit
{
    public class UvScrollerMat : MonoBehaviour
    {
        public Material targetMaterial;

        public float speedX;

        public float speedY;

        private Vector2 offset;

        private Vector2 initOffset;

        private void Start()
        {
            if (targetMaterial != null)
            {
                initOffset = targetMaterial.mainTextureOffset;
                offset = initOffset;
            }
        }

        private void OnDisable()
        {
        }

        private void Update()
        {
            if (targetMaterial != null)
            {
                offset.x += speedX * Time.deltaTime;
                offset.y += speedY * Time.deltaTime;
                targetMaterial.mainTextureOffset = offset;
            }
        }
    }
}
