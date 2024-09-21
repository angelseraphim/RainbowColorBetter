using Exiled.API.Features;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ColorTag.Data;

namespace ColorTag
{
    public class Data
    {
        [Serializable]
        public class PlayerInfo
        {
            [BsonId]
            public string ID { get; set; }
            public List<string> colors { get; set; } 
        }
    }

    public static class Extensions
    {
        public static ILiteCollection<PlayerInfo> PlayerInfoCollection => Plugin.plugin.db.GetCollection<PlayerInfo>($"ColorSetting{Server.Port}");

        public static async Task InsertPlayerAsync(Player player, List<string> colors)
        {
            PlayerInfo insert = new PlayerInfo()
            {
                ID = player.UserId,
                colors = new List<string>() { player.RankColor }
            };

            await Task.Run(() => PlayerInfoCollection.Insert(insert));
        }

        public static bool GetPlayer(string id, out PlayerInfo info)
        {
            info = PlayerInfoCollection.FindById(id);
            return info != null;
        }
        public static void DeletePlayer(string playerId)
        {
            if (!GetPlayer(playerId, out PlayerInfo info))
                return;
            PlayerInfoCollection.Delete(playerId);
        }
        public static void DeleteAll() => PlayerInfoCollection.DeleteAll();
    }
}