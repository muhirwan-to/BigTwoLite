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
    private List<Card>          m_cardListPrefab;
    [SerializeField]
    private List<Actor>         m_playerList;

    public GameObject           Canvas => m_canvas;
    public List<Card>           CardListPrefab => m_cardListPrefab;
    public List<Actor>          PlayerList => m_playerList;

    public GameState            CurrentGameState { get; private set; }

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
            SetGameState(GameState.EId.GS_PlayerSelection);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectPlayer(Actor _selected)
    {
        foreach (var player in PlayerList)
        {
            player.SetRole(player == _selected);
        }
    }

    public void SetGameState(int _id)
    {
        SetGameState((GameState.EId)_id);
    }

    public void SetGameState(GameState.EId _id)
    {
        if (CurrentGameState)
        {
            for (int i = 0; i < m_canvas.transform.childCount; i++)
            {
                // destroy UI objects
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
}
