using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CVProject.Data;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

namespace CVProject.Models
{
    [JsonObject]
    public class VTeam : Team
    {
    }


}
