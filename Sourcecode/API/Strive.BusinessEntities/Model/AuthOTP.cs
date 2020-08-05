using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblAuthOTP")]
public class AuthOTP
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int AuthOTPId { get; set; }

	[Column]
	public int AuthId { get; set; }

	[Column]
	public long? MobileNumber { get; set; }

	[Column]
	public string EmailId { get; set; }

	[Column]
	public short IsVerified { get; set; }

	[Column]
	public DateTime? CreatedDate { get; set; }

	[Column]
	public string OTP { get; set; }

}
}