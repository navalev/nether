// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Nether.Analytics.EventProcessor.Output.Blob
{
    public static class BlobOutputFolderStructure
    {
        public static string YearMonthDayHour()
        {
            var now = DateTime.UtcNow;
            return $"{now.Year:D4}/{now.Month:D2}/{now.Day:D2}/{now.Hour:D2}";
        }

        public static string YearMonthDay()
        {
            var now = DateTime.UtcNow;
            return $"{now.Year:D4}/{now.Month:D2}/{now.Day:D2}";
        }

        public static string YearMonth()
        {
            var now = DateTime.UtcNow;
            return $"{now.Year:D4}/{now.Month:D2}";
        }

        public static string Year()
        {
            var now = DateTime.UtcNow;
            return $"{now.Year:D4}";
        }
    }
}