using TMPro;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public CanvasGroup panelStory;
    public TextMeshProUGUI storyText;
    public static StoryManager instance;
    void Awake()
    {
        instance = this;
    }

    public void SetStory(string story)
    {
        panelStory.alpha =1;
        panelStory.blocksRaycasts =true;
        panelStory.interactable = true;
        storyText.text = story;
    }
    public void Next()
    {
        panelStory.alpha =0;
        panelStory.blocksRaycasts =false;
        panelStory.interactable = false;
        DungeonManager.instance.NextDungeonEvent();
    }
}
