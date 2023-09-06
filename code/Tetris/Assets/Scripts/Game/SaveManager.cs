using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mini.Core;
using Mini.Game;

namespace Mini.Shared
{
    public class SaveManager : AbstractSingleton<SaveManager>
    {
        public void SavePlayerData(PlayerData playerData)
        {
            PlayerPrefsUtils.Write("PlayerData", playerData);
        }

        public PlayerData LoadPlayerData()
        {
            return PlayerPrefsUtils.Read<PlayerData>("PlayerData");
        }
    }
}
