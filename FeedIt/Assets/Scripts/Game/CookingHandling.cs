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
    public bool isCooking = false;

    private List<GameObject> ingredients = new List<GameObject>();

    // Sound effects
    public AudioSource pickUpSound;
    public AudioSource bonApetitSound;

    /*
       All recipies should be in the recipies.txt. Follow the format: Ingredient,Ingredient,Ingredient;Dish
       Ingredients should prefabs found in Assets/Prefabs/Ingredients and the dishes should be prefabs in Assets/Resources
       ! NAMES HAS TO BE THE SAME IN THE TXT AND PREFABS, they need to correspond !
    */
    private String[] recipies;
    /* private var dish_score = new List<(string,int)>(); */
    private Dictionary<(string, string, string), String[]> CookingDict = new Dictionary<(string, string, string), String[]>();
    private GameObject player;
    private ScoreCounter scoreCounter;

    public void Start(){

        recipies = Resources.Load<TextAsset>("recipies").text.Split('\n');
        player = GameObject.Find("Ostrich");
        scoreCounter = GameObject.FindGameObjectWithTag("ScoreCounter").GetComponent<ScoreCounter>();

        foreach (var recipy in recipies)
        {
            var recipy_split = recipy.Split(';');
            var recipy_split_ingredients = recipy_split[0].Split(',');
            String[] recipy_dish_score = recipy_split[1].Split(',');
            Array.Sort(recipy_split_ingredients);
            var recipy_ingredients = (recipy_split_ingredients[0],recipy_split_ingredients[1],recipy_split_ingredients[2]);

            CookingDict.Add(recipy_ingredients, recipy_dish_score);
        }
    }

    public void Update(){
        if(!isCooking && ingredients.Count == 3 && player.transform.childCount > 0)
        {
            isCooking = true;
            cookingTimer = COOKING_TIME;
            //Cooking sound
            FindObjectOfType<AudioManager>().Play("cooking_sound");
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
        int dish_score = 30; // Default for BowlOfGoods and BowlOfGoodsTranquilizer

        if (ingredients_sorted[0].tag == "Mushroom" || ingredients_sorted[1].tag == "Mushroom" || ingredients_sorted[2].tag == "Mushroom")
        {
            cookedDish = "TranquilizerDish";
        }
        else if (
        CookingDict.ContainsKey(dish_key)) {
            cookedDish = CookingDict[dish_key][0];
            dish_score = Int16.Parse(CookingDict[dish_key][1]);
        }
        else {
            cookedDish = "BowlOfGoodies";
        }

        ingredients.ForEach(Destroy);
        ingredients = new List<GameObject>();

        dish = Instantiate(Resources.Load(cookedDish, typeof(GameObject))) as GameObject;
        scoreCounter.addScore(dish_score);
        dish.transform.position = player.transform.position;
        bonApetitSound.Play();
    }

    public bool CollectIngredient(GameObject ingredient)
    {
        var numIngredients = ingredients.Count;
        if(numIngredients < 3)
        {
            ingredient.transform.parent = transform;
            ingredient.transform.position = positions[numIngredients];
            ingredients.Add(ingredient);
            pickUpSound.Play();
            return true;
        }
        return false;
    }
}
