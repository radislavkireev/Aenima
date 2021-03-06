﻿using System;
using System.Threading.Tasks;

namespace Aenima.Logging
{
    /// <summary>
    ///     Indicates the ability to log diagnostic information.
    /// </summary>
    /// <remarks>
    ///     Object instances which implement this interface must be designed to be multi-thread safe.
    /// </remarks>
    public interface ILog
    {
        /// <summary>
        ///     Logs the most detailed level of diagnostic information.
        /// </summary>
        /// <param name="message">The diagnostic message to be logged.</param>
        /// <param name="values">All parameter to be formatted into the message, if any.</param>
        Task Verbose(string message, params object[] values);

        /// <summary>
        ///     Logs the debug-level diagnostic information.
        /// </summary>
        /// <param name="message">The diagnostic message to be logged.</param>
        /// <param name="values">All parameter to be formatted into the message, if any.</param>
        Task Debug(string message, params object[] values);

        /// <summary>
        ///     Logs important runtime diagnostic information.
        /// </summary>
        /// <param name="message">The diagnostic message to be logged.</param>
        /// <param name="values">All parameter to be formatted into the message, if any.</param>
        Task Information(string message, params object[] values);

        /// <summary>
        ///     Logs diagnostic issues to which attention should be paid.
        /// </summary>
        /// <param name="message">The diagnostic message to be logged.</param>
        /// <param name="values">All parameter to be formatted into the message, if any.</param>
        Task Warning(string message, params object[] values);

        /// <summary>
        ///     Logs diagnostic issues to which attention should be paid.
        /// </summary>
        /// <param name="exception">The relevant exception.</param>
        /// <param name="message">The diagnostic message to be logged.</param>
        /// <param name="values">All parameter to be formatted into the message, if any.</param>
        Task Warning(Exception exception, string message, params object[] values);

        /// <summary>
        ///     Logs application and infrastructure-level errors.
        /// </summary>
        /// <param name="message">The diagnostic message to be logged.</param>
        /// <param name="values">All parameter to be formatted into the message, if any.</param>
        Task Error(string message, params object[] values);

        /// <summary>
        ///     Logs application and infrastructure-level errors.
        /// </summary>
        /// <param name="exception">The relevant exception.</param>
        /// <param name="message">The diagnostic message to be logged.</param>
        /// <param name="values">All parameter to be formatted into the message, if any.</param>
        Task Error(Exception exception, string message, params object[] values);

        /// <summary>
        ///     Logs fatal errors which result in process termination.
        /// </summary>
        /// <param name="message">The diagnostic message to be logged.</param>
        /// <param name="values">All parameter to be formatted into the message, if any.</param>
        Task Fatal(string message, params object[] values);

        /// <summary>
        ///     Logs fatal errors which result in process termination.
        /// </summary>
        /// <param name="exception">The relevant exception.</param>
        /// <param name="message">The diagnostic message to be logged.</param>
        /// <param name="values">All parameter to be formatted into the message, if any.</param>
        Task Fatal(Exception exception, string message, params object[] values);
    }
}
