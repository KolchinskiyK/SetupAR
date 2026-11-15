using UnityEngine;
using UnityEngine.UI;

public class MicMenuController : MonoBehaviour
{
    public Mikro linkedMic; // the mic, which the menu is hanging on to

    /* Info an mich selbst: Buttons im Inspector zuweisen, 
       ODER  OnClick im Editor und ziehe das Mikro rein.
       Aktuelle Versuch ist der Weg hier:
    */

    public void OnClick_SelectModel(int modelIndex)
    {
        if (linkedMic != null) linkedMic.SetModelFromUI(modelIndex);
    }

    public void OnClick_SelectHeight(int heightIndex)
    {
        if (linkedMic != null) linkedMic.SetHeightFromUI(heightIndex);
    }

    public void OnClick_Play()
    {
        if (linkedMic != null) linkedMic.PlayCurrentSound();
    }
}