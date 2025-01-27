using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private Sprite normalBox;
    [SerializeField] private Sprite targetBox;

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
            Vector3 interpolatedDir = Vector3.Lerp(startPosition, targetPos, elapsedTime / GameManager.MOVE_TIME);

            elapsedTime += Time.deltaTime;
            transform.position = interpolatedDir;
            yield return null;
        }

        transform.position = targetPos;

        GameManager.Instance.CheckWinning();

        yield return null;
    }

    public void ChangeColor(bool isInTarget)
    {
        GetComponent<SpriteRenderer>().sprite = isInTarget ? targetBox : normalBox;
    }
}
