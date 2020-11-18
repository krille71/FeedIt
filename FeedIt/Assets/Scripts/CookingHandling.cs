using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CookingHandling : MonoBehaviour
{

    [SerializeField] private List<Vector3> positions;

    private List<GameObject> ingredients = new List<GameObject>();

    // Sound effects
    public AudioSource pickUpSound;

    public void CollectIngredient(GameObject ingredient)
    {
        var numIngredients = ingredients.Count;
        if(numIngredients < 3)
        {
            ingredient.transform.parent = transform;
            ingredient.transform.position = positions[numIngredients];
            ingredients.Add(ingredient);
            pickUpSound.Play();
        }
    }
}
