using NUnit.Framework;
using PortableTrello.Client.Fields;

namespace PortableTrello.Tests
{
    [TestFixture]
    public class FlagsEnumFormatter
    {
        [Test]
        public void GivenAFlagsEnum_WhenPreparingTheRequest_ThenAllValuesShouldBeFormattedCorrectly()
        {
            var thingsIWant = MemberFields.avatarHash | MemberFields.gravatarHash | MemberFields.avatarSource;


            Assert.AreEqual("avatarHash, avatarSource, gravatarHash", thingsIWant.ToString());
        }
    }
}
