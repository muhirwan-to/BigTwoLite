using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GUIBase : MonoBehaviour
{
    private GameState       m_gameState;

    public _GameStateClass  GetGamseState<_GameStateClass>() where _GameStateClass : GameState => m_gameState as _GameStateClass;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameState(GameState _gameState)
    {
        m_gameState = _gameState;
    }
}
