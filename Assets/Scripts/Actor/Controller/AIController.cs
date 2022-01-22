using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAIDifficulty
{
    Easy,
    Medium,
    Hard
}

public class AIController : PlayerController
{
    [SerializeField]
    private EAIDifficulty m_difficulty;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CurrentGameState.ID == EGameStateID.GS_Gameplay)
        {
            GS_Gameplay gs = GameManager.Instance.CurrentGameState as GS_Gameplay;

            if (gs.GamePhase == EGamePhase.PlayingCard)
            {
                switch (m_difficulty)
                {
                    case EAIDifficulty.Easy:
                        {
                            break;
                        }
                    case EAIDifficulty.Medium:
                        {
                            break;
                        }
                    case EAIDifficulty.Hard:
                        {
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }
    }
}
