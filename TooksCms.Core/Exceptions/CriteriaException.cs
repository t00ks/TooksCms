using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace TooksCms.Core.Exceptions
{
    /// <summary>
    /// An exception triggered by incorrect criteria supplied
    /// by a user or calling module.
    /// </summary>
    /// <remarks>
    /// These exceptions should not be regarded as fatal, but
    /// result from user error.
    /// </remarks>
    [Serializable]
    public abstract class CriteriaException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the CriteriaException class.
        /// </summary>
        /// <param name="message">Exception message</param>
        public CriteriaException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CriteriaException class.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="paramName">Parameter name</param>
        public CriteriaException(string message, string paramName)
            : base(message, paramName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CriteriaException class.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public CriteriaException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CriteriaException class.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="paramName">Parameter name</param>
        /// <param name="innerException">Inner exception</param>
        public CriteriaException(string message, string paramName, Exception innerException)
            : base(message, paramName, innerException)
        {
        }
    }
}
