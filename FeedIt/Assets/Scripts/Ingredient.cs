using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{

    private CookingHandling cookingHandling;

    void Start()
    {
        cookingHandling = GameObject.FindGameObjectWithTag("Collection").GetComponent<CookingHandling>();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            cookingHandling.CollectIngredient(gameObject);
        }
        else if (other.gameObject.CompareTag("Beast"))
        {
            other.gameObject.GetComponent<Beast>().Move(1.0f);
            Destroy(gameObject);
        }
    }
}
