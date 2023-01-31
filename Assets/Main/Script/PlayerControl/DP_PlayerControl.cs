//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Main/Script/PlayerControl/DP_PlayerControl.inputactions
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

public partial class @DP_PlayerControl : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @DP_PlayerControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DP_PlayerControl"",
    ""maps"": [
        {
            ""name"": ""Player Movement"",
            ""id"": ""0225d5ad-a853-455b-9906-7c84a0503f07"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4208e540-bc57-428a-a7f4-70f68519a22a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Camera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2db01f1c-f5e3-4b1e-b8fb-b70a0fac4102"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LockOnTarget"",
                    ""type"": ""Button"",
                    ""id"": ""c921a790-2eea-46df-b18f-dfd3e6decc0e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LockOnLeft"",
                    ""type"": ""Button"",
                    ""id"": ""b21f85b2-44f8-4187-899a-f5e2b8d27bf3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LockOnRight"",
                    ""type"": ""Value"",
                    ""id"": ""64fca2de-dfdc-4b37-9587-ec7525ad08fe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""7fce4077-b97e-4504-a525-0157484a745e"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""da046e10-68be-459c-a9fe-2fa82f2683fd"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""59e61635-73a9-434f-9dd9-71589ae01e9a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f556e75f-4e45-4eee-8fc5-c0f782e1f3f3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""82d95d73-9c64-4daf-a60d-9b1b41120bbd"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""05fb3ecc-e170-40db-8121-58078b7c9fab"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d45a6721-a13e-4f56-bc08-633cf8c7eccd"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66bcd012-91f9-4228-8e76-089cd6346726"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockOnTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cffe0d41-62d9-43d6-9f3e-c380efe7e8fa"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockOnLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d94afd6-807d-4c95-a094-8367ab1526cb"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockOnRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player Action"",
            ""id"": ""cd80f619-71eb-4e3e-a2f1-6accc5e0e30e"",
            ""actions"": [
                {
                    ""name"": ""Roll"",
                    ""type"": ""Button"",
                    ""id"": ""90287583-87c0-44f4-b90a-9123ee0a32c6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RB"",
                    ""type"": ""Button"",
                    ""id"": ""b204735c-f693-4aa1-a237-b45ecaf325d1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RT"",
                    ""type"": ""Button"",
                    ""id"": ""23836ac6-f842-4c8e-9d8d-4e27b53cb489"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""9120b982-1fce-4cc6-be19-5ea15c9a48b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d85533b0-5de8-4313-a84c-4b92b6f74c57"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""62c07541-2057-4156-9495-aafbdbdbfe0e"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1bac7d0-1319-49f0-a74c-ef1a04675c8c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""531f429a-8f76-4a00-a0b4-905ebda3515c"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3fa08dfa-0b9c-4ed6-ac9e-bc0688dcd95c"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1231e40f-90d8-4745-a2c7-2708c8e77d9f"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player Quick Slot"",
            ""id"": ""e1971c88-751c-407a-8d93-001defb4a794"",
            ""actions"": [
                {
                    ""name"": ""D Pad Up"",
                    ""type"": ""Button"",
                    ""id"": ""e39ff090-ab18-4cc2-a8f6-1a4ed2d7b4c3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""D Pad Down"",
                    ""type"": ""Button"",
                    ""id"": ""49346a14-4259-442f-ad44-58bb10bb7250"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""D Pad Left"",
                    ""type"": ""Button"",
                    ""id"": ""bd105668-e93b-40fe-ae14-b043bb586830"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""D Pad Right"",
                    ""type"": ""Button"",
                    ""id"": ""a7269060-9f70-49e0-b7f8-9744f170de9b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pick Up"",
                    ""type"": ""Button"",
                    ""id"": ""71e5a8ad-7037-4091-8909-08601ee97729"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenMenu"",
                    ""type"": ""Button"",
                    ""id"": ""06085065-798a-4f22-81c9-fb2ae1ac8c85"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8ab3fb2f-7ffb-4c4c-b5a8-aafe9f2bd4f3"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""D Pad Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27f7f571-cf09-4535-8010-cfed3d4eb8b3"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""D Pad Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""57c9c22a-7d73-4df5-9339-6c43157d85ee"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""D Pad Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0880ae47-b33d-47a8-ab39-6fcde7408301"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""D Pad Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4edf43c6-054d-47b3-832d-165ab8470f5e"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""D Pad Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f745d954-12e7-4120-bc9a-5404ce4b15fe"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""D Pad Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60582cc3-6e36-4fdb-bdbc-b763bf2371fc"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""D Pad Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9721ff65-c02e-4cc4-bc5c-58ed24067a6b"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""D Pad Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""935a4c72-ade7-4f24-8552-cb4ed0e8c170"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""D Pad Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aa4f939d-1ca7-4400-ad22-d62bfcf895a7"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pick Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""529bd703-0c3f-46f9-b468-7dde29977d80"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pick Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08dc78cf-0a44-4592-aef1-751ede18bc89"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player Movement
        m_PlayerMovement = asset.FindActionMap("Player Movement", throwIfNotFound: true);
        m_PlayerMovement_Movement = m_PlayerMovement.FindAction("Movement", throwIfNotFound: true);
        m_PlayerMovement_Camera = m_PlayerMovement.FindAction("Camera", throwIfNotFound: true);
        m_PlayerMovement_LockOnTarget = m_PlayerMovement.FindAction("LockOnTarget", throwIfNotFound: true);
        m_PlayerMovement_LockOnLeft = m_PlayerMovement.FindAction("LockOnLeft", throwIfNotFound: true);
        m_PlayerMovement_LockOnRight = m_PlayerMovement.FindAction("LockOnRight", throwIfNotFound: true);
        // Player Action
        m_PlayerAction = asset.FindActionMap("Player Action", throwIfNotFound: true);
        m_PlayerAction_Roll = m_PlayerAction.FindAction("Roll", throwIfNotFound: true);
        m_PlayerAction_RB = m_PlayerAction.FindAction("RB", throwIfNotFound: true);
        m_PlayerAction_RT = m_PlayerAction.FindAction("RT", throwIfNotFound: true);
        m_PlayerAction_Jump = m_PlayerAction.FindAction("Jump", throwIfNotFound: true);
        // Player Quick Slot
        m_PlayerQuickSlot = asset.FindActionMap("Player Quick Slot", throwIfNotFound: true);
        m_PlayerQuickSlot_DPadUp = m_PlayerQuickSlot.FindAction("D Pad Up", throwIfNotFound: true);
        m_PlayerQuickSlot_DPadDown = m_PlayerQuickSlot.FindAction("D Pad Down", throwIfNotFound: true);
        m_PlayerQuickSlot_DPadLeft = m_PlayerQuickSlot.FindAction("D Pad Left", throwIfNotFound: true);
        m_PlayerQuickSlot_DPadRight = m_PlayerQuickSlot.FindAction("D Pad Right", throwIfNotFound: true);
        m_PlayerQuickSlot_PickUp = m_PlayerQuickSlot.FindAction("Pick Up", throwIfNotFound: true);
        m_PlayerQuickSlot_OpenMenu = m_PlayerQuickSlot.FindAction("OpenMenu", throwIfNotFound: true);
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

    // Player Movement
    private readonly InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerMovement_Movement;
    private readonly InputAction m_PlayerMovement_Camera;
    private readonly InputAction m_PlayerMovement_LockOnTarget;
    private readonly InputAction m_PlayerMovement_LockOnLeft;
    private readonly InputAction m_PlayerMovement_LockOnRight;
    public struct PlayerMovementActions
    {
        private @DP_PlayerControl m_Wrapper;
        public PlayerMovementActions(@DP_PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerMovement_Movement;
        public InputAction @Camera => m_Wrapper.m_PlayerMovement_Camera;
        public InputAction @LockOnTarget => m_Wrapper.m_PlayerMovement_LockOnTarget;
        public InputAction @LockOnLeft => m_Wrapper.m_PlayerMovement_LockOnLeft;
        public InputAction @LockOnRight => m_Wrapper.m_PlayerMovement_LockOnRight;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Camera.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @Camera.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @Camera.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @LockOnTarget.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnLockOnTarget;
                @LockOnTarget.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnLockOnTarget;
                @LockOnTarget.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnLockOnTarget;
                @LockOnLeft.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnLockOnLeft;
                @LockOnLeft.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnLockOnLeft;
                @LockOnLeft.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnLockOnLeft;
                @LockOnRight.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnLockOnRight;
                @LockOnRight.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnLockOnRight;
                @LockOnRight.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnLockOnRight;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Camera.started += instance.OnCamera;
                @Camera.performed += instance.OnCamera;
                @Camera.canceled += instance.OnCamera;
                @LockOnTarget.started += instance.OnLockOnTarget;
                @LockOnTarget.performed += instance.OnLockOnTarget;
                @LockOnTarget.canceled += instance.OnLockOnTarget;
                @LockOnLeft.started += instance.OnLockOnLeft;
                @LockOnLeft.performed += instance.OnLockOnLeft;
                @LockOnLeft.canceled += instance.OnLockOnLeft;
                @LockOnRight.started += instance.OnLockOnRight;
                @LockOnRight.performed += instance.OnLockOnRight;
                @LockOnRight.canceled += instance.OnLockOnRight;
            }
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);

    // Player Action
    private readonly InputActionMap m_PlayerAction;
    private IPlayerActionActions m_PlayerActionActionsCallbackInterface;
    private readonly InputAction m_PlayerAction_Roll;
    private readonly InputAction m_PlayerAction_RB;
    private readonly InputAction m_PlayerAction_RT;
    private readonly InputAction m_PlayerAction_Jump;
    public struct PlayerActionActions
    {
        private @DP_PlayerControl m_Wrapper;
        public PlayerActionActions(@DP_PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Roll => m_Wrapper.m_PlayerAction_Roll;
        public InputAction @RB => m_Wrapper.m_PlayerAction_RB;
        public InputAction @RT => m_Wrapper.m_PlayerAction_RT;
        public InputAction @Jump => m_Wrapper.m_PlayerAction_Jump;
        public InputActionMap Get() { return m_Wrapper.m_PlayerAction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionActions instance)
        {
            if (m_Wrapper.m_PlayerActionActionsCallbackInterface != null)
            {
                @Roll.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnRoll;
                @RB.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnRB;
                @RB.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnRB;
                @RB.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnRB;
                @RT.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnRT;
                @RT.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnRT;
                @RT.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnRT;
                @Jump.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_PlayerActionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @RB.started += instance.OnRB;
                @RB.performed += instance.OnRB;
                @RB.canceled += instance.OnRB;
                @RT.started += instance.OnRT;
                @RT.performed += instance.OnRT;
                @RT.canceled += instance.OnRT;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public PlayerActionActions @PlayerAction => new PlayerActionActions(this);

    // Player Quick Slot
    private readonly InputActionMap m_PlayerQuickSlot;
    private IPlayerQuickSlotActions m_PlayerQuickSlotActionsCallbackInterface;
    private readonly InputAction m_PlayerQuickSlot_DPadUp;
    private readonly InputAction m_PlayerQuickSlot_DPadDown;
    private readonly InputAction m_PlayerQuickSlot_DPadLeft;
    private readonly InputAction m_PlayerQuickSlot_DPadRight;
    private readonly InputAction m_PlayerQuickSlot_PickUp;
    private readonly InputAction m_PlayerQuickSlot_OpenMenu;
    public struct PlayerQuickSlotActions
    {
        private @DP_PlayerControl m_Wrapper;
        public PlayerQuickSlotActions(@DP_PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @DPadUp => m_Wrapper.m_PlayerQuickSlot_DPadUp;
        public InputAction @DPadDown => m_Wrapper.m_PlayerQuickSlot_DPadDown;
        public InputAction @DPadLeft => m_Wrapper.m_PlayerQuickSlot_DPadLeft;
        public InputAction @DPadRight => m_Wrapper.m_PlayerQuickSlot_DPadRight;
        public InputAction @PickUp => m_Wrapper.m_PlayerQuickSlot_PickUp;
        public InputAction @OpenMenu => m_Wrapper.m_PlayerQuickSlot_OpenMenu;
        public InputActionMap Get() { return m_Wrapper.m_PlayerQuickSlot; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerQuickSlotActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerQuickSlotActions instance)
        {
            if (m_Wrapper.m_PlayerQuickSlotActionsCallbackInterface != null)
            {
                @DPadUp.started -= m_Wrapper.m_PlayerQuickSlotActionsCallbackInterface.OnDPadUp;
                @DPadUp.performed -= m_Wrapper.m_PlayerQuickSlotActionsCallbackInterface.OnDPadUp;
                @DPadUp.canceled -= m_Wrapper.m_PlayerQuickSlotActionsCallbackInterface.OnDPadUp;
                @DPadDown.started -= m_Wrapper.m_PlayerQuickSlotActionsCallbackInterface.OnDPadDown;
                @DPadDown.performed -= m_Wrapper.m_PlayerQuickSlotActionsCallbackInterface.OnDPadDown;
                @DPadDown.canceled -= m_Wrapper.m_PlayerQuickSlotActionsCallbackInterface.OnDPadDown;
                @DPadLeft.started -= m_Wrapper.m_PlayerQuickSlotActionsCallbackInterface.OnDPadLeft;
                @DPadLeft.performed -= m_Wrapper.m_PlayerQuickSlotActionsCallbackInterface.OnDPadLeft;
                @DPadLeft.canceled -= m_Wrapper.m_PlayerQuickSlotActionsCallbackInterface.OnDPadLeft;
                @DPadRight.started -= m_Wrapper.m_PlayerQuickSlotActionsCallbackInterface.OnDPadRight;
                @DPadRight.performed -= m_Wrapper.m_PlayerQuickSlotActionsCallbackInterface.OnDPadRight;
                @DPadRight.canceled -= m_Wrapper.m_PlayerQuickSlotActionsCallbackInterface.OnDPadRight;
                @PickUp.started -= m_Wrapper.m_PlayerQuickSlotActionsCallbackInterface.OnPickUp;
                @PickUp.performed -= m_Wrapper.m_PlayerQuickSlotActionsCallbackInterface.OnPickUp;
                @PickUp.canceled -= m_Wrapper.m_PlayerQuickSlotActionsCallbackInterface.OnPickUp;
                @OpenMenu.started -= m_Wrapper.m_PlayerQuickSlotActionsCallbackInterface.OnOpenMenu;
                @OpenMenu.performed -= m_Wrapper.m_PlayerQuickSlotActionsCallbackInterface.OnOpenMenu;
                @OpenMenu.canceled -= m_Wrapper.m_PlayerQuickSlotActionsCallbackInterface.OnOpenMenu;
            }
            m_Wrapper.m_PlayerQuickSlotActionsCallbackInterface = instance;
            if (instance != null)
            {
                @DPadUp.started += instance.OnDPadUp;
                @DPadUp.performed += instance.OnDPadUp;
                @DPadUp.canceled += instance.OnDPadUp;
                @DPadDown.started += instance.OnDPadDown;
                @DPadDown.performed += instance.OnDPadDown;
                @DPadDown.canceled += instance.OnDPadDown;
                @DPadLeft.started += instance.OnDPadLeft;
                @DPadLeft.performed += instance.OnDPadLeft;
                @DPadLeft.canceled += instance.OnDPadLeft;
                @DPadRight.started += instance.OnDPadRight;
                @DPadRight.performed += instance.OnDPadRight;
                @DPadRight.canceled += instance.OnDPadRight;
                @PickUp.started += instance.OnPickUp;
                @PickUp.performed += instance.OnPickUp;
                @PickUp.canceled += instance.OnPickUp;
                @OpenMenu.started += instance.OnOpenMenu;
                @OpenMenu.performed += instance.OnOpenMenu;
                @OpenMenu.canceled += instance.OnOpenMenu;
            }
        }
    }
    public PlayerQuickSlotActions @PlayerQuickSlot => new PlayerQuickSlotActions(this);
    public interface IPlayerMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnCamera(InputAction.CallbackContext context);
        void OnLockOnTarget(InputAction.CallbackContext context);
        void OnLockOnLeft(InputAction.CallbackContext context);
        void OnLockOnRight(InputAction.CallbackContext context);
    }
    public interface IPlayerActionActions
    {
        void OnRoll(InputAction.CallbackContext context);
        void OnRB(InputAction.CallbackContext context);
        void OnRT(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
    public interface IPlayerQuickSlotActions
    {
        void OnDPadUp(InputAction.CallbackContext context);
        void OnDPadDown(InputAction.CallbackContext context);
        void OnDPadLeft(InputAction.CallbackContext context);
        void OnDPadRight(InputAction.CallbackContext context);
        void OnPickUp(InputAction.CallbackContext context);
        void OnOpenMenu(InputAction.CallbackContext context);
    }
}