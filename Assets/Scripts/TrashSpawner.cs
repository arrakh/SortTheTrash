using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] toSpawnCollection;
    [SerializeField] private float spawnDelay = 3f;
    [SerializeField] private float trashSpeed = 4f;
    [SerializeField] private Transform spawnPosition;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    //Coroutine ini akan berjalan selamanya
    //Saat berjalan, spawn sampah random dari toSpawnCollection lalu tunggu selama spawnDelay
    private IEnumerator SpawnCoroutine()
    {
        while(true)
        {
            var toSpawn = toSpawnCollection[Random.Range(0, toSpawnCollection.Length)];

            SpawnTrash(toSpawn, trashSpeed);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnTrash(GameObject toSpawn, float trashSpeed)
    {
        Trash spawned = Instantiate(toSpawn, spawnPosition.position, Quaternion.identity).GetComponent<Trash>();
        spawned.speed = trashSpeed;
    }
}
