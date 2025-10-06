using UnityEngine;
using UnityEngine.UI;

public class GigButton : MonoBehaviour
{
    public Image diceButton;

    public AudioSource shakeSound;

    public AudioSource rollSound;

    private int timesPressed = 0;
    private int requiredRoll = 0;

    public void ButtonLogic()
    {
        if (timesPressed == 0)
        {
            // Deduct cost of dice roll
            Player.Instance.Money -= 50;

            // Start dice animation
            Dice diceScript = diceButton.GetComponent<Dice>();
            shakeSound.Play();
            diceScript.beginAnimation();

            // Schedule dice roll and scene transition
            Invoke("DiceRoll", 2.0f);
            Invoke("PlayRollSound", 2.0f);
            Invoke("SwitchToMap", 3.5f);

            timesPressed = 1;
        }
    }

    public void setRequiredRoll(int num)
    {
        requiredRoll = num;
    }

    private void DiceRoll()
    {
        Debug.Log("Dice roll function required roll " + requiredRoll);

        // Roll the dice (1-6)
        int randomPick = Random.Range(1, 7);

        // Complete the dice animation with the result
        Dice diceScript = diceButton.GetComponent<Dice>();
        diceScript.Complete(randomPick);

        // Award money if the roll exceeds the requirement
        if (randomPick > requiredRoll)
        {
            Player.Instance.Money += 200;
        }
    }

    private void SwitchToMap()
    {
        GameManager.Instance.OpenMap();
    }

    private void PlayRollSound()
    {
        rollSound.Play();
    }
}
