using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyScript : MonoBehaviour
{
    public float movementDuration = 5.0f;
    public float waitBeforeMoving = 2.0f;
    public bool hasArrived = false;

    public Transform xValue;
    public Transform yValue;

    public Transform fly;

    public float x1 = 0f;
    public float x2 = 0f;
    public float y1 = 0f;
    public float y2 = 0f;

    private void Update()
    {
        if (!hasArrived)
        {
            hasArrived = true;

            x1 = xValue.position.x;
            x2 = yValue.position.x;
            y1 = xValue.position.y;
            y2 = yValue.position.y;

            float randX = Random.Range(xValue.position.x, yValue.position.x);
            float randZ = Random.Range(xValue.position.y, yValue.position.y);
            StartCoroutine(MoveToPoint(new Vector3(randX, -1.504f, randZ)));
        }
    }

    private IEnumerator MoveToPoint(Vector3 targetPos)
    {
        float timer = 0.0f;
        Vector3 startPos = fly.transform.position;

        while (timer < movementDuration)
        {
            timer += Time.deltaTime;
            float t = timer / movementDuration;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            fly.transform.position = Vector3.Lerp(startPos, targetPos, t);

            yield return null;
        }

        yield return new WaitForSeconds(waitBeforeMoving);
        hasArrived = false;
    }
}
