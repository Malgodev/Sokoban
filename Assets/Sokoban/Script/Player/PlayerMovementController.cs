using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Vector2 defaultLocation;

    private bool IsMoving = false;

    void Start()
    {
        
    }

    void Update()
    {
        float horAxis = Input.GetAxisRaw("Horizontal");
        float verAxis = Input.GetAxisRaw("Vertical");

        Vector3 targetDirec = GetTargetPos(horAxis, verAxis);

        if (targetDirec != Vector3.zero && IsMoveable(targetDirec + transform.position))
        {


            StartCoroutine(MoveToPosition(transform.position + targetDirec));
        }
    }

    private Vector3 GetTargetPos(float horAxis, float verAxis)
    {
        Vector3 targetVector = new Vector3(horAxis, verAxis, 0);

        if (horAxis != 0)
        {
            targetVector.y = 0;
        }

        return targetVector;
    }

    private IEnumerator MoveToPosition(Vector3 targetPos)
    {
        Vector3 startPosition = transform.position;

        IsMoving = true;

        float elapsedTime = 0f;

        while (elapsedTime < GameManager.MOVE_TIME)
        {
            Vector3 interpolatedDir = Vector3.Lerp(startPosition, targetPos, elapsedTime / GameManager.MOVE_TIME);
            elapsedTime += Time.deltaTime;
            transform.position = interpolatedDir;
            yield return null;
        }

        IsMoving = false;

        transform.position = targetPos;

        yield return null;
    }

    private bool IsMoveable(Vector2 targetPos)
    {
        if (IsMoving)
        {
            return false;
        }

        // TODO Hard code
        if (Utility.OverlapPoint(targetPos, "Wall"))
        {
            return false;
        }

        Vector2 targetDirec = targetPos - (Vector2) transform.position;
        Collider2D boxCollider = Utility.OverlapPoint(targetPos, "Box");

        if (boxCollider)
        {
            if (Physics2D.OverlapPoint(targetPos + targetDirec) != null)
            {
                return false;
            }

            BoxController boxController = boxCollider.GetComponent<BoxController>();

            boxController.MoveToTargetPosition(targetPos + targetDirec);
        }

        return true;
    }

    public void SetDefaultPosition(Vector2 position)
    {
        defaultLocation = position;

        this.transform.position = defaultLocation;
    }
}
