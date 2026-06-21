using KanKikuchi.AudioManager;
using UnityEngine;

public class ButtonSEManager : MonoBehaviour
{
    public void PlaySelectedButtonSE()
    {
        SEManager.Instance.Play(SEPath.SELECTEDBUTTON);
    }

    public void PlayHoveringButtonSE()
    {
        SEManager.Instance.Play(SEPath.HOVERINGBUTTON);
    }

    public void PlayNextStageSE()
    {
        SEManager.Instance.Play(SEPath.NEXTSTAGE);
    }
}
