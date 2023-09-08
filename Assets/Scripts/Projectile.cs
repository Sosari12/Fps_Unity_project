using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float power;
    [HideInInspector]
    public float penetration;
    [HideInInspector]
    public bool collided;

    public bool stick;
    private Shooting Player;
    public float projectileSpeed;
    public LayerMask shootableLayer;

    private void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Shooting>();
    }

    private void Update()
    {
        //CheckInFront();
    }

    void CheckInFront()
    {
        Ray ray = new Ray(transform.position, transform.forward); 
        if(Physics.Raycast(ray, out RaycastHit hit, 1f, shootableLayer))
        {
            Vector3 destination;
            destination = hit.point;
            float dist = Vector3.Distance(destination, transform.position);
            if(dist <= 0)
            {
                stopMoving();
            }
        }
    }


    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Prop"))
        {
            PropManager hitProp = other.GetComponent<PropManager>();
            if(hitProp.durability > penetration)
            {
                if(transform.parent == null)
                {
                    stopMoving();
                    transform.SetParent(other.transform);
                    hitProp.GetComponent<Rigidbody>().AddExplosionForce(1f, transform.position, 4f);
                }
            }
            else
            {
                
                hitProp.getHit(damage, penetration);
                Player.hitPoint = transform;
                if (hitProp.health > 0)
                {
                    if(transform.parent == null)
                    {
                        stopMoving();
                        transform.SetParent(other.transform);
                        hitProp.GetComponent<Rigidbody>().AddExplosionForce(1f, transform.position, 4f);
                    }

                }
            }
        }

        if (other.CompareTag("Ground"))
        {
            if (stick)
            {
                stopMoving();
            }
        }
    }
    */


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Prop"))
        {
            PropManager hitProp = collision.gameObject.GetComponent<PropManager>();
            if (hitProp.durability > penetration)
            {
                if (transform.parent == null)
                {
                    stopMoving();
                    transform.SetParent(collision.gameObject.transform);
                    hitProp.GetComponent<Rigidbody>().AddExplosionForce(1f, transform.position, 4f);
                }
            }
            else
            {

                hitProp.getHit(damage, penetration);
                Player.hitPoint = transform;
                if (hitProp.health > 0)
                {
                    if (transform.parent == null)
                    {
                        stopMoving();
                        transform.SetParent(collision.gameObject.transform);
                        hitProp.GetComponent<Rigidbody>().AddExplosionForce(1f, transform.position, 4f);
                    }

                }
            }
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            if (stick)
            {
                stopMoving();
            }
        }
    }


    void stopMoving()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.velocity = new Vector3(0f, 0f, 0f);
        rb.useGravity = false;
        transform.GetComponent<BoxCollider>().enabled = false;
    }
    
}
