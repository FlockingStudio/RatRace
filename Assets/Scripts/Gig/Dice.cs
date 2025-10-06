using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public List<Sprite> DiceSides;

    public float timePerSide;

    private float count;
    private int current;
    private bool complete;
    private bool animationStarted;

    private void Start()
    {
        count = 0;
        complete = false;
        animationStarted = false;
    }

    private void Update()
    {
        if (!complete && animationStarted)
        {
            if (count >= timePerSide)
            {
                // Cycle to next sprite (loops back to 0 after 5)
                current += 1;
                current %= 6;
                GetComponent<Image>().sprite = DiceSides[current];

                // Reset timer
                count = 0;
            }
            else
            {
                count += Time.deltaTime;
            }
        }
    }

    public void beginAnimation()
    {
        animationStarted = true;
    }

    public void Complete(int side)
    {
        complete = true;
        GetComponent<Image>().sprite = DiceSides[side - 1];
    }
}
