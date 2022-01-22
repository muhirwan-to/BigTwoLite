using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIController : Controller
{
    private Dictionary<EGameDifficulty, float> AIThinkingInterval = new Dictionary<EGameDifficulty, float>
    {
        { EGameDifficulty.Easy, 2.0f },
        { EGameDifficulty.Medium, 1.0f },
        { EGameDifficulty.Hard, 0.5f },
    };


    private Coroutine   m_coroutine;
    public bool         IsArranging { get; private set; }

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
            EGameDifficulty difficulty = GameManager.Instance.GameDifficulty;

            if (gs.GamePhase == EGamePhase.PlayingCard)
            {
                if (IsArranging)
                {
                    return;
                }

                switch (difficulty)
                {
                    case EGameDifficulty.Easy:
                        {
                            m_coroutine = StartCoroutine(ArrangeCardDumb());
                            break;
                        }
                    case EGameDifficulty.Medium:
                        {
                            m_coroutine = StartCoroutine(ArrangeCardSmart());
                            break;
                        }
                    case EGameDifficulty.Hard:
                        {
                            m_coroutine = StartCoroutine(ArrangeCardGenius());
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            else if (gs.GamePhase == EGamePhase.CompareCard)
            {
                StopCoroutine(m_coroutine);

                IsArranging = false;
            }
        }
    }

    IEnumerator ArrangeCardDumb()
    {
        IsArranging = true;

        for (int i = 0; i < Actor.InHandCards.Count; i++)
        {
            print("AI: " + Actor.Name + " is arranging [" + i + "] cards, with thinking speed: " + AIThinkingInterval[EGameDifficulty.Easy]);

            Actor.InHandCards.Sort(0, i, new CardComparator(
                (card1, card2) => Utility.SwapParent(card1.transform, card2.transform, false))
                );

            yield return new WaitForSeconds(AIThinkingInterval[EGameDifficulty.Easy]);
        }

        yield return new WaitForSeconds(AIThinkingInterval[EGameDifficulty.Easy]);
    }

    IEnumerator ArrangeCardSmart()
    {
        IsArranging = true;

        for (int i = 0; i < Actor.InHandCards.Count; i++)
        {
            print("AI: " + Actor.Name + " is arranging [" + i + "] cards, with thinking speed: " + AIThinkingInterval[EGameDifficulty.Medium]);

            Actor.InHandCards.Sort(0, i, new CardComparator(
                (card1, card2) => Utility.SwapParent(card1.transform, card2.transform, false))
                );

            yield return new WaitForSeconds(AIThinkingInterval[EGameDifficulty.Medium]);
        }

        yield return new WaitForSeconds(AIThinkingInterval[EGameDifficulty.Medium]);
    }

    IEnumerator ArrangeCardGenius()
    {
        IsArranging = true;

        for (int i = 0; i < Actor.InHandCards.Count; i++)
        {
            print("AI: " + Actor.Name + " is arranging [" + i + "] cards, with thinking speed: " + AIThinkingInterval[EGameDifficulty.Hard]);

            Actor.InHandCards.Sort(0, i, new CardComparator(
                (card1, card2) => Utility.SwapParent(card1.transform, card2.transform, false))
                );

            yield return new WaitForSeconds(AIThinkingInterval[EGameDifficulty.Hard]);
        }

        yield return new WaitForSeconds(AIThinkingInterval[EGameDifficulty.Hard]);
    }
}
