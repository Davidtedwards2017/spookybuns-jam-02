using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_IC : MonoBehaviour
{
    public GameInfo.Character Character;
    public Collider2D Collider;

    public void OnMouseDown()
    {
        Debug.Log(string.Format("on mouse down: {0}", gameObject.name));
        ConversationController.Instance.StartConversation(Character);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
