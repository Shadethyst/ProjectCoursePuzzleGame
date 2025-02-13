using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Air : Item
{

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        id = 4;
        range = 1;
        GameManager.Instance.changeInteractor(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void interact(int interaction)
    {
        base.interact(interaction);
    }
}
