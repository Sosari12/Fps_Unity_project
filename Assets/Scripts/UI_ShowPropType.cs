using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_ShowPropType : MonoBehaviour
{
    private Camera cam;
    public LayerMask detectable;
    [Header("UI")]
    public GameObject propTypeText;
    public GameObject propHpText;
   

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, 1000f, detectable))
        {
            PropManager detectedProp = hit.collider.GetComponent<PropManager>();
            if(detectedProp != null)EnableTexts(detectedProp.materialType, detectedProp.health);
        }
        else
        {
            DisableTexts();
        }
    }

    private void EnableTexts(string type, float hp)
    {
        propTypeText.SetActive(true);
        propHpText.SetActive(true);
        propTypeText.GetComponent<TextMeshProUGUI>().text = "Material: " + type;
        propHpText.GetComponent<TextMeshProUGUI>().text = "Hp: " + hp;
    }

    private void DisableTexts()
    {
        propTypeText.SetActive(false);
        propHpText.SetActive(false);
    }
}
