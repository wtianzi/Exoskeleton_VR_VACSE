using UnityEngine;
using Valve.VR;

public class ShieldController : MonoBehaviour
{
    public SteamVR_Action_Boolean action;
    public SteamVR_Input_Sources handType;
    public Transform shield;

    private void OnEnable()
    {
        if (action == null)
        {
            Debug.LogError("<b>[SteamVR Interaction]</b> No action assigned");
            return;
        }

        action.AddOnChangeListener(OnActionChange, handType);
    }

    private void OnDisable()
    {
        if (action != null)
            action.RemoveOnChangeListener(OnActionChange, handType);
    }

    private void OnActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
    {
        if (newValue && shield.GetComponent<Shield>().totalShieldTime > 0)
        {
            shield.GetComponent<Shield>().active = true;
        }
        else
        {
            shield.GetComponent<Shield>().active = false;
        }
    }
}