using UnityEngine;
[CreateAssetMenu(fileName = "Story", menuName ="Dungeon/Story")]
public class StoryDungeonEvent : DungeonEvent
{
    public string storyText;
    public override void TriggerEvent()
    {
        StoryManager.instance.SetStory(storyText);
    }
}
