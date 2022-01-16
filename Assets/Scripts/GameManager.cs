using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager   Instance => s_instance;
 
    private static GameManager  s_instance = null;

    [SerializeField]
    private List<GameState>     m_gameStateListPrefab;
    [SerializeField]
    private List<Character>     m_playerListPrefab;
    public List<Character>      PlayerListPrefab => m_playerListPrefab;
    [HideInInspector]
    public Character            MCPrefab;

    [SerializeField]
    private GameObject          m_canvas;
    public GameObject           Canvas => m_canvas;

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
        MCPrefab = null;

        if (m_gameStateListPrefab.Any())
        {
            SetGameState(GameState.Id.GS_CharacterSelection);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameState(int _id)
    {
        SetGameState((GameState.Id)_id);
    }

    public void SetGameState(GameState.Id _id)
    {
        if (CurrentGameState)
        {
            for (int i = 0; i < m_canvas.transform.childCount; i++)
            {
                // destroy UI objects
                Destroy(m_canvas.transform.GetChild(i).gameObject);
            }

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

    public void SelectCharacter(string _name)
    {
        Character c = null;
        foreach (var player in PlayerListPrefab)
        {
            player.IsMC = false;

            if (player.Name == _name)
            {
                c = player;
            }
        }

        if (c)
        {
            MCPrefab = c;
            MCPrefab.IsMC = true;
        }
    }
}
