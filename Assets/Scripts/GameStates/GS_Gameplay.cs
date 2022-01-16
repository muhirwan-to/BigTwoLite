using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GS_Gameplay : GameState
{
    public enum GamePhase
    {
        Preparation,
        ShuffleCard,
        PlayingCard,
        CompareCard,
        Result
    }

    public GameObject       CharacterContainerPlayer { get; private set; }
    public List<GameObject> CharacterContainerAI { get; private set; }

    public GamePhase Phase { get; private set; }

    private void Awake()
    {
        UI = Instantiate(UIPrefab, GameManager.Instance.Canvas.transform);

        CharacterContainerPlayer = UI.transform.Find("Player").gameObject;

        CharacterContainerAI = new List<GameObject>();
        CharacterContainerAI.Add(UI.transform.Find("AI-0").gameObject);
        CharacterContainerAI.Add(UI.transform.Find("AI-1").gameObject);
        CharacterContainerAI.Add(UI.transform.Find("AI-2").gameObject);

        Phase = GamePhase.Preparation;
    }

    // Start is called before the first frame update
    void Start()
    {
        int AIIdx = 0;
        foreach (var player in GameManager.Instance.PlayerListPrefab)
        {
            if (player.IsMC)
            {
                Instantiate(player, CharacterContainerPlayer.transform);
            }
            else
            {
                Instantiate(player, CharacterContainerAI[AIIdx++].transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {        
    }
}
