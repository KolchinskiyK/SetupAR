using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.UIElements;

// LEGACY Script to change Bass Values
public class Scala : MonoBehaviour
{
    public int sValue = 0;

    void Start()
    {
        //
    }
    void Update()
    {
        //Debug.Log("Bass Value is " + value);
    }
    
    // As we work with 20-s steps, if its not 100 yet, it have to be at worst case 80, so add 20 works 
    public void Add(int i)
    {
        if(sValue < 100)
        {
            sValue = sValue + i;
        }
    }

    // As we work with 20-s steps, if its not 0 yet, it have to be at worst case 20, so sub 20 works 
    public void Sub(int i)
    {
        if (sValue > 0)
        {
            sValue = sValue - i;
        }
    }

}