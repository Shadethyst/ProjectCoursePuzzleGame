using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;
    public static event Action<GameState> OnGameStateChanged;
    private int movers;
    private int movementDone;
    private int interactionDone;
    private int interactors;
    private int storySceneNumber;
    

    [SerializeField] private StoryManager storyManager;
    [SerializeField] private SceneTransition sceneTransition;

    private void Awake()
    {
        Instance = this;
        movementDone = 0;
        movers = 0;
        storySceneNumber = 0;
    }
    private void Start()
    {
        UpdateGameState(GameState.Story);
    }
    private IEnumerator minTurnDuration(GameState state)
    {
        yield return new WaitForSeconds(0.2f);
        UpdateGameState(state);
    }


    private void Update()
    {
        if(state == GameState.ItemMovement && movementDone >= movers)
        {
            //UpdateGameState(GameState.Turn);
            //movementDone = 0;
        }
        if(state == GameState.Turn && interactionDone >= interactors)
        {
            //UpdateGameState(GameState.WaitForInput);
            //interactionDone = 0;
        }
    }
    /*
     *updates game state
     *current game states:
     *generate level, at start of game the state is GenerateLevel, should go to state from LevelComplete state to move to next level
     *WaitForInput state, moved to from generation and whenever a turn has passed, waits for player to input a command to proceed a turn
     *Turn state, state where turn actions such as movement, element interactions and level end state check happen, moves into either WaitForInput or LevelComplete depending on if the level completes or not
     *LevelComplete state, state which is moved into when level objectives are met, cleanup of level and showing of level end screen happens in this state
     *Defeat state, state which is moved into when player is standing in a spot with harmful objects, allows for restart of level and maybe? rewinding of last action
     *
     */
    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.Story:
                storyManager.PlayScene(storySceneNumber);
                storySceneNumber++;
                break;
            case GameState.GenerateLevel:
                GridManager.instance.generatePuzzle(GridManager.instance.getVariant());
                break;
            case GameState.WaitForInput:
                break;
            case GameState.Turn:
                interactionDone = 0;
                StartCoroutine(minTurnDuration(GameState.WaitForInput));
                
                break;
            case GameState.ItemMovement:
                movementDone = 0;
                StartCoroutine(minTurnDuration(GameState.Turn));
                break;
            case GameState.LevelComplete:
                sceneTransition.SetReadyToEndScene(true);
                break;
            case GameState.Defeat:
                break;
            case GameState.Movement:
                
                break;
            case GameState.Placement:
                PlayerController.instance.placeSelected();
                break;
            case GameState.Pause:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Invoke(newState);
        Debug.Log(newState);
    }
    public void changeMover(int add)
    {
        movers += add;
    }
    public void doneMoving()
    {
        movementDone += 1;
    }
    public void changeInteractor(int add)
    {
        interactors += add;
    }
    public void doneInteracting()
    {
        interactionDone += 1;
    }
} 

public enum GameState
{
    Story,
    GenerateLevel,
    WaitForInput,
    Movement,
    Placement,
    ItemMovement,
    Turn,
    LevelComplete,
    Defeat,
    Pause

}
