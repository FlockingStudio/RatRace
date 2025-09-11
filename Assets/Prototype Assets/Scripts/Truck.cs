using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Truck : MonoBehaviour
{
    public Transform[] topMovement;
    public Transform[] centerMovement;
    public Transform[] bottomMovement;
    public Transform destination;
    public Scene dogScene;
    public Scene personScene;

    private Scene[][] sceneMatrix;
    // Start is called before the first frame update
    void Start()
    {
        sceneMatrix = new Scene[][] {new Scene[] {dogScene, personScene}, new Scene[] {personScene, dogScene}, new Scene[] {dogScene, dogScene}};
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveInOrder(int movement)
    {
        StartCoroutine(MoveInOrderCoroutine(movement));
    }

    IEnumerator MoveInOrderCoroutine(int movement)
    {
        Transform[] movements = movement == 0 ? topMovement : movement == 1 ? centerMovement : bottomMovement;

        int sceneIndex = 0;
        foreach (Transform waypoint in movements)
        {
            yield return StartCoroutine(MoveTo(waypoint.position));
            Scene usedScene = sceneMatrix[movement][sceneIndex];
            yield return StartCoroutine(usedScene.ComeInAndBackCoroutine());
            yield return new WaitForSeconds(1f);
            sceneIndex++;
        }
        yield return StartCoroutine(MoveTo(destination.position));
        // Switches to dice minigame
        SceneManager.LoadScene("DiceScene");
    }

    IEnumerator MoveTo(Vector3 target)
    {
        Vector3 start = transform.position;
        float t = 0;
        
        while (t < 1)
        {
            transform.position = Vector3.Lerp(start, target, t);
            t += Time.deltaTime;
            yield return null;
        }
        transform.position = target;
    }
}
