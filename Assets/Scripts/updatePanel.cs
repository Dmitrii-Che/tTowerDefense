using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updatePanel : MonoBehaviour
{
    public Text costText;
    public void Close()
    {
        Destroy(gameObject);
    }
}
