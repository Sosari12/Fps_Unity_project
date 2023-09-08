using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{
    public string materialType;
    public float durability;
    public float health;
    public bool destroyed;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void getHit(float damage, float penetration)
    {
        if(penetration > durability && !destroyed)
        {
            calculateDamage(damage);
        }
    }

    private void calculateDamage(float damage)
    {

        if (health > damage)
        {
            health -= damage;
        }else if(health <= damage)
        {
            health = 0;
            destroyed = true;
            Destroy(gameObject, 0.05f);
        }
    }
}
