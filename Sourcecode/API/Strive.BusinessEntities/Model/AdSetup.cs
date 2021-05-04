using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblAdSetup")]
public class AdSetup
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int AdSetupId { get; set; }

	[Column]
	public int? DocumentId { get; set; }

	[Column]
	public string Name { get; set; }

	[Column]
	public string Description { get; set; }

	[Column]
	public bool? IsActive { get; set; }

	[Column]
	public bool? IsDeleted { get; set; }

	[Column]
	public int? CreatedBy { get; set; }

	[Column]
	public DateTimeOffset? CreatedDate { get; set; }

	[Column]
	public int? UpdatedBy { get; set; }

	[Column]
	public DateTimeOffset? UpdatedDate { get; set; }

        [Column]
        public DateTime? LaunchDate{ get; set; }

    }
}