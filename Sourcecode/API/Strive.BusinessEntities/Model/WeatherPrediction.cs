namespace Strive.BusinessEntities.Model
{
    using Cocoon.ORM;
    using System;

    /// <summary>
    /// Defines the <see cref="WeatherPrediction" />.
    /// </summary>
    [OverrideName("tblWeatherPrediction")]
    public class WeatherPrediction
    {
        /// <summary>
        /// Gets or sets the WeatherId.
        /// </summary>
        [Column, IgnoreOnInsert, IgnoreOnUpdate]
        public int WeatherId { get; set; }

        /// <summary>
        /// Gets or sets the LocationId.
        /// </summary>
        [Column]
        public int? LocationId { get; set; }

        /// <summary>
        /// Gets or sets the Weather.
        /// </summary>
        [Column]
        public string Weather { get; set; }

        /// <summary>
        /// Gets or sets the RainProbability.
        /// </summary>
        [Column]
        public string RainProbability { get; set; }

        /// <summary>
        /// Gets or sets the PredictedBusiness.
        /// </summary>
        [Column]
        public string PredictedBusiness { get; set; }

        /// <summary>
        /// Gets or sets the TargetBusiness.
        /// </summary>
        [Column]
        public string TargetBusiness { get; set; }

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
