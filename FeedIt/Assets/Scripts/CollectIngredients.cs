using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectIngredients : MonoBehaviour
{
    string ingredient;

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            ingredient = gameObject.tag;
            CookingHandling.instance.HandleIngredientCollecting(ingredient);
        }
    }
}
