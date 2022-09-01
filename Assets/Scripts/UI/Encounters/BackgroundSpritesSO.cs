using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BackgroundSpritesSO")]
public class BackgroundSpritesSO : ScriptableObject
{
    [SerializeField] List<Sprite> backgroundSprites;
}
