using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

namespace InventoryQuest.Backgrounds
{
    public class ParallaxBackground : MonoBehaviour
    {

        [SerializeField] CinemachineVirtualCamera virtualCamera;

        [SerializeField] bool infiniteHorizontal;
        [SerializeField] bool infiniteVertical;

        [SerializeField] List<SpriteRenderer> backgrounds;

        [SerializeField] List<BackgroundSpritesSO> backgroundSOs;

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

            SelectBackground(backgroundSOs[0].Id);
        }

        void Update()
        {
            Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
            lastCameraPosition = cameraTransform.position;
            for (int i = 0; i < backgrounds.Count; i++)
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

        public void SelectBackground(string backgroundId)
        {
            if (backgroundSOs.Count == 0) return;
            var backgroundSO = backgroundSOs.Find(x => x.Id == backgroundId);
            if (backgroundSO is null)
            {
                backgroundSO = backgroundSOs[0];
            }

            Sprite sprite = backgroundSO.BackgroundSprites[0];
            Texture2D texture = sprite.texture;
            textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
            textureUnitSizeY = texture.height / sprite.pixelsPerUnit;

            effectMultipliers.Clear();

            for (int i = 0; i < backgroundSO.BackgroundSprites.Count; i++)
            {
                backgrounds[i].sprite = backgroundSO.BackgroundSprites[i];
                effectMultipliers.Add(new(i / (float)backgrounds.Count, i / (float)backgrounds.Count));
            }
        }
    }
}