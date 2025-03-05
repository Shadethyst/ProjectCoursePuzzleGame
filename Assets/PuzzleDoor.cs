using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDoor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] firePuzzles;
    private int puzzlesCompleted;
    private bool open;
    void Start()
    {
        puzzlesCompleted = 0;
        open = false;
        GridManager.instance.getTileAtPos(this.transform.position).setWalkable(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.Instance.state == GameState.Defeat)
        {
            open = false;
        }

        foreach(GameObject firePuzzle in firePuzzles)
        {
            if (!firePuzzle.GetComponent<FirePuzzle>().GetIsActivated())
            {
                puzzlesCompleted = 0;
            }
            else
            {
                puzzlesCompleted++;
            }
        }

        if (puzzlesCompleted >= firePuzzles.Length)
        {
            open = true;
            GridManager.instance.getTileAtPos(this.transform.position).setWalkable(true);
        }

        if (open)
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
            GridManager.instance.getTileAtPos(this.transform.position).setWalkable(true);
        }
        else
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
            GridManager.instance.getTileAtPos(this.transform.position).setWalkable(false);
        }
    }
}
