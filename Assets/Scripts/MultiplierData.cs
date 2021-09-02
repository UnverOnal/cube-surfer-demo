using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierData : MonoBehaviour
{
    [SerializeField]private Text number;

    public int Multiplier
    {
        get
        {
            return int.Parse(number.text);
        }
    }
}
