using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ChessAbility : MonoBehaviour
{
    // Start is called before the first frame update
    protected Chess _owner { get; private set; }

    protected virtual void Awake()
    {
        _owner = GetComponentInParent<Chess>();
    }
}
