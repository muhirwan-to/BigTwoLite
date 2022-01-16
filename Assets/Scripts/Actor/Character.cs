using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Character : Selectable
{
    [SerializeField]
    private string  m_name;

    public string   Name => m_name;
    public bool     IsMC { get; private set; }
    public bool     IsSelected => currentSelectionState == SelectionState.Selected;

    // Start is called before the first frame update
    protected override void Start()
    {
        IsMC = false;

        Transform txtName = gameObject.transform.Find("TxtName");
        if (txtName != null)
        {
            txtName.gameObject.GetComponent<Text>().text = Name;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
