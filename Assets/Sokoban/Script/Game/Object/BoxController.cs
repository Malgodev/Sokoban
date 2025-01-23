using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public void MoveToTargetPosition(Vector2 targetPosition)
    {
        StartCoroutine(MoveToPosition(targetPosition));
    }

    private IEnumerator MoveToPosition(Vector3 targetPos)
    {
        Vector3 startPosition = transform.position;

        float elapsedTime = 0f;

        while (elapsedTime < GameManager.MOVE_TIME)
        {
            Vector3 interpolatedDir = Vector3.Slerp(startPosition, targetPos, elapsedTime / GameManager.MOVE_TIME);
            elapsedTime += Time.deltaTime;
            transform.position = interpolatedDir;
            yield return null;
        }

        transform.position = targetPos;

        yield return null;
    }
}
