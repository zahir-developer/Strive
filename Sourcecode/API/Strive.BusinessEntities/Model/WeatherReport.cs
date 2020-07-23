namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="WeatherReport" />.
    /// </summary>
    [OverrideName("tblWeatherReport")]
    public class WeatherReport
    {
        /// <summary>
        /// Gets or sets the locationId.
        /// </summary>
        [Column]
        public int? locationId { get; set; }

        /// <summary>
        /// Gets or sets the Lan.
        /// </summary>
        [Column]
        public decimal? Lan { get; set; }

        /// <summary>
        /// Gets or sets the Lon.
        /// </summary>
        [Column]
        public decimal? Lon { get; set; }

        /// <summary>
        /// Gets or sets the Timestep.
        /// </summary>
        [Column]
        public int? Timestep { get; set; }

        /// <summary>
        /// Gets or sets the UnitSystem.
        /// </summary>
        [Column]
        public string UnitSystem { get; set; }

        /// <summary>
        /// Gets or sets the Fields.
        /// </summary>
        [Column]
        public string Fields { get; set; }

        /// <summary>
        /// Gets or sets the StartTime.
        /// </summary>
        [Column]
        public DateTimeOffset? StartTime { get; set; }

        /// <summary>
        /// Gets or sets the EndTime.
        /// </summary>
        [Column]
        public DateTimeOffset? EndTime { get; set; }

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
