using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMove : MonoBehaviour
{
    private Vector3 moveDir;
    private float moveSpeed;
    private float lifeTime;
    private bool stepMove;
    private Vector3 groundSize => GridManager.instance.GetGroundSize();
    private int gridSize => GridManager.instance.gridSize;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!stepMove)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
            float rotateZ = (moveDir == Vector3.right || moveDir == Vector3.back) ? -360 : 360;
            transform.Rotate(0, 0, rotateZ * Time.deltaTime);
        }
    }

    public void SetUp(Vector3 direction, float speed, float time, bool state)
    {
        moveDir = direction;
        moveSpeed = speed;
        lifeTime = time;
        stepMove = state;

        if (stepMove) StartCoroutine(StepMove());
        Destroy(gameObject, lifeTime);
    }

    private IEnumerator StepMove()
    {
        while (true)
        {
            transform.position += new Vector3(moveDir.x * groundSize.x, 0, moveDir.z * groundSize.z);
            yield return new WaitForSeconds(lifeTime / gridSize);
        }
    }
}
