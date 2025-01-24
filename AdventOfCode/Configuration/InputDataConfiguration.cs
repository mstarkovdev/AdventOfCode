using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Configuration;
internal class InputDataConfiguration
{
    public required string AuthorizationSessionCookie { get; set; }

    public required string PathToInputDataFiles { get; set; }
}
