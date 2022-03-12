//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.2.0
//     from Assets/Scripts/Player Scripts/PlayerControls.inputactions
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

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Echolocation"",
            ""id"": ""a9b6e75b-a1f6-46cc-971e-4b24509a8bcd"",
            ""actions"": [
                {
                    ""name"": ""Echolocate"",
                    ""type"": ""Button"",
                    ""id"": ""e99cdd7e-0358-4e10-b412-e0465866693e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0c26eb54-bde6-4b3d-b411-fc1fe9ce873f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Echolocate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Echolocation
        m_Echolocation = asset.FindActionMap("Echolocation", throwIfNotFound: true);
        m_Echolocation_Echolocate = m_Echolocation.FindAction("Echolocate", throwIfNotFound: true);
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

    // Echolocation
    private readonly InputActionMap m_Echolocation;
    private IEcholocationActions m_EcholocationActionsCallbackInterface;
    private readonly InputAction m_Echolocation_Echolocate;
    public struct EcholocationActions
    {
        private @PlayerControls m_Wrapper;
        public EcholocationActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Echolocate => m_Wrapper.m_Echolocation_Echolocate;
        public InputActionMap Get() { return m_Wrapper.m_Echolocation; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(EcholocationActions set) { return set.Get(); }
        public void SetCallbacks(IEcholocationActions instance)
        {
            if (m_Wrapper.m_EcholocationActionsCallbackInterface != null)
            {
                @Echolocate.started -= m_Wrapper.m_EcholocationActionsCallbackInterface.OnEcholocate;
                @Echolocate.performed -= m_Wrapper.m_EcholocationActionsCallbackInterface.OnEcholocate;
                @Echolocate.canceled -= m_Wrapper.m_EcholocationActionsCallbackInterface.OnEcholocate;
            }
            m_Wrapper.m_EcholocationActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Echolocate.started += instance.OnEcholocate;
                @Echolocate.performed += instance.OnEcholocate;
                @Echolocate.canceled += instance.OnEcholocate;
            }
        }
    }
    public EcholocationActions @Echolocation => new EcholocationActions(this);
    public interface IEcholocationActions
    {
        void OnEcholocate(InputAction.CallbackContext context);
    }
}
