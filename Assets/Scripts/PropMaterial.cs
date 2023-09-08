using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropMaterial : MonoBehaviour
{
    public string[] materialsName;
    public float[] materialDurability;
    [HideInInspector]
    public string matName;
    [HideInInspector]
    public float durValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getMaterial(float value)
    {
        
        if(value < materialDurability[0])
        {
            matName = materialsName[0];
        }
        else if(value >= materialDurability[0] && value < materialDurability[1])
        {
            matName = materialsName[1];
        }
        else if (value >= materialDurability[1] && value < materialDurability[2])
        {
            matName = materialsName[2];
        }

        return matName;
    }

    public float getDurability(string nameString)
    {


        if (nameString == materialsName[0])
        {
            durValue = materialDurability[0];
        }
        else if (nameString == materialsName[1])
        {
            durValue = materialDurability[1];
        }
        else if (nameString == materialsName[2])
        {
            durValue = materialDurability[2];
        }

        return durValue;
    }
}
