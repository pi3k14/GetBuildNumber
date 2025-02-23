using System;

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Kodefabrikken.GetBuildNumber
{
    public class GetBuildNumber : Task
    {
        static readonly DateTime _January1th2025 = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// A build number constructed as days since January 1th 2025 (UTC).
        /// </summary>
        /// <remarks>
        /// <see cref="System.Diagnostics.FileVersionInfo"/> calls this property <see cref="System.Diagnostics.FileVersionInfo.FileBuildPart"/> with 16 bit length.
        /// </remarks>
        [Output]
        public int Build { get; set; }

        /// <summary>
        /// A revision number constructed as seconds / 2 since midnight (UTC).
        /// </summary>
        /// <remarks><see cref="System.Version"/> splits this in <see cref="System.Version.MajorRevision"/> and <see cref="System.Version.MinorRevision"/> each 16 bit. 
        /// <see cref="System.Diagnostics.FileVersionInfo"/> calls this property <see cref="System.Diagnostics.FileVersionInfo.FilePrivatePart"/> with 16 bit length.
        /// </remarks>
        [Output]
        public int Revision { get; set; }

        /// <summary>
        /// Constructs the build number parts.
        /// </summary>
        /// <returns>Always true.</returns>
        public override bool Execute()
        {
            var now = DateTime.UtcNow;
            Build = (now - _January1th2025).Days;
            Revision = (int) now.TimeOfDay.TotalSeconds / 2;

            return true;
        }
    }
}
