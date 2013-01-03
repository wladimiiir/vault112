// Original authors: cvet, dagnir
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FOnline.Den
{
    public class DenBus
    {
        class Str
        {
            public const uint DenBusIn = 1050; // "Вы испытываете странное чувство полной разрухи."
            public const uint DenBusOut = 1051;// "Где-то в глубине души вы очень рады, что покинули это место."
        }
        public static Map _DenBusInit(IntPtr ptr, bool firstTime)
        {
            Map den_bus = new Map(ptr);
			den_bus.InCritter += (map, e) =>
                {
                    if (e.Cr.IsPlayer)
                        e.Cr.SayMsg(Say.NetMsg, TextMsg.Text, Str.DenBusIn);
                };
            den_bus.OutCritter += (map, e) =>
                {
                    if (e.Cr.IsPlayer)
                        e.Cr.SayMsg(Say.NetMsg, TextMsg.Text, Str.DenBusOut);
                };
            return den_bus;
        }
    }
}
