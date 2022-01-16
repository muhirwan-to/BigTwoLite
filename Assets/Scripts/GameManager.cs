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
    public GameObject           Canvas => m_canvas;

    [SerializeField]
    private List<GameState>     m_gameStateList;

    [SerializeField]
    private List<Character>     m_playerList;
    public List<Character>      PlayerList => m_playerList;

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
        if (m_gameStateList.Any())
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

        CurrentGameState = m_gameStateList.Find(gs => gs.ID == _id);
        CurrentGameState = Instantiate(CurrentGameState, transform);
    }

    public void SelectCharacter(string _name)
    {
        Character c = m_playerList.Find(player => player.Name == _name);

        if (c)
        {
            c.Select();
        }
    }
}
