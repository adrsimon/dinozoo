using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoScript : MonoBehaviour
{
    public GameObject dinosaur_body;
    public GameObject dinosaur_extremity;

    private Color originalBodyColor;
    private Color originalExtremityColor;

    // Start is called before the first frame update
    void Start()
    {
        originalBodyColor = dinosaur_body.GetComponent<Renderer>().material.color;
        originalExtremityColor = dinosaur_extremity.GetComponent<Renderer>().material.color;
        StartCoroutine(HungerCycle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Fish"))
        {
            dinosaur_body.GetComponent<Renderer>().material.color = originalBodyColor;
            dinosaur_extremity.GetComponent<Renderer>().material.color = originalExtremityColor;
            Destroy(other.gameObject);
        }
    }

    IEnumerator HungerCycle()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(10, 30));
            dinosaur_body.GetComponent<Renderer>().material.color = Color.red;
            dinosaur_extremity.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}