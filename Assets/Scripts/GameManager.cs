using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;
    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        UpdateGameState(GameState.GenerateLevel);
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
            case GameState.GenerateLevel:
                GridManager.instance.generatePuzzle(GridManager.instance.getVariant());
                break;
            case GameState.WaitForInput:
                break;
            case GameState.Turn:
                break;
            case GameState.LevelComplete:
                break;
            case GameState.Defeat:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Invoke(newState);
    }
}
public enum GameState
{
    GenerateLevel,
    WaitForInput,
    Turn,
    LevelComplete,
    Defeat

}
