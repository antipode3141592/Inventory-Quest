//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.2.0
//     from Assets/Settings/MyControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Inputs
{
    public partial class @MyControls : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @MyControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""MyControls"",
    ""maps"": [
        {
            ""name"": ""Game"",
            ""id"": ""eff73ff7-0e07-46f6-a4fc-924954ef7655"",
            ""actions"": [
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""862a123d-8ab0-4574-841f-e411fd9a3e1d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""7042768f-3355-4167-90a5-3e8ab10f45d4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CursorPosition"",
                    ""type"": ""Value"",
                    ""id"": ""f5fd3ade-529d-44e7-9308-da77780acfac"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""11a99b56-e50a-4e9f-ba6f-37ba1b23a580"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""797a4b41-79ba-4b7c-b85e-03a487fca9e7"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b39b4c68-9085-479e-81f5-ad047fa17378"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""CursorPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse"",
            ""bindingGroup"": ""Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Game
            m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
            m_Game_LeftClick = m_Game.FindAction("LeftClick", throwIfNotFound: true);
            m_Game_RightClick = m_Game.FindAction("RightClick", throwIfNotFound: true);
            m_Game_CursorPosition = m_Game.FindAction("CursorPosition", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }
        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }
        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // Game
        private readonly InputActionMap m_Game;
        private IGameActions m_GameActionsCallbackInterface;
        private readonly InputAction m_Game_LeftClick;
        private readonly InputAction m_Game_RightClick;
        private readonly InputAction m_Game_CursorPosition;
        public struct GameActions
        {
            private @MyControls m_Wrapper;
            public GameActions(@MyControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @LeftClick => m_Wrapper.m_Game_LeftClick;
            public InputAction @RightClick => m_Wrapper.m_Game_RightClick;
            public InputAction @CursorPosition => m_Wrapper.m_Game_CursorPosition;
            public InputActionMap Get() { return m_Wrapper.m_Game; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
            public void SetCallbacks(IGameActions instance)
            {
                if (m_Wrapper.m_GameActionsCallbackInterface != null)
                {
                    @LeftClick.started -= m_Wrapper.m_GameActionsCallbackInterface.OnLeftClick;
                    @LeftClick.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnLeftClick;
                    @LeftClick.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnLeftClick;
                    @RightClick.started -= m_Wrapper.m_GameActionsCallbackInterface.OnRightClick;
                    @RightClick.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnRightClick;
                    @RightClick.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnRightClick;
                    @CursorPosition.started -= m_Wrapper.m_GameActionsCallbackInterface.OnCursorPosition;
                    @CursorPosition.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnCursorPosition;
                    @CursorPosition.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnCursorPosition;
                }
                m_Wrapper.m_GameActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @LeftClick.started += instance.OnLeftClick;
                    @LeftClick.performed += instance.OnLeftClick;
                    @LeftClick.canceled += instance.OnLeftClick;
                    @RightClick.started += instance.OnRightClick;
                    @RightClick.performed += instance.OnRightClick;
                    @RightClick.canceled += instance.OnRightClick;
                    @CursorPosition.started += instance.OnCursorPosition;
                    @CursorPosition.performed += instance.OnCursorPosition;
                    @CursorPosition.canceled += instance.OnCursorPosition;
                }
            }
        }
        public GameActions @Game => new GameActions(this);
        private int m_MouseSchemeIndex = -1;
        public InputControlScheme MouseScheme
        {
            get
            {
                if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.FindControlSchemeIndex("Mouse");
                return asset.controlSchemes[m_MouseSchemeIndex];
            }
        }
        private int m_GamepadSchemeIndex = -1;
        public InputControlScheme GamepadScheme
        {
            get
            {
                if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
                return asset.controlSchemes[m_GamepadSchemeIndex];
            }
        }
        public interface IGameActions
        {
            void OnLeftClick(InputAction.CallbackContext context);
            void OnRightClick(InputAction.CallbackContext context);
            void OnCursorPosition(InputAction.CallbackContext context);
        }
    }
}