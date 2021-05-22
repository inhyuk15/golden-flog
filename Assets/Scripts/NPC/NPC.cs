using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCConversation m_NPCConversation;
    public bool IsInteracting { get; set; }

    public bool firstComplete;

    public void SetFirstComplete()
    {
        firstComplete = true;
    }

    private void OnMouseDown()
    {
        if (!IsInteracting)
        {
            ConversationManager.Instance.StartConversation(m_NPCConversation);
            if (gameObject.name == "Frog")
            {
                if (firstComplete)
                {
                    ConversationManager.Instance.SetBool("firstComplete", true);
                }
            }
            
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

