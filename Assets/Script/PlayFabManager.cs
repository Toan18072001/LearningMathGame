using PlayFab.ClientModels;
using PlayFab;
using UnityEngine;
using System;

public class PlayFabManager : MonoBehaviour
{
    public static PlayFabManager Instance { get; set; }
    protected string status;
    protected UserInfor useri4;

    protected static event Action<string> OnRegisterEvent;
    protected static event Action<string> OnLoginEvent;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        // Initialize PlayFab SDK
        PlayFabSettings.TitleId = "AFDDD";
    }
    #region Login
    public void Login(string email, string password)
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        status = "LoginSuccess";
        OnLoginEvent?.Invoke(status);
        // TODO: Handle successful login
        Debug.Log("OnLoginSuccess ");
        
    }

    private void OnLoginFailure(PlayFabError error)
    {
        status = error.ToString();
        OnLoginEvent?.Invoke(status);
        // TODO: Handle failed login
        Debug.Log("OnLoginFailure "+ error.ToString());
    }
    #endregion
    #region Register
    public void Register(string username, string email, string password)
    {
        var request = new RegisterPlayFabUserRequest
        {
            Username = username,
            Email = email,
            Password = password
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        status = "RegisterSuccess";
        OnRegisterEvent?.Invoke(status);
        // TODO: Handle successful registration
        Debug.Log("OnRegisterSuccess ");
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        status = error.ErrorMessage.ToString();
        OnRegisterEvent?.Invoke(status); //kích hoạt sự kiện
        // TODO: Handle failed registration
        Debug.Log("OnRegisterFailure "+ error.ToString());
    }
    #endregion
    #region VeryEgmail
    //public void GetPlayerProfile(string username, string email)
    //{
    //    var request = new GetAccountInfoRequest
    //    {
    //        Email = email,
    //        TitleDisplayName = username
    //    };
    //    PlayFabClientAPI.GetAccountInfo(request, OnGetAccountInfoSuccess, OnGetAccountInfoFailure);
    //}
    //private void OnGetAccountInfoSuccess(GetAccountInfoResult result)
    //{
    //    // Step 2: Get the player profile using the PlayFabId
    //    var playerId = result.AccountInfo.PlayFabId;
    //    var profileRequest = new GetPlayerProfileRequest
    //    {
    //        PlayFabId = playerId
    //    };
    //    PlayFabClientAPI.GetPlayerProfile(profileRequest, OnGetPlayerProfileSuccess, OnGetPlayerProfileFailure);
    //}

    //private void OnGetAccountInfoFailure(PlayFabError error)
    //{
    //    Debug.LogError("Failed to get account info: " + error.ErrorMessage);
    //}

    //private void OnGetPlayerProfileSuccess(GetPlayerProfileResult result)
    //{
    //    useri4.UserName = result.PlayerProfile.DisplayName;
    //    useri4.Email = result.PlayerProfile.Email;
    //    Debug.Log("Player profile retrieved successfully");
    //    // TODO: Handle the retrieved player profile
    //}

    //private void OnGetPlayerProfileFailure(PlayFabError error)
    //{
    //    Debug.LogError("Failed to get player profile: " + error.ErrorMessage);
    //}
    #endregion

    public class UserInfor
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
