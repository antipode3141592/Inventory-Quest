using Data;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace InventoryQuest.Managers
{
    public class SceneController : MonoBehaviour, ISceneManager
    {
        IGameStateDataSource _gameStateDataSource;
        IAdventureManager _adventureManager;

        string _currentSceneName;

        [Inject]
        public void Init(IGameStateDataSource gameStateDataSource, IAdventureManager adventureManager)
        {
            _gameStateDataSource = gameStateDataSource;
            _adventureManager = adventureManager;
        }

        void Start()
        {
            _gameStateDataSource.OnCurrentLocationSet += OnLocationSet;
            _adventureManager.Idle.StateExited += OnIdleEnd;
        }

        void OnIdleEnd(object sender, EventArgs e)
        {
            int index = SceneUtility.GetBuildIndexByScenePath(_currentSceneName);
            Debug.Log($"Current Scene: {_currentSceneName}, at build index{index}");
            var scene = SceneManager.GetSceneByBuildIndex(index);
            if (!scene.IsValid())
            {
                Debug.Log($"invalid scene, unload skipped");
                return;
            }
            StartCoroutine(AwaitUnload(index));
        }

        IEnumerator AwaitUnload(int index)
        {
            Debug.Log($"Current Scene: {_currentSceneName}");
            var awaitUnload = SceneManager.UnloadSceneAsync(index);
            while (!awaitUnload.isDone)
                yield return null;
            Debug.Log($"Scene unloaded");
        }

        void OnLocationSet(object sender, string e)
        {

            StartCoroutine(AwaitLoad(e));
        }

        IEnumerator AwaitLoad(string sceneName)
        {
            var awaitLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            _currentSceneName = sceneName;
            while (!awaitLoad.isDone)
                yield return null;
            Debug.Log($"Scene Loaded: {sceneName}");
            
        }
    }
}
