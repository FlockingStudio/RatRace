using System.Collections.Generic;
using UnityEngine;

public class Gig : MonoBehaviour
{
    public List<Sprite> DiceSides;

    public float timePerSide;

    private SpriteRenderer sp;
    private float count;
    private int current;
    private bool complete;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        count = 0;
        complete = false;
    }

    private void Update()
    {
        if (!complete)
        {
            if (count >= timePerSide)
            {
                // Cycle to next sprite (loops back to 0 after 5)
                current += 1;
                current %= 6;
                sp.sprite = DiceSides[current];

                // Reset timer
                count = 0;
            }
            else
            {
                count += Time.deltaTime;
            }
        }
    }

    private void OnMouseUpAsButton()
    {
        if (!complete)
        {
            // Roll the dice
            int randomPick = Random.Range(0, 6);
            sp.sprite = DiceSides[randomPick];
            complete = true;

            // Deduct cost
            Player.Instance.SubtractMoney(10);

            // Note: SwitchScene method is commented out in original code
            // Invoke("SwitchScene", 3.0f);
        }
    }

    // Legacy scene switching code (commented out)
    // void SwitchScene()
    // {
    //     SceneManager.LoadScene("MapScene");
    // }
}
