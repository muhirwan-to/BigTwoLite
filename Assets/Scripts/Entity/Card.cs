using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardComparator : IComparer<Card>
{
    public delegate void TrueCallback(Card _1, Card _2);

    public CardComparator(TrueCallback _callback)
    {
        callback = _callback;
    }

    public int Compare(Card _1, Card _2)
    {
        int compare = _1.Value.CompareTo(_2.Value);
        if (compare < 0)
        {
            if (callback != null)
            {
                callback(_1, _2);
            }
        }

        return compare;
    }

    private TrueCallback callback;
}

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
        GetComponent<DragAndDrop>().OnDropCallback = OnDrop;
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

    public void OnDrop(GameObject _object)
    {
        DropArea dropArea = _object.GetComponent<DropArea>();
        if (dropArea && dropArea.LinkedCardParent)
        {
            if (IsGUI && LinkedCard && !LinkedCard.IsGUI && LinkedCard.transform.parent != dropArea.LinkedCardParent)
            {
                Utility.SwapParent(LinkedCard.transform, dropArea.LinkedCardParent.GetChild(0), false);
            }
        }
    }
}
