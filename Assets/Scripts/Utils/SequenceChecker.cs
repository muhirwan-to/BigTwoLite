using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceChecker : MonoBehaviour
{
    public enum ESequence
    {
        HighCard,
        Pair,
        TwoPairs,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
    }

    public enum ESpecialSequence
    {
        None,
        TripleFlush,
        TripleStraight,
        SizPair,
        QuartetTriple,
        OneColor,
        AllSmall,
        AllBig,
        TripleQuartet,
        TripleStraightFlush,
        RoyalKing,
        Dragon,
        RoyalDragon
    }

    public string[] SequenceName = 
    {
        "High Card",
        "Pair",
        "Two Pairs",
        "3 of A Kind",
        "Straight",
        "Flush",
        "Full House",
        "4 of A Kind",
        "Straight Flush"
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ESequence GetSequence(List<Card> _cardList)
    {
        _cardList.Sort((card1, card2) => card1.Value.CompareTo(card2.Value));

        int spadeCount = 0, heartCount = 0, clubCount = 0, diamondCount = 0;
        int valueDifference = 0;

        Dictionary<int, int> pairMap = new Dictionary<int, int>();

        Card prev = null;
        foreach (Card card in _cardList)
        {
            spadeCount += card.Flag == Card.EFlag.Spade ? 1 : 0;
            heartCount += card.Flag == Card.EFlag.Heart ? 1 : 0;
            clubCount += card.Flag == Card.EFlag.Club ? 1 : 0;
            diamondCount += card.Flag == Card.EFlag.Diamond ? 1 : 0;

            if (!pairMap.ContainsKey(card.ValueInt))
            {
                pairMap[card.ValueInt] = 0;
            }
            
            pairMap[card.ValueInt]++;

            if (prev)
            {
                valueDifference = Mathf.Max(valueDifference, card.ValueInt - prev.ValueInt);
            }

            prev = card;
        }

        bool flush = spadeCount == 5 || heartCount == 5 || clubCount == 5 || diamondCount == 5;
        bool straight = _cardList.Count == 5 && valueDifference == 1;
        bool hasQuartet = false, hasTriplet = false, hasPair = false, hasTwoPairs = false;

        int tripletInt = 0, pairInt = 0;

        foreach (var pair in pairMap)
        {
            if (pair.Value == 4)
            {
                hasQuartet = true;
            }
            else if (pair.Value == 3)
            {
                tripletInt = pair.Key;
                hasTriplet = true;
            }
            else if (pair.Value == 2)
            {
                pairInt = pair.Key;

                if (!hasPair)
                {
                    hasPair = true;
                }
                else
                {
                    hasTwoPairs = true;
                }
            }
        }

        if (straight && flush)
        {
            return ESequence.StraightFlush;
        }
        else if (hasQuartet)
        {
            return ESequence.FourOfAKind;
        }
        else if (hasTriplet && hasPair && tripletInt > pairInt)
        {
            return ESequence.FullHouse;
        }
        else if (flush)
        {
            return ESequence.Flush;
        }
        else if (straight)
        {
            return ESequence.Straight;
        }
        else if (hasTriplet)
        {
            return ESequence.ThreeOfAKind;
        }
        else if (hasTwoPairs)
        {
            return ESequence.TwoPairs;
        }
        else if (hasPair)
        {
            return ESequence.Pair;
        }
        else
        {
            return ESequence.HighCard;
        }
    }

    //public ESpecialSequence GetSpecialSequence(List<Card> _cardList)
    //{
    //    _cardList.Sort((card1, card2) => card1.Value.CompareTo(card2.Value));
    //}
}
