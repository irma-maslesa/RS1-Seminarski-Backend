using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudbalskaLigaBiH.Data.DTOs
{
    public class EmailOptionsDTO
    {
        public string Host { get; set; }
        public string ApiKey { get; set; }
        public string ApiKeySecret { get; set; }
        public int Port { get; set; }
        public string SenderEmail { get; set; }
    }
}
