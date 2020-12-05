using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    [SerializeField] private List<Transform> chunks;
    [SerializeField] private Transform endChunk;
    [SerializeField] private const float ORIGIN_DISTANCE_SPAWN_CHUNK = 0.1f;
    private Vector3 width = new Vector3(30f, 0f, 0f);
    private Transform lastChunk;

    private bool spawnedLastChunk = false;
    public static bool gameFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnChunk(new Vector3(0f, 0f, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn a new chunk if the last one is getting close to the screen
        if(lastChunk.position.magnitude < ORIGIN_DISTANCE_SPAWN_CHUNK)
        {
            spawnChunk(lastChunk.position);
        }
    }

    // Spawns a chunk
    private void spawnChunk(Vector3 pos)
    {
        if(spawnedLastChunk && Beast.isSleeping)
        {
            gameFinished = true;
        }
        else if (Beast.isSleeping)
        {
            lastChunk = Instantiate(endChunk, pos + width, Quaternion.identity);
            spawnedLastChunk = true;
        }
        else
        {
            // TODO make so that the same chunk can not appear twice in a row
            Transform chunk = chunks[Random.Range(0, chunks.Count)];
            lastChunk = Instantiate(chunk, pos + width, Quaternion.identity);
        }
    }
}
