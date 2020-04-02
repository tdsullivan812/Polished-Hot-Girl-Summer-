using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimator : MonoBehaviour
{

    public Sprite[] frames;
    public float speed;

    private int currentFrame;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= speed)
        {
            timer -= speed;
            currentFrame = (currentFrame + 1) % frames.Length;
            gameObject.GetComponent<SpriteRenderer>().sprite = frames[currentFrame];

        }



    }

}
