using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingNPC : NPCParent
{
    public void Awake()
    {
        npcName = "TalkingNPC";

        gameObject.tag = "TalkingNPC";
    }

    public override void StartInteraction()
    {
        base.StartInteraction();

        Debug.Log("Player interacts with the talking NPC");
        Debug.Log(npcName);

    }

    public override void ExitInteraction()
    {
        base.ExitInteraction();

        Debug.Log("Bye");
    }
}
