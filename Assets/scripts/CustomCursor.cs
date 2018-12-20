using UnityEngine;


namespace Assets.scripts
{
    public class CustomCursor : MonoBehaviour
    {
        public Texture2D texture;

        public void Start()
        {
            Cursor.SetCursor(texture, new Vector2(0,0), CursorMode.Auto);
        }
    }
}
