//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Input/PlayerControls.inputactions
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

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""BaseDriving"",
            ""id"": ""f54d0cd6-3c69-4827-a619-772884abda79"",
            ""actions"": [
                {
                    ""name"": ""Boost"",
                    ""type"": ""Button"",
                    ""id"": ""7a57967c-16b5-46ae-b206-1947024150cd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Handbreak"",
                    ""type"": ""Button"",
                    ""id"": ""3eed9f86-e9dd-4e37-ac59-4d6f7e8b0642"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Accelerate"",
                    ""type"": ""Value"",
                    ""id"": ""8f1614e8-9bcc-4d1b-929c-49b4ddcc5ce6"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Decelerate"",
                    ""type"": ""Button"",
                    ""id"": ""076a5c27-3956-4106-b59c-0466b8722722"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Steer"",
                    ""type"": ""Value"",
                    ""id"": ""3e606170-5cec-4a16-8ccb-53971b7bd3e2"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ResetRotation"",
                    ""type"": ""Button"",
                    ""id"": ""0fd2ad38-d4e1-4758-bdd3-9d3988c9f41e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Tricking"",
                    ""type"": ""Value"",
                    ""id"": ""9267ae80-8b2f-4b04-bf98-b38cb8953b19"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""TwistRotation"",
                    ""type"": ""Value"",
                    ""id"": ""3ec01bf8-2b0f-4c6e-8b3d-1f645b5e6206"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""FlipRotation"",
                    ""type"": ""Value"",
                    ""id"": ""9fdb2f10-cb2b-49d0-9e74-fbb8cb254914"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""91136f86-2d01-4d8b-a95b-2fd3ca873835"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Boost"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""776e900d-7bbd-4769-8d2d-f40720270dae"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Boost"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bc61d124-4e02-4512-b49d-3205d728d092"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Handbreak"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""20768d78-6406-45dc-b0fb-066b066926a4"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Handbreak"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce677905-cc98-4137-9ffb-7e45b198f44b"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4990ed89-f97b-4046-be5f-4e28e3223da3"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bfdb3d40-5749-4a06-8d65-38ce7cd9e558"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Decelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67098bfc-6fc1-48ad-ad5c-df71a9ce97ea"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Decelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""14ddb8ee-b662-484e-9da9-e0f96e782ae2"",
                    ""path"": ""<Gamepad>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""6fbc386d-7df2-415f-8c94-7ce3ea75e747"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steer"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3d53045b-10ca-4f8c-9fca-1f40c038635f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""9f92e0f8-7c7a-4cb7-8000-48ec34a1e76b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c257fd27-09cc-4c00-90eb-a27fbdc6d5e9"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResetRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41d96015-bdd9-4e28-b813-60a5431a577e"",
                    ""path"": ""<Keyboard>/alt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResetRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""73594db1-d5be-4986-a20a-d1df5c5b02a9"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tricking"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""05f729ec-206d-4462-a5cc-f978df2d1d1d"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TwistRotation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""31088c9f-4b13-4139-98a1-8c264e93bfae"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TwistRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""54cda3ee-01d6-462b-8ccf-7a61ad36a37b"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TwistRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""3aa4e43d-51f5-4597-85ef-53cf43d3aa65"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FlipRotation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""2b18498c-71e4-4c97-9bfd-9c68f17772be"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FlipRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""1263bbe1-7467-4227-969f-a42cc23d029c"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FlipRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // BaseDriving
        m_BaseDriving = asset.FindActionMap("BaseDriving", throwIfNotFound: true);
        m_BaseDriving_Boost = m_BaseDriving.FindAction("Boost", throwIfNotFound: true);
        m_BaseDriving_Handbreak = m_BaseDriving.FindAction("Handbreak", throwIfNotFound: true);
        m_BaseDriving_Accelerate = m_BaseDriving.FindAction("Accelerate", throwIfNotFound: true);
        m_BaseDriving_Decelerate = m_BaseDriving.FindAction("Decelerate", throwIfNotFound: true);
        m_BaseDriving_Steer = m_BaseDriving.FindAction("Steer", throwIfNotFound: true);
        m_BaseDriving_ResetRotation = m_BaseDriving.FindAction("ResetRotation", throwIfNotFound: true);
        m_BaseDriving_Tricking = m_BaseDriving.FindAction("Tricking", throwIfNotFound: true);
        m_BaseDriving_TwistRotation = m_BaseDriving.FindAction("TwistRotation", throwIfNotFound: true);
        m_BaseDriving_FlipRotation = m_BaseDriving.FindAction("FlipRotation", throwIfNotFound: true);
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

    // BaseDriving
    private readonly InputActionMap m_BaseDriving;
    private List<IBaseDrivingActions> m_BaseDrivingActionsCallbackInterfaces = new List<IBaseDrivingActions>();
    private readonly InputAction m_BaseDriving_Boost;
    private readonly InputAction m_BaseDriving_Handbreak;
    private readonly InputAction m_BaseDriving_Accelerate;
    private readonly InputAction m_BaseDriving_Decelerate;
    private readonly InputAction m_BaseDriving_Steer;
    private readonly InputAction m_BaseDriving_ResetRotation;
    private readonly InputAction m_BaseDriving_Tricking;
    private readonly InputAction m_BaseDriving_TwistRotation;
    private readonly InputAction m_BaseDriving_FlipRotation;
    public struct BaseDrivingActions
    {
        private @PlayerControls m_Wrapper;
        public BaseDrivingActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Boost => m_Wrapper.m_BaseDriving_Boost;
        public InputAction @Handbreak => m_Wrapper.m_BaseDriving_Handbreak;
        public InputAction @Accelerate => m_Wrapper.m_BaseDriving_Accelerate;
        public InputAction @Decelerate => m_Wrapper.m_BaseDriving_Decelerate;
        public InputAction @Steer => m_Wrapper.m_BaseDriving_Steer;
        public InputAction @ResetRotation => m_Wrapper.m_BaseDriving_ResetRotation;
        public InputAction @Tricking => m_Wrapper.m_BaseDriving_Tricking;
        public InputAction @TwistRotation => m_Wrapper.m_BaseDriving_TwistRotation;
        public InputAction @FlipRotation => m_Wrapper.m_BaseDriving_FlipRotation;
        public InputActionMap Get() { return m_Wrapper.m_BaseDriving; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BaseDrivingActions set) { return set.Get(); }
        public void AddCallbacks(IBaseDrivingActions instance)
        {
            if (instance == null || m_Wrapper.m_BaseDrivingActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_BaseDrivingActionsCallbackInterfaces.Add(instance);
            @Boost.started += instance.OnBoost;
            @Boost.performed += instance.OnBoost;
            @Boost.canceled += instance.OnBoost;
            @Handbreak.started += instance.OnHandbreak;
            @Handbreak.performed += instance.OnHandbreak;
            @Handbreak.canceled += instance.OnHandbreak;
            @Accelerate.started += instance.OnAccelerate;
            @Accelerate.performed += instance.OnAccelerate;
            @Accelerate.canceled += instance.OnAccelerate;
            @Decelerate.started += instance.OnDecelerate;
            @Decelerate.performed += instance.OnDecelerate;
            @Decelerate.canceled += instance.OnDecelerate;
            @Steer.started += instance.OnSteer;
            @Steer.performed += instance.OnSteer;
            @Steer.canceled += instance.OnSteer;
            @ResetRotation.started += instance.OnResetRotation;
            @ResetRotation.performed += instance.OnResetRotation;
            @ResetRotation.canceled += instance.OnResetRotation;
            @Tricking.started += instance.OnTricking;
            @Tricking.performed += instance.OnTricking;
            @Tricking.canceled += instance.OnTricking;
            @TwistRotation.started += instance.OnTwistRotation;
            @TwistRotation.performed += instance.OnTwistRotation;
            @TwistRotation.canceled += instance.OnTwistRotation;
            @FlipRotation.started += instance.OnFlipRotation;
            @FlipRotation.performed += instance.OnFlipRotation;
            @FlipRotation.canceled += instance.OnFlipRotation;
        }

        private void UnregisterCallbacks(IBaseDrivingActions instance)
        {
            @Boost.started -= instance.OnBoost;
            @Boost.performed -= instance.OnBoost;
            @Boost.canceled -= instance.OnBoost;
            @Handbreak.started -= instance.OnHandbreak;
            @Handbreak.performed -= instance.OnHandbreak;
            @Handbreak.canceled -= instance.OnHandbreak;
            @Accelerate.started -= instance.OnAccelerate;
            @Accelerate.performed -= instance.OnAccelerate;
            @Accelerate.canceled -= instance.OnAccelerate;
            @Decelerate.started -= instance.OnDecelerate;
            @Decelerate.performed -= instance.OnDecelerate;
            @Decelerate.canceled -= instance.OnDecelerate;
            @Steer.started -= instance.OnSteer;
            @Steer.performed -= instance.OnSteer;
            @Steer.canceled -= instance.OnSteer;
            @ResetRotation.started -= instance.OnResetRotation;
            @ResetRotation.performed -= instance.OnResetRotation;
            @ResetRotation.canceled -= instance.OnResetRotation;
            @Tricking.started -= instance.OnTricking;
            @Tricking.performed -= instance.OnTricking;
            @Tricking.canceled -= instance.OnTricking;
            @TwistRotation.started -= instance.OnTwistRotation;
            @TwistRotation.performed -= instance.OnTwistRotation;
            @TwistRotation.canceled -= instance.OnTwistRotation;
            @FlipRotation.started -= instance.OnFlipRotation;
            @FlipRotation.performed -= instance.OnFlipRotation;
            @FlipRotation.canceled -= instance.OnFlipRotation;
        }

        public void RemoveCallbacks(IBaseDrivingActions instance)
        {
            if (m_Wrapper.m_BaseDrivingActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IBaseDrivingActions instance)
        {
            foreach (var item in m_Wrapper.m_BaseDrivingActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_BaseDrivingActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public BaseDrivingActions @BaseDriving => new BaseDrivingActions(this);
    public interface IBaseDrivingActions
    {
        void OnBoost(InputAction.CallbackContext context);
        void OnHandbreak(InputAction.CallbackContext context);
        void OnAccelerate(InputAction.CallbackContext context);
        void OnDecelerate(InputAction.CallbackContext context);
        void OnSteer(InputAction.CallbackContext context);
        void OnResetRotation(InputAction.CallbackContext context);
        void OnTricking(InputAction.CallbackContext context);
        void OnTwistRotation(InputAction.CallbackContext context);
        void OnFlipRotation(InputAction.CallbackContext context);
    }
}
