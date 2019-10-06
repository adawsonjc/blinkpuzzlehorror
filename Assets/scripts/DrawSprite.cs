using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSprite : MonoBehaviour
{
    // Start is called before the first frame update


    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //1. Read the old sprites texture
        Texture2D old = spriteRenderer.sprite.texture;
        //2. Create a new Texture.
        Texture2D left = new Texture2D((int)(old.width), old.height, old.format, false);
        Color[] colors = old.GetPixels(0, 0, (int)(old.width), old.height);
        left.SetPixels(colors);
        left.Apply();
        /*Sprite sprite = Sprite.Create(left,
               new Rect(0, 0, left.width, left.height),
               new Vector2(0.5f, 0.5f),
               40);*/
        Sprite sprite = Sprite.Create(left,
               new Rect(0, 0, left.width/2, left.height/2),
               new Vector2(0.5f, 0.5f),
               40);
        Debug.Log("Old Bounds: " + spriteRenderer.sprite.bounds + " Rect: " + spriteRenderer.sprite.rect + " TexRect: " + spriteRenderer.sprite.textureRect);
        Debug.Log("Bounds: " + sprite.bounds + " Rect: " + sprite.rect + " TexRect: " + sprite.textureRect);
        spriteRenderer.sprite = sprite;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
