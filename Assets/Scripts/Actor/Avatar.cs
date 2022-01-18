using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Avatar : Button
{
    public Actor Actor { get; set; }

    protected override void Awake()
    {
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        Transform txtName = gameObject.transform.Find("TxtName");
        if (txtName != null)
        {
            txtName.gameObject.GetComponent<Text>().text = Actor != null ? Actor.Name : "";
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
