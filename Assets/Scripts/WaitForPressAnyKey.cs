using UnityEngine;

public class WaitForPressAnyKey : CustomYieldInstruction {

    public override bool keepWaiting
    {
        get
        {
            return !Input.GetMouseButtonDown(0) && !Input.anyKeyDown;
        }
    }
}
