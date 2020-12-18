using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace interview_crud.EDM
{
    public partial class tblemployee
    {
        public List<checkmodel> checklist = new List<checkmodel>();
    }

    public class checkmodel
    {
        public int id{ get; set; }
        public string  name { get; set; }
        public bool ischecked{ get; set; }
    }
}