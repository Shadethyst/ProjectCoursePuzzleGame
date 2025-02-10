using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : Item
{
    [SerializeField] protected Item transformElement;
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void interact(Item interaction)
    {
        if(interaction is Element){
            
            var spawnedElement = Instantiate(transformElement, this.transform.position, Quaternion.identity);
            spawnedElement.name = "interactedElement";
            this.enabled = false;
        }
    }
}