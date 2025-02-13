using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : Item
{

    private void Awake()
    {
        id = 7;
        range = 0;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position == PlayerController.instance.transform.position)
        {
            GameManager.Instance.UpdateGameState(GameState.LevelComplete);
        }
    }
}
