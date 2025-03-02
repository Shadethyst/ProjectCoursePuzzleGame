using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerTrap : Item
{
    private Animator rockAnimator;

    private void Awake()
    {
        id = Id.DANGER_TRAP;
        range = 0;
        GameManager.Instance.changeInteractor(1);
        rockAnimator = this.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public override void placeItem(Vector2 pos)
    {
        Tile tile = GridManager.instance.getTileAtPos(pos);
        if (tile.getItem(id) == false)
        {
            base.placeItem(pos);
            tile.addItem(id);
            tile.setWalkable(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact(Id other)
    {
        base.Interact(other);
        if (other == Id.FIRE)
        {
            rockAnimator.SetBool("Burning", true);
        }
        if (other == Id.WATER && rockAnimator.GetBool("Burning") == true)
        {
            rockAnimator.SetBool("Crumbling", true);
            Tile tile = GridManager.instance.getTileAtPos(this.transform.position);
            tile.setWalkable(true);
        }
    }
}
