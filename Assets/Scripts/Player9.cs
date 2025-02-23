using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player9 : MonoBehaviour
{
    private Vector3 targetPos;
    private Vector3 groundSize;
    private int gridSize;
    [SerializeField] private float speed = 20f;
    [SerializeField] private Button upBtn, downBtn, leftBtn, rightBtn;

    // Start is called before the first frame update
    void Start()
    {
        groundSize = GridManager.instance.GetGroundSize();
        gridSize = GridManager.instance.gridSize;
        targetPos = transform.position;

        upBtn.onClick.AddListener(MoveUp);
        downBtn.onClick.AddListener(MoveDown);
        leftBtn.onClick.AddListener(MoveLeft);
        rightBtn.onClick.AddListener(MoveRight);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) MoveUp();
            if (Input.GetKeyDown(KeyCode.DownArrow)) MoveDown();
            if (Input.GetKeyDown(KeyCode.LeftArrow)) MoveLeft();
            if (Input.GetKeyDown(KeyCode.RightArrow)) MoveRight();
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void MoveUp()
    {
        if (targetPos.z < (gridSize - 1) * groundSize.z)
        {
            targetPos += Vector3.forward * groundSize.z;
            transform.rotation = Quaternion.Euler(-90, 0, 0);
        }
    }

    private void MoveDown()
    {
        if (targetPos.z > 0)
        {
            targetPos += Vector3.back * groundSize.z;
            transform.rotation = Quaternion.Euler(-90, 180, 0);
        }
    }

    private void MoveLeft()
    {
        if (targetPos.x > 0)
        {
            targetPos += Vector3.left * groundSize.x;
            transform.rotation = Quaternion.Euler(-90, -90, 0);
        }
    }

    private void MoveRight()
    {
        if (targetPos.x < (gridSize - 1) * groundSize.x)
        {
            targetPos += Vector3.right * groundSize.x;
            transform.rotation = Quaternion.Euler(-90, 90, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            GameManager9.instance.GameLose();
        }
    }
}
