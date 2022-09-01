using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] GameObject background;

    [SerializeField] int Width;

    [SerializeField] GameObject Party;

    private void Update()
    {
        if (Party.transform.position.x <= Width/2f && Party.transform.position.x >= Width/2f - 1)
        {
            GameObject go = Instantiate(background);
            go.transform.position = new Vector3(background.transform.position.x + Width, background.transform.position.y, 0);
        }
    }
}
