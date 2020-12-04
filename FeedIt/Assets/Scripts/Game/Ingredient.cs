using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{

    private CookingHandling cookingHandling;
    private PlayerOstrich ostrich;
    private Beast beast;
    [SerializeField] private float BEAST_EATING_MOVEMENT = 0.1f;

    void Start()
    {
        cookingHandling = GameObject.FindGameObjectWithTag("Collection").GetComponent<CookingHandling>();
        ostrich = GameObject.FindGameObjectWithTag("Ostrich").GetComponent<PlayerOstrich>();
        beast = GameObject.FindGameObjectWithTag("Beast").GetComponent<Beast>();
    }

    /*
        Eat and collect the ingredient   
    */
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Ostrich"))
        {
            // Ostrich may eat the ingredient
            bool eaten = ostrich.eat();
            if (eaten)
            {
                Destroy(gameObject);
            }

            // If not eaten then try and collect it
            else
            {
                cookingHandling.CollectIngredient(gameObject);
            }
        } else if (other.gameObject.CompareTag("Racoon"))
        {
            cookingHandling.CollectIngredient(gameObject);
        }
        else if (other.gameObject.CompareTag("Beast"))
        {
            beast.Move(BEAST_EATING_MOVEMENT);
            Destroy(gameObject);
        }
    }
}
