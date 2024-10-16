using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using static ColorTag.Data;

namespace ColorTag
{
    public class Coroutines
    {
        public IEnumerator<float> ChangeColor(Player player)
        {
            int NowColor = 0;
            while (player.IsConnected)
            {
                yield return Timing.WaitForSeconds(1f);
                try
                {
                    if (!Extensions.GetPlayer(player.UserId, out PlayerInfo info))
                        continue;
                    player.RankColor = info.colors[NowColor];
                    if (info.colors.Count - 1 > NowColor)
                    {
                        NowColor++;
                        continue;
                    }
                    NowColor = 0;
                }
                catch (Exception ex)
                {
                    NowColor = 0;
                }
            }
            yield break;
        }
    }
}
