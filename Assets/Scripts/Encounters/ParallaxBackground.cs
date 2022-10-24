using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class ParallaxBackground : MonoBehaviour {

    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] Vector2 parallaxEffectMultiplier;
    [SerializeField] bool infiniteHorizontal;
    [SerializeField] bool infiniteVertical;

    [SerializeField] List<SpriteRenderer> backgrounds;

    List<Vector2> effectMultipliers;

    Transform cameraTransform;
    Vector3 lastCameraPosition;
    float textureUnitSizeX;
    float textureUnitSizeY;


    void Awake()
    {
        backgrounds = new(GetComponentsInChildren<SpriteRenderer>());
        effectMultipliers = new List<Vector2>();
    }

    void Start()
    {
        cameraTransform = virtualCamera.transform;
        lastCameraPosition = cameraTransform.position;

        Sprite sprite = backgrounds[0].sprite;
        //Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        textureUnitSizeY = texture.height / sprite.pixelsPerUnit;

        for (int i = 0; i < backgrounds.Count; i++)
        {
            effectMultipliers.Add(new((float)i / (float)backgrounds.Count, (float)i / (float)backgrounds.Count));
        }
    }

    void Update() {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        lastCameraPosition = cameraTransform.position;
        for(int i = 0; i < backgrounds.Count; i++)
        {
            Transform backgroundTransform = backgrounds[i].gameObject.transform;
            backgroundTransform.position += new Vector3(deltaMovement.x * effectMultipliers[i].x, deltaMovement.y * effectMultipliers[i].y);   

            if (infiniteHorizontal)
            {
                if (Mathf.Abs(cameraTransform.position.x - backgroundTransform.position.x) >= textureUnitSizeX)
                {
                    float offsetPositionX = (cameraTransform.position.x - backgroundTransform.position.x) % textureUnitSizeX;
                    backgroundTransform.position = new Vector3(cameraTransform.position.x + offsetPositionX, backgroundTransform.position.y);
                }
            }

            if (infiniteVertical)
            {
                if (Mathf.Abs(cameraTransform.position.y - backgroundTransform.position.y) >= textureUnitSizeY)
                {
                    float offsetPositionY = (cameraTransform.position.y - backgroundTransform.position.y) % textureUnitSizeY;
                    backgroundTransform.position = new Vector3(backgroundTransform.position.x, cameraTransform.position.y + offsetPositionY);
                }
            }
        }
    }

}
