using UnityEngine;

public class DoTutorialTest : Entity
{
    protected override void OnInteract()
    {
        TutorialManagerUi.DoTutorialMessage("This is test message #1", () =>
        {
            TutorialManagerUi.DoTutorialMessage("This is test message #2, which should Debug.Log tutorial finished once this message ends!", () =>
            {
                Debug.Log("Finished tutorial");
            });
        });
    }

    protected override void OnStart() { }
}
