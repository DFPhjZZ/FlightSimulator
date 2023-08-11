// //------------------------------------------------------------------------------
// // <auto-generated>
// //     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
// //     version 1.5.1
// //     from Assets/CraftInput.inputactions
// //
// //     Changes to this file may cause incorrect behavior and will be lost if
// //     the code is regenerated.
// // </auto-generated>
// //------------------------------------------------------------------------------
//
// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine.InputSystem;
// using UnityEngine.InputSystem.Utilities;
//
// public partial class @CraftInput: IInputActionCollection2, IDisposable
// {
//     public InputActionAsset asset { get; }
//     public @CraftInput()
//     {
//         asset = InputActionAsset.FromJson(@"{
//     ""name"": ""CraftInput"",
//     ""maps"": [
//         {
//             ""name"": ""Aircraft"",
//             ""id"": ""b3077e71-acf4-42d9-b346-c7f00298f859"",
//             ""actions"": [
//                 {
//                     ""name"": ""Throttle"",
//                     ""type"": ""Value"",
//                     ""id"": ""260c1bfa-6326-44f3-8af1-e3a3a22ef24d"",
//                     ""expectedControlType"": ""Axis"",
//                     ""processors"": """",
//                     ""interactions"": """",
//                     ""initialStateCheck"": true
//                 },
//                 {
//                     ""name"": ""Pitch"",
//                     ""type"": ""Value"",
//                     ""id"": ""149157bb-1fbc-44ab-b60f-031d5f027889"",
//                     ""expectedControlType"": ""Axis"",
//                     ""processors"": """",
//                     ""interactions"": """",
//                     ""initialStateCheck"": true
//                 },
//                 {
//                     ""name"": ""Roll"",
//                     ""type"": ""Value"",
//                     ""id"": ""1c4d3ce0-6878-4937-9e1a-3e8a24dd5230"",
//                     ""expectedControlType"": ""Axis"",
//                     ""processors"": """",
//                     ""interactions"": """",
//                     ""initialStateCheck"": true
//                 },
//                 {
//                     ""name"": ""Yaw"",
//                     ""type"": ""Value"",
//                     ""id"": ""902009d9-c394-4f2e-98e5-d5bb3cbbdae7"",
//                     ""expectedControlType"": ""Axis"",
//                     ""processors"": """",
//                     ""interactions"": """",
//                     ""initialStateCheck"": true
//                 },
//                 {
//                     ""name"": ""Camera"",
//                     ""type"": ""Value"",
//                     ""id"": ""8b6c6dcb-00b8-4aa2-b3bb-27619c5c8142"",
//                     ""expectedControlType"": ""Vector2"",
//                     ""processors"": """",
//                     ""interactions"": """",
//                     ""initialStateCheck"": true
//                 },
//                 {
//                     ""name"": ""FireCannon"",
//                     ""type"": ""Button"",
//                     ""id"": ""329ca77a-0556-4251-b1ac-e454000bf657"",
//                     ""expectedControlType"": ""Button"",
//                     ""processors"": """",
//                     ""interactions"": """",
//                     ""initialStateCheck"": false
//                 },
//                 {
//                     ""name"": ""FireMissile"",
//                     ""type"": ""Button"",
//                     ""id"": ""9300e16d-235b-488b-bb25-d84d732a0522"",
//                     ""expectedControlType"": ""Button"",
//                     ""processors"": """",
//                     ""interactions"": """",
//                     ""initialStateCheck"": false
//                 },
//                 {
//                     ""name"": ""RollPitch"",
//                     ""type"": ""Value"",
//                     ""id"": ""66edee5a-0643-451e-9c13-f19af6c5337e"",
//                     ""expectedControlType"": ""Vector2"",
//                     ""processors"": """",
//                     ""interactions"": """",
//                     ""initialStateCheck"": true
//                 }
//             ],
//             ""bindings"": [
//                 {
//                     ""name"": ""1D Axis"",
//                     ""id"": ""b3f27a61-d105-482b-9e3f-5d5a9eb991f9"",
//                     ""path"": ""1DAxis"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Throttle"",
//                     ""isComposite"": true,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": ""negative"",
//                     ""id"": ""1ed3354f-6460-4887-8114-7bed26767ccc"",
//                     ""path"": ""<Keyboard>/s"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Throttle"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""positive"",
//                     ""id"": ""52102740-8c01-459d-8c02-0953b107b12f"",
//                     ""path"": ""<Keyboard>/w"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Throttle"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""1D Axis"",
//                     ""id"": ""44c0c5a5-b700-4c47-bc86-77d1d1a40653"",
//                     ""path"": ""1DAxis"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Throttle"",
//                     ""isComposite"": true,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": ""negative"",
//                     ""id"": ""1f9ccf7e-740d-4463-a4a8-0e3386d480f2"",
//                     ""path"": ""<XInputController>/leftShoulder"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Throttle"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""positive"",
//                     ""id"": ""a9157693-81cf-48d4-99b4-f5efc17edd7c"",
//                     ""path"": ""<XInputController>/rightShoulder"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Throttle"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""1D Axis"",
//                     ""id"": ""e30d6ed5-2821-4387-84ff-f765a08d3fa1"",
//                     ""path"": ""1DAxis"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Throttle"",
//                     ""isComposite"": true,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": ""negative"",
//                     ""id"": ""956b6b1b-4077-4756-9f1c-443d22adeb72"",
//                     ""path"": ""<DualShockGamepad>/leftShoulder"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Throttle"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""positive"",
//                     ""id"": ""92cb208f-c9f4-4968-a852-5f5d480a76da"",
//                     ""path"": ""<DualShockGamepad>/rightShoulder"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Throttle"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""1D Axis"",
//                     ""id"": ""a0a7eff2-596f-46fe-b97d-69bcd9d7d309"",
//                     ""path"": ""1DAxis"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Pitch"",
//                     ""isComposite"": true,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": ""negative"",
//                     ""id"": ""2abd8bf8-44cc-43c9-8709-14103647788b"",
//                     ""path"": ""<Keyboard>/downArrow"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Pitch"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""positive"",
//                     ""id"": ""9ef38f4c-80c2-4084-a3da-2ea62ea548cb"",
//                     ""path"": ""<Keyboard>/upArrow"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Pitch"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""1D Axis"",
//                     ""id"": ""d3af113e-954c-4062-b144-d5a7a7cd1b14"",
//                     ""path"": ""1DAxis"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Pitch"",
//                     ""isComposite"": true,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": ""negative"",
//                     ""id"": ""ef9036a7-7a43-409e-99d1-e83260ed0f33"",
//                     ""path"": ""<Keyboard>/numpad5"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Pitch"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""positive"",
//                     ""id"": ""30db18a8-beab-4a53-bd1c-d21b0c193673"",
//                     ""path"": ""<Keyboard>/numpad8"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Pitch"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""1D Axis"",
//                     ""id"": ""4f4fc82c-d1a0-4449-8bbe-78f6a3caf1be"",
//                     ""path"": ""1DAxis"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Roll"",
//                     ""isComposite"": true,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": ""negative"",
//                     ""id"": ""4a95526e-5422-48bd-8006-2fcd54e867da"",
//                     ""path"": ""<Keyboard>/leftArrow"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Roll"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""positive"",
//                     ""id"": ""c830fd4d-81af-4a85-8e6d-b6ea642c8d50"",
//                     ""path"": ""<Keyboard>/rightArrow"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Roll"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""1D Axis"",
//                     ""id"": ""87388ca6-f9bc-4a29-9dad-556e120d234f"",
//                     ""path"": ""1DAxis"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Roll"",
//                     ""isComposite"": true,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": ""negative"",
//                     ""id"": ""92f30695-db2c-47c2-956f-57ce4bcd6018"",
//                     ""path"": ""<Keyboard>/numpad4"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Roll"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""positive"",
//                     ""id"": ""e6759925-2f82-4164-af1b-a612dc26cc37"",
//                     ""path"": ""<Keyboard>/numpad6"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Roll"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""1D Axis"",
//                     ""id"": ""570e3385-9938-43fa-87d6-0d0e388b5afe"",
//                     ""path"": ""1DAxis"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Yaw"",
//                     ""isComposite"": true,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": ""negative"",
//                     ""id"": ""b6c25ea7-e3aa-46db-962e-b542b822e326"",
//                     ""path"": ""<Keyboard>/a"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Yaw"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""positive"",
//                     ""id"": ""42304636-d49c-4384-bc87-0a4a42f22f21"",
//                     ""path"": ""<Keyboard>/d"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Yaw"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""1D Axis"",
//                     ""id"": ""9b5df471-9bce-4eea-bba3-6869ad9410c1"",
//                     ""path"": ""1DAxis"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Yaw"",
//                     ""isComposite"": true,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": ""negative"",
//                     ""id"": ""f3a01b2b-a7d9-42ea-83c6-6d8259af7416"",
//                     ""path"": ""<XInputController>/leftTrigger"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Yaw"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""positive"",
//                     ""id"": ""e64c026c-b572-472d-9795-3d2d190d4370"",
//                     ""path"": ""<XInputController>/rightTrigger"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Yaw"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""1D Axis"",
//                     ""id"": ""48c56373-3979-4450-ad26-be8690f10d1f"",
//                     ""path"": ""1DAxis"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Yaw"",
//                     ""isComposite"": true,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": ""negative"",
//                     ""id"": ""6a9b10c9-e981-490e-ae17-364f5c548e00"",
//                     ""path"": ""<DualShockGamepad>/leftTrigger"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Yaw"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": ""positive"",
//                     ""id"": ""2b97f4a8-a60a-4710-a5e2-58b0026cd6e5"",
//                     ""path"": ""<DualShockGamepad>/rightTrigger"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Yaw"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": true
//                 },
//                 {
//                     ""name"": """",
//                     ""id"": ""712ff56f-ca96-4a18-8e08-6f07355a6761"",
//                     ""path"": ""<Mouse>/delta"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Camera"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": """",
//                     ""id"": ""8122d03e-e52d-49e3-9e9f-16db73380aee"",
//                     ""path"": ""<XInputController>/rightStick"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Camera"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": """",
//                     ""id"": ""6c26d347-84e5-4648-8849-c882caba110a"",
//                     ""path"": ""<DualShockGamepad>/rightStick"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""Camera"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": """",
//                     ""id"": ""4e35106e-8294-4e65-93c7-65953ccb67e0"",
//                     ""path"": ""<Mouse>/leftButton"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""FireCannon"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": """",
//                     ""id"": ""84b4d321-8077-4e7d-8069-7bdd0e775749"",
//                     ""path"": ""<XInputController>/buttonSouth"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""FireCannon"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": """",
//                     ""id"": ""cc259801-2885-45de-a6a4-a8c9daede5d4"",
//                     ""path"": ""<DualShockGamepad>/buttonSouth"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""FireCannon"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": """",
//                     ""id"": ""d882def0-cc79-40f5-9789-268a4740d8f6"",
//                     ""path"": ""<Mouse>/rightButton"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""FireMissile"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": """",
//                     ""id"": ""6e959f2c-dec1-45c2-b7d3-12751afc8696"",
//                     ""path"": ""<XInputController>/buttonEast"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""FireMissile"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": """",
//                     ""id"": ""648916d8-2003-4a76-a337-34d0bdcde78b"",
//                     ""path"": ""<DualShockGamepad>/buttonEast"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""FireMissile"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": """",
//                     ""id"": ""25f10112-5c18-4bc4-9a3e-1a554c151ed2"",
//                     ""path"": ""<DualShockGamepad>/leftStick"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""RollPitch"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": false
//                 },
//                 {
//                     ""name"": """",
//                     ""id"": ""2d16f6e5-9505-4139-b4d1-94c702bf5322"",
//                     ""path"": ""<XInputController>/leftStick"",
//                     ""interactions"": """",
//                     ""processors"": """",
//                     ""groups"": """",
//                     ""action"": ""RollPitch"",
//                     ""isComposite"": false,
//                     ""isPartOfComposite"": false
//                 }
//             ]
//         }
//     ],
//     ""controlSchemes"": []
// }");
//         // Aircraft
//         m_Aircraft = asset.FindActionMap("Aircraft", throwIfNotFound: true);
//         m_Aircraft_Throttle = m_Aircraft.FindAction("Throttle", throwIfNotFound: true);
//         m_Aircraft_Pitch = m_Aircraft.FindAction("Pitch", throwIfNotFound: true);
//         m_Aircraft_Roll = m_Aircraft.FindAction("Roll", throwIfNotFound: true);
//         m_Aircraft_Yaw = m_Aircraft.FindAction("Yaw", throwIfNotFound: true);
//         m_Aircraft_Camera = m_Aircraft.FindAction("Camera", throwIfNotFound: true);
//         m_Aircraft_FireCannon = m_Aircraft.FindAction("FireCannon", throwIfNotFound: true);
//         m_Aircraft_FireMissile = m_Aircraft.FindAction("FireMissile", throwIfNotFound: true);
//         m_Aircraft_RollPitch = m_Aircraft.FindAction("RollPitch", throwIfNotFound: true);
//     }
//
//     public void Dispose()
//     {
//         UnityEngine.Object.Destroy(asset);
//     }
//
//     public InputBinding? bindingMask
//     {
//         get => asset.bindingMask;
//         set => asset.bindingMask = value;
//     }
//
//     public ReadOnlyArray<InputDevice>? devices
//     {
//         get => asset.devices;
//         set => asset.devices = value;
//     }
//
//     public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;
//
//     public bool Contains(InputAction action)
//     {
//         return asset.Contains(action);
//     }
//
//     public IEnumerator<InputAction> GetEnumerator()
//     {
//         return asset.GetEnumerator();
//     }
//
//     IEnumerator IEnumerable.GetEnumerator()
//     {
//         return GetEnumerator();
//     }
//
//     public void Enable()
//     {
//         asset.Enable();
//     }
//
//     public void Disable()
//     {
//         asset.Disable();
//     }
//
//     public IEnumerable<InputBinding> bindings => asset.bindings;
//
//     public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
//     {
//         return asset.FindAction(actionNameOrId, throwIfNotFound);
//     }
//
//     public int FindBinding(InputBinding bindingMask, out InputAction action)
//     {
//         return asset.FindBinding(bindingMask, out action);
//     }
//
//     // Aircraft
//     private readonly InputActionMap m_Aircraft;
//     private List<IAircraftActions> m_AircraftActionsCallbackInterfaces = new List<IAircraftActions>();
//     private readonly InputAction m_Aircraft_Throttle;
//     private readonly InputAction m_Aircraft_Pitch;
//     private readonly InputAction m_Aircraft_Roll;
//     private readonly InputAction m_Aircraft_Yaw;
//     private readonly InputAction m_Aircraft_Camera;
//     private readonly InputAction m_Aircraft_FireCannon;
//     private readonly InputAction m_Aircraft_FireMissile;
//     private readonly InputAction m_Aircraft_RollPitch;
//     public struct AircraftActions
//     {
//         private @CraftInput m_Wrapper;
//         public AircraftActions(@CraftInput wrapper) { m_Wrapper = wrapper; }
//         public InputAction @Throttle => m_Wrapper.m_Aircraft_Throttle;
//         public InputAction @Pitch => m_Wrapper.m_Aircraft_Pitch;
//         public InputAction @Roll => m_Wrapper.m_Aircraft_Roll;
//         public InputAction @Yaw => m_Wrapper.m_Aircraft_Yaw;
//         public InputAction @Camera => m_Wrapper.m_Aircraft_Camera;
//         public InputAction @FireCannon => m_Wrapper.m_Aircraft_FireCannon;
//         public InputAction @FireMissile => m_Wrapper.m_Aircraft_FireMissile;
//         public InputAction @RollPitch => m_Wrapper.m_Aircraft_RollPitch;
//         public InputActionMap Get() { return m_Wrapper.m_Aircraft; }
//         public void Enable() { Get().Enable(); }
//         public void Disable() { Get().Disable(); }
//         public bool enabled => Get().enabled;
//         public static implicit operator InputActionMap(AircraftActions set) { return set.Get(); }
//         public void AddCallbacks(IAircraftActions instance)
//         {
//             if (instance == null || m_Wrapper.m_AircraftActionsCallbackInterfaces.Contains(instance)) return;
//             m_Wrapper.m_AircraftActionsCallbackInterfaces.Add(instance);
//             @Throttle.started += instance.OnThrottle;
//             @Throttle.performed += instance.OnThrottle;
//             @Throttle.canceled += instance.OnThrottle;
//             @Pitch.started += instance.OnPitch;
//             @Pitch.performed += instance.OnPitch;
//             @Pitch.canceled += instance.OnPitch;
//             @Roll.started += instance.OnRoll;
//             @Roll.performed += instance.OnRoll;
//             @Roll.canceled += instance.OnRoll;
//             @Yaw.started += instance.OnYaw;
//             @Yaw.performed += instance.OnYaw;
//             @Yaw.canceled += instance.OnYaw;
//             @Camera.started += instance.OnCamera;
//             @Camera.performed += instance.OnCamera;
//             @Camera.canceled += instance.OnCamera;
//             @FireCannon.started += instance.OnFireCannon;
//             @FireCannon.performed += instance.OnFireCannon;
//             @FireCannon.canceled += instance.OnFireCannon;
//             @FireMissile.started += instance.OnFireMissile;
//             @FireMissile.performed += instance.OnFireMissile;
//             @FireMissile.canceled += instance.OnFireMissile;
//             @RollPitch.started += instance.OnRollPitch;
//             @RollPitch.performed += instance.OnRollPitch;
//             @RollPitch.canceled += instance.OnRollPitch;
//         }
//
//         private void UnregisterCallbacks(IAircraftActions instance)
//         {
//             @Throttle.started -= instance.OnThrottle;
//             @Throttle.performed -= instance.OnThrottle;
//             @Throttle.canceled -= instance.OnThrottle;
//             @Pitch.started -= instance.OnPitch;
//             @Pitch.performed -= instance.OnPitch;
//             @Pitch.canceled -= instance.OnPitch;
//             @Roll.started -= instance.OnRoll;
//             @Roll.performed -= instance.OnRoll;
//             @Roll.canceled -= instance.OnRoll;
//             @Yaw.started -= instance.OnYaw;
//             @Yaw.performed -= instance.OnYaw;
//             @Yaw.canceled -= instance.OnYaw;
//             @Camera.started -= instance.OnCamera;
//             @Camera.performed -= instance.OnCamera;
//             @Camera.canceled -= instance.OnCamera;
//             @FireCannon.started -= instance.OnFireCannon;
//             @FireCannon.performed -= instance.OnFireCannon;
//             @FireCannon.canceled -= instance.OnFireCannon;
//             @FireMissile.started -= instance.OnFireMissile;
//             @FireMissile.performed -= instance.OnFireMissile;
//             @FireMissile.canceled -= instance.OnFireMissile;
//             @RollPitch.started -= instance.OnRollPitch;
//             @RollPitch.performed -= instance.OnRollPitch;
//             @RollPitch.canceled -= instance.OnRollPitch;
//         }
//
//         public void RemoveCallbacks(IAircraftActions instance)
//         {
//             if (m_Wrapper.m_AircraftActionsCallbackInterfaces.Remove(instance))
//                 UnregisterCallbacks(instance);
//         }
//
//         public void SetCallbacks(IAircraftActions instance)
//         {
//             foreach (var item in m_Wrapper.m_AircraftActionsCallbackInterfaces)
//                 UnregisterCallbacks(item);
//             m_Wrapper.m_AircraftActionsCallbackInterfaces.Clear();
//             AddCallbacks(instance);
//         }
//     }
//     public AircraftActions @Aircraft => new AircraftActions(this);
//     public interface IAircraftActions
//     {
//         void OnThrottle(InputAction.CallbackContext context);
//         void OnPitch(InputAction.CallbackContext context);
//         void OnRoll(InputAction.CallbackContext context);
//         void OnYaw(InputAction.CallbackContext context);
//         void OnCamera(InputAction.CallbackContext context);
//         void OnFireCannon(InputAction.CallbackContext context);
//         void OnFireMissile(InputAction.CallbackContext context);
//         void OnRollPitch(InputAction.CallbackContext context);
//     }
// }
