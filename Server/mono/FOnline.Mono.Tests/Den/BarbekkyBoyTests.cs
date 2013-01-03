using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Moq;
using FOnline.Den;

namespace FOnline.Mono.Tests.Den
{
    public class BarbekkyBoyTests
    {
        [Fact]
        public static void BoyAlwaysRespawnsWithRadio()
        {
            var boy = new Mock<Critter>(IntPtr.Zero);
            boy.Setup(b => b.AddItem(It.Is<ushort>(pid => pid == Pid.RADIO), It.Is<uint>(i => i == 1))); // on respawn boy should receive one radio
            boy.Setup(b => b.CountItem(It.Is<ushort>(pid => pid == Pid.RADIO))).Returns(0); // but only if he hasn't got one
            BarbekkyBoy.Init(boy.Object); // init events

            // invoke respawn event
            boy.Raise(b => b.Respawn += null, new CritterEventArgs(boy.Object));

            boy.Verify();
        }
        [Fact]
        public static void BoyIsNotReceivingRadioWhenAlreadyGotOne()
        {
            var boy = new Mock<Critter>(IntPtr.Zero);
            boy.Setup(b => b.CountItem(It.Is<ushort>(pid => pid == Pid.RADIO))).Returns(1); // he's got radio in his inv, no one looted it
            BarbekkyBoy.Init(boy.Object); // init events

            // invoke respawn event
            boy.Raise(b => b.Respawn += null, new CritterEventArgs(boy.Object));

            boy.Verify(b => b.AddItem(It.Is<ushort>(pid => pid == Pid.RADIO), It.IsAny<uint>()), Times.Never());
        }
        [Fact]
        public static void BuyingHummerSpawnsCarGivesKeyAndDeducesMoney()
        {
            Utils.MockRandomizer();
            Utils.MockLogging();

            var player = new Mock<Critter>(IntPtr.Zero);
            var map = new Mock<Map>(IntPtr.Zero);
            player.Setup(p => p.Map).Returns(map.Object); // the map check is performed, so it's needed
            // 10K caps deduced
            player.Setup(p => p.DeleteItem(It.Is<ushort>(pid => pid == Pid.BOTTLE_CAPS), It.Is<uint>(i => i == 10000)));
            // 1 hummer spawned
            var hummer = new Mock<Item>(IntPtr.Zero);
            map.Setup(m => m.AddItem(It.IsAny<ushort>(), It.IsAny<ushort>(), It.Is<ushort>(pid => pid == Pid.HUMMER), It.Is<uint>(i => i == 1)))
                .Returns(hummer.Object);
            
            BarbekkyBoy.r_CreateHummer(player.Object.ThisPtr, IntPtr.Zero);

            player.Verify();
            map.Verify();
        }
    }
}
