using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Exceptions
{
    /// <summary>
    /// This exception is thrown when data requested by a user
    /// does not exist, usually due to an incorrect key or
    /// other criteria.
    /// </summary>
    [Serializable]
    public class DataNotFoundException : CriteriaException
    {
        /// <summary>
        /// Initializes a new instance of the DataNotFoundException class.
        /// </summary>
        /// <param name="message">Exception message</param>
        public DataNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DataNotFoundException class.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public DataNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DataNotFoundException class.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="paramName">Parameter name</param>
        public DataNotFoundException(string message, string paramName)
            : base(message, paramName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DataNotFoundException class.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="paramName">Parameter name</param>
        /// <param name="innerException">Inner exception</param>
        public DataNotFoundException(string message, string paramName, Exception innerException)
            : base(message, paramName, innerException)
        {
        }
    }
}
