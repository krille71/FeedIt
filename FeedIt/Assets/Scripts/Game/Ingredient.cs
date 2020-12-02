using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{

    private CookingHandling cookingHandling;
    [SerializeField] private float BEAST_EATING_MOVEMENT = 0.1f;

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
            other.gameObject.GetComponent<Beast>().Move(BEAST_EATING_MOVEMENT);
            Destroy(gameObject);
        }
    }
}
