using NUnit.Framework;
using PortableTrello.Client.Fields;

namespace PortableTrello.Tests
{
    [TestFixture]
    public class FlagsEnumFormatter
    {
        public void GivenAFlagsEnum_WhenPreparingTheRequest_ThenAllValuesShouldBeFormattedCorrectly()
        {
            var thingsIWant = MemberFields.avatarHash | MemberFields.gravatarHash | MemberFields.avatarSource;


            Assert.AreEqual("avatarHash,gravatarHash,avatarSource", thingsIWant.ToString());
        }
    }
}
