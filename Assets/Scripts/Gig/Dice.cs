using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public List<Sprite> DiceSides;
    public float timePerSide;
    // Keeps a count to add until the next frame
    private float count;
    // Keeps track of the current image being displayed
    private int current;
    private bool complete;
    private bool animationStarted;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        complete = false;
        animationStarted = false;
    }

    // Begins the dice roll animation
    public void beginAnimation()
    {
        animationStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        // If the player hasn't chosen yet run the routine to change the sprite
        if (!complete && animationStarted)
        {
            // Changes the image
            if (count >= timePerSide)
            {
                // Changes to the next sprite and loops the list back around
                current += 1;
                current %= 6;
                GetComponent<UnityEngine.UI.Image>().sprite = DiceSides[current];
                // Restarts the count
                count = 0;
            }
            // If it isn't time yet to change the sprite wait and count until it is
            else
            {
                count += Time.deltaTime;
            }
        }
    }

    // Stops the dice animation
    public void Complete(int side)
    {
        complete = true;
        GetComponent<UnityEngine.UI.Image>().sprite = DiceSides[side - 1];
    }
}
