using GHLearning.TPLDataflow.Infrastructure;

namespace GHLearning.TPLDataflow.InfrastructureTests;

public class PhoneUtensilUnitTest
{
	[Theory]
	[InlineData("0912345678","0912***678")]
	[InlineData("09123456789", "0912****789")]
	public void HidePhoneNumber(string phone, string expected)
	{
		var phoneUtensil = new PhoneUtensil();

		var actual = phoneUtensil.HidePhoneNumber(phone);

		Assert.Equal(expected, actual);
	}
}
