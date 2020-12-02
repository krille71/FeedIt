using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{

    [SerializeField] private List<Transform> ingredients;
    [SerializeField] private List<float> ingredientsSpawnChance;

    // Spawn a random ingredient based on the spawn chance
    void Start()
    {
        var n = Random.Range(0.0f, 1.0f);
        float currentChance = 0;
        for(int i = 0; i < ingredients.Count; i++)
        {
            currentChance += ingredientsSpawnChance[i];

            // If n is lower than current chance, spawn in that object and break
            if(n < currentChance)
            {
                Transform ingredient = Instantiate(ingredients[i], transform.position + new Vector3(0.5f,0,41), Quaternion.identity);
                ingredient.transform.parent = transform.parent;
                break;
            }
        }

        Destroy(this.gameObject);
    }
}
