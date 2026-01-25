using System.Collections.Generic;
using UnityEngine;

public class DoTutorialTest : Entity
{
    protected override void OnInteract()
    {
        TutorialManagerUi.DoTutorialMessages(new List<string>(){
            "This is test message #1",
            "This is test message #2, which should Debug.Log tutorial finished once this message ends!",
        }, () =>
        {
            Debug.Log("Finished tutorial");
        });
    }

    protected override void OnStart() { }
}
