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
        
    public List<Character>      Players;

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

    void SetGameState(GameState.Id id)
    {
        if (CurrentGameState)
        {
            Destroy(CurrentGameState.gameObject);
        }

        CurrentGameState = m_gameStateList.Find(gs => gs.ID == id);

        Instantiate(CurrentGameState, transform);
    }
}
