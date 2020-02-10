using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public GameObject panelToReturnToOnExit;

    public void ExitOptions()
    {
        gameObject.SetActive(false);
        panelToReturnToOnExit.SetActive(true);
    }
}
