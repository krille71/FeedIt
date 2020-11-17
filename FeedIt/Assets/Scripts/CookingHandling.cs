using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CookingHandling : MonoBehaviour
{
    public static CookingHandling instance;

    void Start()
    {
        if(instance == null){
            instance = this;
        }
    }

    public void HandleIngredientCollecting(GameObject ingredient){
        //GameObject activeIngredientSlot = GameObject.Find("Ingredient_slot_1").gameObject;
        //Image activeIngredient = ingredient.GetComponent<Image>();
        Debug.Log("Collected: " + ingredient);
        //Debug.Log(activeIngredient.image);

    }
}
