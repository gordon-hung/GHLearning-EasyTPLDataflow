using GHLearning.TPLDataflow.SharedKernel;

namespace GHLearning.TPLDataflow.Infrastructure;

internal class PhoneUtensil : IPhoneUtensil
{
	public string HidePhoneNumber(string phoneNumber)
	{
		if (string.IsNullOrEmpty(phoneNumber))
		{
			throw new ArgumentException("手機號碼不能為空。");
		}

		int length = phoneNumber.Length;

		if (length <= 7)
		{
			throw new ArgumentException("手機號碼長度過短，無法隱藏中間部分。");
		}

		Span<char> phoneSpan = phoneNumber.ToCharArray().AsSpan();

		Span<char> prefix = phoneSpan[..4];

		Span<char> suffix = phoneSpan.Slice(length - 3, 3);

		Span<char> hiddenPart = new char[length - 7];
		hiddenPart.Fill('*');

		Span<char> result = new char[length];
		prefix.CopyTo(result);
		hiddenPart.CopyTo(result[4..]);
		suffix.CopyTo(result[(length - 3)..]);

		return new string(result);
	}
}
