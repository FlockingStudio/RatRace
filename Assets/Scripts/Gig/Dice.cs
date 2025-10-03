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

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        complete = false;
        // Sets the dice as invisible to start
        GetComponent<UnityEngine.UI.Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If the player hasn't chosen yet run the routine to change the sprite
        if (!complete)
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
    public int Complete()
    {
        complete = true;
        return current + 1;
    }
}
