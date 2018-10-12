using System;
using System.Collections.Generic;
using System.Text;

namespace Emceelee.Import.DST
{
    public class ImportDateTimeInfo
    {
        public DateTime DateTimeUtc { get; set; }

        public ContractDateTimeInfo ContractDateTimeInfo { get; set; } = new ContractDateTimeInfo();
    }
}
