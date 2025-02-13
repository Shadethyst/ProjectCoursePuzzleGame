using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Item
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        id = 3;
        range = 1;
        GameManager.Instance.changeInteractor(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void interact(Item interaction)
    {
        base.interact(interaction);
    }
}
