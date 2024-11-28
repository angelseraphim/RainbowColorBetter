namespace ColorTag
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Exiled.API.Features;

    using LiteDB;

    using static ColorTag.Data;

    public class Data
    {
        [Serializable]
        public class PlayerInfo
        {
            [BsonId]
            public string UserId { get; set; }
            public List<string> Colors { get; set; } 
        }
    }

    public static class Extensions
    {
        public static ILiteCollection<PlayerInfo> PlayerInfoCollection => Plugin.database.GetCollection<PlayerInfo>($"ColorSetting{Server.Port}");

        public static async Task InsertPlayerAsync(Player player, List<string> colors)
        {
            PlayerInfo insert = new PlayerInfo()
            {
                UserId = player.UserId,
                Colors = new List<string>() { player.RankColor }
            };

            await Task.Run(() => PlayerInfoCollection.Insert(insert));
        }

        public static bool TryGetValue(string userId, out PlayerInfo info)
        {
            info = PlayerInfoCollection.FindById(userId);
            return info != null;
        }
        public static bool Contains(string userId)
        {
            return PlayerInfoCollection.FindById(userId) != null;
        }
        public static void DeletePlayer(string userId)
        {
            if (!Contains(userId))
                return;
            PlayerInfoCollection.Delete(userId);
        }
        public static void DeleteAll() => PlayerInfoCollection.DeleteAll();
    }
}