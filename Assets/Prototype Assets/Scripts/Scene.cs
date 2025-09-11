using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator ComeInAndBackCoroutine()
    {
        float destinationX = 7f;
        Vector3 startPos = transform.position;
        Vector3 targetPos = new Vector3(destinationX, startPos.y, startPos.z);

        // Move in
        yield return StartCoroutine(MoveTo(targetPos));
        
        // Hold position for 2 seconds
        yield return new WaitForSeconds(2f);
        
        // Move back out
        yield return StartCoroutine(MoveTo(startPos));
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
