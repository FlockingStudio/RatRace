using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gig : MonoBehaviour
{
    public List<Sprite> DiceSides;
    public float timePerSide;
    private SpriteRenderer sp;
    // Keeps a count to add until the next frame
    private float count;
    // Keeps track of the current image being displayed
    private int current;
    private bool complete;

    // Start is called before the first frame update
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        count = 0;
        complete = false;
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
                sp.sprite = DiceSides[current];
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

    // Makes the dice pick a side
    void OnMouseUpAsButton()
    {
        if (!complete)
        {
            // Gets the random dice side
            int randomPick = Random.Range(0, 6);
            // Sets that dice side as the permanent image
            sp.sprite = sp.sprite = DiceSides[randomPick];
            complete = true;
            // Makes the player pay to roll the dice
            Player.money -= 10;
            Invoke("SwitchScene", 3.0f);
        }
    }

    // Switches back to main scene
    // void SwitchScene()
    // {
    //     SceneManager.LoadScene("MapScene");
    // }
}
