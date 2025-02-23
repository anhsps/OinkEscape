using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance { get; private set; }

    [HideInInspector] public Vector3 groundSize;
    [HideInInspector] public int gridSize = 4;
    [SerializeField] private GameObject groundPrefab;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;

        GetGroundSize();
        GenerateGrid();
    }

    void Start()
    {

    }

    public Vector3 GetGroundSize()
    {
        if (groundPrefab != null)
        {
            Renderer[] renderers = groundPrefab.GetComponentsInChildren<Renderer>();
            if (renderers.Length > 0)
            {
                Bounds bounds = renderers[0].bounds;
                foreach (Renderer renderer in renderers)
                {
                    bounds.Encapsulate(renderer.bounds);// gop all renderer.bounds de tinh tong size cua bounds
                }
                groundSize = bounds.size;
            }
            else groundSize = Vector3.one;
        }
        return groundSize;
    }

    void GenerateGrid()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                Vector3 position = new Vector3(x * groundSize.x, 0, z * groundSize.z);
                Instantiate(groundPrefab, position, Quaternion.identity);
            }
        }

        PositionCamera();
    }

    void PositionCamera()
    {
        Camera mainCam = Camera.main;
        Vector3 center = new Vector3(groundSize.x * (gridSize - 1) / 2, 0, groundSize.z * (gridSize - 1) / 2);
        float height = Mathf.Max(groundSize.x * gridSize, groundSize.z * gridSize) * 1.25f;
        mainCam.transform.position = center + new Vector3(0, height, -height);
        mainCam.transform.LookAt(center);
        mainCam.transform.rotation = Quaternion.Euler(45, 0, 0);
    }
}
