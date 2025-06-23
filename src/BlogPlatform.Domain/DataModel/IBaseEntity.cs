using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPlatform.Shared.Interface
{
    public class IBaseEntity
    {
        string CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        string UpdatedBy { get; set; }
        DateTime? UpdateOn { get; set; }
        bool IsDeleted { get; set; }
    }
}
