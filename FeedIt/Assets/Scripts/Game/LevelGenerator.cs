using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private List<Transform> chunks;
    [SerializeField] private Transform endChunk;
    [SerializeField] private const float MIN_X_SPAWN_CHUNK = -20;
    private const float MIN_X_NEXT_BEAST_RAWR = 10;
    private Vector3 width = new Vector3(50f, 0f, 0f);
    private Transform lastChunk;

    private bool spawnedLastChunk = false;
    public bool gameFinished = false;
    private bool newBeastRawr = false;

    private int lastChunkNumber = -1;

    private Beast beast;

    // Start is called before the first frame update
    void Start()
    {
        beast = GameObject.FindGameObjectWithTag("Beast").GetComponent<Beast>();
        spawnChunk(new Vector3(0f, 0f, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn a new chunk if the last one is getting close to the screen
        if(lastChunk.position.x < MIN_X_SPAWN_CHUNK)
        {
            spawnChunk(lastChunk.position);
        }

        if(spawnedLastChunk && !newBeastRawr && lastChunk.position.x < MIN_X_NEXT_BEAST_RAWR)
        {
            newBeastRawr = true;
            Debug.Log("NEW BEAST RAWR"); // TODO lilly remove this when adding the sound
            //TODO lilly new beast rawr
        }
    }

    // Spawns a chunk
    private void spawnChunk(Vector3 pos)
    {
        if(spawnedLastChunk && beast.isSleeping)
        {
            gameFinished = true;
        }
        else if (beast.isSleeping)
        {
            lastChunk = Instantiate(endChunk, pos + width, Quaternion.identity);
            spawnedLastChunk = true;
        }
        else
        {
            // Random new chunk that cannot be the last one
            int chunkNumber = lastChunkNumber;
            while(chunkNumber == lastChunkNumber)
            {
                chunkNumber = Random.Range(0, chunks.Count);
            }

            Transform chunk = chunks[chunkNumber];
            lastChunkNumber = chunkNumber;
            lastChunk = Instantiate(chunk, pos + width, Quaternion.identity);
        }
    }
}
