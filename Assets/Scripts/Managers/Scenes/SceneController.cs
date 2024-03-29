﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace InventoryQuest.Managers
{
    public class SceneController : MonoBehaviour, ISceneController
    {
        IGameStateDataSource _gameStateDataSource;
        IAdventureManager _adventureManager;
        IGameManager _gameManager;

        public event EventHandler RequestShowLoading;
        public event EventHandler RequestHideLoading;

        string _currentSceneName = string.Empty;

        [Inject]
        public void Init(IGameStateDataSource gameStateDataSource, IAdventureManager adventureManager, IGameManager gameManager)
        {
            _gameStateDataSource = gameStateDataSource;
            _adventureManager = adventureManager;
            _gameManager = gameManager;
        }

        void Start()
        {
            _gameStateDataSource.OnCurrentLocationSet += OnLocationSet;
            _adventureManager.InLocation.StateExited += OnIdleEnd;
            //_gameManager.OnGameOver += OnGameOverHandler;
            //_gameManager.OnGameRestart += OnGameRestartHandler;
        }

        void OnGameRestartHandler(object sender, EventArgs e)
        {
            SceneManager.LoadScene(0);
        }

        void OnGameOverHandler(object sender, EventArgs e)
        {
            UnloadCurrentScene();
        }

        void OnIdleEnd(object sender, EventArgs e)
        {
            UnloadCurrentScene();
        }

        void UnloadCurrentScene()
        {
            if (Debug.isDebugBuild)
                Debug.Log($"Unload Current Scene: { _currentSceneName}");
            int index = SceneUtility.GetBuildIndexByScenePath(_currentSceneName);
            if (index == -1)
            {
                if (Debug.isDebugBuild)
                    Debug.Log($"build index invalid, unload skipped");
                return; 
            }
            var scene = SceneManager.GetSceneByBuildIndex(index);
            if (!scene.IsValid())
            {
                if (Debug.isDebugBuild)
                    Debug.Log($"invalid scene, unload skipped");
                return;
            }
            StartCoroutine(AwaitUnload(index));
        }

        IEnumerator AwaitUnload(int index)
        {
            if(Debug.isDebugBuild)
                Debug.Log($"Unloading Current Scene: {_currentSceneName}...");
            var awaitUnload = SceneManager.UnloadSceneAsync(index);
            while (!awaitUnload.isDone)
                yield return null;
            _currentSceneName = string.Empty;
            if(Debug.isDebugBuild)
                Debug.Log($"Scene unloaded");
        }

        void OnLocationSet(object sender, string sceneName)
        {
            if (Debug.isDebugBuild)
                Debug.Log($"OnLocationSet handling event from {sender}");
            LoadLocation(sceneName);
        }

        IEnumerator AwaitLoad(string sceneName)
        {
            RequestShowLoading?.Invoke(this, EventArgs.Empty);
            if (Debug.isDebugBuild)
                Debug.Log($"Beginning Scene Load: {sceneName}...");
            var awaitLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            
            _currentSceneName = sceneName;
            while (!awaitLoad.isDone)
                yield return null;
            if (Debug.isDebugBuild)
                Debug.Log($"Scene Loaded: {sceneName}");
            RequestHideLoading?.Invoke(this, EventArgs.Empty);
        }

        public void LoadLocation(string locationSceneName)
        {
            StartCoroutine(UnloadThenLoad(locationSceneName));
        }

        IEnumerator UnloadThenLoad(string sceneName)
        {
            if (Debug.isDebugBuild)
                Debug.Log($"Unload Current Scene: { _currentSceneName}, then load {sceneName}");
            RequestShowLoading?.Invoke(this, EventArgs.Empty);
            //try unload
            int index = SceneUtility.GetBuildIndexByScenePath(_currentSceneName);
            if (index != -1)
            {
                var scene = SceneManager.GetSceneByBuildIndex(index);
                if (scene.IsValid())
                {
                    if (Debug.isDebugBuild)
                        Debug.Log($"Unloading Current Scene: {_currentSceneName}...");
                    var awaitUnload = SceneManager.UnloadSceneAsync(index);
                    while (!awaitUnload.isDone)
                        yield return null;
                    _currentSceneName = string.Empty;
                    if (Debug.isDebugBuild)
                        Debug.Log($"Scene unloaded");
                }
            }
            
            //load
            if (Debug.isDebugBuild)
                Debug.Log($"Beginning Scene Load: {sceneName}...");
            var awaitLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            _currentSceneName = sceneName;
            while (!awaitLoad.isDone)
                yield return null;
            if (Debug.isDebugBuild)
                Debug.Log($"Scene Loaded: {sceneName}");
            RequestHideLoading?.Invoke(this, EventArgs.Empty);
        }
    }
}
