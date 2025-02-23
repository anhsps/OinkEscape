using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFall : MonoBehaviour
{
    [SerializeField] private GameObject bomNoPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        {
            SoundManager9.instance.PlaySound(6);
            Vector3 hitPoint = collision.contacts[0].point;
            Vector3 spawnPos = new Vector3(transform.position.x, hitPoint.y, transform.position.z);
            Instantiate(bomNoPrefab, spawnPos, bomNoPrefab.transform.rotation);
            bomNoPrefab.GetComponent<Destroy>().timeLife = 0.5f;
            if (collision.gameObject.CompareTag("Player"))
                GameManager9.instance.GameLose();
            Destroy(gameObject);
        }
    }
}
