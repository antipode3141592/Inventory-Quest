using Data;
using Data.Characters;
using Data.Locations;
using Rewired;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryQuest.Managers
{
    public class LocationController : MonoBehaviour
    {
        [SerializeField] List<LocationCharacter> characters;
        [SerializeField] List<ILocation> directlyConnectedLocations;
        [SerializeField] 

        Player player;
        int playerId = 0;
        ILocation _location;

        void Awake()
        {
            player = ReInput.players.GetPlayer(playerId);
        }
        //void Start()
        //{
        //    _gameStateDataSource.OnCurrentLocationSet += OnCurrentLocationUpdated;
        //}

       

        // Update is called once per frame
        void Update()
        {
            //if (player.GetButtonUp("UISubmit"))
            //{
            //    Vector2 clickLocation = player.GetAxis2D("UIHorizontal", "UIVertical");
            //    Debug.Log($"click detected at [{clickLocation.x}, {clickLocation.y}]");

            //    var hit = Physics2D.Raycast(clickLocation, new Vector2(0, -1));

            //    if (hit.collider is null) return;
            //    var _char = hit.transform.gameObject.GetComponent<LocationCharacter>();

            //    if (_char is null) return;
            //    Debug.Log($"LocationCharacter found, questGiverId {_char.questGiverId}");
            //    _char.Chat();
            //}
        }


        //void OnCurrentLocationUpdated(object sender, string e)
        //{
        //    _location = _gameStateDataSource.CurrentLocation;
        //}
    }
}