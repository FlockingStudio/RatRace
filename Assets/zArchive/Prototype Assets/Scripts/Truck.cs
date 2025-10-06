using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Truck : MonoBehaviour
{
    public Transform[] topMovement;
    public Transform[] centerMovement;
    public Transform[] bottomMovement;
    public Transform destination;
    public Animation dogScene;
    public Animation personScene;

    private Animation[][] sceneMatrix;
    // Start is called before the first frame update
    void Start()
    {
        sceneMatrix = new Animation[][] {new Animation[] {dogScene, personScene}, new Animation[] {personScene, dogScene}, new Animation[] {dogScene, dogScene}};
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
            Animation usedScene = sceneMatrix[movement][sceneIndex];
            yield return StartCoroutine(usedScene.ComeInAndBackCoroutine());
            yield return new WaitForSeconds(1f);
            sceneIndex++;
        }
        yield return StartCoroutine(MoveTo(destination.position));
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
