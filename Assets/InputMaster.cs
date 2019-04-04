// GENERATED AUTOMATICALLY FROM 'Assets/InputMaster.inputactions'

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Input;


[Serializable]
public class InputMaster : InputActionAssetReference
{
    public InputMaster()
    {
    }
    public InputMaster(InputActionAsset asset)
        : base(asset)
    {
    }
    [NonSerialized] private bool m_Initialized;
    private void Initialize()
    {
        // Player
        m_Player = asset.GetActionMap("Player");
        m_Player_Shoot = m_Player.GetAction("Shoot");
        if (m_PlayerShootActionStarted != null)
            m_Player_Shoot.started += m_PlayerShootActionStarted.Invoke;
        if (m_PlayerShootActionPerformed != null)
            m_Player_Shoot.performed += m_PlayerShootActionPerformed.Invoke;
        if (m_PlayerShootActionCancelled != null)
            m_Player_Shoot.cancelled += m_PlayerShootActionCancelled.Invoke;
        m_Player_Move = m_Player.GetAction("Move");
        if (m_PlayerMoveActionStarted != null)
            m_Player_Move.started += m_PlayerMoveActionStarted.Invoke;
        if (m_PlayerMoveActionPerformed != null)
            m_Player_Move.performed += m_PlayerMoveActionPerformed.Invoke;
        if (m_PlayerMoveActionCancelled != null)
            m_Player_Move.cancelled += m_PlayerMoveActionCancelled.Invoke;
        m_Initialized = true;
    }
    private void Uninitialize()
    {
        if (m_PlayerActionsCallbackInterface != null)
        {
            Player.SetCallbacks(null);
        }
        m_Player = null;
        m_Player_Shoot = null;
        if (m_PlayerShootActionStarted != null)
            m_Player_Shoot.started -= m_PlayerShootActionStarted.Invoke;
        if (m_PlayerShootActionPerformed != null)
            m_Player_Shoot.performed -= m_PlayerShootActionPerformed.Invoke;
        if (m_PlayerShootActionCancelled != null)
            m_Player_Shoot.cancelled -= m_PlayerShootActionCancelled.Invoke;
        m_Player_Move = null;
        if (m_PlayerMoveActionStarted != null)
            m_Player_Move.started -= m_PlayerMoveActionStarted.Invoke;
        if (m_PlayerMoveActionPerformed != null)
            m_Player_Move.performed -= m_PlayerMoveActionPerformed.Invoke;
        if (m_PlayerMoveActionCancelled != null)
            m_Player_Move.cancelled -= m_PlayerMoveActionCancelled.Invoke;
        m_Initialized = false;
    }
    public void SetAsset(InputActionAsset newAsset)
    {
        if (newAsset == asset) return;
        var PlayerCallbacks = m_PlayerActionsCallbackInterface;
        if (m_Initialized) Uninitialize();
        asset = newAsset;
        Player.SetCallbacks(PlayerCallbacks);
    }
    public override void MakePrivateCopyOfActions()
    {
        SetAsset(ScriptableObject.Instantiate(asset));
    }
    // Player
    private InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private InputAction m_Player_Shoot;
    [SerializeField] private ActionEvent m_PlayerShootActionStarted;
    [SerializeField] private ActionEvent m_PlayerShootActionPerformed;
    [SerializeField] private ActionEvent m_PlayerShootActionCancelled;
    private InputAction m_Player_Move;
    [SerializeField] private ActionEvent m_PlayerMoveActionStarted;
    [SerializeField] private ActionEvent m_PlayerMoveActionPerformed;
    [SerializeField] private ActionEvent m_PlayerMoveActionCancelled;
    public struct PlayerActions
    {
        private InputMaster m_Wrapper;
        public PlayerActions(InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Shoot { get { return m_Wrapper.m_Player_Shoot; } }
        public ActionEvent ShootStarted { get { return m_Wrapper.m_PlayerShootActionStarted; } }
        public ActionEvent ShootPerformed { get { return m_Wrapper.m_PlayerShootActionPerformed; } }
        public ActionEvent ShootCancelled { get { return m_Wrapper.m_PlayerShootActionCancelled; } }
        public InputAction @Move { get { return m_Wrapper.m_Player_Move; } }
        public ActionEvent MoveStarted { get { return m_Wrapper.m_PlayerMoveActionStarted; } }
        public ActionEvent MovePerformed { get { return m_Wrapper.m_PlayerMoveActionPerformed; } }
        public ActionEvent MoveCancelled { get { return m_Wrapper.m_PlayerMoveActionCancelled; } }
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                Shoot.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                Shoot.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                Shoot.cancelled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShoot;
                Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                Move.cancelled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                Shoot.started += instance.OnShoot;
                Shoot.performed += instance.OnShoot;
                Shoot.cancelled += instance.OnShoot;
                Move.started += instance.OnMove;
                Move.performed += instance.OnMove;
                Move.cancelled += instance.OnMove;
            }
        }
    }
    public PlayerActions @Player
    {
        get
        {
            if (!m_Initialized) Initialize();
            return new PlayerActions(this);
        }
    }
    private int m_KBMSchemeIndex = -1;
    public InputControlScheme KBMScheme
    {
        get

        {
            if (m_KBMSchemeIndex == -1) m_KBMSchemeIndex = asset.GetControlSchemeIndex("KBM");
            return asset.controlSchemes[m_KBMSchemeIndex];
        }
    }
    [Serializable]
    public class ActionEvent : UnityEvent<InputAction.CallbackContext>
    {
    }
}
public interface IPlayerActions
{
    void OnShoot(InputAction.CallbackContext context);
    void OnMove(InputAction.CallbackContext context);
}
