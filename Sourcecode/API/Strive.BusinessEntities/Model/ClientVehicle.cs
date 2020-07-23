namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="ClientVehicle" />.
    /// </summary>
    [OverrideName("tblClientVehicle")]
    public class ClientVehicle
    {
        /// <summary>
        /// Gets or sets the VehicleId.
        /// </summary>
        [Column]
        public int VehicleId { get; set; }

        /// <summary>
        /// Gets or sets the ClientId.
        /// </summary>
        [Column]
        public int? ClientId { get; set; }

        /// <summary>
        /// Gets or sets the LocationId.
        /// </summary>
        [Column]
        public int? LocationId { get; set; }

        /// <summary>
        /// Gets or sets the VehicleNumber.
        /// </summary>
        [Column]
        public string VehicleNumber { get; set; }

        /// <summary>
        /// Gets or sets the VehicleMake.
        /// </summary>
        [Column]
        public int? VehicleMake { get; set; }

        /// <summary>
        /// Gets or sets the VehicleModel.
        /// </summary>
        [Column]
        public int? VehicleModel { get; set; }

        /// <summary>
        /// Gets or sets the VehicleModelNo.
        /// </summary>
        [Column]
        public int? VehicleModelNo { get; set; }

        /// <summary>
        /// Gets or sets the VehicleYear.
        /// </summary>
        [Column]
        public string VehicleYear { get; set; }

        /// <summary>
        /// Gets or sets the VehicleColor.
        /// </summary>
        [Column]
        public int? VehicleColor { get; set; }

        /// <summary>
        /// Gets or sets the Upcharge.
        /// </summary>
        [Column]
        public int? Upcharge { get; set; }

        /// <summary>
        /// Gets or sets the Barcode.
        /// </summary>
        [Column]
        public string Barcode { get; set; }

        /// <summary>
        /// Gets or sets the Notes.
        /// </summary>
        [Column]
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the CreatedBy.
        /// </summary>
        [Column]
        public int? CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate.
        /// </summary>
        [Column]
        public DateTimeOffset? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedBy.
        /// </summary>
        [Column]
        public int? UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the UpdatedDate.
        /// </summary>
        [Column]
        public DateTimeOffset? UpdatedDate { get; set; }
    }
}
