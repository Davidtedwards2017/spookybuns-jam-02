using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogueNode
{
    string Who { get; }
    string Expression { get; }
}