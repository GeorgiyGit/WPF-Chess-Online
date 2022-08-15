using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Db
{
    //[DataContract(IsReference = true)]
    public class Rating
    {
        public int Id { get; set; }

        public int Points { get; set; }
        public int MaxPoints { get; set; }
        public int MinPoints { get; set; }

        public int? Rang { get; set; }

        public int WinCount { get; set; }
        public int MaxWinCount { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
