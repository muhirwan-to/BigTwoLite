using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_Gameplay : GameState
{
    public enum EGamePhase
    {
        Preparation,
        ShuffleCard,
        PlayingCard,
        CompareCard,
        Result
    }

    public EGamePhase Phase { get; private set; }

    private void Awake()
    {
        UI = Instantiate(UIPrefab, GameManager.Instance.Canvas.transform);
        Phase = EGamePhase.Preparation;
    }

    // Start is called before the first frame update
    void Start()
    {
        int AIIdx = 0;
        foreach (var player in GameManager.Instance.CharacterListPrefab)
        {
            Character playerClone = Instantiate(player, UI.transform);
            Transform positionRef;

            if (player.IsMC)
            {
                positionRef = UI.transform.Find("PositionPlayer");
            }
            else
            {
                positionRef = UI.transform.Find("PositionAI_" + AIIdx++);
            }

            playerClone.transform.position = positionRef.position;
            playerClone.transform.rotation = positionRef.rotation;
            playerClone.transform.localScale = positionRef.localScale;

            Destroy(positionRef.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {        
    }
}
