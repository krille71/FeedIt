using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingHandling : MonoBehaviour
{
    public static CookingHandling instance;

    void Start()
    {
        if(instance == null){
            instance = this;
        }
    }

    public void HandleIngredientCollecting(string ingredient){
        Debug.Log("Collected: " + ingredient);
    }
}
