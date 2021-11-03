using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Models
{
    public class RecaptchaResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Action
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets the ErrorCodes
        /// </summary>
        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }

        /// <summary>
        /// Gets or sets the Score
        /// </summary>
        [JsonProperty("score")]
        public decimal Score { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Success
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        #endregion
    }

}
