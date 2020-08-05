using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblAuthOTP_BAK")]
public class AuthOTP_BAK
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int AuthOTP_BAKId { get; set; }

	[Column]
	public int AuthId { get; set; }

	[Column, PrimaryKey]
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