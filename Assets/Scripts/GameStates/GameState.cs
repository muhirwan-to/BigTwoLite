using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public enum Id
    {
        GS_CharacterSelection,
        GS_Preparation,
        GS_Gameplay,
        GS_Result
    }

    public Id ID { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
