using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;

namespace FOnline.Mono.Tests
{
    static class Utils
    {
        /// <summary>
        /// Mocks global randomizer to return given numbers in a sequence,
        /// regardless of parameters given to Random function.
        /// </summary>
        /// <param name="seq"></param>
        public static Mock<IRandom> MockRandomizer(params int[] seq)
        {
            var mock = new Mock<IRandom>();
            int i = 0;
            mock.Setup(r => r.Random(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() => seq[i++]); // that way we capture closure over copied i and can use it as index to sequence
            Global.Randomizer = mock.Object;
            return mock;
        }
        /// <summary>
        /// Mocks global randomizer to return our-generated random numbers
        /// </summary>
        public static Mock<IRandom> MockRandomizer()
        {
            var mock = new Mock<IRandom>();
            var rnd = new Random();
            mock.Setup(r => r.Random(It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int, int>((min, max) => rnd.Next(min, max+1)); // fonline min,max are inclusive bounds
            Global.Randomizer = mock.Object;
            return mock;
        }
        /// <summary>
        /// Mocks Log function to just print output to console
        /// </summary>
        public static Mock<ILogging> MockLogging()
        {
            var mock = new Mock<ILogging>();
            mock.Setup(l => l.Log(It.IsAny<string>()))
                .Callback<string>(s => Console.WriteLine(s));
            Global.Logging = mock.Object;
            return mock;
        }
    }
}
