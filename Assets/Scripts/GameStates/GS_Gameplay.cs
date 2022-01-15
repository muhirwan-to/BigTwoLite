using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_Gameplay : GameState
{
    private enum GamePhase
    {
        Preparation,
        ShuffleCard,
        PlayingCard,
        CompareCard,
        Result
    }

    private void Awake()
    {
        ID = Id.GS_Gameplay;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
