using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropFracture : MonoBehaviour
{
    public GameObject originalObject;
    public GameObject fracturedObject;
    public GameObject VFX;
    public float explosionMinForce = 5;
    public float explosionMaxForce = 100;
    public float explosionForceRadius = 10;
    public float fragScaleFactor = 1;
    private GameObject fracObj;
    public PropManager father;
    private bool fractured;
    private bool destroyed;
    public Transform shockWavePoint;

    [Header("SwitchObject")]
    public bool switchObj;
    public GameObject objToSwitch;

    // Start is called before the first frame update
    void Start()
    {
        //father = GetComponent<PropManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(father != null)destroyed = father.destroyed;

        if (destroyed)
        {
            if (!fractured) 
            {
                this.transform.parent = null;
                Explode();
            }
            Destroy(gameObject, 7f);
        }
    }

    void Explode()
    {
        fractured = true;
        Shooting weaponStats = GameObject.Find("Player").GetComponent<Shooting>();
        shockWavePoint = weaponStats.hitPoint;
        explosionMaxForce = weaponStats.penetration;
        explosionForceRadius = weaponStats.power;

            if (fracturedObject != null)
            {
                if (switchObj)
                {
                    objToSwitch.SetActive(true);
                    objToSwitch.transform.parent = null;
                    fracObj = fracturedObject;
                    //fracObj.SetActive(true);
                }
                else
                {
                    fracObj = Instantiate(fracturedObject, transform.position, transform.rotation) as GameObject;
                }

                foreach (Transform t in fracObj.transform)
                {
                    var rb = t.GetComponent<Rigidbody>();

                    if (rb != null) rb.AddExplosionForce(Random.Range(explosionMinForce, explosionMaxForce), shockWavePoint.transform.position, explosionForceRadius);


                    StartCoroutine(Shrink(t, 2));
                }

                Destroy(fracObj, 5);

                if(VFX != null)
                {
                    GameObject vfxObj = Instantiate(VFX) as GameObject;
                    Destroy(VFX, 7);
                }
            }
    }


    IEnumerator Shrink(Transform t, float delay)
    {
        if(t != null)
        {
            yield return new WaitForSeconds(delay);

            Vector3 newScale = t.localScale;

            while (newScale.x >= 0)
            {
                newScale -= new Vector3(fragScaleFactor, fragScaleFactor, fragScaleFactor);

                t.localScale = newScale;
                yield return new WaitForSeconds(0.05f);
            }
        }

    }
}
