using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Interfaces
{
    /// <summary>
    /// Provides logging interface and utility functions.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Writes the diagnostic message at the <c>Trace</c> level.
        /// </summary>
        /// <param name="message">Log message.</param>
        void Trace([Localizable(false)] string message);

        /// <summary>
        /// Writes the diagnostic message and exception at the <c>Trace</c> level.
        /// </summary>
        /// <param name="message">A <see langword="string" /> to be written.</param>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="args">Arguments to format.</param>
        void Trace(Exception exception, [Localizable(false)] string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message at the <c>Trace</c> level using the specified parameters.
        /// </summary>
        /// <param name="message">A <see langword="string" /> containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        void Trace([Localizable(false)] string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message at the <c>Debug</c> level.
        /// </summary>
        /// <param name="message">Log message.</param>
        void Debug([Localizable(false)] string message);

        /// <summary>
        /// Writes the diagnostic message and exception at the <c>Debug</c> level.
        /// </summary>
        /// <param name="message">A <see langword="string" /> to be written.</param>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="args">Arguments to format.</param>
        void Debug(Exception exception, [Localizable(false)] string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message at the <c>Debug</c> level using the specified parameters.
        /// </summary>
        /// <param name="message">A <see langword="string" /> containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        void Debug([Localizable(false)] string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message at the <c>Info</c> level.
        /// </summary>
        /// <param name="message">Log message.</param>
        void Info([Localizable(false)] string message);

        /// <summary>
        /// Writes the diagnostic message and exception at the <c>Info</c> level.
        /// </summary>
        /// <param name="message">A <see langword="string" /> to be written.</param>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="args">Arguments to format.</param>
        void Info(Exception exception, [Localizable(false)] string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message at the <c>Info</c> level using the specified parameters.
        /// </summary>
        /// <param name="message">A <see langword="string" /> containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        void Info([Localizable(false)] string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message at the <c>Warn</c> level.
        /// </summary>
        /// <param name="message">Log message.</param>
        void Warn([Localizable(false)] string message);

        /// <summary>
        /// Writes the diagnostic message and exception at the <c>Warn</c> level.
        /// </summary>
        /// <param name="message">A <see langword="string" /> to be written.</param>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="args">Arguments to format.</param>
        void Warn(Exception exception, [Localizable(false)] string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message at the <c>Warn</c> level using the specified parameters.
        /// </summary>
        /// <param name="message">A <see langword="string" /> containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        void Warn([Localizable(false)] string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message at the <c>Error</c> level.
        /// </summary>
        /// <param name="message">Log message.</param>
        void Error([Localizable(false)] string message);

        /// <summary>
        /// Writes the diagnostic message and exception at the <c>Error</c> level.
        /// </summary>
        /// <param name="message">A <see langword="string" /> to be written.</param>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="args">Arguments to format.</param>
        void Error(Exception exception, [Localizable(false)] string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message at the <c>Error</c> level using the specified parameters.
        /// </summary>
        /// <param name="message">A <see langword="string" /> containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        void Error([Localizable(false)] string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message at the <c>Fatal</c> level.
        /// </summary>
        /// <param name="message">Log message.</param>
        void Fatal([Localizable(false)] string message);

        /// <summary>
        /// Writes the diagnostic message and exception at the <c>Fatal</c> level.
        /// </summary>
        /// <param name="message">A <see langword="string" /> to be written.</param>
        /// <param name="exception">An exception to be logged.</param>
        /// <param name="args">Arguments to format.</param>
        void Fatal(Exception exception, [Localizable(false)] string message, params object[] args);

        /// <summary>
        /// Writes the diagnostic message at the <c>Fatal</c> level using the specified parameters.
        /// </summary>
        /// <param name="message">A <see langword="string" /> containing format items.</param>
        /// <param name="args">Arguments to format.</param>
        void Fatal([Localizable(false)] string message, params object[] args);
    }
}

