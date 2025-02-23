using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] trapPrefabs;
    [SerializeField] private float trapSpeed = 10f;
    [SerializeField] private float spawnTime = 2f;
    private float lifeTime;

    private Vector3 groundSize;
    private int gridSize;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        groundSize = GridManager.instance.GetGroundSize();
        gridSize = GridManager.instance.gridSize;
        lifeTime = groundSize.x * (gridSize + 1) / trapSpeed;
        StartCoroutine(SpawnTrapLoop());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SpawnTrapLoop()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            SpawnTrap();
            GameManager9.instance.UpdateScore(++score);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void SpawnTrap()
    {
        int trapType = Random.Range(0, 3);
        Vector3 originalRotation = trapPrefabs[trapType].transform.eulerAngles;

        if (trapType == 2) // trap3 sinh tren ground
        {
            int randX = Random.Range(0, gridSize);
            int randZ = Random.Range(0, gridSize);
            Vector3 spawnPos = new Vector3(randX * groundSize.x, 10f, randZ * groundSize.z);
            GameObject trap = Instantiate(trapPrefabs[2], spawnPos, Quaternion.Euler(originalRotation));
            trap.AddComponent<Rigidbody>().useGravity = true;
        }
        else // trap1 2 sinh ngoai ground
        {
            int side = Random.Range(0, 4);
            int randIndex = Random.Range(0, gridSize);
            Vector3 spawnPos = Vector3.zero, moveDir = Vector3.zero;
            Quaternion rotation = trapPrefabs[trapType].transform.rotation;

            switch (side)
            {
                case 0: // from down to up
                    spawnPos = new Vector3(randIndex * groundSize.x, 0, -groundSize.z);
                    moveDir = Vector3.forward;
                    rotation = Quaternion.Euler(originalRotation.x, originalRotation.y + 90, originalRotation.z);
                    break;

                case 1: // from up to down
                    spawnPos = new Vector3(randIndex * groundSize.x, 0, gridSize * groundSize.x);
                    moveDir = Vector3.back;
                    rotation = Quaternion.Euler(originalRotation.x, originalRotation.y + 90, originalRotation.z);
                    break;

                case 2: // from right to left
                    spawnPos = new Vector3(gridSize * groundSize.x, 0, randIndex * groundSize.z);
                    moveDir = Vector3.left;
                    break;

                case 3: // from left to right
                    spawnPos = new Vector3(-groundSize.x, 0, randIndex * groundSize.z);
                    moveDir = Vector3.right;
                    break;
            }

            GameObject trap = Instantiate(trapPrefabs[trapType], spawnPos, rotation);
            TrapMove trapMove = trap.AddComponent<TrapMove>();
            bool stepMove = (trapType == 1);
            trapMove.SetUp(moveDir, trapSpeed, lifeTime, stepMove);
        }
    }
}
