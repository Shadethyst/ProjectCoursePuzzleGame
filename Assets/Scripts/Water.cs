using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Item
{
    [SerializeField] protected Item transformElement;
    private float movementDir;
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
        if (interaction is Element)
        {
            gameObject.SetActive(false);
            var spawnedElement = Instantiate(transformElement, this.transform.position, Quaternion.identity);
            spawnedElement.name = "interactedElement";
            this.enabled = false;
        }
    }
    protected void OnOnGameStateChanged(GameState state)
    {
        if(state == GameState.Turn)
        {

        }
    }
}
