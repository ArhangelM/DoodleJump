using Assets.DoodleJump.Scripts.Models;
using UnityEngine;

namespace Assets.DoodleJump.Scripts.Common.Helpers
{
    internal static class FileHelper
    {
        public static void SaveStatistic()
        {
            if(Statistic.Instance.Score > PlayerPrefs.GetInt("BestScore"))
            {
                PlayerPrefs.SetInt("BestScore", Statistic.Instance.Score);
                Statistic.Instance.UpdateBestScore(PlayerPrefs.GetInt("BestScore", 0));
                PlayerPrefs.Save();
            }
        }
        
        public static void LoadStatistic()
        {
            Statistic.Instance.UpdateBestScore(PlayerPrefs.GetInt("BestScore", 0));
        }
    }
}
