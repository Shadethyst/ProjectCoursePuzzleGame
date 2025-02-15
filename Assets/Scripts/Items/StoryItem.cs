using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryItem : Item
{
    private bool played;

    private void Awake()
    {
        id = 8;
        range = 0;
        played = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position == PlayerController.instance.transform.position && !played)
        {
            played = true;
            GameManager.Instance.UpdateGameState(GameState.Story);
        }
    }
}
