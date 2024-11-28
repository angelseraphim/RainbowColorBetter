namespace ColorTag
{
    using System.Collections.Generic;

    using Exiled.API.Features;

    using MEC;

    using static ColorTag.Data;

    public class Coroutines
    {
        public IEnumerator<float> ChangeColor(Player player)
        {
            int currentIndex = 0;

            while (player.IsConnected)
            {
                yield return Timing.WaitForSeconds(Plugin.plugin.Config.Interval);

                if (!Extensions.TryGetValue(player.UserId, out PlayerInfo info))
                    break;

                if (currentIndex >= info.Colors.Count)
                {
                    currentIndex = 0;
                }

                player.RankColor = info.Colors[currentIndex];
                currentIndex++;
            }
            yield break;
        }
    }
}
