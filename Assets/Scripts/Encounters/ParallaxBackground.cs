using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class ParallaxBackground : MonoBehaviour {

    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] Vector2 parallaxEffectMultiplier;
    [SerializeField] bool infiniteHorizontal;
    [SerializeField] bool infiniteVertical;

    [SerializeField] List<SpriteRenderer> backgrounds;

    Transform cameraTransform;
    Vector3 lastCameraPosition;
    float textureUnitSizeX;
    float textureUnitSizeY;


    void Awake()
    {
        backgrounds = new(GetComponentsInChildren<SpriteRenderer>());
    }

    void Start() {
        cameraTransform = virtualCamera.transform;
        lastCameraPosition = cameraTransform.position;

        Sprite sprite = backgrounds[0].sprite;
        //Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }

    void LateUpdate() {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        Vector2 effectMultiplier;
        lastCameraPosition = cameraTransform.position;
        for(int i = 0; i < backgrounds.Count; i++)
        {
            effectMultiplier = new((float)i / (float)backgrounds.Count, (float)i / (float)backgrounds.Count);
            //Debug.Log($"effectMultiplier = {effectMultiplier}");
            Transform backgroundTransform = backgrounds[i].gameObject.transform;
            backgroundTransform.position += new Vector3(deltaMovement.x * effectMultiplier.x, deltaMovement.y * effectMultiplier.y);
            //transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
            

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
