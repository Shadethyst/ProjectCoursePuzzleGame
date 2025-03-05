using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositioner : MonoBehaviour
{
    [SerializeField] Vector2 startPoint;
    // Start is called before the first frame update
    private void movePlayer()
    {
        PlayerController.instance.gameObject.transform.position = startPoint;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        movePlayer();
    }
    }
