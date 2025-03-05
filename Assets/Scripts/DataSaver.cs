using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DataSaver : MonoBehaviour
{
    private int incarnation;
    private Vector2 respawnPosition;
    private List<Item> savedItems;
    private List<int> savedItemAmounts;

    [SerializeField] private GameObject grid;
    [SerializeField] GameObject[] tilemaps;
    private GameObject activeTilemap;
    [SerializeField] private GameObject sceneTransition;

    // Start is called before the first frame update
    public void Start()
    {
        respawnPosition = PlayerController.instance.transform.position;
        incarnation = 0;
        activeTilemap = grid.transform.GetComponentsInChildren<Transform>()[0].gameObject;
        activeTilemap.SetActive(true);
        respawnPosition = PlayerController.instance.transform.position;
    }

    public void Update()
    {

    }


    public void Restart()
    {
        sceneTransition.gameObject.GetComponent<SceneTransition>().SetReadyToEndScene(true);
        GameObject[] environmentItems = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject environmentItem in environmentItems)
        {
            Destroy(environmentItem);
        }

        StartCoroutine(ResetToCheckpoint());
    }

    public IEnumerator ResetToCheckpoint()
    {
        yield return new WaitForSeconds(2.0f);
        PlayerController.instance.transform.position = respawnPosition;
        Destroy(grid.gameObject.transform.GetChild(0).gameObject);
        GridManager.instance.EmptyList();
        Instantiate(tilemaps[0], grid.transform, false);
        AddItems(savedItems, savedItemAmounts);
        yield return new WaitForSeconds(1.0f);
        PlayerController.instance.setOccupiedTile(GridManager.instance.getTileAtPos(respawnPosition));
        GameManager.Instance.UpdateGameState(GameState.WaitForInput);
        sceneTransition.gameObject.GetComponent<SceneTransition>().SetReadyToBeginScene(true);
    }


    public int GetIncarnation()
    {
        return this.incarnation;
    }

    public void SetRespawnPosition(Vector2 changedPosition)
    {
        this.respawnPosition = changedPosition;
        incarnation++;
    }

    public void AddItems(List<Item> items, List<int> amounts)
    {
        foreach (Item item in items)
        {
            PlayerController.instance.gameObject.GetComponent<PlayerController>().setItem(item, amounts[items.IndexOf(item)]);
        }

        savedItems = items;
        savedItemAmounts = amounts;
    }
}