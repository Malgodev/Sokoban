using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private float movementDelay = 1f;
    [SerializeField] private float moveTime = 2f;

    private bool IsMoving = false;

    void Start()
    {
        
    }

    void Update()
    {
        Vector2 inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (inputVector != Vector2.zero && IsMoveable())
        {
            StartCoroutine(MoveToPosition(new Vector3(1, 0, 0)));
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPos)
    {
        Vector3 startPosition = transform.position;

        IsMoving = true;

        float elapsedTime = 0f;

        while (elapsedTime < moveTime)
        {
            Vector3 interpolatedDir = Vector3.Slerp(startPosition, targetPos, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            transform.position = interpolatedDir;
            yield return null;
        }

        IsMoving = false;

        transform.position = targetPos;

        yield return null;
    }

    private bool IsMoveable()
    {
        return true;
    }
}
