using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public enum ESide
    {
        FaceUp,
        FaceDown
    }

    public enum EFlag
    {
        Diamond,
        Club,
        Heart,
        Spade
    }

    public enum EValue
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    [SerializeField]
    private EFlag       m_flag;
    [SerializeField]
    private EValue      m_value;

    public EFlag        Flag => m_flag;
    public EValue       Value => m_value;
    public int          ValueInt => (int)m_value;

    public bool         IsGUI { get; set; }
    public Actor        Actor { get; set; }
    public Card         LinkedCard { get; set; }
    

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DropArea"))
        {
            if (IsGUI && collision.transform.childCount == 0)
            {
                GetComponent<DragAndDrop>().SetObjectHover(collision.gameObject);
            }
        }
    }
}
