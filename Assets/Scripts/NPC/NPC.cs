using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCConversation m_NPCConversation;
    public bool IsInteracting { get; set; }

    private void OnMouseDown()
    {
        if (!IsInteracting)
        {
            ConversationManager.Instance.StartConversation(m_NPCConversation);
        }
    }

    public virtual void Interact()
    {
        if (!IsInteracting)
        {
            IsInteracting = true;
        }
    }

    public virtual void StopInteract()
    {
        if (IsInteracting)
        {
            IsInteracting = false;
            
        }
    }
}

