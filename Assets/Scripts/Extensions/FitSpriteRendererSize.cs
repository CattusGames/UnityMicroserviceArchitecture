using UnityEngine;

namespace Extensions
{
    public static class FitSpriteRendererSize
    {
        public static void SetObjectSizeInPixels(GameObject obj, float sizeInPixels, Camera camera)
        {
            var spriteRenderer = obj.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null || spriteRenderer.sprite == null)
            {
                Debug.LogError("SpriteRenderer or Sprite is missing.");
                return;
            }
            
            float screenHeight = Screen.height;
            float worldHeight = camera.orthographicSize * 2f;
            float worldUnitsPerPixel = worldHeight / screenHeight;

            float targetWorldSize = sizeInPixels * worldUnitsPerPixel;
            float spriteSize = spriteRenderer.sprite.bounds.size.x;

            float scale = targetWorldSize / spriteSize;

            obj.transform.localScale = new Vector3(scale, scale, 1f);
        }

    }
}