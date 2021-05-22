using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class QuestGiver : NPC
{
    [SerializeField]
    private Quest[] quests;

    [SerializeField]
    private Sprite question, questionSilver, exclamation;

    [SerializeField]
    private SpriteRenderer statusBackground;

    [SerializeField]
    private SpriteRenderer statusRenderer;

    public Quest[] MyQuests {
        get
        {
            return quests;
        }
    }

    private void Start()
    {
        foreach (Quest quest in quests)
        {
            quest.MyQuestGiver = this;
        }

        UpdateQuestStatus();
    }

    public void UpdateQuestStatus()
    {
        foreach (Quest quest in quests)
        {
            if (quest != null)
            {
                statusBackground.color = SetAlpha(statusBackground.color, true);
                if (quest.IsComplete && QuestLog.MyInstance.HasQuest(quest))
                {
                    statusRenderer.sprite = question;
                    break;
                }
                else if (!QuestLog.MyInstance.HasQuest(quest))
                {
                    statusRenderer.sprite = exclamation;
                    break;
                }
                else if (!quest.IsComplete && QuestLog.MyInstance.HasQuest(quest))
                {
                    statusRenderer.sprite = questionSilver;
                }
            }
        }

        if(quests.Length == 0)
        {
            statusRenderer.sprite = null;
            statusBackground.color = SetAlpha(statusBackground.color, false);
            
        }
    }

    Color SetAlpha(Color color, bool alpha)
    {
        Color next = color;
        if (alpha)
        {
            next.a = 1;
        }
        else
        {
            next.a = 0;
        }

        return next;
    }

}
