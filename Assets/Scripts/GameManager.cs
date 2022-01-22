using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager   Instance => s_instance;
 
    private static GameManager  s_instance = null;


    [SerializeField]
    private GameObject          m_canvas;
    [SerializeField]
    private List<GameState>     m_gameStateListPrefab;
    [SerializeField]
    private List<Card>          m_cardList;
    [SerializeField]
    private List<Actor>         m_playerList;

    public GameObject           Canvas => m_canvas;
    public List<Card>           CardList => m_cardList;
    public List<Actor>          PlayerList => m_playerList;
    public Actor                MainCharacter { get; private set; }

    public GameState            CurrentGameState { get; private set; }
    public SequenceChecker      SequenceChecker => GetComponent<SequenceChecker>();

    private void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }
        else if (s_instance == this)
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (m_gameStateListPrefab.Any())
        {
            SetGameState(EGameStateID.GS_PlayerSelection);
        }

        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameState(EGameStateID _id)
    {
        if (CurrentGameState)
        {
            for (int i = 0; i < m_canvas.transform.childCount; i++)
            {
                // destroy GUI objects
                Destroy(m_canvas.transform.GetChild(i).gameObject);
            }

            // destroy old state object
            Destroy(CurrentGameState.gameObject);
        }

        var prefab = m_gameStateListPrefab.Find(gs => gs.ID == _id);
        if (prefab)
        {
            CurrentGameState = Instantiate(prefab, transform);
        }
        else
        {
            CurrentGameState = null;
        }
    }

    public void SetMainCharacter(Actor _mc)
    {
        foreach (Actor player in PlayerList)
        {
            player.SetRole(player == _mc);
        }

        MainCharacter = _mc;
    }
}
