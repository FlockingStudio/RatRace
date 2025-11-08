using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public List<Sprite> SixSides;
    public List<Sprite> EightSides;
    public List<Sprite> TwelveSides;
    private List<Sprite> CurrentDice;

    public float timePerSide;
    private int diceType;

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
                current %= diceType;
                GetComponent<Image>().sprite = CurrentDice[current];

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
        // Makes sure complete is false to allow dor multiple rerolls
        complete = false;
    }


    public void Complete(int side)
    {
        complete = true;
        GetComponent<Image>().sprite = CurrentDice[side - 1];
    }

    public void SetDiceType(int diceKind)
    {
        diceType = diceKind;
        // Sets which look of dice will be rolled
        if (diceKind == 6)
        {
            CurrentDice = SixSides;
        }
        if (diceKind == 8)
        {
            CurrentDice = EightSides;
        }
        if (diceKind == 12)
        {
            CurrentDice = TwelveSides;
        }
        GetComponent<Image>().sprite = CurrentDice[0];

    }
}
