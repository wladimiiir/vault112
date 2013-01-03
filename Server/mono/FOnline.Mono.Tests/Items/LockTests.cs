using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Moq;
using FOnline.Items;

namespace FOnline.Mono.Tests.Items
{
    public class LockTests
    {
        [Fact]
        public void UsingLockOnLockerGivesProperKey()
        {
            var _lock = new Mock<Item>(IntPtr.Zero);
            var locker = new Mock<Item>(IntPtr.Zero);
            locker.Setup(i => i.Type).Returns(ItemType.Container); // when target item is container
            locker.Setup(i => i.LockerIsClose).Returns(false); // and it's not locked
            ushort complexity = 50;
            uint lock_id = 10;
            Utils.MockRandomizer((int)complexity, (int)lock_id);
            var key = new Mock<Item>(IntPtr.Zero);
            var cr = new Mock<Critter>(IntPtr.Zero);
            cr.Setup(c => c.AddItem(It.Is<ushort>(i => i == Pid.KEY), 1))
                .Returns(key.Object); // critter will receive key

            Lock.Init(_lock.Object);

            var e = new ItemUseEventArgs(_lock.Object, cr.Object, null, null, null);
            _lock.Raise(i => i.Use += null, _lock.Object, cr.Object, null, locker.Object, null, e); // using lock on the locker

            _lock.VerifySet(l => l.LockerComplexity = complexity);
            locker.Verify();
            cr.Verify();
            key.VerifySet(k => k.LockerId = lock_id); // that random lock id is gonna be assigned to locker
        }
    }
}
