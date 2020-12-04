using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    [SerializeField] private float BEAST_EATING_MOVEMENT = 0.5f;
    [SerializeField] private float scrollSpeed = 10f;
    [SerializeField] private float destroyPoistionX = -45f;

    private Beast beast;

    void Start()
    {
        beast = GameObject.FindGameObjectWithTag("Beast").GetComponent<Beast>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Beast"))
        {
            Beast.FoodType type = BEAST_EATING_MOVEMENT > 0 ? Beast.FoodType.NormalDish : Beast.FoodType.TranquilizedDish;
            beast.Move(BEAST_EATING_MOVEMENT, type);
            Destroy(gameObject);
        }
    }

    protected virtual void FixedUpdate()
    {
        if(transform.position.x < destroyPoistionX)
        {
            Destroy(this.gameObject);
        }
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
    }
}
