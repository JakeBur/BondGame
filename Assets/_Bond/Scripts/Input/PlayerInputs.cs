// GENERATED AUTOMATICALLY FROM 'Assets/_Bond/Scripts/Input/PlayerInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputs"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""c52ad786-443a-41d9-8c9f-03ee56fa13b9"",
            ""actions"": [
                {
                    ""name"": ""movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3dbdb543-75ef-4414-b8f9-23b5ceca160c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""interact"",
                    ""type"": ""Button"",
                    ""id"": ""a4ef92f6-1ed0-463e-9f67-8898db1f67f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""attack1"",
                    ""type"": ""Button"",
                    ""id"": ""4704af23-c554-438e-a3b3-dcb204994a31"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""attack2"",
                    ""type"": ""Button"",
                    ""id"": ""2a3ad70c-1a35-40b2-89b9-d4d5c680e85c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""attack3"",
                    ""type"": ""Button"",
                    ""id"": ""eb71bdbe-3c61-4bf9-8d45-236209bf4072"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""crouch"",
                    ""type"": ""Button"",
                    ""id"": ""07bcee4e-bf2a-42c0-b493-6f99a2e0a393"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""heavyAttack"",
                    ""type"": ""Value"",
                    ""id"": ""49b69718-c1ea-4d93-9d8c-58c5974ed64f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FruitSpawn"",
                    ""type"": ""Button"",
                    ""id"": ""47f82084-3376-4531-afbc-b14bec32f5f9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Swap"",
                    ""type"": ""Button"",
                    ""id"": ""9e7b003d-184b-402a-9678-248b2c4765bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""pause"",
                    ""type"": ""Button"",
                    ""id"": ""fd8646a0-b3a6-474e-929b-ec56826770d9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""dash"",
                    ""type"": ""Button"",
                    ""id"": ""bedf2fdc-5b46-4f74-8e9d-9a0a228af581"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""mousePos"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8eb3b0fa-5f2c-464c-8b80-4806df1867b5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""creatureAutoAttack"",
                    ""type"": ""Button"",
                    ""id"": ""a64a9512-07bd-4628-aed9-d96e803bb9c0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""openStats"",
                    ""type"": ""Button"",
                    ""id"": ""5ae8ab5c-242b-49dd-877c-ce4e357dcca3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""whistle"",
                    ""type"": ""Button"",
                    ""id"": ""01407524-9676-4728-a35e-7d9c6fbe5af3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""959da4d2-9590-4a86-8120-9ccac38441c2"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a71dd5d2-f0e6-4e15-812f-9ce69c11273e"",
                    ""path"": ""<Keyboard>/#(W)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""28484528-73c0-44ca-ab94-596d9b5c4f32"",
                    ""path"": ""<Keyboard>/#(A)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""251fc74b-2c04-421e-98f2-f5cb879c2df1"",
                    ""path"": ""<Keyboard>/#(S)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4d47ef87-97d2-4e8e-b979-9ecb13b1029b"",
                    ""path"": ""<Keyboard>/#(D)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""fe6c87eb-b259-4e5a-8a6c-535c32c13022"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48b2b188-3c21-42a7-a133-3c58d905cf38"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0609f1d0-a639-4ecf-a4b7-06998da8d922"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c871bcc5-1b95-4844-948d-8c9417f0f000"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""attack1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""59f1477a-4b67-46e0-b288-796d5ddb6db9"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""attack1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4378cad-060b-4db2-addf-73b00f236578"",
                    ""path"": ""<Keyboard>/#(Q)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""attack2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98a9e6b6-cf63-4467-bab9-1e6d29b141fe"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""attack2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63167295-8c67-47e4-8411-8e10f397dea5"",
                    ""path"": ""<Keyboard>/#(E)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""attack3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2fdc11b-2b90-4524-abfa-68421e005f8f"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""attack3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d3d9255e-6af9-436c-a839-cb38012b5eb0"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4cd502b-835e-4bb3-bc33-0da0d4bc0fbe"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""89883276-c7e7-4004-a41a-e4cf5f29e5f4"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Hold(duration=0.3)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""heavyAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""208fbb25-b1e8-438d-84c2-9196bbb94e8b"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""Hold(duration=0.3)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""heavyAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""62016e48-e87e-4650-abc7-964de771f93a"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FruitSpawn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b889c221-0ab8-4f0c-a580-36e6856999a1"",
                    ""path"": ""<Keyboard>/#(X)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Swap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""368a5af7-77fa-4806-8e7c-c4cd95d70fc1"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Swap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f89fc27-6a66-46b5-97f4-46146e5dbef8"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f57d2f7f-25ac-45bb-83dc-ad0f287846e4"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db806932-8136-4375-95e5-c1ddf6e4e415"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b6faa4cd-2cc1-45c0-b267-b7c74e8bc873"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f15b241e-fceb-4f3a-a778-55dd1a318581"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""mousePos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""623b3dcc-c57b-45e1-8ee8-b7e7e53c14cc"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""creatureAutoAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c9cc6b91-898f-4f62-a466-64e7fa6fb403"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""openStats"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a3e88274-6596-4cc7-bc62-41ad2a4c4e2f"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""whistle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""keyboard"",
            ""id"": ""43ee5e64-4e2b-4f5e-918f-3b792f0bdd8d"",
            ""actions"": [
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""0fd8d336-379a-412c-837a-e935f6bd82fc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""6d4c03b6-e584-4b19-8830-d630ee944b52"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""interact"",
                    ""type"": ""Button"",
                    ""id"": ""4324a1fb-7b99-41e1-8ef3-1961c22e523a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8c5cbbff-dc71-4cd7-abac-5e48a583c864"",
                    ""path"": ""<Mouse>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""86617896-6c64-4194-9995-3f9f6178f47d"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""859e682e-cbbc-45d5-9f0a-b6ae73d1e605"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""965e7a0e-9d7f-4572-965d-eb38808f952b"",
            ""actions"": [
                {
                    ""name"": ""pause"",
                    ""type"": ""Button"",
                    ""id"": ""dd9c305e-5bfe-41dc-8e01-a852a027a4c9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b2ce5b9d-65d3-4e81-a033-969f9b13f9a3"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ca7b9e7-b865-41b3-a396-b5a759b0bffb"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_movement = m_Player.FindAction("movement", throwIfNotFound: true);
        m_Player_interact = m_Player.FindAction("interact", throwIfNotFound: true);
        m_Player_attack1 = m_Player.FindAction("attack1", throwIfNotFound: true);
        m_Player_attack2 = m_Player.FindAction("attack2", throwIfNotFound: true);
        m_Player_attack3 = m_Player.FindAction("attack3", throwIfNotFound: true);
        m_Player_crouch = m_Player.FindAction("crouch", throwIfNotFound: true);
        m_Player_heavyAttack = m_Player.FindAction("heavyAttack", throwIfNotFound: true);
        m_Player_FruitSpawn = m_Player.FindAction("FruitSpawn", throwIfNotFound: true);
        m_Player_Swap = m_Player.FindAction("Swap", throwIfNotFound: true);
        m_Player_pause = m_Player.FindAction("pause", throwIfNotFound: true);
        m_Player_dash = m_Player.FindAction("dash", throwIfNotFound: true);
        m_Player_mousePos = m_Player.FindAction("mousePos", throwIfNotFound: true);
        m_Player_creatureAutoAttack = m_Player.FindAction("creatureAutoAttack", throwIfNotFound: true);
        m_Player_openStats = m_Player.FindAction("openStats", throwIfNotFound: true);
        m_Player_whistle = m_Player.FindAction("whistle", throwIfNotFound: true);
        // keyboard
        m_keyboard = asset.FindActionMap("keyboard", throwIfNotFound: true);
        m_keyboard_LeftClick = m_keyboard.FindAction("LeftClick", throwIfNotFound: true);
        m_keyboard_Newaction = m_keyboard.FindAction("New action", throwIfNotFound: true);
        m_keyboard_interact = m_keyboard.FindAction("interact", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_pause = m_Menu.FindAction("pause", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_movement;
    private readonly InputAction m_Player_interact;
    private readonly InputAction m_Player_attack1;
    private readonly InputAction m_Player_attack2;
    private readonly InputAction m_Player_attack3;
    private readonly InputAction m_Player_crouch;
    private readonly InputAction m_Player_heavyAttack;
    private readonly InputAction m_Player_FruitSpawn;
    private readonly InputAction m_Player_Swap;
    private readonly InputAction m_Player_pause;
    private readonly InputAction m_Player_dash;
    private readonly InputAction m_Player_mousePos;
    private readonly InputAction m_Player_creatureAutoAttack;
    private readonly InputAction m_Player_openStats;
    private readonly InputAction m_Player_whistle;
    public struct PlayerActions
    {
        private @PlayerInputs m_Wrapper;
        public PlayerActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @movement => m_Wrapper.m_Player_movement;
        public InputAction @interact => m_Wrapper.m_Player_interact;
        public InputAction @attack1 => m_Wrapper.m_Player_attack1;
        public InputAction @attack2 => m_Wrapper.m_Player_attack2;
        public InputAction @attack3 => m_Wrapper.m_Player_attack3;
        public InputAction @crouch => m_Wrapper.m_Player_crouch;
        public InputAction @heavyAttack => m_Wrapper.m_Player_heavyAttack;
        public InputAction @FruitSpawn => m_Wrapper.m_Player_FruitSpawn;
        public InputAction @Swap => m_Wrapper.m_Player_Swap;
        public InputAction @pause => m_Wrapper.m_Player_pause;
        public InputAction @dash => m_Wrapper.m_Player_dash;
        public InputAction @mousePos => m_Wrapper.m_Player_mousePos;
        public InputAction @creatureAutoAttack => m_Wrapper.m_Player_creatureAutoAttack;
        public InputAction @openStats => m_Wrapper.m_Player_openStats;
        public InputAction @whistle => m_Wrapper.m_Player_whistle;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @attack1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack1;
                @attack1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack1;
                @attack1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack1;
                @attack2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack2;
                @attack2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack2;
                @attack2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack2;
                @attack3.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack3;
                @attack3.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack3;
                @attack3.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack3;
                @crouch.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                @crouch.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                @crouch.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                @heavyAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHeavyAttack;
                @heavyAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHeavyAttack;
                @heavyAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHeavyAttack;
                @FruitSpawn.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFruitSpawn;
                @FruitSpawn.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFruitSpawn;
                @FruitSpawn.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFruitSpawn;
                @Swap.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwap;
                @Swap.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwap;
                @Swap.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwap;
                @pause.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @pause.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @pause.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPause;
                @dash.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @dash.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @dash.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @mousePos.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePos;
                @mousePos.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePos;
                @mousePos.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePos;
                @creatureAutoAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCreatureAutoAttack;
                @creatureAutoAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCreatureAutoAttack;
                @creatureAutoAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCreatureAutoAttack;
                @openStats.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenStats;
                @openStats.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenStats;
                @openStats.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenStats;
                @whistle.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWhistle;
                @whistle.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWhistle;
                @whistle.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnWhistle;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @movement.started += instance.OnMovement;
                @movement.performed += instance.OnMovement;
                @movement.canceled += instance.OnMovement;
                @interact.started += instance.OnInteract;
                @interact.performed += instance.OnInteract;
                @interact.canceled += instance.OnInteract;
                @attack1.started += instance.OnAttack1;
                @attack1.performed += instance.OnAttack1;
                @attack1.canceled += instance.OnAttack1;
                @attack2.started += instance.OnAttack2;
                @attack2.performed += instance.OnAttack2;
                @attack2.canceled += instance.OnAttack2;
                @attack3.started += instance.OnAttack3;
                @attack3.performed += instance.OnAttack3;
                @attack3.canceled += instance.OnAttack3;
                @crouch.started += instance.OnCrouch;
                @crouch.performed += instance.OnCrouch;
                @crouch.canceled += instance.OnCrouch;
                @heavyAttack.started += instance.OnHeavyAttack;
                @heavyAttack.performed += instance.OnHeavyAttack;
                @heavyAttack.canceled += instance.OnHeavyAttack;
                @FruitSpawn.started += instance.OnFruitSpawn;
                @FruitSpawn.performed += instance.OnFruitSpawn;
                @FruitSpawn.canceled += instance.OnFruitSpawn;
                @Swap.started += instance.OnSwap;
                @Swap.performed += instance.OnSwap;
                @Swap.canceled += instance.OnSwap;
                @pause.started += instance.OnPause;
                @pause.performed += instance.OnPause;
                @pause.canceled += instance.OnPause;
                @dash.started += instance.OnDash;
                @dash.performed += instance.OnDash;
                @dash.canceled += instance.OnDash;
                @mousePos.started += instance.OnMousePos;
                @mousePos.performed += instance.OnMousePos;
                @mousePos.canceled += instance.OnMousePos;
                @creatureAutoAttack.started += instance.OnCreatureAutoAttack;
                @creatureAutoAttack.performed += instance.OnCreatureAutoAttack;
                @creatureAutoAttack.canceled += instance.OnCreatureAutoAttack;
                @openStats.started += instance.OnOpenStats;
                @openStats.performed += instance.OnOpenStats;
                @openStats.canceled += instance.OnOpenStats;
                @whistle.started += instance.OnWhistle;
                @whistle.performed += instance.OnWhistle;
                @whistle.canceled += instance.OnWhistle;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // keyboard
    private readonly InputActionMap m_keyboard;
    private IKeyboardActions m_KeyboardActionsCallbackInterface;
    private readonly InputAction m_keyboard_LeftClick;
    private readonly InputAction m_keyboard_Newaction;
    private readonly InputAction m_keyboard_interact;
    public struct KeyboardActions
    {
        private @PlayerInputs m_Wrapper;
        public KeyboardActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftClick => m_Wrapper.m_keyboard_LeftClick;
        public InputAction @Newaction => m_Wrapper.m_keyboard_Newaction;
        public InputAction @interact => m_Wrapper.m_keyboard_interact;
        public InputActionMap Get() { return m_Wrapper.m_keyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardActions instance)
        {
            if (m_Wrapper.m_KeyboardActionsCallbackInterface != null)
            {
                @LeftClick.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnLeftClick;
                @LeftClick.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnLeftClick;
                @LeftClick.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnLeftClick;
                @Newaction.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnNewaction;
                @interact.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnInteract;
                @interact.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnInteract;
                @interact.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_KeyboardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LeftClick.started += instance.OnLeftClick;
                @LeftClick.performed += instance.OnLeftClick;
                @LeftClick.canceled += instance.OnLeftClick;
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
                @interact.started += instance.OnInteract;
                @interact.performed += instance.OnInteract;
                @interact.canceled += instance.OnInteract;
            }
        }
    }
    public KeyboardActions @keyboard => new KeyboardActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_pause;
    public struct MenuActions
    {
        private @PlayerInputs m_Wrapper;
        public MenuActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @pause => m_Wrapper.m_Menu_pause;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @pause.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnPause;
                @pause.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnPause;
                @pause.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @pause.started += instance.OnPause;
                @pause.performed += instance.OnPause;
                @pause.canceled += instance.OnPause;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnAttack1(InputAction.CallbackContext context);
        void OnAttack2(InputAction.CallbackContext context);
        void OnAttack3(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnHeavyAttack(InputAction.CallbackContext context);
        void OnFruitSpawn(InputAction.CallbackContext context);
        void OnSwap(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnMousePos(InputAction.CallbackContext context);
        void OnCreatureAutoAttack(InputAction.CallbackContext context);
        void OnOpenStats(InputAction.CallbackContext context);
        void OnWhistle(InputAction.CallbackContext context);
    }
    public interface IKeyboardActions
    {
        void OnLeftClick(InputAction.CallbackContext context);
        void OnNewaction(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnPause(InputAction.CallbackContext context);
    }
}
