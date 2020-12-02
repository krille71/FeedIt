using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class CookingHandling : MonoBehaviour
{

    [SerializeField] private List<Vector3> positions;
    [SerializeField] private float COOKING_TIME = 0.5f;
    private float cookingTimer = 0f;
    public static bool isCooking = false;

    private List<GameObject> ingredients = new List<GameObject>();

    // Sound effects
    public AudioSource pickUpSound;

    /* 
       All recipies should be in the recipies.txt. Follow the format: Ingredient,Ingredient,Ingredient;Dish 
       Ingredients should prefabs found in Assets/Prefabs/Ingredients and the dishes should be prefabs in Assets/Resources
       ! NAMES HAS TO BE THE SAME IN THE TXT AND PREFABS, they need to correspond !    
    */
    private String[] recipies = System.IO.File.ReadAllLines(@"./Assets/Scripts/recipies.txt"); 
    private Dictionary<(string, string, string), string> CookingDict = new Dictionary<(string, string, string), string>();
    private GameObject player;

    public void Start(){

        player = GameObject.Find("Ostrich");

        foreach (var recipy in recipies)
        {
            var recipy_split = recipy.Split(';');
            var recipy_split_ingredients = recipy_split[0].Split(',');
            Array.Sort(recipy_split_ingredients);
            var recipy_ingredients = (recipy_split_ingredients[0],recipy_split_ingredients[1],recipy_split_ingredients[2]);
            String recipy_dish = recipy_split[1];

            CookingDict.Add(recipy_ingredients, recipy_dish);  
        }
    }

    public void Update(){
        if(!isCooking && ingredients.Count == 3 && player.transform.childCount > 0)
        {
            isCooking = true;
            cookingTimer = COOKING_TIME;
        }
        else if (isCooking)
        {
            if (cookingTimer <= 0)
            {
                isCooking = false;
                Cooking();
            }
            else
            {
                cookingTimer -= Time.deltaTime;
            }
        }
    }

    public void Cooking(){
        List<GameObject> ingredients_sorted = ingredients.OrderBy(key=>key.tag).ToList();
        var dish_key = (ingredients_sorted[0].tag, ingredients_sorted[1].tag, ingredients_sorted[2].tag);
        GameObject dish;
        String cookedDish;

        if (
        CookingDict.ContainsKey(dish_key)) {
            Debug.Log(CookingDict[dish_key]); //TODO: Remove
            cookedDish = "BowlOfGoods"; // Replace with CookingDict[dish_key]
        } else {
            Debug.Log("Bowl of goods"); // TODO: Remove
            cookedDish = "BowlOfGoods"; // Replace with "Bowl of goods"
        }

        ingredients.ForEach(Destroy);
        ingredients = new List<GameObject>();

        dish = Instantiate(Resources.Load(cookedDish, typeof(GameObject))) as GameObject;
        dish.transform.position = player.transform.position;
    }

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
