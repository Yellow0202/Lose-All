using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cargold;


[System.Serializable]
public class UserData : Cargold.FrameWork.UserData_C
{
    public List<UserWealthData> userWealthDataList;

    public UserPlayInfo userPlayInfo;
    public UserLoginData userLoginData;

    public UserData()
    {
        this.userWealthDataList = new List<UserWealthData>();

        this.userPlayInfo = new UserPlayInfo();
        this.userLoginData = new UserLoginData();
    }
}

#region Wealth
[System.Serializable]
public class UserWealthData : Cargold.FrameWork.UserSystem_Manager.Wealth_C<WealthType, int>.IWealthData
{
    public WealthType wealthType;
    public int quantity;

    public UserWealthData(WealthType _wealthType, int _quantity)
    {
        this.wealthType = _wealthType;
        this.quantity = _quantity;
    }

    WealthType Cargold.FrameWork.UserSystem_Manager.Wealth_C<WealthType, int>.IWealthData.GetWealthType => this.wealthType;
    int Cargold.FrameWork.UserSystem_Manager.Wealth_C<WealthType, int>.IWealthData.GetQuantity => this.quantity;

    void Cargold.FrameWork.UserSystem_Manager.Wealth_C<WealthType, int>.IWealthData.AddQuantity_Func(int _quantity)
    {
        this.quantity += _quantity;
    }

    void Cargold.FrameWork.UserSystem_Manager.Wealth_C<WealthType, int>.IWealthData.SetQuantity_Func(int _quantity)
    {
        this.quantity = _quantity;
    }

    bool Cargold.FrameWork.UserSystem_Manager.Wealth_C<WealthType, int>.IWealthData.TryGetSubtract_Func(int _quantity, bool _isJustCheck)
    {
        if (_quantity <= this.quantity)
        {
            if (_isJustCheck == false)
                this.quantity -= _quantity;

            return true;
        }
        else
        {
            return false;
        }
    }
}
#endregion

#region UserPlayInfo
[System.Serializable]
public class UserPlayInfo
{
    public int score;
    public int smashedScore;
    public int itemCount;
    public int smashedItemCount;

    public UserPlayInfo(int a_StartScore = 0, int a_StartCount = 0, int a_StartSmashedScore = 0, int StartSmashedItemCount = 0)
    {
        this.score = a_StartScore;
        this.itemCount = a_StartCount;
        this.smashedScore = a_StartSmashedScore;
        this.smashedItemCount = StartSmashedItemCount;
    }
}
#endregion
#region UserLoginData
[System.Serializable]
public class UserLoginData
{
    public string userName;
    public int userScore;

    public UserLoginData(string a_UserName = "", int a_UserScore = 0)
    {
        this.userName = a_UserName;
        this.userScore = a_UserScore;
    }
}
#endregion
